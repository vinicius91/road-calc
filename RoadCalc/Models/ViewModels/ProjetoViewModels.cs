using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using RoadCalc.Models.Entities;

namespace RoadCalc.Models.ViewModels
{

    public class RelatorioProjetoViewModel
    {

        public int Id { get; set; }

        [DisplayName("Nome do Projeto")]
        public string Nome { get; set; }

        [Required]
        [DisplayName("Classe do Projeto")]
        public int ClasseDeProjetoId { get; set; }

        [DisplayName("Ponto Inicial")]
        public int PontoInicialId { get; set; }

        [DisplayName("Ponto Final")]
        public int PontoFinalId { get; set; }

        [DisplayName("Classe do Projeto")]
        public ClasseDeProjeto ClasseDeProjeto { get; set; }

        [DisplayName("Nome do Projeto")]
        public string NomeResponsavel { get; set; }

        public int NumeroDePontos { get; set; }

        public IEnumerable<PontoNotavel> PontoNotaveis { get; set; }

        public int NumeroDeInicializadoresDeCurvas { get; set; }

        public int NumeroDeCurvas { get; set; }

        public IEnumerable<CurvaHorizontal> Curvas { get; set; }

        public IEnumerable<Trecho> Trechos { get; set; }

        public int NumeroDeTrechos { get; set; }

        public string CoordenadasReais { get; set; }

        [DisplayName("Coordenadas no padrão UTM?")]
        public bool CoordenadasReaisBool { get; set; }

        public bool MapaRenderizado { get; set; }

        public List<Estaca> Estacas { get; set; }

        public List<Estaca> Mapa { get; set; }

        public List<Greide> Greides { get; set; }

        public RelatorioProjetoViewModel()
        {

        }

        public RelatorioProjetoViewModel(Projeto projeto)
        {
            Id = projeto.Id;
            Nome = projeto.Nome;
            ClasseDeProjeto = projeto.ClasseDeProjeto;
            NomeResponsavel = projeto.User.GetNomeCompleto();
            NumeroDePontos = projeto.PontosNotaveis?.Count ?? 0;
            PontoNotaveis = projeto.PontosNotaveis ?? new List<PontoNotavel>();
            NumeroDeCurvas = projeto.Curvas?.Count ?? 0;
            NumeroDeCurvas = projeto.Curvas?.Count ?? 0;
            Curvas = projeto.Curvas ?? new List<CurvaHorizontal>();
            NumeroDeTrechos = projeto.Trechos?.Count ?? 0;
            Trechos = projeto.Trechos ?? new List<Trecho>();
            CoordenadasReais = projeto.CoordenadasReais ? "Sim" : "Não";
            CoordenadasReaisBool = projeto.CoordenadasReais;
            MapaRenderizado = projeto.MapaRenderizado;


        }

    }


    public class EditProjetoViewModel
    {

        public int Id { get; set; }

        [DisplayName("Nome do Projeto")]
        public string Nome { get; set; }

        [Required]
        [DisplayName("Classe do Projeto")]
        public int ClasseDeProjetoId { get; set; }

        [DisplayName("Ponto Inicial")]
        public int PontoInicialId { get; set; }

        [DisplayName("Ponto Final")]
        public int PontoFinalId { get; set; }

        [DisplayName("Classe do Projeto")]
        public ClasseDeProjeto ClasseDeProjeto { get; set; }

        [DisplayName("Nome do Projeto")]
        public string NomeResponsavel { get; set; }

        public int NumeroDePontos { get; set; }

        public IEnumerable<PontoNotavel> PontoNotaveis { get; set; }

        public int NumeroDeInicializadoresDeCurvas { get; set; }

        public int NumeroDeCurvas { get; set; }

        public IEnumerable<CurvaHorizontal> Curvas { get; set; }

        public IEnumerable<Trecho> Trechos { get; set; }

        public int NumeroDeTrechos { get; set; }
        
        public string CoordenadasReais { get; set; }

        [DisplayName("Coordenadas no padrão UTM?")]
        public bool CoordenadasReaisBool {get; set; }

        public bool MapaRenderizado { get; set; }

        public bool EstacasGeradas { get; set; }


        public EditProjetoViewModel()
        {
            
        }

