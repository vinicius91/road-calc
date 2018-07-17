using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Mvc;
using MathNet.Numerics.LinearRegression;
using RoadCalc.Helpers;
using RoadCalc.Models.Entities;
using RoadCalc.Models.ViewModels;

namespace RoadCalc.Repositories
{
    public class ProjetoRepository : RepositoryBase<Projeto>
    {

        #region BaseProjeto

        //Conta inicializadores
        public async Task<int> ContaInicializadoresDeCurva(int projetoId)
        {
            return await Db.InicializadorDeCurvas.CountAsync(x => x.CurvaInicializada == false && x.Projeto.Id == projetoId);
        }

        //Busca SelectItem para Classes
        public Resultado BuscaSelectPontos(int id)
        {
            var resultado = new Resultado(true, new List<string>());
            try
            {
                var selectListItems = Db.PontosNotaveis.Where(x => x.ProjetoId == id).Select(
                    x => new SelectListItem() { Text = x.Nome, Value = x.Id.ToString() }).AsEnumerable();
                resultado.TransportBag = selectListItems;
            }
            catch (Exception e)
            {
                resultado.Valor = false;
                resultado.ErrorsList = new List<string>() { e.Message };
            }

            return resultado;
        }


        //Busca SelectItem para Classes
        public Resultado BuscaSelectEstacasDeProjeto(int projetoId)
        {
            var resultado = new Resultado(true, new List<string>());
            try
            {
                var selectListItems = Db.Estacas.Where(x => x.ProjetoId == projetoId && x.Relatorio).OrderBy(x => x.Numero).ThenBy(x => x.Intermediario).ToList().Select(
                    x => new SelectListItem() { Text = @"Estaca " + x.Numero + @" - " + x.Intermediario.ToString("F2") + @" metros ", Value = x.Id.ToString() }).AsEnumerable();
                resultado.TransportBag = selectListItems;
            }
            catch (Exception e)
            {
                resultado.Valor = false;
                resultado.ErrorsList = new List<string>() { e.Message };
            }

            return resultado;
        }

        //Busca SelectItem para Classes
        public Resultado BuscaSelectClassesDeProjeto()
        {
            var resultado = new Resultado(true, new List<string>());
            try
            {
                var selectListItems = Db.ClasseDeProjetos.Select(
                    x => new SelectListItem() { Text = @"Classe " + x.Nome + @" - Terreno " + x.Relevo.ToString() + @" - " + x.Caracteristicas.ToString(), Value = x.Id.ToString() }).AsEnumerable();
                resultado.TransportBag = selectListItems;
            }
            catch (Exception e)
            {
                resultado.Valor = false;
                resultado.ErrorsList = new List<string>() { e.Message };
            }

            return resultado;
        }

        //Busca Pontos Notaveis com coordenadas corretas
        public async Task<List<PontoNotavel>> GetPontosComCoord(int projetoId)
        {
            List<PontoNotavel> pontosNotaveis = await Db.PontosNotaveis.Where(x => x.ProjetoId == projetoId).ToListAsync();
            List<PontoNotavel> pontosNotaveisAt = new List<PontoNotavel>();
            List<Coordenada> coordenadas = await Db.Coordenadas.ToListAsync();
            foreach (PontoNotavel ponto in pontosNotaveis.AsParallel())
            {
                pontosNotaveisAt.Add(new PontoNotavel()
                {
                    Coordenada = coordenadas.FirstOrDefault(x => x.Id == ponto.CoordenadaId),
                    CoordenadaId = ponto.CoordenadaId,
                    Id = ponto.Id,
                    Nome = ponto.Nome,
                    Projeto = ponto.Projeto,
                    ProjetoId = ponto.ProjetoId

                });
            }

            return pontosNotaveisAt;


        }

        //Busca Projeto Edit
        public async Task<Resultado> BuscaProjetosEdit(int projetoId)
        {
            var resultado = new Resultado(true, new List<string>());
            try
            {
                Projeto projeto = await Db.Projetos.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == projetoId);
                ClasseDeProjeto classeDeProjeto = await Db.ClasseDeProjetos.FirstOrDefaultAsync(x => x.Id == projeto.ClasseDeProjetoId);
                List<CurvaHorizontal> curvas = await Db.Curvas.Include(x => x.EstacaPC).Include(x => x.EstacaPT).Where(x => x.ProjetoId == projetoId).ToListAsync();
                List<PontoNotavel> pontosNotaveis = await Db.PontosNotaveis.Where(x => x.ProjetoId == projetoId).ToListAsync();
                List<Trecho> trechos = await Db.Trechos.Where(x => x.ProjetoId == projetoId).ToListAsync();
                List<PontoNotavel> pontosNotaveisAt = new List<PontoNotavel>();
                List<Coordenada> coordenadas = await Db.Coordenadas.ToListAsync();

                foreach (PontoNotavel ponto in pontosNotaveis.AsParallel())
                {
                    pontosNotaveisAt.Add(new PontoNotavel()
                    {
                        Coordenada = coordenadas.FirstOrDefault(x => x.Id == ponto.CoordenadaId),
                        CoordenadaId = ponto.CoordenadaId,
                        Id = ponto.Id,
                        Nome = ponto.Nome,
                        Projeto = ponto.Projeto,
                        ProjetoId = ponto.ProjetoId

                    });
                }

                projeto.ClasseDeProjeto = classeDeProjeto;
                projeto.Curvas = curvas;
                projeto.PontosNotaveis = pontosNotaveisAt;
                projeto.Trechos = trechos;
                resultado.TransportBag = projeto;

            }
            catch (Exception e)
            {
                resultado.Valor = false;
                resultado.ErrorsList = new List<string>() { e.Message };
            }

