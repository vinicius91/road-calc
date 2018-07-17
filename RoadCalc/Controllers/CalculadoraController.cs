using System.Collections.Generic;
using System.Web.Mvc;
using RoadCalc.Models.Entities;
using RoadCalc.Models.ViewModels;

namespace RoadCalc.Controllers
{
    public class CalculadoraController : Controller
    {

        //Actions de Curva Circular
        // GET: Calculadora/CurvaCircular
        public ActionResult CurvaCircular()
        {
            return View();
        }
        //Métodos para cálculo do Azimute entre dois pontos - Decimal
        //Editor
        public ActionResult CurvaCircularCalculo()
        {
            return PartialView();
        }
        //Resultado
        public ActionResult CalculaCurvaCircular(CurvaHorizontalCircularViewModel valores)
        {
            if (ModelState.IsValid)
            {
                PontoNotavel pontoNotavel1 =
                    new PontoNotavel(valores.CoordXPt1, valores.CoordYPt1, true);
                PontoNotavel pontoNotavel2 =
                    new PontoNotavel(valores.CoordXPt2, valores.CoordYPt2, true);
                PontoNotavel pontoNotavel3 =
                    new PontoNotavel(valores.CoordXPt3, valores.CoordYPt3, true);

                Estaca estacaInicial = new Estaca() {Numero = valores.EstacaInicial, Intermediario = valores.EstacaInicialInter};

                var trecho1 = new Trecho(pontoNotavel1, pontoNotavel2);
                var trecho2 = new Trecho(pontoNotavel2, pontoNotavel3);
                var curva = new CurvaHorizontal(trecho1, trecho2, valores.Raio, valores.Corda);


                return PartialView(curva);
            }

            return PartialView();
        }


        //Actions de Azimutes
        // GET: Calculadora/Azimute
        public ActionResult Azimute()
        {
            return View();
        }
        //Métodos para cálculo do Azimute entre dois pontos - Decimal
        //Editor
        public ActionResult AzimuteDecimal()
        {
            return PartialView();
        }
        //Resultado
        public ActionResult CalculaAzimuteDecimal(DistDecimal valores)
        {
            if (ModelState.IsValid)
            {
                PontoNotavel pontoNotavel1 =
                    new PontoNotavel(valores.CoordXPt1, valores.CoordYPt1, true);
                PontoNotavel pontoNotavel2 =
                    new PontoNotavel(valores.CoordXPt2, valores.CoordYPt2, true);
                ViewBag.Resultado = pontoNotavel1.AzimuteEntrePontos(pontoNotavel2).ToString("N2");

                return PartialView();
            }

            return PartialView();
        }
        //Métodos para cálculo do Azimute entre dois pontos - Grau
        //Editor
        public ActionResult AzimuteGrau()
        {
            return PartialView();
        }
        //Resultado
        public ActionResult CalculaAzimuteGrau(DistGrau valores)
        {
            if (ModelState.IsValid)
            {
                var coordXPt1 = new GrauMinSeg(valores.CoordXPt1Graus, valores.CoordXPt1Min, valores.CoordXPt1Seg)
                    .ToDouble();
                var coordYPt1 = new GrauMinSeg(valores.CoordYPt1Graus, valores.CoordYPt1Min, valores.CoordYPt1Seg)
                    .ToDouble();
                var coordXPt2 = new GrauMinSeg(valores.CoordXPt2Graus, valores.CoordXPt2Min, valores.CoordXPt2Seg)
                    .ToDouble();
                var coordYPt2 = new GrauMinSeg(valores.CoordYPt2Graus, valores.CoordYPt2Min, valores.CoordYPt2Seg)
                    .ToDouble();

                PontoNotavel pontoNotavel1 = new PontoNotavel(coordXPt1, coordYPt1, true);
                PontoNotavel pontoNotavel2 = new PontoNotavel(coordXPt2, coordYPt2, true);

                ViewBag.Resultado = pontoNotavel1.AzimuteEntrePontos(pontoNotavel2).ToString("N2");

                return PartialView();
            }

            return PartialView();
        }
        //Actions de Coordenadas
        // GET: Calculadora/Coordenadas
        public ActionResult Coordenadas()
        {
            return View();
        }


        //Métodos para cálculo da distância entre dois pontos - Decimal
        //Editor
        public ActionResult DoisPontosDecimal()
        {
            return PartialView();
        }
        //Resultado
        public ActionResult CalculaDistDecimal(DistDecimal valores)
        {
            if (ModelState.IsValid)
            {
                PontoNotavel pontoNotavel1 = new PontoNotavel(valores.CoordXPt1, valores.CoordYPt1, true);
                PontoNotavel pontoNotavel2 = new PontoNotavel(valores.CoordXPt2, valores.CoordYPt2, true);
                ViewBag.Resultado = pontoNotavel1.DistanciaEntrePontos(pontoNotavel2).ToString("N2");

                return PartialView();
            }

            return PartialView();
        }


        //Métodos para cálculo da distância entre dois pontos - GrauMinSeg
        //Editor
        public ActionResult DoisPontosGrau()
        {
            return PartialView();
        }

