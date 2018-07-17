using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RoadCalc.Models.ViewModels
{
    public class DistDecimal
    {

        //Distancia entre dois pontos - Decimal

        [DisplayName("Coordenada X")]
        [Required]
        public double CoordXPt1 { get; set; }

        [DisplayName("Coordenada Y")]
        [Required]
        public double CoordYPt1 { get; set; }

        [DisplayName("Coordenada X")]
        [Required]
        public double CoordXPt2 { get; set; }

        [DisplayName("Coordenada Y")]
        [Required]
        public double CoordYPt2 { get; set; }
    }

    public class DistGrau
    {
        //Distancia entre dois pontos - Grau, Minuto e Segundo

        //Ponto 1
        //Eixo X
        [DisplayName("Graus")]
        [Required]
        public int CoordXPt1Graus { get; set; }

        [DisplayName("Minutos")]
        [Range(0.000000001d, 60.0000000000001d, ErrorMessage = "Os valores de minutos e segundos devem estar entre 0 e 60")]
        [Required]
        public int CoordXPt1Min { get; set; }

        [DisplayName("Segundos")]
        [Range(0.000000001d, 60.0000000000001d, ErrorMessage = "Os valores de minutos e segundos devem estar entre 0 e 60")]
        [Required]
        public double CoordXPt1Seg { get; set; }

        //EixoY
        [DisplayName("Graus")]
        [Required]
        public int CoordYPt1Graus { get; set; }

        [DisplayName("Minutos")]
        [Range(0.000000001d, 60.0000000000001d, ErrorMessage = "Os valores de minutos e segundos devem estar entre 0 e 60")]
        [Required]
        public int CoordYPt1Min { get; set; }

        [DisplayName("Segundos")]
        [Range(0.000000001d, 60.0000000000001d, ErrorMessage = "Os valores de minutos e segundos devem estar entre 0 e 60")]
        [Required]
        public double CoordYPt1Seg { get; set; }

        //Ponto 2
        //Eixo X
        [DisplayName("Graus")]
        [Required]
        public int CoordXPt2Graus { get; set; }

        [DisplayName("Minutos")]
        [Range(0.000000001d, 60.0000000000001d, ErrorMessage = "Os valores de minutos e segundos devem estar entre 0 e 60")]
        [Required]
        public int CoordXPt2Min { get; set; }

        [DisplayName("Segundos")]
        [Range(0.000000001d, 60.0000000000001d, ErrorMessage = "Os valores de minutos e segundos devem estar entre 0 e 60")]
        [Required]
        public double CoordXPt2Seg { get; set; }

        //Eixo Y
        [DisplayName("Graus")]
        [Required]
        public int CoordYPt2Graus { get; set; }

        [DisplayName("Minutos")]
        [Range(0.000000001d, 60.0000000000001d, ErrorMessage = "Os valores de minutos e segundos devem estar entre 0 e 60")]
        [Required]
        public int CoordYPt2Min { get; set; }

        [DisplayName("Segundos")]
        [Range(0.000000001d, 60.0000000000001d, ErrorMessage = "Os valores de minutos e segundos devem estar entre 0 e 60")]
        [Required]
        public double CoordYPt2Seg { get; set; }

        
    }

    public class ConvDecToGrauMinSeg
    {

        [DisplayName("Coordenada X")]
        [Required]
        public double CoordXPt1 { get; set; }

        [DisplayName("Coordenada Y")]
        [Required]
        public double CoordYPt1 { get; set; }

        [DisplayName("Cota")]
        [Required]
        public double Cota { get; set; }
    }

    public class ConvGrauMinSegDec
    {
        
        //Eixo X
        [DisplayName("Graus")]
        [Required]
        public int CoordXGraus { get; set; }

        [DisplayName("Minutos")]
        [Range(0.000000001d, 60.0000000000001d, ErrorMessage = "Os valores de minutos e segundos devem estar entre 0 e 60")]
        [Required]
        public int CoordXMin { get; set; }

        [DisplayName("Segundos")]
        [Range(0.000000001d, 60.0000000000001d, ErrorMessage = "Os valores de minutos e segundos devem estar entre 0 e 60")]
        [Required]
        public double CoordXSeg { get; set; }

        //EixoY
        [DisplayName("Graus")]
        [Required]
        public int CoordYGraus { get; set; }

        [DisplayName("Minutos")]
        [Range(0.000000001d, 60.0000000000001d, ErrorMessage = "Os valores de minutos e segundos devem estar entre 0 e 60")]
        [Required]
        public int CoordYMin { get; set; }

        [DisplayName("Segundos")]
        [Range(0.000000001d, 60.0000000000001d, ErrorMessage = "Os valores de minutos e segundos devem estar entre 0 e 60")]
        [Required]
        public double CoordYSeg { get; set; }
    }

    public class InclinaDec
    {
        //Ponto 1
        [DisplayName("Coordenada X")]
        [DataType("number")]
        [Required]
        public double CoordXPt1 { get; set; }

        [DisplayName("Coordenada Y")]
        [DataType("number")]
        [Required]
        public double CoordYPt1 { get; set; }

        [DisplayName("Cota")]
        [DataType("number")]
        [Required]
        public double CotaPt1 { get; set; }


        //Ponto 2
        [DisplayName("Coordenada X")]
        [DataType("number")]
        [Required]
        public double CoordXPt2 { get; set; }

        [DisplayName("Coordenada Y")]
        [DataType("number")]
        [Required]
        public double CoordYPt2 { get; set; }

        [DisplayName("Cota")]
        [DataType("number")]
        [Required]
        public double CotaPt2 { get; set; }
    }

    public class InclinaGrau
    {
        //Ponto 1
        //Eixo X
        [DisplayName("Graus")]
        [Required]
        public int CoordXPt1Graus { get; set; }

        [DisplayName("Minutos")]
        [Range(0.000000001d, 60.0000000000001d, ErrorMessage = "Os valores de minutos e segundos devem estar entre 0 e 60")]
        [Required]
        public int CoordXPt1Min { get; set; }

        [DisplayName("Segundos")]
        [Range(0.000000001d, 60.0000000000001d, ErrorMessage = "Os valores de minutos e segundos devem estar entre 0 e 60")]
        [Required]
        public double CoordXPt1Seg { get; set; }

        //EixoY
        [DisplayName("Graus")]
        [Required]
        public int CoordYPt1Graus { get; set; }

        [DisplayName("Minutos")]
        [Range(0.000000001d, 60.0000000000001d, ErrorMessage = "Os valores de minutos e segundos devem estar entre 0 e 60")]
        [Required]
        public int CoordYPt1Min { get; set; }

        [DisplayName("Segundos")]
        [Range(0.000000001d, 60.0000000000001d, ErrorMessage = "Os valores de minutos e segundos devem estar entre 0 e 60")]
        [Required]
        public double CoordYPt1Seg { get; set; }

        [DisplayName("Cota")]
        [Required]
        public double CotaPt1 { get; set; }

        //Ponto 2
        //Eixo X
        [DisplayName("Graus")]
        [Required]
        public int CoordXPt2Graus { get; set; }

        [DisplayName("Minutos")]
        [Range(0.000000001d, 60.0000000000001d, ErrorMessage = "Os valores de minutos e segundos devem estar entre 0 e 60")]
        [Required]
        public int CoordXPt2Min { get; set; }

        [DisplayName("Segundos")]
        [Range(0.000000001d, 60.0000000000001d, ErrorMessage = "Os valores de minutos e segundos devem estar entre 0 e 60")]
        [Required]
        public double CoordXPt2Seg { get; set; }

        //Eixo Y
        [DisplayName("Graus")]
        [Required]
        public int CoordYPt2Graus { get; set; }

        [DisplayName("Minutos")]
        [Range(0.000000001d, 60.0000000000001d, ErrorMessage = "Os valores de minutos e segundos devem estar entre 0 e 60")]
        [Required]
        public int CoordYPt2Min { get; set; }

        [DisplayName("Segundos")]
        [Range(0.000000001d, 60.0000000000001d, ErrorMessage = "Os valores de minutos e segundos devem estar entre 0 e 60")]
        [Required]
        public double CoordYPt2Seg { get; set; }

        [DisplayName("Cota")]
        [Required]
        public double CotaPt2 { get; set; }
    }



}
