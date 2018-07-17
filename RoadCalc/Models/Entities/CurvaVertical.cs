using System;
using RoadCalc.Helpers;

namespace RoadCalc.Models.Entities
{
    public class CurvaVertical : EntityBase
    {
        public string Nome { get; set; }

        public int ProjetoId { get; set; }

        public virtual Projeto Projeto { get; set; }

        public int PontoNotavelVerticalId { get; set; }

        public virtual PontoNotavelVertical PontoNotavelVertical { get; set; }

        public TipoVertical TipoVertical { get; set; }

        //VelDiretriz
        public double VelDiretriz { get; set; }

        //Parâmetro de Curvatura K
        public double K { get; set; }

        //Inclinação do Greide Inicial
        public double IInicial { get; set; }
        
        //Inclinação do Greide Final
        public double IFinal { get; set; }

        //Diferença Algébrica Entre as declividades
        public double A { get; set; }

        //Comprimento
        public double L { get; set; }

        //Distância de Visibilidade de Parada
        public double DistVisPara { get; set; }

        //Raio mínimo de curvatura
        public double RaioMinimoCurv { get; set; }

        //Flecha ou ordenada máxima
        public double OMax { get; set; }

        public int EstacaPCVId { get; set; }

        public virtual Estaca EstacaPCV { get; set; }

        public int EstacaPIVId { get; set; }
        
        public virtual Estaca EstacaPIV { get; set; }

        public int EstacaPTVId { get; set; }

        public virtual Estaca EstacaPTV { get; set; }

        public CurvaVertical()
        {
            
        }

        public CurvaVertical(PontoNotavelVertical pontoNotavelVertical, double iinicial, double ifinal)
        {
            PontoNotavelVertical = pontoNotavelVertical;
            PontoNotavelVerticalId = pontoNotavelVertical.Id;
            Projeto = pontoNotavelVertical.Projeto;
            Nome = pontoNotavelVertical.Nome;
            IInicial = iinicial;
            IFinal = ifinal;
            VelDiretriz = Projeto.ClasseDeProjeto.VelDirMin;
            TipoVertical = DefineTipoVertical();
            A = CalcA();
            DistVisPara = CalcDistVisParada();
            L = CalculaComprimento();
            K = L / A;
            RaioMinimoCurv = 100 * K;
            OMax = CurvaVerticalHelper.CalcOMax(L, A);
        }


        public double CalcA()
        {
            if (IInicial > IFinal)
            {
                if ((IInicial > 0 && IFinal > 0) || IInicial > 0 && IFinal < 0)
                {
                    return IInicial - IFinal;
                }
                if (IInicial < 0 && IFinal < 0)
                {
                    return Math.Abs(IFinal) - Math.Abs(IInicial);
                }
            }
            return IFinal - IInicial;
        }

        private double CalcKMin()
        {
            double kmin = TipoVertical == TipoVertical.Concava
                ? Projeto.ClasseDeProjeto.KCvMinD
                : Projeto.ClasseDeProjeto.KCxMinD;
            kmin = Math.Min(kmin, KMinACentripeda());
            kmin = Math.Min(kmin, KMinVisParada());

            return kmin;
        }

        private double KMinVisParada()
        {
            return Math.Pow(DistVisPara, 2) / 412;
        }

        private double KMinACentripeda()
        {
            var aMin = (Projeto.ClasseDeProjeto.Nome == "IV-A" || Projeto.ClasseDeProjeto.Nome == "IV-B")
                ? 0.0015 * Math.Pow(VelDiretriz, 2.0099) : 0.0052 * Math.Pow(VelDiretriz, 2.0025);
            return Math.Pow(VelDiretriz, 2) / (1296 * aMin);
        }
        public TipoVertical DefineTipoVertical()
        {
            //Caso dos dois positivos
            if (IInicial > 0 && IFinal > 0)
            {
                if (IInicial > IFinal)
                {
                    return TipoVertical.Convexa;
                }
                return TipoVertical.Concava;
            }
            //Caso das duas negativas
            if (IInicial < 0 && IFinal < 0)
            {
                if (IInicial > IFinal)
                {
                    return TipoVertical.Convexa;
                }
                return TipoVertical.Concava;
            }
            if (IInicial < 0)
            {
                return TipoVertical.Concava;
            }
            return TipoVertical.Convexa;
        }


        private double CalcDistVisParada()
        {
            var f = 1.0647 * Math.Pow(VelDiretriz, -0.285);
            var DMin = 0.7 * VelDiretriz + Math.Pow(VelDiretriz, 2) / (255 * (f * IInicial));
            return DMin > Projeto.ClasseDeProjeto.DistMinVisbParMinD
                ? DMin
                : Projeto.ClasseDeProjeto.DistMinVisbParMinD;
        }

        private double CalcLminVisibilidade()
        {
            double lMin;
            if (TipoVertical == TipoVertical.Convexa)
            {
                lMin = CurvaVerticalHelper.LminVisbilidadeDeParada((int)Math.Round(VelDiretriz), A, true);
                if (lMin < DistVisPara)
                {
                    lMin = 2 * CurvaVerticalHelper.DistanciaVisibilidadeDeParada((int)Math.Round(VelDiretriz), A) - 412 / A;
                }
            }
            else
            {
                lMin = CurvaVerticalHelper.LminVisbilidadeDeParada((int)Math.Round(VelDiretriz), A, false);
                if (lMin < DistVisPara)
                {
                    lMin = 2 * CurvaVerticalHelper.DistanciaVisibilidadeDeParada((int)Math.Round(VelDiretriz), A) - (122 +3.5* CurvaVerticalHelper.DistanciaVisibilidadeDeParada((int)Math.Round(VelDiretriz), A)) /A;
                }
            }
            return lMin;
        }

        private bool IsElevada()
        {
            return Projeto.ClasseDeProjeto.Nome != "IV-A" && Projeto.ClasseDeProjeto.Nome != "IV-B" &&
                   Projeto.ClasseDeProjeto.Nome != "III";
        }

        private double CalculaComprimento()
        {
            double lmin;
            //Verificação pelo K mínimo
            lmin = CurvaVerticalHelper.LminAceleracaoCentripeda((int)Math.Round(VelDiretriz), A, IsElevada());
            //Criterio da visibilidade de paradas
            lmin = Math.Max(lmin, CalcLminVisibilidade());
            //Critério de Drenagem em curvas Convexas
            //if (TipoVertical == TipoVertical.Convexa)
            //{
            //    lmin = Math.Min(lmin, A * 43);
            //}
            if (lmin % 20 != 0)
            {
                var temp = Math.Round(lmin / 20) * 20;
                if (temp < lmin)
                {
                    temp = temp + 20;
                }
                lmin = temp;
            }
            return lmin;
        }
    }

    public enum TipoVertical
    {
        Concava,
        Convexa
    }
}