            return resultado;
        }


        //Busca Projetos para Index
        public async Task<Resultado> BuscaProjetosLista(int userId)
        {
            var resultado = new Resultado(true, new List<string>());
            List<ListProjetoViewModel> projetosViewModel = new List<ListProjetoViewModel>();
            try
            {
                var projetos = await Db.Projetos
                    .Where(x => x.UserId == userId)
                    .Include(p => p.User)
                    .ToListAsync();

                foreach (var projeto in projetos.AsParallel())
                {
                    projetosViewModel.Add(new ListProjetoViewModel(projeto)
                    {
                        NumeroDeTrechos = await Db.Trechos.CountAsync(x => x.ProjetoId == projeto.Id),
                        NumeroDePontos = await Db.PontosNotaveis.CountAsync(x => x.ProjetoId == projeto.Id),
                        NumeroDeCurvas = await Db.Curvas.CountAsync(x => x.ProjetoId == projeto.Id)
                    });

                }
                resultado.TransportBag = projetosViewModel;

            }
            catch (Exception e)
            {
                resultado.Valor = false;
                resultado.ErrorsList = new List<string>() { e.Message };
            }

            return resultado;
        }

        //Método para limpeza de projeto até a parte dos pontos salvos
        public async Task<Resultado> LimpaProjeto(int id)
        {
            var resultado = new Resultado(true, new List<string>());
            try
            {
                var curvas = await Db.Curvas.Where(x => x.ProjetoId == id).ToListAsync();
                var trechos = await Db.Trechos.Where(x => x.ProjetoId == id).ToListAsync();
                Db.Curvas.RemoveRange(curvas);
                Db.Trechos.RemoveRange(trechos);
                Db.SaveChanges();
            }
            catch (Exception e)
            {
                resultado.Valor = false;
                resultado.ErrorsList = new List<string>() { e.Message };
            }

            return resultado;
        }
        #endregion

        #region Estacas
        public List<Estaca> CriaEstacas(List<CurvaHorizontal> curvas)
        {
            var estacas = new List<Estaca>();

            for (int i = 0; i < curvas.Count; i++)
            {
                if (i == 0)
                {
                    //Calcula a quantidade de estacas para cada um dos trechos
                    var qtdEstacasInicial = curvas[i].TrechoInicial.EstacaFinal.Numero - curvas[i].TrechoInicial.EstacaInicial.Numero;
                    var qtdEstacasFinal = (curvas[i].TrechoFinal.Distancia - curvas[i].TangenteExterior) / 20;
                    //Busca a estaca inicial do projeto
                    var estacaInicial = curvas[i].TrechoInicial.EstacaInicial;
                    estacas.Add(new Estaca()
                    {
                        Coordenada = estacaInicial.Coordenada,
                        CotaVermelha = estacaInicial.Coordenada.Z,
                        Numero = estacaInicial.Numero,
                        Intermediario = estacaInicial.Intermediario,
                        Projeto = curvas[i].Projeto,
                        Relatorio = true
                    });
                    Estaca estacaZero;
                    //Cria a estaca Zero para Incremento das outras estacas
                    if (estacaInicial.Intermediario != 0)
                    {
                        estacaZero = new Estaca()
                        {
                            Coordenada =
                                CalculadoraCoordenadas.CalculaCoordenadaComCoorAziDist(estacaInicial.Coordenada,
                                    curvas[i].TrechoInicial.Azimute,
                                    estacaInicial.Intermediario * (-1)),
                            CotaVermelha = estacaInicial.Coordenada.Z,
                            Numero = estacaInicial.Numero,
                            Intermediario = 0,
                            Projeto = curvas[i].Projeto
                        };
                    }
                    else
                    {
                        estacaZero = estacaInicial;
                    }
                    //Cria as estacas do Trecho Inicial
                    for (int b = 0; estacaZero.Numero + b + 1 <= curvas[i].TrechoInicial.EstacaFinal.Numero; b++)
                    {
                        estacas.Add(new Estaca()
                        {
                            Coordenada = CalculadoraCoordenadas.CalculaCoordenadaComCoorAziDist(estacaZero.Coordenada,
                                curvas[i].TrechoInicial.Azimute, 20 * (b + 1)),
                            CotaVermelha = estacaZero.Coordenada.Z,
                            Numero = estacaZero.Numero + (b + 1),
                            Intermediario = 0,
                            Projeto = curvas[i].Projeto,
                            Relatorio = true
                        });
                    }
                    foreach (var estacaDeCurva in curvas[i].GetEstacasDeCurva())
                    {
                        estacas.Add(estacaDeCurva);
                    }
                    //Muda a estaca Zero para o trecho final
                    estacaZero = curvas[i].TrechoFinal.EstacaInicial;
                    //Adiciona Curvas do Trecho Final
                    for (int b = 0; estacaZero.Numero + b + 1 <= curvas[i].TrechoFinal.EstacaFinal.Numero; b++)
                    {
                        estacas.Add(new Estaca()
                        {
                            Coordenada = CalculadoraCoordenadas.CalculaCoordenadaComCoorAziDist(estacaZero.Coordenada,
                                curvas[i].TrechoFinal.Azimute,
                                (20 * (b + 1)) - estacaZero.Intermediario),
                            CotaVermelha = estacaZero.Coordenada.Z,
                            Numero = estacaZero.Numero + (b + 1),
                            Intermediario = 0,
                            Projeto = curvas[i].Projeto
                        });
                    }


                }
                else
                {
                    foreach (var estacaDeCurva in curvas[i].GetEstacasDeCurva())
                    {
                        estacas.Add(estacaDeCurva);
                    }

                    var estacaZero = curvas[i].TrechoFinal.EstacaInicial;
                    //Adiciona Curvas do Trecho Final
                    for (int b = 0; estacaZero.Numero + b + 1 <= curvas[i].TrechoFinal.EstacaFinal.Numero; b++)
                    {
                        estacas.Add(new Estaca()
                        {
                            Coordenada = CalculadoraCoordenadas.CalculaCoordenadaComCoorAziDist(estacaZero.Coordenada,
                                curvas[i].TrechoFinal.Azimute,
                                (20 * (b + 1)) - estacaZero.Intermediario),
                            CotaVermelha = estacaZero.Coordenada.Z,
                            Numero = estacaZero.Numero + (b + 1),
                            Intermediario = 0,
                            Projeto = curvas[i].Projeto
                        });
                    }
                }

            }

            return estacas;
        }



