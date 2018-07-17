namespace RoadCalc.Models.Entities
{
    public class Greide : EntityBase
    {
        public virtual Projeto Projeto { get; set; }

        public int ProjetoId { get; set; }

        public string Nome { get; set; }

        public int Numero { get; set; }

        public double Cota { get; set; }

        public double CotaNatural { get; set; }

        public double Movimentacao { get; set; }

        public Locacao Locacao { get; set; }

        public Greide()
        {
            
        }

        public Greide(Estaca estaca)
        {
            ProjetoId = estaca.ProjetoId;
            Nome = "E-"+estaca.Numero+"-"+estaca.Intermediario.ToString("F2");
            Numero = (int)estaca.Numero;
            Cota = estaca.CotaVermelha;
            CotaNatural = estaca.Coordenada.Z;
            Movimentacao = CotaNatural - Cota;
        }


    }

    public enum Locacao
    {
        Eixo,
        Esquerda,
        Direita
    }
}