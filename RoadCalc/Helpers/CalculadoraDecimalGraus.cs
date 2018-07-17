using System;
using RoadCalc.Models.Entities;

namespace RoadCalc.Helpers
{
    public class CalculadoraDecimalGraus
    {
        public static double GrauMinSegParaDecimal(GrauMinSeg valor)
        {
            return valor.Grau + (valor.Minuto + (valor.Minuto/60))/60;
        }

        public static GrauMinSeg DecimalParaGrauMinSeg(double valor)
        {
            double grausInteiro = Math.Truncate(valor);
            double minutoCompleto = valor - grausInteiro;
            minutoCompleto = minutoCompleto * 60;
            double segundoCompleto = minutoCompleto - Math.Truncate(minutoCompleto);
            segundoCompleto = segundoCompleto * 60;
            minutoCompleto = Math.Truncate(minutoCompleto);
            segundoCompleto = Math.Round(segundoCompleto, 0);

            return new GrauMinSeg(grausInteiro, minutoCompleto, segundoCompleto);
        }


    }
}