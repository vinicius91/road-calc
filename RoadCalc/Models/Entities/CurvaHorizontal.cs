using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using RoadCalc.Helpers;

namespace RoadCalc.Models.Entities
{
    public class CurvaHorizontal : EntityBase
    {
        #region ReferenciasExternas
        public string Nome { get; set; }

        public int EstacaInicialId { get; set; }

        public int EstacaFinalId { get; set; }

        public int TrechoInicialId { get; set; }

        public int TrechoFinalId { get; set; }

        public int ProjetoId { get; set; }

        public virtual Projeto Projeto { get; set; }
        #endregion

        #region CurvaSimples
        //Raio da Curva em metros
        [DisplayName("Raio")]
        public int Raio { get; set; }

        //Âmguçp central da  curva expresso em graus - Curva Horizontal Circular
        [DisplayName("Ângulo Central")]
        public double AnguloCentral { get; set; }

        //Desenvolvimento da curva em metros
        public double Desenvolvimento { get; set; }

        //Corda da curva em metros
        public int Corda { get; set; }

        //Grau de curva expresso em graus
        [DisplayName("Grau de Curva")]
        public double GrauDeCurva { get; set; }

        //Deflexão expressa em graus
        [DisplayName("Deflexão")]
        public double Deflexao { get; set; }

        //Trecho inicial da curva expresso pela entidade de Trecho
        public virtual Trecho TrechoInicial { get; set; }

        //Trecho final de curva expresso pela entidade Trecho
        public virtual Trecho TrechoFinal { get; set; }

        //Tangente exterior da curva - Simétrico
        [DisplayName("Tangente Exterior")]
        public double TangenteExterior { get; set; }

        //Estaca PC para curva horizontal circular
        public int EstacaPCId { get; set; }
        public virtual Estaca EstacaPC { get; set; }

        //Estaca do PT para Curva horizontal circular
        public int EstacaPTId { get; set; }
        public virtual Estaca EstacaPT { get; set; }
        
        //Super elevação aplicada à curva
        public double SuperElevacao { get; set; }

        //Velocidade Diretriz da via
        public int VelDiretriz { get; set; }

        //Booleano para verificação da necessidade de curva de transição
        public bool Transicao { get; set; }

        #endregion

        #region CurvaComTransicao
        //Parâmetros Referentes à curvas com Transição
        //Lc mínimo absoluto
        public double LcMinAbsoluto { get; set; }

        //Lc mínimo pelo critério da fluência ótica
        public double LcMinFluenciaOtica { get; set; }

        //C para taxa máxima adimissível para critério de conforto
        public double CTaxaMaximaAdmissivel { get; set; }

        //Lc mínimo pelo critério de conforto
        public double LcMinConforto { get; set; }

        //Lc maximo pelo critério do angulo central
        public double LcMaxAnguloCentral { get; set; }

        //LC máximo pelo critério do tempo de percurso 
        public double LcMaxTempoPercurso { get; set; }

        //LC da transição em espiral
        public double Lc { get; set; }

        //Angulo Central da Espiral expresso em graus, conhecido tambem como Sc
        public double ACEspiral { get; set; }

        //Angulo Central da Curva com correção devido à espiral de transição expresso em graus
        public double AcComTransicao { get; set; }

        public double Xc { get; set; }

        public double Yc { get; set; }

        //Ordenada do ponto recuado PC' ou PT'
        public double Q { get; set; }

        //Afastamento da curva circular com relação à tangente
        public double P { get; set; }

        public double DesenvolvimentoCircular { get; set; }

        //Estacas de Curva com transição
        public int EstacaTSId { get; set; }

        public virtual Estaca EstacaTS { get; set; }

        public int EstacaSCId { get; set; }

        public virtual Estaca EstacaSC { get; set; }

        public int EstacaCSId { get; set; }

        public virtual Estaca EstacaCS { get; set; }

        public int EstacaSTId { get; set; }

        public virtual Estaca EstacaST { get; set; }

        //Corda do ramo das espiral
        public double Co { get; set; }

        //Corda do ramo da curva circular
        public double Cc { get; set; }

        //Angulo entre a tangente externa e a corda da espiral(Radianos)
        public double Io { get; set; }

