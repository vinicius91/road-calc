namespace RoadCalc.Models.Entities
{
    public class PontoNotavelVertical : EntityBase
    {
        public string Nome { get; set; }

        public int CoordenadaId { get; set; }

        public virtual Coordenada Coordenada { get; set; }

        public int ProjetoId { get; set; }

        public virtual Projeto Projeto { get; set; }

        public int EstacaId { get; set; }

        public virtual Estaca Estaca { get; set; }


        public PontoNotavelVertical()
        {
            
        }

        public PontoNotavelVertical(Estaca estaca)
        {
            Nome = "Estaca " + estaca.Numero + " - " + estaca.Intermediario.ToString("F2") + " m";
            Coordenada = new Coordenada(estaca.Coordenada.X, estaca.Coordenada.Y, estaca.CotaVermelha, estaca.Coordenada.ZoneNumber, estaca.Coordenada.ZoneLetter);
            ProjetoId = estaca.ProjetoId;
            EstacaId = estaca.Id;
        }

    }

   
}