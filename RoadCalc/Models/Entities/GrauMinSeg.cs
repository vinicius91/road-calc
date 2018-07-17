using RoadCalc.Helpers;

namespace RoadCalc.Models.Entities
{
    public class GrauMinSeg : EntityBase
    {
        public double Grau { get; set; }

        public double Minuto { get; set; }

        public double Segundo { get; set; }

        public GrauMinSeg(double grau, double minuto, double segundo)
        {
            Grau = grau;
            Minuto = minuto;
            Segundo = segundo;
        }

        public GrauMinSeg(double valorDecimal)
        {
            var intermediario = CalculadoraDecimalGraus.DecimalParaGrauMinSeg(valorDecimal);
            Grau = intermediario.Grau;
            Minuto = intermediario.Minuto;
            Segundo = intermediario.Segundo;
        }


        public double ToDouble()
        {
            return CalculadoraDecimalGraus.GrauMinSegParaDecimal(this);
        }




    }

   
}