        //Angulo entre a tangente da curva circular e a corda do ramo da espiral
        //No ponto de transição do ramo da espiral com o trecho circular
        public double Jo { get; set; }

        //Angulo entre as tangentes à curva circular nos pontos
        //de transição dos ramos da espiral com o trecho circular
        public double Teta { get; set; }

        #endregion

        #region Construtores
        public CurvaHorizontal()
        {

        }

        public CurvaHorizontal(Trecho trechoInicial, Trecho trechoFinal, int raio, Projeto projeto, int corda = 20, int velDiretriz = 80, string nome = "Sem nome")
        {
            Nome = nome;
            TrechoInicial = trechoInicial;
            TrechoFinal = trechoFinal;
            Projeto = projeto;
            Deflexao = CalculadorConcordanciaHorizontal.CalculaDeflexao(trechoInicial, trechoFinal);
            AnguloCentral = Deflexao;
            Raio = ValidaRaio(projeto.ClasseDeProjeto, raio);
            SuperElevacao = CalcSuperElevacao(Raio, velDiretriz);
            Desenvolvimento = CalculadorConcordanciaHorizontal.CalculaDesenvolvimento(AnguloCentral, Raio);
            TangenteExterior = CalculadorConcordanciaHorizontal.CalculaTangenteExterna(AnguloCentral, Raio);
            Corda = ValidaCorda(corda, raio);
            GrauDeCurva = CalculadorConcordanciaHorizontal.CalculaGrauDeCruva(Corda, Raio);
            EstacaPC = CalculadorConcordanciaHorizontal.CalculaEstacaDePc(trechoInicial.EstacaInicial, trechoInicial, TangenteExterior);
            EstacaPC.Projeto = projeto;
            EstacaPT = CalculadorConcordanciaHorizontal.CalculaEstacaDePt(EstacaPC, Desenvolvimento, trechoFinal, TangenteExterior);
            EstacaPT.Projeto = projeto;
            VelDiretriz = velDiretriz;
            

            //Parte do Cosntrutor referente à curvas com transição
            if (IsTransicaoNec())
            {
                DefineTransicao(projeto, trechoInicial, trechoFinal, Double.MinValue);

            }
        }

        public CurvaHorizontal(Trecho trechoInicial, Trecho trechoFinal, int raio, Projeto projeto, double lc, int corda = 20, int velDiretriz = 80, string nome = "Sem nome")
        {
            Nome = nome;
            TrechoInicial = trechoInicial;
            TrechoFinal = trechoFinal;
            Projeto = projeto;
            Deflexao = CalculadorConcordanciaHorizontal.CalculaDeflexao(trechoInicial, trechoFinal);
            AnguloCentral = Deflexao;
            Raio = ValidaRaio(projeto.ClasseDeProjeto, raio);
            SuperElevacao = CalcSuperElevacao(Raio, velDiretriz);
            Desenvolvimento = CalculadorConcordanciaHorizontal.CalculaDesenvolvimento(AnguloCentral, Raio);
            TangenteExterior = CalculadorConcordanciaHorizontal.CalculaTangenteExterna(AnguloCentral, Raio);
            Corda = ValidaCorda(corda, raio);
            GrauDeCurva = CalculadorConcordanciaHorizontal.CalculaGrauDeCruva(Corda, Raio);
            EstacaPC = CalculadorConcordanciaHorizontal.CalculaEstacaDePc(trechoInicial.EstacaInicial, trechoInicial, TangenteExterior);
            EstacaPC.Projeto = projeto;
            EstacaPT = CalculadorConcordanciaHorizontal.CalculaEstacaDePt(EstacaPC, Desenvolvimento, trechoFinal, TangenteExterior);
            EstacaPT.Projeto = projeto;
            VelDiretriz = velDiretriz;


            //Parte do Cosntrutor referente à curvas com transição
            if (IsTransicaoNec())
            {
                DefineTransicao(projeto, trechoInicial, trechoFinal, lc);

            }
        }