        public async Task<List<Estaca>> GeraEstacas(int projetoId)
        {
            var curvas = await BuscaCurvasParaEstaca(projetoId);
            var estacas =  CriaEstacas(curvas);
            for (int i = 0; i < estacas.Count; i++)
            {
                estacas[i].Coordenada.Z = await ProjetosHelper.GetElevationAsync(estacas[i].Coordenada);
            }
            estacas = CalculaCotas(projetoId, estacas);
            

            return estacas;
        }


        public List<Estaca> CalculaCotas(int projetoId, List<Estaca> estacas)
        {
            if (Db.PontosNotaveisVerticais.Count(x => x.ProjetoId == projetoId) == 0)
            {
                var cotaInicial = estacas.FirstOrDefault()?.CotaVermelha??0;
                var cotaFinal = estacas.LastOrDefault()?.CotaVermelha??0;
                var distancia = estacas.LastOrDefault()?.Numero * 20 + estacas.LastOrDefault()?.Intermediario?? 0;
                var inclinacao = (cotaFinal - cotaInicial) / distancia;
                for (int i = 0; i < estacas.Count; i++)
                {
                    estacas[i].CotaVermelha = cotaInicial + (estacas[i].Numero * 20) * inclinacao;
                }
            }
            return estacas;
        }

        public async Task<List<CurvaHorizontal>> BuscaCurvasParaEstaca(int projetoId)
        {
            var curvas = await Db.Curvas
                .Include(x => x.Projeto)
                .Include(x => x.TrechoInicial)
                .Include("TrechoInicial.EstacaInicial")
                .Include("TrechoInicial.EstacaInicial.Coordenada")
                .Include("TrechoInicial.EstacaFinal")
                .Include("TrechoInicial.EstacaFinal.Coordenada")
                .Include(x => x.TrechoFinal)
                .Include("TrechoFinal.EstacaInicial")
                .Include("TrechoFinal.EstacaInicial.Coordenada")
                .Include("TrechoFinal.EstacaFinal")
                .Include("TrechoFinal.EstacaFinal.Coordenada")
                .Include(x => x.EstacaPC)
                .Include("EstacaPC.Coordenada")
                .Include(x => x.EstacaPT)
                .Include("EstacaPT.Coordenada")
                .Where(x => x.ProjetoId == projetoId)
                .ToListAsync();

            return curvas;
        }

        public int SalvaEstacas(List<Estaca> estacas, int projetoId)
        {
            int qtd = 0;
            foreach (var estaca in estacas)
            {
                try
                {
                    estaca.Relatorio = true;
                    if (estaca.Id == 0)
                    {
                        Db.Estacas.Add(estaca);
                    }
                    else
                    {
                        Db.Entry(estaca).State = EntityState.Modified;
                    }
                    qtd++;
                }
                catch (InvalidOperationException e)
                {
                    Debug.WriteLine(e.Message);
                    Debug.WriteLine(e.Source);
                    throw;
                }
            }
            var projeto = Db.Projetos.FirstOrDefault(x => x.Id == projetoId);
            projeto.EstacasGeradas = true;
            Db.Entry(projeto).State = EntityState.Modified;
            Db.SaveChanges();
            return qtd;
        }

