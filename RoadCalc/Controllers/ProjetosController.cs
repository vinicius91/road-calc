using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using RoadCalc.Context;
using RoadCalc.Models.Entities;
using RoadCalc.Models.ViewModels;
using RoadCalc.Repositories;

namespace RoadCalc.Controllers
{
    [Authorize]
    public class ProjetosController : Controller
    {
        private readonly EstradasContext _db = new EstradasContext();
        private readonly ProjetoRepository _pr = new ProjetoRepository();


        #region FuncoesBasicas

        // GET: Projetos
        public async Task<ActionResult> Index()
        {
            var resultado = await _pr.BuscaProjetosLista(int.Parse(User.Identity.GetUserId()));
            return View(resultado.TransportBag as List<ListProjetoViewModel>);
        }

        // GET: Projetos/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Projeto projeto = await _db.Projetos.FindAsync(id);
            if (projeto == null)
            {
                return HttpNotFound();
            }
            return View(projeto);
        }

        // GET: Projetos/Create
        public ActionResult Create()
        {
            var objetos = _db.ClasseDeProjetos.Select(
                x => new SelectListItem() { Text = @"Classe " + x.Nome + @" - Terreno " + x.Relevo.ToString() + @" - " + x.Caracteristicas.ToString(), Value = x.Id.ToString() }).AsEnumerable();

            ViewBag.ClasseDeProjetoId = new SelectList(objetos, "Value", "Text");
            ViewBag.UserId = new SelectList(_db.Users, "Id", "Email");
            return View();
        }

        // POST: Projetos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateProjetoViewModel projetoModel)
        {
            var userId = int.Parse(User.Identity.GetUserId());
            if (ModelState.IsValid)
            {
                var projeto = new Projeto()
                {
                    ClasseDeProjeto = _db.ClasseDeProjetos.Find(projetoModel.ClasseDeProjetoId),
                    Nome = projetoModel.Nome,
                    UserId = userId,
                    DateIncluded = DateTime.Now,
                    DateAltered = DateTime.Now
                };

                _db.Projetos.Add(projeto);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ClasseDeProjetoId = new SelectList(_db.ClasseDeProjetos, "Id", "Nome", projetoModel.ClasseDeProjetoId.ToString());
            return View();
        }

        // GET: Projetos/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var resultado = await _pr.BuscaProjetosEdit(id);
            int numeroDeInicializadores = await _pr.ContaInicializadoresDeCurva(id);
            Projeto projeto = resultado.TransportBag as Projeto;

            if (projeto == null)
            {
                return HttpNotFound();
            }
            var coordenadas = new List<Coordenada>();
            foreach (var pontos in projeto.PontosNotaveis)
            {
                coordenadas.Add(pontos.Coordenada);
            }
            var objetos = _pr.BuscaSelectClassesDeProjeto().TransportBag as IEnumerable<SelectListItem>;
            var objetosPontos = _pr.BuscaSelectPontos(id).TransportBag as IEnumerable<SelectListItem>;

            ViewBag.PontoInicialId = new SelectList(objetosPontos, "Value", "Text", projeto.PontoInicialId);
            ViewBag.PontoFinalId = new SelectList(objetosPontos, "Value", "Text", projeto.PontoFinalId);
            ViewBag.ClasseDeProjetoId = new SelectList(objetos, "Value", "Text", projeto.ClasseDeProjetoId);


            return View(new EditProjetoViewModel(projeto) { NumeroDeInicializadoresDeCurvas = numeroDeInicializadores });
        }

        // POST: Projetos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditProjetoViewModel valores)
        {
            if (ModelState.IsValid)
            {
                var projeto = _db.Projetos.Find(valores.Id);
                projeto.Nome = valores.Nome;
                projeto.ClasseDeProjetoId = valores.ClasseDeProjetoId;
                projeto.CoordenadasReais = valores.CoordenadasReaisBool;
                projeto.PontoInicialId = valores.PontoInicialId;
                projeto.PontoFinalId = valores.PontoFinalId;
                _db.Entry(projeto).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return RedirectToAction("Edit", new { id = valores.Id });
            }

            var objetos = _db.ClasseDeProjetos.Select(
                x => new SelectListItem() { Text = @"Classe " + x.Nome + @" - Terreno " + x.Relevo.ToString() + @" - " + x.Caracteristicas.ToString(), Value = x.Id.ToString() }).AsEnumerable();
            ViewBag.ClasseDeProjetoId = new SelectList(objetos, "Value", "Text", valores.ClasseDeProjeto.Id);
            return View(valores);
        }