        public CurvaHorizontal(Trecho trechoInicial, Trecho trechoFinal, int raio, int corda = 20, int velDiretriz = 80, string nome = "Sem nome")
        {
            Nome = nome;
            TrechoInicial = trechoInicial;
            TrechoFinal = trechoFinal;
            Deflexao = CalculadorConcordanciaHorizontal.CalculaDeflexao(trechoInicial, trechoFinal);
            AnguloCentral = Deflexao;
            Raio = raio;
            Desenvolvimento = CalculadorConcordanciaHorizontal.CalculaDesenvolvimento(AnguloCentral, Raio);
            TangenteExterior = CalculadorConcordanciaHorizontal.CalculaTangenteExterna(AnguloCentral, Raio);
            Corda = ValidaCorda(corda, raio);
            GrauDeCurva = CalculadorConcordanciaHorizontal.CalculaGrauDeCruva(Corda, Raio);
            EstacaPC = CalculadorConcordanciaHorizontal.CalculaEstacaDePc(trechoInicial.EstacaInicial, trechoInicial, TangenteExterior);
            EstacaPT = CalculadorConcordanciaHorizontal.CalculaEstacaDePt(EstacaPC, Desenvolvimento, trechoFinal, TangenteExterior);
            VelDiretriz = velDiretriz;
            SuperElevacao = CalcSuperElevacao(raio, velDiretriz);

            //Parte do Cosntrutor referente à curvas com transição
            if (IsTransicaoNec())
            {
                DefineTransicao(null, trechoInicial, trechoFinal, double.MinValue);
            }
        }
        //Método que adiciona a transição à curva
        private void DefineTransicao(Projeto projeto, Trecho trechoInicial, Trecho trechoFinal, double lc)
        {
            Transicao = IsTransicaoNec();
            LcMinAbsoluto = CalcLcMinAbsoluto();
            LcMinFluenciaOtica = CalcLcFluenciaOtica();
            CTaxaMaximaAdmissivel = CalcCTaxaMaxima();
            LcMinConforto = CalcLcMinConforto();
            LcMaxAnguloCentral = CalcLcMaxAnguloCentral();
            LcMaxTempoPercurso = CalcLcMaxTempoPercurso();
            Lc = CalculaLc(lc);
            ACEspiral = CalcACEspiral();
            AcComTransicao = CalcACComTransicao();
            Xc = GetXcDeSC();
            Yc = GetYcDeSC();
            Q = GetQ();
            P = GetP();
            TangenteExterior = GetTgExtComTrans();
            DesenvolvimentoCircular = GetDesenvCircular();
            Desenvolvimento = GetDesenvComTrans();
            Io = GetIo();
            Jo = GetJo();
            Teta = GetTeta();
            Co = GetCo();
            Cc = GetCc();
            
            if (trechoInicial != null && trechoFinal != null)
            {
                EstacaTS = CalculadorConcordanciaHorizontal.CalculaEstacaDePc(trechoInicial.EstacaInicial, trechoInicial, TangenteExterior);

                EstacaST = CalculadorConcordanciaHorizontal.CalculaEstacaDePt(EstacaTS, Desenvolvimento, trechoFinal, TangenteExterior);

                EstacaSC = CalculadorConcordanciaHorizontal.CalculaEstacaDeSC(Lc, EstacaTS, Co, Io + trechoInicial.Azimute);

                EstacaCS = CalculadorConcordanciaHorizontal.CalculaEstacaDeCS(DesenvolvimentoCircular, EstacaSC, Cc, trechoInicial.Azimute + Io + Jo + Teta/2);
            }
            if (projeto != null)
            {
                EstacaTS.Projeto = projeto;
                EstacaST.Projeto = projeto;
                EstacaSC.Projeto = projeto;
                EstacaCS.Projeto = projeto;
            }
        }