        public Task<List<Estaca>> GeraEstacasRelatorio(int id)
        {
            return Db.Estacas.Where(x => x.ProjetoId == id && x.Relatorio).OrderBy(x => x.Numero).ThenBy(x => x.Intermediario).ToListAsync();
        }

        #endregion

        #region Pontos

        //Método para salvar Pontos
        public async Task<Resultado> SalvaPonto(double coordX, double coordY, double cota, string zoneLetter, int zoneNumber, string nome, int projetoId)
        {
            var resultado = new Resultado(true, new List<string>());
            try
            {
                var projeto = await Db.Projetos.FirstOrDefaultAsync(x => x.Id == projetoId);
                Coordenada coordenada;
                coordenada = projeto.CoordenadasReais ? new Coordenada(coordX, coordY, cota, zoneNumber, zoneLetter) : new Coordenada(coordX, coordY, cota, false);
                PontoNotavel ponto = new PontoNotavel() { Nome = nome, Coordenada = coordenada };
                var elevation = ProjetosHelper.GetElevation(coordenada);
                ponto.Coordenada.Z = elevation;
                if (projeto.PontosNotaveis != null)
                {
                    projeto.PontosNotaveis.Add(ponto);
                }
                else
                {
                    projeto.PontosNotaveis = new List<PontoNotavel> { ponto };
                }
                Db.Entry(projeto).State = EntityState.Modified;
                Db.SaveChanges();
            }
            catch (Exception e)
            {
                resultado.Valor = false;
                resultado.ErrorsList = new List<string>() { e.Message };
                throw;
            }

            return resultado;
        }

        #endregion

        #region Curvas

        //Método para salvar Curvas
        public async Task<Resultado> SalvaCurva(int projetoId, int trechoInicialId, int trechoFinalId, int raio, string nome, double lc)
        {
            var resultado = new Resultado(true, new List<string>());
            try
            {
                //BuscaInicializador Completo
                var inicializador = await Db.InicializadorDeCurvas.FirstOrDefaultAsync(x => x.ProjetoId == projetoId && x.TrechoInicialId == trechoInicialId && x.TrechoFinalId == trechoFinalId);
                //Busca Projeto
                var projeto = await Db.Projetos.Include(x => x.Curvas).Include(x => x.ClasseDeProjeto).FirstOrDefaultAsync(x => x.Id == projetoId);
                //Busca Trecho Inicial
                var trechoInicial = await Db.Trechos.Include(x => x.EstacaInicial).Include("EstacaInicial.Coordenada").FirstOrDefaultAsync(x => x.Id == trechoInicialId);
                //Buscar Trecho Final
                var trechoFinal = await Db.Trechos.Include(x => x.EstacaFinal).Include("EstacaInicial.Coordenada").FirstOrDefaultAsync(x => x.Id == trechoFinalId);
                //Cria a curva para o projeto
                var curva = new CurvaHorizontal(trechoInicial, trechoFinal, raio, projeto, lc, 20, projeto.ClasseDeProjeto.VelProjeto, nome)
                {
                    EstacaInicialId = trechoInicial.EstacaInicialId,
                    EstacaFinalId = trechoFinal.EstacaFinalId
                };

                //Adiciona Curva ao projeto
                projeto.Curvas.Add(curva);
                //Salva Alterações
                Db.Entry(projeto).State = EntityState.Modified;
                Db.InicializadorDeCurvas.Remove(inicializador);
                Db.SaveChanges();
                var curvaSalva = Db.Curvas.FirstOrDefault(x => x.Nome == curva.Nome && x.ProjetoId == projetoId);
                if (curva.Transicao)
                {
                    trechoInicial.EstacaFinal = curvaSalva?.EstacaTS;
                    trechoFinal.EstacaInicial = curvaSalva?.EstacaST;
                }
                else
                {
                    trechoInicial.EstacaFinal = curvaSalva?.EstacaPC;
                    trechoFinal.EstacaInicial = curvaSalva?.EstacaPT;
                }
                Db.Entry(trechoInicial).State = EntityState.Modified;
                Db.Entry(trechoFinal).State = EntityState.Modified;
                Db.SaveChanges();
            }
            catch (Exception e)
            {
                resultado.Valor = false;
                resultado.ErrorsList = new List<string>() { e.Message };
                throw;
            }

            return resultado;
        }

