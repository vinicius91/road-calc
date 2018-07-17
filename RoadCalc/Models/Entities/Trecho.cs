using RoadCalc.Helpers;

namespace RoadCalc.Models.Entities
{
    public class Trecho : EntityBase
    {

        public string Nome { get; set; }

        public int EstacaInicialId { get; set; }

        public int EstacaFinalId { get; set; }

        public int PontoInicialId { get; set; }

        public int PontoFinalId { get; set; }

        public virtual Estaca EstacaInicial { get; set; }

        public virtual Estaca EstacaFinal { get; set; }

        public virtual PontoNotavel PontoInicial { get; set; }

        public virtual PontoNotavel PontoFinal { get; set; }

        public double Distancia { get; set; }

        public double Inclinacao { get; set; }

        public double Azimute { get; set; }

        public int ProjetoId { get; set; }

        public virtual Projeto Projeto { get; set; }


        public Trecho()
        {
            
        }

        public Trecho(PontoNotavel pontoInicial, PontoNotavel pontoFinal, Estaca estacaInicial = null, string nome = "Sem nome")
        {
            PontoInicial = pontoInicial;
            PontoFinal = pontoFinal;
            Inclinacao = PontoInicial.InclinacaoEntrePontos(PontoFinal);
            Azimute = PontoInicial.AzimuteEntrePontos(PontoFinal);
            Distancia = PontoInicial.DistanciaEntrePontos(PontoFinal);
            Nome = nome;
            if (estacaInicial == null)
            {
                EstacaInicial = new Estaca() {Numero = 0, Intermediario = 0, Coordenada = pontoInicial.Coordenada};
            }
            else
            {
                EstacaInicial = estacaInicial;
            }
            EstacaFinal = CalculadorConcordanciaHorizontal.SomaEstaca(EstacaInicial, Distancia);
            EstacaFinal.Coordenada = pontoFinal.Coordenada;


        }

        public Trecho(PontoNotavel pontoInicial, PontoNotavel pontoFinal, Projeto projeto, Estaca estacaInicial = null, string nome = "Sem nome")
        {
            PontoInicial = pontoInicial;
            PontoFinal = pontoFinal;
            Inclinacao = PontoInicial.InclinacaoEntrePontos(PontoFinal);
            Azimute = PontoInicial.AzimuteEntrePontos(PontoFinal);
            Distancia = PontoInicial.DistanciaEntrePontos(PontoFinal);
            Projeto = projeto;
            Nome = nome;
            if (estacaInicial == null)
            {
                EstacaInicial = new Estaca() { Numero = 0, Intermediario = 0, Coordenada = pontoInicial.Coordenada, Projeto = projeto};
            }
            else
            {
                estacaInicial.Projeto = projeto;
                EstacaInicial = estacaInicial;
            }
            EstacaFinal = CalculadorConcordanciaHorizontal.SomaEstaca(EstacaInicial, Distancia);
            EstacaFinal.Coordenada = pontoFinal.Coordenada;
            EstacaFinal.Projeto = projeto;


        }



        public double CalculaDeflexao(Trecho trecho2)
        {
            return CalculadorConcordanciaHorizontal.CalculaDeflexao(this, trecho2);
        }
    }

}