        private void DefineTransicao(double lc)
        {
            Transicao = IsTransicaoNec();
            LcMinAbsoluto = CalcLcMinAbsoluto();
            LcMinFluenciaOtica = CalcLcFluenciaOtica();
            CTaxaMaximaAdmissivel = CalcCTaxaMaxima();
            LcMinConforto = CalcLcMinConforto();
            LcMaxAnguloCentral = CalcLcMaxAnguloCentral();
            LcMaxTempoPercurso = CalcLcMaxTempoPercurso();
            Lc = CalculaLc(lc);
            ACEspiral = CalcACEspiral();
            AcComTransicao = CalcACComTransicao();
            Xc = GetXcDeSC();
            Yc = GetYcDeSC();
            Q = GetQ();
            P = GetP();
            TangenteExterior = GetTgExtComTrans();
            DesenvolvimentoCircular = GetDesenvCircular();
            Desenvolvimento = GetDesenvComTrans();
            Io = GetIo();
            Jo = GetJo();
            Teta = GetTeta();
            Co = GetCo();
            Cc = GetCc();

            if (TrechoInicial != null && TrechoFinal != null)
            {
                EstacaTS = CalculadorConcordanciaHorizontal.CalculaEstacaDePc(TrechoInicial.EstacaInicial, TrechoInicial, TangenteExterior);

                EstacaST = CalculadorConcordanciaHorizontal.CalculaEstacaDePt(EstacaTS, Desenvolvimento, TrechoFinal, TangenteExterior);

                EstacaSC = CalculadorConcordanciaHorizontal.CalculaEstacaDeSC(Lc, EstacaTS, Co, Io + TrechoInicial.Azimute);

                EstacaCS = CalculadorConcordanciaHorizontal.CalculaEstacaDeCS(DesenvolvimentoCircular, EstacaSC, Cc, TrechoInicial.Azimute + Io + Jo + Teta / 2);
            }
            if (Projeto != null)
            {
                EstacaTS.Projeto = Projeto;
                EstacaST.Projeto = Projeto;
                EstacaSC.Projeto = Projeto;
                EstacaCS.Projeto = Projeto;

                EstacaTS.ProjetoId = ProjetoId;
                EstacaST.ProjetoId = ProjetoId;
                EstacaSC.ProjetoId = ProjetoId;
                EstacaCS.ProjetoId = ProjetoId;
            }
        }

        #endregion

        #region MetodosAuxiliares

        private double CalcLcMaxTempoPercurso()
        {
            return VelDiretriz * 2.2;
        }

        private double CalcLcMaxAnguloCentral()
        {
            return Raio;
        }

        private double CalcSuperElevacao(int raio, int velDiretriz)
        {
            double f;
            int Vr;
            switch (raio)
            {
                case (30):
                {
                    f = 0.17;
                    Vr = 30;
                    break;
                }
                case (40):
                {
                    f = 0.17;
                    Vr = 40;
                    break;
                }
                case (50):
                {
                    f = 0.16;
                    Vr = 47;
                    break;
                }
                case (60):
                {
                    f = 0.15;
                    Vr = 55;
                    break;
                }
                case (70):
                {
                    f = 0.14;
                    Vr = 30;
                    break;
                }
                case (80):
                {
                    f = 0.14;
                    Vr = 70;
                    break;
                }
                case (90):
                {
                    f = 0.13;
                    Vr = 77;
                    break;
                }
                case (100):
                {
                    f = 0.12;
                    Vr = 85;
                    break;
                }
                case (110):
                {
                    f = 0.11;
                    Vr = 91;
                    break;
                }
                case (120):
                {
                    f = 0.09;
                    Vr = 98;
                    break;
                }
                default:
                {
                    f = 0.7413 * Math.Pow(velDiretriz, -0.384);
                    Vr = velDiretriz;
                    break;
                }
            }
            var eCalculado = (Math.Pow(Vr, 2) / (127 * raio)) - f;
            //Se eCalculado for menor do que 0 significa que a curva não necessita de superelevação
            if (eCalculado < 0)
            {
                return 0;
            }
            return eCalculado;
        }

        private int ValidaRaio(ClasseDeProjeto classe, int raio)
        {
            if (classe != null)
            {
                return ((raio < classe.RaioMinSupEleMax) ? classe.RaioMinSupEleMax : raio);

            }
            return raio;
        }

        private int ValidaCorda(int corda, int raio)
        {
            if (raio < 100 && corda > 5)
            {
                return 5;
            }
            if (raio >= 100 && raio < 600 && corda > 10)
            {
                return 10;
            }
            return 20;
        }


        private bool IsTransicaoNec()
        {
            if (Raio < 0.186 * Math.Pow(VelDiretriz, 2.0031))
            {
                return true;
            }

            return false;
        }

