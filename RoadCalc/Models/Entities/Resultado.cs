using System;
using System.Collections.Generic;

namespace RoadCalc.Models.Entities
{
    public class Resultado
    {

        public bool Valor { get; set; }

        public IEnumerable<string> ErrorsList { get; set; }

        public object TransportBag { get; set; }

        internal Resultado()
        {
            
        }

        public Resultado(bool resultado, string erro)
        {
            Valor = resultado;
            ErrorsList = new List<string>(){erro};
        }


        public Resultado(bool resultado, IEnumerable<String> erros)
        {
            Valor = resultado;
            ErrorsList = erros;
        }
    }
}