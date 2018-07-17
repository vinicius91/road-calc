using System.ComponentModel;
using RoadCalc.Helpers;

namespace RoadCalc.Models.Entities
{
    public class PontoNotavel : EntityBase
    {
        [DisplayName("Nome")]
        public string Nome { get; set; }

        public int CoordenadaId { get; set; }

        public virtual Coordenada Coordenada { get; set; }

        public int ProjetoId { get; set; }

        public virtual Projeto Projeto { get; set; }


        public double DistanciaEntrePontos(PontoNotavel segundoPonto)
        {
            return CalculadoraCoordenadas.DistanciaEntreDoisPontos(this, segundoPonto);
        }

        public double InclinacaoEntrePontos(PontoNotavel segundoPonto)
        {
            return CalculadoraCoordenadas.InclinacaoEntreDoisPontos(this, segundoPonto);
        }

        public double AnguloEntrePontos(PontoNotavel ponto2)
        {
            return CalculadoraCoordenadas.AnguloEntreDoisPontos(this, ponto2);
        }

        public double AzimuteEntrePontos(PontoNotavel ponto2)
        {
            return CalculadoraCoordenadas.AzimuteEntreDoisPontos(this, ponto2);
        }

        public PontoNotavel()
        {

        }

        public PontoNotavel(double x, double y, bool isUtm)
        {
            Coordenada = new Coordenada(x, y, isUtm);
            Nome = "Sem Nome";
            ProjetoId = 0;
        }

        public PontoNotavel(double x, double y, double z, bool isUtm)
        {
            Coordenada = new Coordenada(x, y, z, isUtm);
            Nome = "Sem Nome";
            ProjetoId = 0;
        }
    }
}