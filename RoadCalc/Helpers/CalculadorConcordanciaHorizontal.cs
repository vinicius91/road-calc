using System;
using RoadCalc.Models.Entities;

namespace RoadCalc.Helpers
{
    public class CalculadorConcordanciaHorizontal
    {
        public static double CalculaDeflexao(Trecho trecho, Trecho trecho2)
        {
            var deflexao = trecho2.Azimute - trecho.Azimute;

            if (deflexao < 0)
            {
                deflexao = deflexao * -1;
            } 
            return deflexao;
        }

        public static double CalculaDesenvolvimento(double anguloCentral, double raio)
        {
            double desenvolvimento = anguloCentral * raio;

            if (desenvolvimento < 0)
            {
                desenvolvimento = desenvolvimento * -1;
            }
            return desenvolvimento;
        }

        public static double CalculaTangenteExterna(double anguloCentral, double raio)
        {
            return raio * Math.Tan(anguloCentral /2);
        }

        public static double CalculaGrauDeCruva(double corda, double raio)
        {
            return (2 * Math.Asin(corda / (2 * raio)));
        }

        public static Estaca CalculaEstacaDePc(Estaca estacaInicial, Trecho trecho, double tangenteExterna)
        {
            var distancia =
                CalculadoraCoordenadas.DistanciaEntreDuasCoordenadas(estacaInicial.Coordenada,
                    trecho.PontoFinal.Coordenada);
            var distanciaTotal = estacaInicial.DistanciaTotal() + distancia - tangenteExterna;
            var estaca = new Estaca(distanciaTotal, CalculadoraCoordenadas.CalculaCoordenadaComCoorAziDist(estacaInicial.Coordenada,
                trecho.Azimute, trecho.Distancia - tangenteExterna));
            return estaca;
        }

        public static Estaca CalculaEstacaDePt(Estaca estacaPc, double desenvolvimento, Trecho trechoFinal, double tangenteExterior)
        {
            var estacaPT = SomaEstaca(estacaPc, desenvolvimento);
            var coordenada =
                CalculadoraCoordenadas.CalculaCoordenadaComCoorAziDist(trechoFinal.EstacaInicial.Coordenada,
                    trechoFinal.Azimute , tangenteExterior);
            estacaPT.Coordenada = coordenada;
            return estacaPT;
        }

        public static Estaca SomaEstaca(Estaca estacaInicial, double distancia)
        {
            var distanciaTotal = estacaInicial.DistanciaTotal() + distancia;
            return new Estaca(distanciaTotal);
        }

        public static Estaca SubtraiEstaca(Estaca estacaInicial, double distancia)
        {
            var distanciaTotal = estacaInicial.DistanciaTotal() - distancia;
            return new Estaca(distanciaTotal);
        }

        public static Estaca CalculaEstacaDeSC(double lc, Estaca estacaTs, double co, double angulo)
        {
            var estacaSC = SomaEstaca(estacaTs, lc);
            var coordenada =
                CalculadoraCoordenadas.CalculaCoordenadaComCoorAziDist(estacaTs.Coordenada,
                    angulo, co);
            estacaSC.Coordenada = coordenada;
            return estacaSC;
        }

        public static Estaca CalculaEstacaDeCS(double desenvolvimentoCircular, Estaca estacaSc, double cc, double angulo)
        {
            var estacaSC = SomaEstaca(estacaSc, desenvolvimentoCircular);
            var coordenada =
                CalculadoraCoordenadas.CalculaCoordenadaComCoorAziDist(estacaSc.Coordenada,
                    angulo, cc);
            estacaSC.Coordenada = coordenada;
            return estacaSC;
        }
    }
}