        //Método que busca as Curvas para serem geradas no projeto criandos os inicializadores de curva que servem como base para a criação das curvas
        public async Task<List<InicializadorDeCurva>> BuscaCurvasDeProjeto(int projetoId)
        {
            Projeto projeto = Db.Projetos.Include("ClasseDeProjeto").FirstOrDefault(x => x.Id == projetoId);
            List<Trecho> trechos = await BuscaTrechosDeProjeto(projetoId);
            List<InicializadorDeCurva> inicializadoresDeCurvas = new List<InicializadorDeCurva>();

            var trechoDaVez = trechos.FirstOrDefault(x => x.PontoInicial.Id == projeto.PontoInicialId);
            do
            {
                var trechoComplementar = trechos.FirstOrDefault(x => x.PontoInicial.Id == trechoDaVez.PontoFinal.Id);
                inicializadoresDeCurvas.Add(new InicializadorDeCurva()
                {
                    Corda = 10,
                    Nome = trechoDaVez.PontoFinal.Nome,
                    Projeto = projeto,
                    Raio = projeto.ClasseDeProjeto.RaioMinSupEleMax,
                    SuperElevacao = (double)projeto.ClasseDeProjeto.SupEleMax / 100,
                    TrechoFinal = trechoComplementar,
                    TrechoInicial = trechoDaVez
                });

                trechos.Remove(trechoDaVez);
                trechoDaVez = trechoComplementar;

            } while (trechos.Count > 0);

            return inicializadoresDeCurvas;
        }

        //Busca os trechos do projeto
        public async Task<List<Trecho>> BuscaTrechosDeProjeto(int projetoId)
        {
            return await Db.Trechos
                    .Include("PontoInicial")
                    .Include("PontoFinal")
                    .Include("PontoInicial.Coordenada")
                    .Include("PontoFinal.Coordenada")
                    .Include("EstacaInicial")
                    .Include("EstacaFinal")
                    .Where(x => x.ProjetoId == projetoId)
                    .ToListAsync();

        }

        //Salva os inicicializadores de curva no projeto habilitando a criação de curvas para o mesmo
        //Esse método verifica se o possível inicializador possui os dois trechos antes de criar o inicializador
        //Evitando a criação de um inicializador com o último trecho do projeto
        public void SalvaInicializadores(List<InicializadorDeCurva> inicializadoresDeCurvas)
        {
            var list = inicializadoresDeCurvas.AsParallel();
            foreach (var inicializadoreDeCurva in list)
            {
                if (inicializadoreDeCurva.TrechoFinal == null) continue;
                Db.InicializadorDeCurvas.Add(inicializadoreDeCurva);
                Db.SaveChanges();
            }
        }

        #endregion

        #region Trechos

        //Método para a geração dos trechos com base nos pontos do projeto
        public async Task<Resultado> SalvaTrechos(int id, int estacaInicial, double complementoDeEstaca)
        {
            var resultado = new Resultado(true, new List<string>());
            try
            {
                Projeto projeto = await Db.Projetos.FirstOrDefaultAsync(x => x.Id == id);
                List<PontoNotavel> pontos = await Db.PontosNotaveis
                    .Include(x => x.Coordenada)
                    .Where(x => x.ProjetoId == id)
                    .ToListAsync();
                var trechos = projeto.CalculaTrechos(new Estaca(estacaInicial, complementoDeEstaca,
                    projeto.PontosNotaveis.FirstOrDefault()), pontos);
                projeto.Trechos = new List<Trecho>();
                foreach (var trecho in trechos)
                {
                    projeto.Trechos.Add(trecho);
                }
                Db.Entry(projeto).State = EntityState.Modified;
                Db.SaveChanges();
            }
            catch (Exception e)
            {
                resultado.Valor = false;
                resultado.ErrorsList = new List<string>() { e.Message };
                throw;
            }

            return resultado;
        }

        #endregion

        #region PontoNotavelVertical

        public Estaca BuscaEstacaPNV(int id)
        {
            return Db.Estacas.Include(x => x.Coordenada).FirstOrDefault(x => x.Id == id);
        }

        public void SalvaPNV(Estaca valor)
        {
            var estaca = Db.Estacas.Find(valor.Id);
            estaca.CotaVermelha = valor.CotaVermelha;
            Db.Entry(estaca).State = EntityState.Modified;
            valor.Coordenada = Db.Coordenadas.Find(valor.CoordenadaId);
            var pnv = new PontoNotavelVertical(valor);
            Db.PontosNotaveisVerticais.Add(pnv);
            Db.SaveChanges();
        }

        public Estaca PrimeiraEstaca(int id)
        {
            return Db.Estacas.FirstOrDefault(x => x.ProjetoId == id && x.Numero == 0);
        }