        public EditProjetoViewModel(Projeto projeto)
        {
            Id = projeto.Id;
            Nome = projeto.Nome;
            ClasseDeProjeto = projeto.ClasseDeProjeto;
            NomeResponsavel = projeto.User.GetNomeCompleto();
            NumeroDePontos = projeto.PontosNotaveis?.Count ?? 0;
            PontoNotaveis = projeto.PontosNotaveis ?? new List<PontoNotavel>();
            NumeroDeCurvas = projeto.Curvas?.Count ?? 0;
            NumeroDeCurvas = projeto.Curvas?.Count ?? 0;
            Curvas = projeto.Curvas ?? new List<CurvaHorizontal>();
            NumeroDeTrechos = projeto.Trechos?.Count ?? 0;
            Trechos = projeto.Trechos ?? new List<Trecho>();
            CoordenadasReais = projeto.CoordenadasReais ? "Sim" : "Não";
            CoordenadasReaisBool = projeto.CoordenadasReais;
            MapaRenderizado = projeto.MapaRenderizado;
            EstacasGeradas = projeto.EstacasGeradas;


        }

    }


    public class CreateProjetoViewModel
    {
        [Required]
        [DisplayName("Nome do Projeto")]
        public string Nome { get; set; }

        [Required]
        [DisplayName("Classe do Projeto")]
        public int ClasseDeProjetoId { get; set; }
    }

    public class PontoNotavelViewModel
    {

        public int Id { get; set; }

        [DisplayName("Nome")]
        [Required]
        public string Nome { get; set; }


        [DisplayName("Coordenada X")]
        [Required]
        public double CoordX { get; set; }

        [DisplayName("Coordenada Y")]
        [Required]
        public double CoordY { get; set; }

        [DisplayName("Cota")]
        [Required]
        public double Cota { get; set; }

        [DisplayName("Letra da Zona")]
        [StringLength(1, ErrorMessage = "No máximo uma letra", MinimumLength = 0)]
        public string ZoneLetter { get; set; }

        [DisplayName("Número da Zona")]
        [Range(1, 60, ErrorMessage = "Os número de Zona deve estar entre 1 e 60")]
        public int ZoneNumber { get; set; }

        public int ProjetoId { get; set; }

        public bool IsUtm { get; set; }
    }



    public class ListProjetoViewModel
    {

        public int Id { get; set; }

        [DisplayName("Nome do Projeto")]
        public string Nome { get; set; }

        [DisplayName("Classe do Projeto")]
        public ClasseDeProjeto ClasseDeProjeto { get; set; }

        [DisplayName("Nome do Projeto")]
        public string NomeResponsavel { get; set; }

        public int NumeroDePontos { get; set; }

        public int NumeroDeCurvas { get; set; }

        public int NumeroDeTrechos { get; set; }

        public string CoordenadasReais { get; set; }

        public bool MapaRenderizado { get; set; }

        public bool EstacasGeradas { get; set; }

        public ListProjetoViewModel()
        {
            
        }

        public ListProjetoViewModel(Projeto projeto)
        {
            Id = projeto.Id;
            Nome = projeto.Nome;
            ClasseDeProjeto = projeto.ClasseDeProjeto;
            NomeResponsavel = projeto.User.GetNomeCompleto();
            NumeroDePontos = projeto.PontosNotaveis?.Count ?? 0;
            NumeroDeCurvas = projeto.Curvas?.Count ?? 0;
            NumeroDeTrechos = projeto.Trechos?.Count ?? 0;
            CoordenadasReais = projeto.CoordenadasReais ? "Sim" : "Não";
            MapaRenderizado = projeto.MapaRenderizado;
            EstacasGeradas = projeto.EstacasGeradas;

        }




        
    }

    public class AdicionaTrechosViewModel
    {

        public int ProjetoId { get; set; }
        public int NumeroDePontos { get; set; }
        public int NumeroDeTrechos { get; set; }
        public IEnumerable<PontoNotavel> PontoNotaveis { get; set; }
        public int EstacaInicial { get; set; }
        public double ComplementoEstaca { get; set; }
        public int PontoInicialId { get; set; }
        public PontoNotavel PontoInicial { get; set; }

        public AdicionaTrechosViewModel()
        {
            
        }

        public AdicionaTrechosViewModel(Projeto projeto)
        {
            ProjetoId = projeto.Id;
            NumeroDePontos = projeto.PontosNotaveis?.Count ?? 0;
            NumeroDeTrechos = projeto.PontosNotaveis?.Count - 1?? 0;
            PontoNotaveis = projeto.PontosNotaveis ?? new List<PontoNotavel>();
            PontoInicial = projeto.PontosNotaveis?.FirstOrDefault() ?? new PontoNotavel();
            PontoInicialId = projeto.PontosNotaveis?.FirstOrDefault().Id ?? 0;
        }



    }

    public class AdicionaCurvaViewModel
    {
        public int ProjetoId { get; set; }
        public int NumeroDePontos { get; set; }
        public int NumeroDeTrechos { get; set; }
        public int NumeroDeCurvas { get; set; }
        public IEnumerable<PontoNotavel> PontoNotaveis { get; set; }
    }
}