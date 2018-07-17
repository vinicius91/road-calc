using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoadCalc.Models.Entities
{
    public class InicializadorDeCurva : EntityBase
    {

        public virtual Projeto Projeto { get; set; }

        public int ProjetoId { get; set; }


        [ForeignKey("TrechoInicial")]
        public int TrechoInicialId { get; set; }


        [ForeignKey("TrechoFinal")]
        public int TrechoFinalId { get; set; }

        //Trecho inicial da curva expresso pela entidade de Trecho
        [DisplayName("Trecho Inicial")]
        public virtual Trecho TrechoInicial { get; set; }



        //Trecho final de curva expresso pela entidade Trecho
        [DisplayName("Trecho Final")]
        public virtual Trecho TrechoFinal { get; set; }


        public string Nome { get; set; }

        //Raio da Curva em metros
        [Required]
        [DisplayName("Raio")]
        public int Raio { get; set; }

        //Raio da Curva em metros
        [Required]
        [DisplayName("Caso seja necessária o uso de curva de transição, insira um valor base. Caso o valor não esteja dentro dos parâmetros da curva assumiremos o valor médio entro os limites para o comprimento da transição.")]
        public int Lc { get; set; }

        //Super elevação aplicada à curva
        public double SuperElevacao { get; set; }

        //Velocidade Diretriz da via
        [DisplayName("Velocidade Diretriz")]
        public int VelDiretriz { get; set; }

        //Corda da curva em metros
        [DisplayName("Trecho Final")]
        public int Corda { get; set; }

        public bool CurvaInicializada { get; set; }

        public CurvaHorizontal CriaCurvaHorizontal()
        {
            if (TrechoFinal == null)
            {
                return null;
            }
            if (Raio < Projeto.ClasseDeProjeto.RaioMinSupEleMax)
            {
                Raio = Projeto.ClasseDeProjeto.RaioMinSupEleMax;
                SuperElevacao = (double) Projeto.ClasseDeProjeto.SupEleMax / 100;
            }

            CurvaInicializada = true;
            return new CurvaHorizontal(TrechoInicial, TrechoFinal, Raio, Projeto, Corda, VelDiretriz, TrechoInicial.PontoFinal.Nome);
        }

        public CurvaHorizontal CriaCurvaHorizontal(Trecho trechoInicial, Trecho trechoFinal, int raio, int corda, int velDiretriz, string nome, Projeto projeto)
        {
            if (TrechoFinal == null)
            {
                return null;
            }
            if (Raio < Projeto.ClasseDeProjeto.RaioMinSupEleMax)
            {
                Raio = Projeto.ClasseDeProjeto.RaioMinSupEleMax;
                SuperElevacao = (double)Projeto.ClasseDeProjeto.SupEleMax / 100;
            }
            CurvaInicializada = true;
            return new CurvaHorizontal(trechoInicial, trechoFinal, raio, projeto,  corda, velDiretriz, nome);
        }

        public InicializadorDeCurva()
        {
            
        }
    }
}