        public void CriaCurvasVerticais(int projetoId)
        {

            List<PontoNotavelVertical> pnvs;
            Db.Configuration.LazyLoadingEnabled = true;
            try
            {
                pnvs = Db.PontosNotaveisVerticais.Where(x => x.ProjetoId == projetoId).OrderBy(x => x.Estaca.Numero)
                    .ThenBy(x => x.Estaca.Intermediario).ToList();
            }
            catch (TargetInvocationException e)
            {
                Debug.WriteLine(e);
                throw;
            }
            try
            {
                var pi = pnvs[0];
                var p1 = pnvs[1];
                var p2 = pnvs[2];
                var pf = pnvs[3];

                // Curva P1

                var distanciaPiP1 = p1.Estaca.Numero * 20 + p1.Estaca.Intermediario -
                                           pi.Estaca.Numero * 20 - pi.Estaca.Intermediario;

                var diferencaCotaPiP1 = p1.Estaca.CotaVermelha - pi.Estaca.CotaVermelha;
                var iPiP1 = CurvaVerticalHelper.CalculaInclinacaoEntreDoisPontos(pi.Estaca.CotaVermelha,
                    p1.Estaca.CotaVermelha, pi.Estaca.Numero * 20 - pi.Estaca.Intermediario,
                    p1.Estaca.Numero * 20 + p1.Estaca.Intermediario);

                var iP1P2 = CurvaVerticalHelper.CalculaInclinacaoEntreDoisPontos(p1.Estaca.CotaVermelha,
                    p2.Estaca.CotaVermelha, p1.Estaca.Numero * 20 - p1.Estaca.Intermediario,
                    p2.Estaca.Numero * 20 + p2.Estaca.Intermediario);

                var curvaP1 = new CurvaVertical(p1, iPiP1, iP1P2);

                GeraEstaca(curvaP1, pi, p1, iPiP1, iP1P2, projetoId);
                

                // Curva P2

                var iP2Pf = CurvaVerticalHelper.CalculaInclinacaoEntreDoisPontos(p2.Estaca.CotaVermelha,
                    pf.Estaca.CotaVermelha, p2.Estaca.Numero * 20 - p2.Estaca.Intermediario,
                    pf.Estaca.Numero * 20 + pf.Estaca.Intermediario);

                var curvaP2 = new CurvaVertical(p1, iP1P2, iP2Pf);
                GeraEstaca(curvaP2, p1, p2, iP1P2, iP2Pf, projetoId);


                //var nPontos = pnvs.Count();

                //for (int i = 1; i < nPontos - 1; i++)
                //{
                //    var distanciaInicial = pnvs[i].Estaca.Numero * 20 + pnvs[i].Estaca.Intermediario -
                //                           pnvs[i - 1].Estaca.Numero * 20 - pnvs[i - 1].Estaca.Intermediario;

                //    var diferencaCotaInicial = pnvs[i].Estaca.CotaVermelha - pnvs[i - 1].Estaca.CotaVermelha;
                //    var iinicial = (diferencaCotaInicial / distanciaInicial) * 100;
                //    var distanciaFinal = pnvs[i + 1].Estaca.Numero * 20 + pnvs[i + 1].Estaca.Intermediario -
                //                         pnvs[i].Estaca.Numero * 20 - pnvs[i].Estaca.Intermediario;
                //    var diferencaCotaFinal = pnvs[i + 1].Estaca.CotaVermelha - pnvs[i].Estaca.CotaVermelha;
                //    var ifinal = (diferencaCotaFinal / distanciaFinal) * 100 == 0 ? 0.000000001 : (diferencaCotaFinal / distanciaFinal) * 100;
                //    var curva = new CurvaVertical(pnvs[i], iinicial, ifinal);

                //    //Gera estaca PIV
                //    var estacaPIV = Db.Estacas.Find(pnvs[i].EstacaId);
                //    estacaPIV.CotaVermelha = curva.TipoVertical == TipoVertical.Concava
                //        ? estacaPIV.CotaVermelha - curva.OMax
                //        : estacaPIV.CotaVermelha + curva.OMax;
                //    curva.EstacaPIV = estacaPIV;
                //    curva.EstacaPIVId = estacaPIV.Id;
                //    var numeroPCV = estacaPIV.Numero - (curva.L / 20);
                //    var numeroPTV = estacaPIV.Numero + (curva.L / 20);
                //    var estacaPCV = Db.Estacas.FirstOrDefault(x => x.Numero == numeroPCV && x.ProjetoId == projetoId);
                //    var estacaPTV = Db.Estacas.FirstOrDefault(x => x.Numero == numeroPTV && x.ProjetoId == projetoId);

                //    if (curva.TipoVertical == TipoVertical.Concava)
                //    {
                //        estacaPCV.CotaVermelha = estacaPIV.CotaVermelha - (iinicial/100) * curva.L;
                //        estacaPTV.CotaVermelha = estacaPIV.CotaVermelha + (ifinal/100) * curva.L;
                //    }
                //    else
                //    {
                //        estacaPCV.CotaVermelha = estacaPIV.CotaVermelha - (iinicial / 100) * curva.L;
                //        estacaPTV.CotaVermelha = estacaPIV.CotaVermelha + (ifinal / 100) * curva.L;
                //    }

                //    curva.EstacaPCV = estacaPCV;
                //    curva.EstacaPCVId = estacaPCV.Id;
                //    curva.EstacaPTV = estacaPTV;
                //    curva.EstacaPIVId = estacaPTV.Id;
                //    var numeroInicial = pnvs[i - 1].Estaca.Numero;
                //    Db.CurvasVerticais.Add(curva);
                //    var estacasIniciais = Db.Estacas
                //        .Where(x => x.Numero > numeroInicial && x.Numero < numeroPCV && x.ProjetoId == projetoId).ToList();
                //    for (int j = 0; j < estacasIniciais.Count(); j++)
                //    {
                //        estacasIniciais[j].CotaVermelha = CalculaNovaElevacao(pnvs[i - 1].Estaca, estacasIniciais[j], iinicial);
                //        Db.Entry(estacasIniciais[j]).State = EntityState.Modified;
                //    }
                //    var estacasIntermediarias = Db.Estacas
                //        .Where(x => x.Numero > numeroPCV && x.Numero < numeroPTV && x.ProjetoId == projetoId).ToList();

                //    //Estacas Intermediarias
                //    if (estacasIntermediarias.Count > 1)
                //    {
                //        double oj;
                //        for (int k = 0; k < estacasIntermediarias.Count; k++)
                //        {
                //            oj = CalculaOj(estacaPTV, estacasIntermediarias[k], curva.L, curva.OMax);
                //            if (estacaPCV.CotaVermelha >= estacaPTV.CotaVermelha)
                //            {
                //                if (estacasIntermediarias[k].Numero < estacaPIV.Numero)
                //                {
                //                    estacasIntermediarias[k].CotaVermelha = estacaPIV.CotaVermelha + (curva.OMax - oj);
                //                }
                //                else
                //                {
                //                    estacasIntermediarias[k].CotaVermelha = estacaPIV.CotaVermelha - (curva.OMax - oj);
                //                }
                //            }
                //            else
                //            {
                //                if (estacasIntermediarias[k].Numero > estacaPIV.Numero)
                //                {
                //                    estacasIntermediarias[k].CotaVermelha = estacaPIV.CotaVermelha - (curva.OMax - oj);
                //                }
                //                else
                //                {
                //                    estacasIntermediarias[k].CotaVermelha = estacaPIV.CotaVermelha + (curva.OMax - oj);
                //                }
                //            }
                //            Db.Entry(estacasIntermediarias[k]).State = EntityState.Modified;
                //        }
                //    }
                //    //Ultima curva
                //    if (i == pnvs.Count -2)
                //    {
                //        var estacasFinais = Db.Estacas
                //            .Where(x => x.Numero > numeroPTV && x.ProjetoId == projetoId).ToList();
                //        for (int m = 0; m < estacasFinais.Count(); m++)
                //        {
                //            estacasFinais[m].CotaVermelha = CalculaNovaElevacao(estacaPTV, estacasFinais[m], ifinal);
                //            Db.Entry(estacasFinais[m]).State = EntityState.Modified;
                //        }
                //    }
                //    Db.SaveChanges();
                //}
                Db.Configuration.LazyLoadingEnabled = false;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }

        }


