using System.Collections.Generic;
using System.ComponentModel;

namespace RoadCalc.Models.Entities
{
    public class ClasseDeProjeto : EntityBase
    {
        public string Nome { get; set; }

        [DisplayName("Características")]
        public string Caracteristicas { get; set; }

        [DisplayName("Critérios de Classificação Técnica")]
        public string CritClassTecnica { get; set; }

        [DisplayName("Critérios de Classificação Técnica")]
        public TiposDeRelevo Relevo { get; set; }

        [DisplayName("Velocidade de projeto")]
        public int VelProjeto { get; set; }

        [DisplayName("Velocidade diretriz Mínima")]
        public int VelDirMin { get; set; }

        [DisplayName("Distância mínima de visibilidade de Parada - Mínimo Desejável")]
        public int DistMinVisbParMinD { get; set; }

        [DisplayName("Distância mínima de visibilidade de Parada - Mínimo Absoluto")]
        public int DistMinVisbParMinA { get; set; }

        [DisplayName("Distância mínima de visibilidade de Ultrapassagem")]
        public int DistMinVisbUltra { get; set; }

        [DisplayName("Raio mínimo de curva horizontal - Para superelevação máxima")]
        public int RaioMinSupEleMax { get; set; }

        [DisplayName("Superelevação máxima")]
        public int SupEleMax { get; set; }

        [DisplayName("Rampa máxima")]
        public double RampaMax { get; set; }

        [DisplayName("Valor de K para Curvas Convexas - Mínimo Desejável")]
        public int KCxMinD { get; set; }

        [DisplayName("Valor de K para Curvas Convexas - Mínimo Absoluto")]
        public int KCxMinA { get; set; }

        [DisplayName("Valor de K para Curvas Côncavas - Mínimo Desejável")]
        public int KCvMinD { get; set; }

        [DisplayName("Valor de K para Curvas Côncavas - Mínimo Absoluto")]
        public int KCvMinA { get; set; }

        [DisplayName("Largura da Faixa de Trânsito")]
        public double LargFxTrans { get; set; }

        [DisplayName("Largura do Acostamento Externo - Mínimo Desejável")]
        public double LargAcoExtD { get; set; }

        [DisplayName("Largura do Acostamento Externo - Mínimo Absoluto")]
        public double LargAcoExtA { get; set; }

        [DisplayName("Largura do Acostamento Interno - Pista de duas faixas - Valor Mínimo")]
        public double LargAcoIntDuasFxMin { get; set; }

        [DisplayName("Largura do Acostamento Interno - Pista de três faixas - Valor Mínimo")]
        public double LargAcoIntTresFxMin { get; set; }

        [DisplayName("Largura do Acostamento Interno - Pista de quatro faixas - Valor Mínimo")]
        public double LargAcoIntQuatroFxMin { get; set; }

        [DisplayName("Largura do Acostamento Interno - Pista de duas faixas - Valor Máximo")]
        public double LargAcoIntDuasFxMax { get; set; }

        [DisplayName("Largura do Acostamento Interno - Pista de três faixas - Valor Máximo")]
        public double LargAcoIntTresFxMax { get; set; }

        [DisplayName("Largura do Acostamento Interno - Pista de quatro faixas - Valor Máximo")]
        public double LargAcoIntQuatroFxMax { get; set; }

        [DisplayName("Gabarito vertical (altura livre) - Mínimo Desejável")]
        public double GabVertD { get; set; }

        [DisplayName("Gabarito vertical (altura livre) - Mínimo Absoluto")]
        public double GabVertA { get; set; }

        [DisplayName("Afastamento mínimo da borda do acostamento - Obstaculos Contínuos")]
        public double AfastMinBordAcoObC { get; set; }

        [DisplayName("Afastamento mínimo da borda do acostamento - Obstaculos Isolados")]
        public double AfastMinBordAcoObI { get; set; }

        [DisplayName("Largura do canteiro central - Largura Desejável - Mínimo")]
        public double LargCantCentDMin { get; set; }

        [DisplayName("Largura do canteiro central - Valor Normal - Mínimo")]
        public double LargCantCentNMin { get; set; }

        [DisplayName("Largura do canteiro central - Mínimo Absoluto - Mínimo")]
        public double LargCantCentAMin { get; set; }

        [DisplayName("Largura do canteiro central - Largura Desejável - Mínimo")]
        public double LargCantCentDMax { get; set; }

        [DisplayName("Largura do canteiro central - Valor Normal - Mínimo")]
        public double LargCantCentNMax { get; set; }

        [DisplayName("Largura do canteiro central - Mínimo Absoluto - Mínimo")]
        public double LargCantCentAMax { get; set; }

        public ICollection<Projeto> Projetos { get; set; }


        public string NomeCompleto()
        {
            return "Classe " + Nome + " - Terreno " + Relevo.ToString() + " - " + Caracteristicas.ToString();
        }

        public enum TiposDeRelevo
        {
            Plano,
            Ondulado,
            Montanhoso

        }
    }
}