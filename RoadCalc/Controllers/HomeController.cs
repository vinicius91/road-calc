using System.Web.Mvc;

namespace RoadCalc.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AnotherLink()
        {
            return View("Index");
        }

        //public ActionResult CanvasTest()
        //{
        //    //Adicionando os pontos notáveis
        //    List<PontoNotavel> pontos = new List<PontoNotavel>()
        //    {
        //        new PontoNotavel() { CoordX = 666891.61, CoordY = 7497073.31, Cota = 0, Nome = "PI"},
        //        new PontoNotavel() { CoordX = 670049.16, CoordY = 7499272.30, Cota = 0, Nome = "P1" },
        //        new PontoNotavel() { CoordX = 671607.06, CoordY = 7498665.60, Cota = 0, Nome = "P2" },
        //        new PontoNotavel() { CoordX = 674587.76, CoordY = 7501793.01, Cota = 0, Nome = "P3" },
        //        new PontoNotavel() { CoordX = 676626.06, CoordY = 7501921.06, Cota = 0, Nome = "P4" },
        //        new PontoNotavel() { CoordX = 676715.81, CoordY = 7503509.80, Cota = 0, Nome = "P5" },
        //        new PontoNotavel() { CoordX = 677001.45, CoordY = 7504498.42, Cota = 0, Nome = "P6" },
        //        new PontoNotavel() { CoordX = 677346.31, CoordY = 7504562.68, Cota = 0, Nome = "P7" },
        //        new PontoNotavel() { CoordX = 678416.84, CoordY = 7505825.10, Cota = 0, Nome = "PF" }
        //    };

        //    //Criando os trechos a partir dos pontos
        //    List<Trecho> trechos = new List<Trecho>();

        //    for (int i = 0; i < pontos.Count - 1; i++)
        //    {
        //        if (i == 0)
        //        {
        //            trechos.Add(new Trecho(pontos[i], pontos[i + 1], null, "Trecho " + pontos[i].Nome + "-" + pontos[i + 1].Nome));
        //        }
        //        else
        //        {
        //            trechos.Add(new Trecho(pontos[i], pontos[i + 1], trechos[i - 1].EstacaFinal, "Trecho " + pontos[i].Nome + "-" + pontos[i + 1].Nome));
                    
        //        }
        //    }

        //    //Criando as curvas a partir dos trechos
        //    List<CurvaHorizontal> curvas = new List<CurvaHorizontal>();

        //    for (int i = 0; i < trechos.Count - 1; i++)
        //    {
        //            curvas.Add(new CurvaHorizontal(trechos[i], trechos[i + 1], 200, trechos[i].EstacaInicial, 20, 70, 0, "Curva " + trechos[i].PontoFinal.Nome));
        //    }

        //    //Passando informações para View

        //    var model = new CanvasViewModel()
        //    {
        //        CurvaHorizontals = curvas,
        //        PontoNotavels = pontos,
        //        Trechos = trechos
        //    };
        //    return View(model);
        //}
    }
}