        private double CalcLcMinAbsoluto()
        {
            return VelDiretriz * 0.56;
        }

        private double CalcCTaxaMaxima()
        {
            return 1.5 - 0.009 * VelDiretriz;
        }

        private double CalcLcFluenciaOtica()
        {
            return (double)Raio / 9;
        }

        private double CalcLcMinConforto()
        {
            return (Math.Pow(VelDiretriz, 3) / (46.656 * CTaxaMaximaAdmissivel * Raio)) - ((SuperElevacao * VelDiretriz) / (0.367 * CTaxaMaximaAdmissivel));
        }

        //Cálculo do Lc é feito com o valor máximo do LC Mínimo e o valor mínimo do LC máximo para a criação dos limites
        //Com os limites calculados o valor estimado para o LC é o valor da médio entre os limites
        //Após esse cálculo o o valor é aproximado para o múltiplo de dez mais proxímo
        private double CalculaLc()
        {
            var min = Math.Max(LcMinFluenciaOtica, Math.Max(LcMinAbsoluto, LcMinConforto));
            var max = Math.Min(LcMaxAnguloCentral, LcMaxTempoPercurso);
            var meanDecimal = (max + min) / 20;
            var Lc = Math.Round(meanDecimal, 0) * 10;
            if (Lc > max)
            {
                Lc = (Math.Round(meanDecimal, 0) - 1) * 10;
            }
            else if (Lc < min)
            {
                Lc = (Math.Round(meanDecimal, 0) + 1) * 10;
            }
            return Lc;
        }

        private double CalculaLc(double lc)
        {
            var min = Math.Max(LcMinFluenciaOtica, Math.Max(LcMinAbsoluto, LcMinConforto));
            var max = Math.Min(LcMaxAnguloCentral, LcMaxTempoPercurso);
            if (lc >= min && lc <= max)
            {
                return lc;
            }
            var meanDecimal = (max + min) / 20;
            var Lc = Math.Round(meanDecimal, 0) * 10;
            if (Lc > max)
            {
                Lc = (Math.Round(meanDecimal, 0) - 1) * 10;
            }
            else if (Lc < min)
            {
                Lc = (Math.Round(meanDecimal, 0) + 1) * 10;
            }
            return Lc;
        }

        //Valor retornado em graus
        private double CalcACEspiral()
        {
            return (Lc / (2 * Raio));
        }

        //Valor retornado em graus
        private double CalcACComTransicao()
        {
            return Deflexao - 2 * ACEspiral;
        }

        private double GetXcDeSC()
        {
            return (Lc * (ACEspiral) / 3 *
                   (1 - Math.Pow(ACEspiral, 2) / 14 + Math.Pow(ACEspiral, 4) / 440));
        }

        private double GetYcDeSC()
        {
            return Lc * (1 - Math.Pow(ACEspiral, 2) / 10 + Math.Pow(ACEspiral, 4) / 216);
        }


        private double GetP()
        {
            return Xc - Raio * (1 - Math.Cos(ACEspiral));
        }

        private double GetQ()
        {
            return Yc - Raio * Math.Sin(ACEspiral);
        }

        private double GetTgExtComTrans()
        {
            return Q + (Raio + P) * Math.Tan((Deflexao) / 2);
        }

        private double GetDesenvComTrans()
        {
            return DesenvolvimentoCircular + 2 * Lc;
        }

        private double GetDesenvCircular()
        {
            return (AcComTransicao) * Raio;
        }

        private double GetIo()
        { 
            return Math.Atan(Xc / Yc);
        }
        //SC em radianos e io em radianos, resultado em radianos
        private double GetJo()
        {
            return ACEspiral - Io;
        }
        //SC em graus e AC em graus, resultado em radianos
        private double GetTeta()
        {
            return AcComTransicao - 2 * ACEspiral;
        }

        private double GetCo()
        {
            return Math.Sqrt(Math.Pow(Xc, 2) + Math.Pow(Yc, 2));
        }

        private double GetCc()
        {
            return 2 * Raio * Math.Sin(Teta / 2);
        }
        #endregion

        #region MetodosExternos

