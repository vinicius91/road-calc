using System.Collections.Generic;
using System.Linq;
using RoadCalc.Models.Identity;

namespace RoadCalc.Models.Entities
{
    public class Projeto : EntityBase
    {

        public int ClasseDeProjetoId { get; set; }

        public int PontoInicialId { get; set; }

        public int PontoFinalId { get; set; }

        public string Nome { get; set; }

        public double FaixaDeDominio { get; set; }

        public virtual ClasseDeProjeto ClasseDeProjeto { get; set; }

        public int UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public bool CoordenadasReais { get; set; }

        public bool MapaRenderizado { get; set; }

        public bool EstacasGeradas { get; set; }

        public ICollection<PontoNotavel> PontosNotaveis { get; set; }

        public ICollection<Trecho> Trechos { get; set; }

        public ICollection<Azimute> Azimutes { get; set; }

        public ICollection<CurvaHorizontal> Curvas { get; set; }

        public double GetFaixaDeDominioMinima()
        {
            return ClasseDeProjeto.LargFxTrans * 2 + ClasseDeProjeto.LargAcoExtA * 2 +
                   ClasseDeProjeto.LargAcoIntDuasFxMin + ClasseDeProjeto.AfastMinBordAcoObC * 2;
        }

        public IList<Trecho> CalculaTrechos()
        {
            List<Trecho> trechos = new List<Trecho>();
            List<PontoNotavel> pontos = PontosNotaveis.ToList();
            if (pontos != null)
            {
                for (int i = 0; i < PontosNotaveis.Count - 1; i++)
                {
                    if (i == 0)
                    {
                        trechos.Add(new Trecho(pontos[i], pontos[i + 1], null, "Trecho " + pontos[i].Nome + "-" + pontos[i + 1].Nome) { ProjetoId = Id });
                    }
                    else
                    {
                        trechos.Add(new Trecho(pontos[i], pontos[i + 1], trechos[i - 1].EstacaFinal, "Trecho " + pontos[i].Nome + "-" + pontos[i + 1].Nome) { ProjetoId = Id });

                    }
                }
            }
            return trechos;
        }


        public IList<Trecho> CalculaTrechos(Estaca estacaInicial)
        {
            List<Trecho> trechos = new List<Trecho>();
            List<PontoNotavel> pontos = PontosNotaveis.ToList();
            if (pontos != null)
            {
                for (int i = 0; i < PontosNotaveis.Count - 1; i++)
                {
                    if (i == 0)
                    {
                        trechos.Add(new Trecho(){ProjetoId = Id, PontoInicialId = pontos[i].Id, PontoFinalId = pontos[i + 1].Id, Nome = "Trecho " + pontos[i].Nome + "-" + pontos[i + 1].Nome, EstacaInicial = estacaInicial});
                    }
                    else
                    {
                        trechos.Add(new Trecho(){ ProjetoId = Id, PontoInicialId = pontos[i].Id, PontoFinalId = pontos[i + 1].Id, Nome = "Trecho " + pontos[i].Nome + "-" + pontos[i + 1].Nome, EstacaInicial = trechos[i - 1].EstacaFinal } );

                    }
                }
            }
            return trechos;
        }

        public IList<Trecho> CalculaTrechos(Estaca estacaInicial, List<PontoNotavel> pontos)
        {
            List<Trecho> trechos = new List<Trecho>();
            if (pontos != null)
            {
                for (int i = 0; i < pontos.Count - 1; i++)
                {
                    if (i == 0)
                    {
                        trechos.Add(new Trecho(pontos[i], pontos[i + 1], this, estacaInicial, "Trecho " + pontos[i].Nome + "-" + pontos[i + 1].Nome) { ProjetoId = Id});
                    }
                    else
                    {
                        trechos.Add(new Trecho(pontos[i], pontos[i + 1], this, trechos[i - 1].EstacaFinal, "Trecho " + pontos[i].Nome + "-" + pontos[i + 1].Nome) { ProjetoId = Id });

                    }

                    
                }
            }
            return trechos;
        }
    }
}