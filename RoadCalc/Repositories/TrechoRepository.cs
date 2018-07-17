using System.Collections.Generic;
using System.Linq;
using RoadCalc.Models.Entities;

namespace RoadCalc.Repositories
{
    public class TrechoRepository : RepositoryBase<Trecho>
    {


        public List<InicializadorDeCurva> BuscaCurvasDeProjeto(int projetoId)
        {
            Projeto projeto = Db.Projetos.Include("ClasseDeProjeto").FirstOrDefault(x => x.Id == projetoId);
            List<Trecho> trechos = BuscaTrechosDeProjeto(projetoId);
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

        public List<Trecho> BuscaTrechosDeProjeto(int projetoId)
        {
            return  Db.Trechos
                    .Include("PontoInicial")
                    .Include("PontoFinal")
                    .Include("EstacaInicial")
                    .Include("EstacaFinal")
                    .Where(x => x.ProjetoId == projetoId)
                    .ToList();

        }

        public void SalvaInicializadores(List<InicializadorDeCurva> inicializadoresDeCurvas)
        {
            foreach (var inicializadoreDeCurva in inicializadoresDeCurvas)
            {
                if (inicializadoreDeCurva.TrechoFinal == null) continue;
                Db.InicializadorDeCurvas.Add(inicializadoreDeCurva);
                Db.SaveChanges();
            }
        }
    }
}