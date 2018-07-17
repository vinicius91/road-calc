using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RoadCalc.Models.ViewModels
{
    public class CurvaHorizontalCircularViewModel
    {
        //Ponto 1
        [DisplayName("Coordenada X")]
        [Required]
        public double CoordXPt1 { get; set; }

        [DisplayName("Coordenada Y")]
        [Required]
        public double CoordYPt1 { get; set; }

        [DisplayName("Cota")]
        [Required]
        public double CotaPt1 { get; set; }


        //Ponto 2
        [DisplayName("Coordenada X")]
        [Required]
        public double CoordXPt2 { get; set; }

        [DisplayName("Coordenada Y")]
        [Required]
        public double CoordYPt2 { get; set; }

        [DisplayName("Cota")]
        [Required]
        public double CotaPt2 { get; set; }

        //Ponto 3
        [DisplayName("Coordenada X")]
        [Required]
        public double CoordXPt3 { get; set; }

        [DisplayName("Coordenada Y")]
        [Required]
        public double CoordYPt3 { get; set; }

        [DisplayName("Cota")]
        [Required]
        public double CotaPt3 { get; set; }


        //Parametros da Curva
        [DisplayName("Raio de Curvatura")]
        [Required]
        public int Raio { get; set; }

        [DisplayName("Corda")]
        [Required]
        public int Corda { get; set; }


        [DisplayName("Estaca Inicial")]
        [Required]
        public double EstacaInicial { get; set; }

        [DisplayName("Estaca Inicial - Metros complementares")]
        [Required]
        public double EstacaInicialInter { get; set; }
    }
}