        private void GeraEstaca(CurvaVertical curvaP1, PontoNotavelVertical pi, PontoNotavelVertical p1, double iPiP1, double iP1P2, int projetoId)
        {
            //Gera estaca PIV
            var estacaPIV = Db.Estacas.Find(p1.EstacaId);
            estacaPIV.CotaVermelha = curvaP1.TipoVertical == TipoVertical.Concava
                ? estacaPIV.CotaVermelha - curvaP1.OMax
                : estacaPIV.CotaVermelha + curvaP1.OMax;
            curvaP1.EstacaPIV = estacaPIV;
            curvaP1.EstacaPIVId = estacaPIV.Id;
            var numeroPCV = estacaPIV.Numero - (curvaP1.L / 20);
            var numeroPTV = estacaPIV.Numero + (curvaP1.L / 20);
            var estacaPCV = Db.Estacas.FirstOrDefault(x => x.Numero == numeroPCV && x.ProjetoId == projetoId);
            var estacaPTV = Db.Estacas.FirstOrDefault(x => x.Numero == numeroPTV && x.ProjetoId == projetoId);

            if (curvaP1.TipoVertical == TipoVertical.Concava)
            {
                estacaPCV.CotaVermelha = estacaPIV.CotaVermelha - (iPiP1 / 100) * curvaP1.L;
                estacaPTV.CotaVermelha = estacaPIV.CotaVermelha + (iP1P2 / 100) * curvaP1.L;
            }
            else
            {
                estacaPCV.CotaVermelha = estacaPIV.CotaVermelha - (iPiP1 / 100) * curvaP1.L;
                estacaPTV.CotaVermelha = estacaPIV.CotaVermelha + (iP1P2 / 100) * curvaP1.L;
            }

            curvaP1.EstacaPCV = estacaPCV;
            curvaP1.EstacaPCVId = estacaPCV.Id;
            curvaP1.EstacaPTV = estacaPTV;
            curvaP1.EstacaPIVId = estacaPTV.Id;
            var numeroInicial = pi.Estaca.Numero;
            Db.CurvasVerticais.Add(curvaP1);
            var estacasIniciais = Db.Estacas
                .Where(x => x.Numero > numeroInicial && x.Numero < numeroPCV && x.ProjetoId == projetoId).ToList();
            for (int j = 0; j < estacasIniciais.Count(); j++)
            {
                estacasIniciais[j].CotaVermelha = CalculaNovaElevacao(pi.Estaca, estacasIniciais[j], iPiP1);
                Db.Entry(estacasIniciais[j]).State = EntityState.Modified;
            }
            var estacasIntermediarias = Db.Estacas
                .Where(x => x.Numero > numeroPCV && x.Numero < numeroPTV && x.ProjetoId == projetoId).ToList();

            //Estacas Intermediarias
            if (estacasIntermediarias.Count > 1)
            {
                double oj;
                for (int k = 0; k < estacasIntermediarias.Count; k++)
                {
                    oj = CalculaOj(estacaPTV, estacasIntermediarias[k], curvaP1.L, curvaP1.OMax);
                    if (estacaPCV.CotaVermelha >= estacaPTV.CotaVermelha)
                    {
                        if (estacasIntermediarias[k].Numero < estacaPIV.Numero)
                        {
                            estacasIntermediarias[k].CotaVermelha = estacaPIV.CotaVermelha + (curvaP1.OMax - oj);
                        }
                        else
                        {
                            estacasIntermediarias[k].CotaVermelha = estacaPIV.CotaVermelha - (curvaP1.OMax - oj);
                        }
                    }
                    else
                    {
                        if (estacasIntermediarias[k].Numero > estacaPIV.Numero)
                        {
                            estacasIntermediarias[k].CotaVermelha = estacaPIV.CotaVermelha - (curvaP1.OMax - oj);
                        }
                        else
                        {
                            estacasIntermediarias[k].CotaVermelha = estacaPIV.CotaVermelha + (curvaP1.OMax - oj);
                        }
                    }
                    Db.Entry(estacasIntermediarias[k]).State = EntityState.Modified;
                }
            }

            Db.SaveChanges();
        }
       

