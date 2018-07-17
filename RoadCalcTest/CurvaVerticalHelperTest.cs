using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RoadCalc.Helpers;

namespace RoadCalcTest
{
    [TestClass]
    public class CurvaVerticalHelperTest
    {
        [TestMethod]
        public void Test_CalculaA_IInicial_Maior_IFinal_Positivos()
        {
            var result = CurvaVerticalHelper.CalculaA(0.833, 0.870);
            Assert.AreEqual(0.037, result, 0.01);
        }

        [TestMethod]
        public void Test_CalculaA_IInicial_Positivo_Maior_IFinal_Negativo()
        {
            var result = CurvaVerticalHelper.CalculaA(0.870, -0.412);
            Assert.AreEqual(1.282, result, 0.01);
        }

        [TestMethod]
        public void Test_DistanciaVisibilidadeParada_Caso01()
        {
            var result = CurvaVerticalHelper.DistanciaVisibilidadeDeParada(100, 2.54);
            Assert.AreEqual(83.81, result, 0.01);
        }

        [TestMethod]
        public void Test_DistanciaVisibilidadeParada_Caso02()
        {
            var result = CurvaVerticalHelper.DistanciaVisibilidadeDeParada(100, 1.90);
            Assert.AreEqual(87.83, result, 0.01);
        }

        [TestMethod]
        public void Test_CalculaInclinacao_Caso01()
        {
            var result = CurvaVerticalHelper.CalculaInclinacaoEntreDoisPontos(60, 100, 0, 2400);
            Assert.AreEqual(1.67, result, 0.01);
        }

        [TestMethod]
        public void Test_CalculaInclinacao_Caso02()
        {
            var result = CurvaVerticalHelper.CalculaInclinacaoEntreDoisPontos(100, 130, 2400, 4700);
            Assert.AreEqual(1.30, result, 0.01);
        }

        [TestMethod]
        public void Test_CalculaInclinacao_Caso03()
        {
            var result = CurvaVerticalHelper.CalculaInclinacaoEntreDoisPontos(130, 80, 4700, 9550.79);
            Assert.AreEqual(-1.03, result, 0.01);
        }

        [TestMethod]
        public void Test_Distancia_Visibilidade_Ultrapassagem()
        {
            var result = CurvaVerticalHelper.DistanciaVisibilidadeUltrapassagem(100);
            Assert.AreEqual(680, result, 0.01);
        }


        [TestMethod]
        public void Test_KminAceleracaoCentripeda_Caso01()
        {
            var result = CurvaVerticalHelper.LminAceleracaoCentripeda(100, 2.54, true);
            Assert.AreEqual(133.33, result, 0.01);
        }

        [TestMethod]
        public void Test_KminAceleracaoCentripeda_Caso02()
        {
            var result = CurvaVerticalHelper.LminAceleracaoCentripeda(100, 1.90, true);
            Assert.AreEqual(99.73, result, 0.01);
        }

        [TestMethod]
        public void Test_LminVisbilidadeDeParada_Convexa()
        {
            var result = CurvaVerticalHelper.LminVisbilidadeDeParada(100, 2.54, true);
            Assert.AreEqual(43.30, result, 0.01);
        }

        [TestMethod]
        public void Test_LminVisbilidadeDeParada_Concava()
        {
            var result = CurvaVerticalHelper.LminVisbilidadeDeParada(100, 1.90, false);
            Assert.AreEqual(34.13, result, 0.01);
        }

        [TestMethod]
        public void Test_CalcOMax_Caso01()
        {
            var result = CurvaVerticalHelper.CalcOMax(140, 2.54);
            Assert.AreEqual(0.45, result, 0.01);
        }

        [TestMethod]
        public void Test_CalcOMax_Caso02()
        {
            var result = CurvaVerticalHelper.CalcOMax(100, 1.90);
            Assert.AreEqual(0.24, result, 0.01);
        }
    }
}
