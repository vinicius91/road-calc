using System;
using RoadCalc.Models.Entities;

namespace RoadCalc.Helpers
{
    public class CalculadoraCoordenadas
    {
        public static double DistanciaEntreDoisPontos(PontoNotavel ponto1, PontoNotavel ponto2)
        {
            return Math.Sqrt(Math.Pow((ponto1.Coordenada.X - ponto2.Coordenada.X), 2) + Math.Pow((ponto1.Coordenada.Y - ponto2.Coordenada.Y), 2));
        }

        public static double DistanciaEntreDuasCoordenadas(Coordenada coord1, Coordenada coord2)
        {
            return Math.Sqrt(Math.Pow((coord1.X - coord2.X), 2) + Math.Pow((coord1.Y - coord2.Y), 2));
        }

        public static double InclinacaoEntreDoisPontos(PontoNotavel ponto1, PontoNotavel ponto2)
        {

            var distancia = DistanciaEntreDoisPontos(ponto1, ponto2);
            if (distancia.Equals(0))
            {
                return 0;
            }
            return (ponto2.Coordenada.Z - ponto1.Coordenada.Z)/distancia;
        }

        public static double AnguloEntreDoisPontos(PontoNotavel ponto1, PontoNotavel ponto2)
        {
            return Math.Atan((ponto2.Coordenada.Y - ponto1.Coordenada.Y) /(ponto2.Coordenada.X - ponto1.Coordenada.X));
        }

        public static double AzimuteEntreDoisPontos(PontoNotavel ponto1, PontoNotavel ponto2)
        {

            var azimute = Math.Atan((ponto2.Coordenada.X - ponto1.Coordenada.X) / (ponto2.Coordenada.Y - ponto1.Coordenada.Y));

            if ((ponto2.Coordenada.X - ponto1.Coordenada.X) > 0 && (ponto2.Coordenada.Y - ponto1.Coordenada.Y) < 0 )
            {
                azimute = Math.PI + azimute;
            }

            if ((ponto2.Coordenada.X - ponto1.Coordenada.X) < 0 && (ponto2.Coordenada.Y - ponto1.Coordenada.Y) < 0)
            {
                azimute = azimute + Math.PI;
            }

            if ((ponto2.Coordenada.X - ponto1.Coordenada.X) < 0 && (ponto2.Coordenada.Y - ponto1.Coordenada.Y) > 0)
            {
                azimute = 2 * Math.PI + azimute;
            }

            return azimute;
        }
        //Calcula a Coordenada com Coordenada inicial, Azimute, Distancia e inclinação
        public static Coordenada CalculaCoordenadaComCoorAziDist(Coordenada coordenadaInicial, double azimuteRadiano,
            double distancia, double inclinacao = 0)
        {
            return new Coordenada(
                coordenadaInicial.X + Math.Sin(azimuteRadiano) * distancia,
                coordenadaInicial.Y + Math.Cos(azimuteRadiano) * distancia,
                coordenadaInicial.Z + distancia * inclinacao,
                true
                );
        }

        public static Estaca CalculaCoordenadaEstacaSeguinte(Estaca estacaInicial, Estaca estacaFinal, double azimuteRadiano, double inclinacao)
        {
            var distancia = estacaFinal.DistanciaTotal() - estacaInicial.DistanciaTotal();
            var coordenadaFinal =
                CalculaCoordenadaComCoorAziDist(estacaInicial.Coordenada, azimuteRadiano, distancia, inclinacao);
            estacaInicial.Coordenada = coordenadaFinal;
            return estacaFinal;
        }
    }
}