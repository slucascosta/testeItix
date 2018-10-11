using Sorteio.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sorteio.Domain
{
    public class Instanciador
    {
        const string nameSpace = "Sorteio.Domain.Business.{0}Controller";

        public static ModeloAposta GerarIntancia(string tipo)
        {
            Type type = Type.GetType(string.Format(nameSpace, tipo));

            if(type == null)
            {
                throw new Exception(
                    string.Concat("Modelo de aposta não encontrado: ", tipo));
            }

            return (ModeloAposta)Activator.CreateInstance(type);
        }
    }
}