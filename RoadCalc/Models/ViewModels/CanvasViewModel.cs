using System.Collections.Generic;
using RoadCalc.Models.Entities;

namespace RoadCalc.Models.ViewModels
{
    public class CanvasViewModel
    {
        public List<PontoNotavel> PontoNotavels { get; set; }

        public List<Trecho> Trechos { get; set; }

        public List<CurvaHorizontal> CurvaHorizontals { get; set; }

    }
}