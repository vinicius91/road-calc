using System;

namespace RoadCalc.Models.Entities
{
    public class Estaca : EntityBase
    {
        public double Numero { get; set; }

        public double Intermediario { get; set; }

        public int CoordenadaId { get; set; }

        public virtual Coordenada Coordenada { get; set; }

        public double CotaVermelha { get; set; }

        public int ProjetoId { get; set; }

        public virtual Projeto Projeto { get; set; }

        public bool Relatorio { get; set; }


        public Estaca()
        {
        }

        public Estaca(double distanciaTotal)
        {
            Numero = CalculaNumero(distanciaTotal);
            Intermediario = CalculaIntermediario(distanciaTotal);
            Coordenada = new Coordenada(0,0,0, true);
        }

        public Estaca(double distanciaTotal, Coordenada coordenada)
        {
            Numero = CalculaNumero(distanciaTotal);
            Intermediario = CalculaIntermediario(distanciaTotal);
            Coordenada = coordenada;
        }

        public Estaca(double numero, double intermediario)
        {
            Numero = numero;
            Intermediario = intermediario;
            Coordenada = new Coordenada(0, 0, 0, true);
        }

        public Estaca(double numero, double intermediario, Coordenada coordenada)
        {
            Numero = numero;
            Intermediario = intermediario;
            Coordenada = coordenada;
        }

        public Estaca(double numero, double intermediario, PontoNotavel pontoNotavel)
        {
            Numero = numero;
            Intermediario = intermediario;
            Coordenada = new Coordenada(pontoNotavel.Coordenada.X, pontoNotavel.Coordenada.Y, pontoNotavel.Coordenada.Z, true);
        }

        private double CalculaIntermediario(double distanciaTotal)
        {
            return distanciaTotal - CalculaNumero(distanciaTotal) * 20;
        }


        private static double CalculaNumero(double distanciaTotal)
        {
            return Math.Truncate(distanciaTotal / 20);
        }

        public double DistanciaTotal()
        {
            return Numero * 20 + Intermediario;
        }

        public string NomeCompleto()
        {
            return Numero.ToString("N0") + @" " + Intermediario.ToString("F2") + @" metros.";
        }
    }
}