        //Chamada para estacas de curva que ira decidir que tipo de algoritimo será utilizado
        //com base na necesseidade ou não de curva de transição
        public List<Estaca> GetEstacasDeCurva()
        {
            return Transicao ? GetEstacaDeCurvaComTransicao() : GetEstacaDeCurvaSimples();
        }
        //Algoritimo para curva sem transição

        private List<Estaca> GetEstacaDeCurvaSimples()
        {
            var fator = (TrechoInicial.Azimute > TrechoFinal.Azimute) ? 1 : -1;
            var faltaPc = 20 - EstacaPC.Intermediario;
            var numeroAtual = EstacaPC.Numero;
            var deflexaoPorMetro = GrauDeCurva / Corda;
            List<Estaca> estacas = new List<Estaca>();
            estacas.Add(EstacaPC);
            Estaca escataTemporaria;
            while (numeroAtual < EstacaPT.Numero)
            {
                if (numeroAtual == EstacaPC.Numero && faltaPc != 20)
                {
                    if (faltaPc == 20)
                    {
                        escataTemporaria = new Estaca()
                        {
                            Coordenada = CalculadoraCoordenadas.CalculaCoordenadaComCoorAziDist(estacas.LastOrDefault().Coordenada, (TrechoInicial.Azimute + fator * faltaPc * deflexaoPorMetro) * Math.PI / 180, faltaPc),
                            Numero = numeroAtual + 1,
                            Intermediario = 0
                        };
                    }
                    else
                    {
                        escataTemporaria = new Estaca()
                        {
                            Coordenada = CalculadoraCoordenadas.CalculaCoordenadaComCoorAziDist(estacas.LastOrDefault().Coordenada, (TrechoInicial.Azimute + fator * faltaPc * deflexaoPorMetro) * Math.PI / 180, faltaPc),
                            Numero = numeroAtual + 1,
                            Intermediario = 0
                        };
                    }
                }
                else
                {
                    escataTemporaria = new Estaca()
                    {
                        Coordenada = CalculadoraCoordenadas.CalculaCoordenadaComCoorAziDist(estacas.LastOrDefault().Coordenada, (TrechoInicial.Azimute + fator * 20 * deflexaoPorMetro) * Math.PI / 180, 20),
                        Numero = numeroAtual + 1,
                        Intermediario = 0
                    };
                }
                estacas.Add(escataTemporaria);
                numeroAtual++;
            }
            if (EstacaPT.Intermediario != 0)
            {
                estacas.Add(EstacaPT);
            }

            return estacas;
        }

        //Algoritimo para curva com transição
        private List<Estaca> GetEstacaDeCurvaComTransicao()
        {
            List<Estaca> estacas = new List<Estaca>();
            estacas.Add(EstacaTS);
            estacas.Add(EstacaSC);
            estacas.Add(EstacaCS);
            estacas.Add(EstacaST);
            return estacas;
        }

        public int DirecaoCurva()
        {
            return (TrechoInicial.Azimute > TrechoFinal.Azimute) ? -1 : 1;
        }


        public void AtualizaCurva()
        {
            Deflexao = CalculadorConcordanciaHorizontal.CalculaDeflexao(TrechoInicial, TrechoFinal);
            Raio = ValidaRaio(Projeto.ClasseDeProjeto, Raio);
            SuperElevacao = CalcSuperElevacao(Raio, VelDiretriz);
            Desenvolvimento = CalculadorConcordanciaHorizontal.CalculaDesenvolvimento(AnguloCentral, Raio);
            TangenteExterior = CalculadorConcordanciaHorizontal.CalculaTangenteExterna(AnguloCentral, Raio);
            Corda = ValidaCorda(Corda, Raio);
            GrauDeCurva = CalculadorConcordanciaHorizontal.CalculaGrauDeCruva(Corda, Raio);
            EstacaPC = CalculadorConcordanciaHorizontal.CalculaEstacaDePc(TrechoInicial.EstacaInicial, TrechoFinal, TangenteExterior);
            EstacaPT = CalculadorConcordanciaHorizontal.CalculaEstacaDePt(EstacaPC, Desenvolvimento, TrechoFinal, TangenteExterior);
            EstacaPC.ProjetoId = ProjetoId;
            EstacaPT.ProjetoId = ProjetoId;
            DefineTransicao(Lc);
        }
        #endregion
    }
}