        // GET: Projetos/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Projeto projeto = await _db.Projetos.FindAsync(id);
            if (projeto == null)
            {
                return HttpNotFound();
            }
            return View(projeto);
        }

        // POST: Projetos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Projeto projeto = await _db.Projetos.FindAsync(id);
            _db.Projetos.Remove(projeto);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        #endregion

        #region Pontos

        //Pontos
        //UI para Adicionar Pontos
        public async Task<ActionResult> AdicionaPonto(int id)
        {

            return PartialView(new PontoNotavelViewModel() { ProjetoId = id });
        }

        //Salva Pontos
        [HttpPost]
        public ActionResult SalvaPonto(PontoNotavelViewModel valores)
        {

            if (ModelState.IsValid)
            {
                var resultado = _pr.SalvaPonto(valores.CoordX, valores.CoordY, valores.Cota, valores.ZoneLetter, valores.ZoneNumber, valores.Nome,
                    valores.ProjetoId);

                return RedirectToAction("Edit", new { id = valores.ProjetoId });
            }

            return RedirectToAction("Edit", new { id = valores.ProjetoId });
        }

        #endregion

        #region Trechos

        //Trechos
        //UI para Salvar Trechos
        public async Task<ActionResult> AdicionaTrechos(int id)
        {
            Projeto projeto = _db.Projetos.Include(p => p.PontosNotaveis).FirstOrDefaultAsync(x => x.Id == id).Result;
            return PartialView(new AdicionaTrechosViewModel(projeto));
        }

        //Salva TRechos
        [HttpPost]
        public async Task<ActionResult> SalvaTrechos(AdicionaTrechosViewModel valores)
        {

            if (ModelState.IsValid)
            {

                var resultado = _pr.SalvaTrechos(valores.ProjetoId, valores.EstacaInicial, valores.ComplementoEstaca);
                return RedirectToAction("Edit", new { id = valores.ProjetoId });
            }

            return RedirectToAction("Edit", new { id = valores.ProjetoId });
        }

        #endregion

        #region CurvaHorizontal

        //Curvas
        //UI para inicializador de curvas
        [HttpPost]
        public async Task<ActionResult> PreparaCurvas(int id)
        {
            var projetoBase = _db.Projetos.Include(p => p.ClasseDeProjeto).Include(p => p.PontosNotaveis)
                .Include(p => p.Trechos).Include(p => p.Curvas).FirstOrDefault(x => x.Id == id);
            EditProjetoViewModel projeto = new EditProjetoViewModel(projetoBase) { PontoInicialId = projetoBase.PontoInicialId, PontoFinalId = projetoBase.PontoFinalId };
            projeto.NumeroDeCurvas = projeto.NumeroDeTrechos - 1;
            return PartialView(projeto);
        }
        //Salva Inicializador de curvas
        public async Task<ActionResult> SalvaPreparaCurvas(EditProjetoViewModel valores)
        {
            var inicializadoresDeCurvas = await _pr.BuscaCurvasDeProjeto(valores.Id);
            _pr.SalvaInicializadores(inicializadoresDeCurvas);

            return RedirectToAction("Edit", new { id = valores.Id });
        }
        //UI para adição de curvas
        [HttpPost]
        public async Task<ActionResult> AdicionaCurva(int id)
        {
            var curva = _db.InicializadorDeCurvas.Include(x => x.Projeto).Include(x => x.TrechoInicial).Include(x => x.TrechoFinal).Include("Projeto.ClasseDeProjeto").FirstOrDefault(x => x.ProjetoId == id);
            return PartialView(curva);
        }
        //Salva Curvas
        [HttpPost]
        public ActionResult SalvaCurva(InicializadorDeCurva valores)
        {

            if (ModelState.IsValid)
            {

                var resultado = _pr.SalvaCurva(valores.ProjetoId, valores.TrechoInicialId, valores.TrechoFinalId,
                    valores.Raio, valores.Nome, valores.Lc);
                return RedirectToAction("Edit", "Projetos", new { id = valores.ProjetoId });
            }

            return RedirectToAction("Edit", "Projetos", new { id = valores.ProjetoId });
        }


        #endregion

        #region PontoNotavelVertical

        [HttpPost]
        public async Task<ActionResult> SelecionaEstacaPNV(int id)
        {
            var estaca = _pr.PrimeiraEstaca(id);
            var estacas = _pr.BuscaSelectEstacasDeProjeto(id).TransportBag as IEnumerable<SelectListItem>;
            ViewBag.Id = new SelectList(estacas, "Value", "Text", estaca.Id);
            return PartialView(estaca);
        }

        [HttpPost]
        public async Task<ActionResult> AdicionaPNV(int id)
        {
            var estacas = _pr.BuscaEstacaPNV(id);
            return PartialView(estacas);
        }
        //Salva Curvas
        [HttpPost]
        public ActionResult SalvaPNV(Estaca valor)
        {

            if (ModelState.IsValid)
            {
                _pr.SalvaPNV(valor);

            }

            return RedirectToAction("Edit", "Projetos", new { id = valor.ProjetoId });
        }

        #endregion

        #region CurvasVerticais

        [HttpPost]
        public async Task<ActionResult> GeraCurvasVerticais(int id)
        {
            _pr.CriaCurvasVerticais(id);
            return RedirectToAction("Edit", "Projetos", new { id = id });
        }

        #endregion

        #region RelatorioEAvancadas

        //Utilitários
        //UI para Limpeza
        public async Task<ActionResult> ConfirmaLimpeza(int id)
        {
            Projeto projeto = _db.Projetos.Include(p => p.PontosNotaveis).FirstOrDefaultAsync(x => x.Id == id).Result;
            return PartialView(new EditProjetoViewModel(projeto));
        }
        //Limpa Projeto até os pontos notáveis
        public async Task<ActionResult> LimpaProjeto(EditProjetoViewModel valores)
        {
            if (ModelState.IsValid)
            {
                var resultado = _pr.LimpaProjeto(valores.Id);
                return RedirectToAction("Edit", new { id = valores.Id });
            }
            return RedirectToAction("Edit", new { id = valores.Id });
        }

        //Gera relatório completo
        public async Task<ActionResult> RelatorioCurvas(int id)
        {
            var resultado = await _pr.BuscaProjetosEdit(id);
            return View(resultado.TransportBag as Projeto);
        }

        //Gera relatório completo
        public async Task<ActionResult> Relatorio(int id)
        {
            var resultado = await _pr.BuscaProjetosEdit(id);
            var projeto = new RelatorioProjetoViewModel(resultado.TransportBag as Projeto);
            projeto.Estacas = await _pr.GeraEstacasRelatorio(id);
            projeto.Greides = _pr.GeraGreide(id);
            var cotas = projeto.Greides.Select(x => x.Cota);
            var cotasNaturais = projeto.Greides.Select(x => x.CotaNatural);
            var nomes = projeto.Greides.Select(x => x.Nome);
            var javaScriptSerializer = new JavaScriptSerializer();
            ViewBag.cotas = javaScriptSerializer.Serialize(cotas);
            ViewBag.cotasNaturais = javaScriptSerializer.Serialize(cotasNaturais);
            ViewBag.nomes = javaScriptSerializer.Serialize(nomes);
            var curvasVerticais = await _pr.GetCurvasVerticais(id);
            ViewBag.curvasVerticais = curvasVerticais;
            return View(projeto);
        }



        //Gera relatório completo
        public async Task<ActionResult> Mapa(int id)
        {
            var resultado = await _pr.BuscaProjetosEdit(id);
            var projeto = new RelatorioProjetoViewModel(resultado.TransportBag as Projeto);
            var meio = projeto.PontoNotaveis.Count() / 2;
            var pontos = projeto.PontoNotaveis as List<PontoNotavel>;
            projeto.Estacas = await _pr.GeraEstacasRelatorio(id);
            var javaScriptSerializer = new JavaScriptSerializer();
            var pontoMedio = new {lat = pontos[meio].Coordenada.Lat, lng = pontos[meio].Coordenada.Lng};
            var path = projeto.PontoNotaveis.Select(x => new {lat = x.Coordenada.Lat, lng = x.Coordenada.Lng});
            ViewBag.PontoMedio = javaScriptSerializer.Serialize(pontoMedio);
            ViewBag.path = javaScriptSerializer.Serialize(path);
            return View(projeto);
        }

        [HttpPost]
        public async Task<ActionResult> GeraEstacas(int id)
        {

            var estacas = await _pr.GeraEstacas(id);
            int qtd = _pr.SalvaEstacas(estacas, id);
            return Json("Foram salvas " + qtd + " estacas.");
        }
       

        #endregion

        

        

        
        


        
       


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