        //Resultado 
        public ActionResult CalculaDistGrau(DistGrau valores)
        {
            if (ModelState.IsValid)
            {
                var coordXPt1 = new GrauMinSeg(valores.CoordXPt1Graus, valores.CoordXPt1Min, valores.CoordXPt1Seg)
                    .ToDouble();
                var coordYPt1 = new GrauMinSeg(valores.CoordYPt1Graus, valores.CoordYPt1Min, valores.CoordYPt1Seg)
                    .ToDouble();
                var coordXPt2 = new GrauMinSeg(valores.CoordXPt2Graus, valores.CoordXPt2Min, valores.CoordXPt2Seg)
                    .ToDouble();
                var coordYPt2 = new GrauMinSeg(valores.CoordYPt2Graus, valores.CoordYPt2Min, valores.CoordYPt2Seg)
                    .ToDouble();

                PontoNotavel pontoNotavel1 = new PontoNotavel(coordXPt1, coordYPt1, true);
                PontoNotavel pontoNotavel2 = new PontoNotavel(coordXPt2, coordYPt2, true);

                ViewBag.Resultado = pontoNotavel1.DistanciaEntrePontos(pontoNotavel2).ToString("N2");

                return PartialView();
            }

            return PartialView();
        }

        //Métodos de Conversão de Decimal GrauMinSeg 
        //Editor
        public ActionResult ConverterUnidadeDecToGMS()
        {
            return PartialView();
        }

        //Resultado
        public ActionResult ConvUnidDec(ConvDecToGrauMinSeg valores)
        {
           
            if (ModelState.IsValid)
            {             
                ICollection<GrauMinSeg> pontoNotavel = new List<GrauMinSeg>();
                pontoNotavel.Add(new GrauMinSeg(valores.CoordXPt1));
                pontoNotavel.Add(new GrauMinSeg(valores.CoordYPt1));
                

                return PartialView(pontoNotavel);
            }

            return PartialView();
        }

        //Métodos de Conversão de GrauMinSeg para decimal
        //Editor
        public ActionResult ConverterUnidadeGMSToDec()
        {
            return PartialView();
        }
        //Resultado
        public ActionResult ConvUnidGrau(ConvGrauMinSegDec valores)
        {
            if (ModelState.IsValid)
            {
                var coordX = new GrauMinSeg(valores.CoordXGraus, valores.CoordXMin, valores.CoordXSeg)
                    .ToDouble();
                var coordY = new GrauMinSeg(valores.CoordYGraus, valores.CoordYMin, valores.CoordYSeg)
                    .ToDouble();

                PontoNotavel pontoNotavel = new PontoNotavel(coordX, coordY, true);

                return PartialView(pontoNotavel);
            }

            return PartialView();
        }
        //Métodos de Cálculo de inclinação entre dois pontos - Decimal
        //Editor
        public ActionResult InclinacaoDecimal()
        {
            return PartialView();
        }
        public ActionResult CalculaInclinaDecimal(InclinaDec valores)
        {

            if (ModelState.IsValid)
            {
                PontoNotavel pontoNotavel1 = new PontoNotavel(valores.CoordXPt1, valores.CoordYPt1, valores.CotaPt1, true);
                PontoNotavel pontoNotavel2 = new PontoNotavel(valores.CoordXPt2, valores.CoordYPt2, valores.CotaPt2, true);

                ViewBag.Resultado = (pontoNotavel1.InclinacaoEntrePontos(pontoNotavel2) * 100).ToString("N2");

                return PartialView();
            }

            return PartialView();
        }


        //Métodos de Cálculo de inclinação entre dois pontos - GrauMinSeg
        //Editor
        public ActionResult InclinacaoGrau()
        {
            return PartialView();
        }
        //Resultado 
        public ActionResult CalculaInclinaGrau(InclinaGrau valores)
        {
            if (ModelState.IsValid)
            {
                var coordXPt1 = new GrauMinSeg(valores.CoordXPt1Graus, valores.CoordXPt1Min, valores.CoordXPt1Seg)
                    .ToDouble();
                var coordYPt1 = new GrauMinSeg(valores.CoordYPt1Graus, valores.CoordYPt1Min, valores.CoordYPt1Seg)
                    .ToDouble();
                var coordXPt2 = new GrauMinSeg(valores.CoordXPt2Graus, valores.CoordXPt2Min, valores.CoordXPt2Seg)
                    .ToDouble();
                var coordYPt2 = new GrauMinSeg(valores.CoordYPt2Graus, valores.CoordYPt2Min, valores.CoordYPt2Seg)
                    .ToDouble();

                PontoNotavel pontoNotavel1 = new PontoNotavel(coordXPt1, coordYPt1, valores.CotaPt1, true);
                PontoNotavel pontoNotavel2 = new PontoNotavel(coordXPt2, coordYPt2, valores.CotaPt2, true);

                ViewBag.Resultado = (pontoNotavel1.InclinacaoEntrePontos(pontoNotavel2) * 100).ToString("N2");

                return PartialView();
            }

            return PartialView();
        }

    }
}


