using Sorteio.Domain.Interface;
using Sorteio.Domain.Singleton;
using Sorteio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sorteio.Domain.Business
{
    public class MegaSenaController : ModeloAposta
    {
        public override void Configurar()
        {
            Modelo = "MEGASENA";
            QuantDezena = 6;
            MinDezena = 1;
            MaxDezena = 60;
        }

        public override List<int> GerarNumero()
        {
            List<int> numero = new List<int>();
            Random ran = new Random();

            while (numero.Count < QuantDezena)
            {
                int num = ran.Next(60);

                if (!numero.Contains(num) && num >= MinDezena && num <= MaxDezena)
                {
                    numero.Add(num);
                }
            }

            if (SingApostas.Instance.VerificarDuplicidade(numero.ToArray(), Modelo))
            {
                numero = GerarNumero();
            }

            return numero;
        }

        public override List<Aposta> ObterApostas()
        {
            List<Aposta> apostas = SingApostas.Instance.ObterApostas(Modelo);

            return apostas;
        }

        public override Aposta Registrar(int[] numero)
        {
            Validar(numero);

            Aposta aposta = SingApostas.Instance.Registrar(numero, Modelo);

            return aposta;
        }

        public override List<Aposta> Sortear(string tipo, int id)
        {
            if (tipo == "Quina")
            {
                return SingApostas.Instance.ObterDezenasIguais(Modelo, id, 5);
            }
            else if (tipo == "Quadra")
            {
                return SingApostas.Instance.ObterDezenasIguais(Modelo, id, 4);
            }
            else
            {
                throw new Exception(string.Concat("Sub tipo inválido: ", tipo));
            }
        }
    }
}