        public double CalculaNovaElevacao(Estaca estacaInicial, Estaca estacaAtual, double inclinacao)
        {
            var distancia = estacaAtual.DistanciaTotal() - estacaInicial.DistanciaTotal();
            var variacao = distancia * inclinacao / 100;
            return estacaInicial.CotaVermelha + variacao;
        }

        

        public double CalculaOj(Estaca estacaPTV, Estaca estacaAtual, double lb, double omax)
        {
            var dj =  estacaPTV.DistanciaTotal() - estacaAtual.DistanciaTotal();
            var oj = omax * Math.Pow(dj/lb, 2);
            return oj;
        }

        #endregion

        #region Greide

        public List<Greide> GeraGreide(int projetoId)
        {
            var greides = new List<Greide>();
            var estacas = Db.Estacas.Where(x => x.ProjetoId == projetoId && x.Relatorio).OrderBy(x => x.Numero)
                .ThenBy(x => x.Intermediario).ToList();
            var total = estacas.LastOrDefault().Numero;
            Greide novoGreide;
            for (int i = 0; i <= total; i++)
            {
                if (estacas.Any(x => x.Numero == i && x.Intermediario == 0))
                {
                    novoGreide = new Greide(estacas.FirstOrDefault(x => x.Numero == i && x.Intermediario == 0));
                    greides.Add(novoGreide);
                }
                else
                {
                    var inclinacao = GetInclinacaoEstaca(i, projetoId)/100;
                    var cotaAnterior = greides.LastOrDefault().Cota;
                    var naturalAnterior = greides.LastOrDefault().CotaNatural;
                    novoGreide = new Greide()
                    {
                        Numero = i,
                        Locacao = Locacao.Eixo,
                        ProjetoId = projetoId,
                        Cota = cotaAnterior + 20*inclinacao,
                        CotaNatural = naturalAnterior,
                        Nome = "E-"+i+"-00"
                    };
                    greides.Add(novoGreide);
                }
            }

            double[] cotas = greides.Select(x => x.Cota).ToArray();
            double[] cotasNaturais = greides.Select(x => x.CotaNatural).ToArray();
            double[] numero = greides.Select(x => (double) x.Numero).ToArray();

            var result = MathNet.Numerics.Fit.Polynomial(numero, cotas, 5);
            var resultNatural = MathNet.Numerics.Fit.Polynomial(numero, cotasNaturais, 7);

            for (int i = 0; i < greides.Count; i++)
            {
                greides[i].Cota = result[0] + Math.Pow(greides[i].Numero, 1) * result[1] +
                                  Math.Pow(greides[i].Numero, 2) * result[2] +
                                  Math.Pow(greides[i].Numero, 3) * result[3] +
                                  Math.Pow(greides[i].Numero, 4) * result[4] +
                                  Math.Pow(greides[i].Numero, 5) * result[5];
            }

            return greides;
        }

        public double GetInclinacaoEstaca(int numero, int projetoId)
        {
            Db.Configuration.LazyLoadingEnabled = true;
            if(Db.CurvasVerticais.Any(x => x.EstacaPCV.Numero > numero))
            {
                return Db.CurvasVerticais.OrderBy(x => x.EstacaPCV.Numero).FirstOrDefault(x => x.EstacaPCV.Numero > numero).IInicial;
            }
            return Db.CurvasVerticais.OrderByDescending(x => x.EstacaPTV.Numero).FirstOrDefault(x => x.EstacaPTV.Numero < numero).IInicial;
        }

        #endregion

        #region CurvaVertical

        public async Task<List<CurvaVertical>> GetCurvasVerticais(int projetoId)
        {
            return await Db.CurvasVerticais.Where(x => x.ProjetoId == projetoId).ToListAsync();
        }

        #endregion
    }
}