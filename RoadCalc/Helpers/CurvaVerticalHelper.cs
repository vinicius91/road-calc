using System;

namespace RoadCalc.Helpers
{
    public class CurvaVerticalHelper
    {
        public static double CalculaInclinacaoEntreDoisPontos(double cotaInicial, double cotaFinal,
            double distanciaInicial, double distanciaFinal)
        {
            var diferencaDeCotas = cotaFinal - cotaInicial;
            var diferencaDistancia = distanciaFinal - distanciaInicial;
            return diferencaDeCotas / diferencaDistancia * 100;
        }

        public static double CalculaA(double iInicial, double iFinal)
        {
            if (iInicial > iFinal)
            {
                if ((iInicial > 0 && iFinal > 0) || iInicial > 0 && iFinal < 0)
                {
                    return iInicial - iFinal;
                }

                if (iInicial < 0 && iFinal < 0)
                {
                    return Math.Abs(iFinal) - Math.Abs(iInicial);
                }
            }

            return iFinal - iInicial;
        }

        public static double DistanciaVisibilidadeDeParada(int velocidadeDiretriz, double diferencaDeclividade)
        {
            double fl;
            switch (velocidadeDiretriz)
            {
                case 30:
                    fl = 0.40;
                    break;
                case 40:
                    fl = 0.39;
                    break;
                case 50:
                    fl = 0.36;
                    break;
                case 60:
                    fl = 0.34;
                    break;
                case 70:
                    fl = 0.33;
                    break;
                case 80:
                    fl = 0.31;
                    break;
                case 90:
                    fl = 0.30;
                    break;
                case 100:
                    fl = 0.30;
                    break;
                case 110:
                    fl = 0.30;
                    break;
                case 120:
                    fl = 0.29;
                    break;
                default:
                    fl = 0.29;
                    break;
            }

            return 0.7 * velocidadeDiretriz + (Math.Pow(velocidadeDiretriz, 2) / (255 * (fl + diferencaDeclividade)));

        }

        public static double DistanciaVisibilidadeUltrapassagem(int velocidadeDiretriz)
        {
            switch (velocidadeDiretriz)
            {
                case 30:
                    return 180;
                case 40:
                    return 270;
                case 50:
                    return 350;
                case 60:
                    return 420;
                case 70:
                    return 490;
                case 80:
                    return 560;
                case 90:
                    return 620;
                case 100:
                    return 680;
                case 110:
                    return 730;
                case 120:
                    return 800;
                default:
                    return 800;
            }
        }

        public static double LminAceleracaoCentripeda(int velocidadeDiretriz, double diferencaDeclividade, bool isElevada)
        {
            if (isElevada)
                return diferencaDeclividade * Math.Pow(velocidadeDiretriz, 2) / (1296 * (0.015 * 9.8));
            return diferencaDeclividade * Math.Pow(velocidadeDiretriz, 2) / (1296 * (0.05 * 9.8));
        }

        public static double LminVisbilidadeDeParada(int velocidadeDiretriz, double diferencaDeclividade,
            bool isConvexa)
        {
            if (isConvexa)
                return Math.Pow(DistanciaVisibilidadeDeParada(velocidadeDiretriz, diferencaDeclividade), 2) * diferencaDeclividade / 412;
            return Math.Pow(DistanciaVisibilidadeDeParada(velocidadeDiretriz, diferencaDeclividade), 2) * diferencaDeclividade /
                   (122 + 3.5 * DistanciaVisibilidadeDeParada(velocidadeDiretriz, diferencaDeclividade));
        }

        public static double LminValorAbsoluto(int velocidadeDiretriz)
        {
            if (velocidadeDiretriz < 34)
                return 20;
            return velocidadeDiretriz * 0.6;
        }

        public static double LmaxDrenagem(int diferencaDeclividade) => diferencaDeclividade * 43;

        public static double CalcOMax(double distancia, double diferencaDeclividade) => (distancia / 8) * (diferencaDeclividade / 100);

        public static double CalcOj(double oMax, double dj, double distancia)
        {
            return oMax * Math.Pow((dj/distancia), 2);
        }

    }
}