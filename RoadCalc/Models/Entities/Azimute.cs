namespace RoadCalc.Models.Entities
{
    public class Azimute : EntityBase
    {
        public string Nome { get; set; }

        public int PontoNotavelId { get; set; }

        public virtual PontoNotavel PontoNotavel { get; set; }

        public double Valor { get; set; }

        public int ProjetoId { get; set; }

        public virtual Projeto Projeto { get; set; }

    }
}