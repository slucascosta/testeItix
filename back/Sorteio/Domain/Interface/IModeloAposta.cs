using Sorteio.Domain.Singleton;
using Sorteio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorteio.Domain.Interface
{
    public interface IModeloAposta
    {
        List<int> GerarNumero();
        Aposta Registrar(int[] numero);
        List<Aposta> ObterApostas();
        void Validar(int[] numero);
        List<Aposta> Sortear(string tipo, int id);
    }

    public abstract class ModeloAposta : IModeloAposta
    {
        public string Modelo;
        public int QuantDezena;
        public int MinDezena;
        public int MaxDezena;

        public ModeloAposta()
        {
            Configurar();
        }

        public abstract void Configurar();
        public abstract List<int> GerarNumero();
        public abstract Aposta Registrar(int[] numero);
        public abstract List<Aposta> ObterApostas();

        public void Validar(int[] numero)
        {
            if (numero.Length != QuantDezena)
            {
                throw new Exception(
                    string.Format("As apostas devem compostas por {0} números.", QuantDezena));
            }

            ValidarDezenas(numero);
            ValidarDuplicidade(numero);
        }

        private void ValidarDezenas(int[] numero)
        {
            for (int i = 0; i < numero.Length; i++)
            {
                if (numero[i] < MinDezena || numero[i] > MaxDezena)
                {
                    throw new Exception(
                        string.Format("O valor das dezenas devem estar entre {0} e {1}.", MinDezena, MaxDezena));
                }

                if (i + 1 == QuantDezena)
                {
                    return;
                }

                for (int j = i + 1; j < numero.Length; j++)
                {
                    if (numero[i] == numero[j])
                    {
                        throw new Exception(
                            string.Concat("Não pode haver dezena repetida: ", string.Join(", ", numero)));
                    }
                }
            }
        }

        private void ValidarDuplicidade(int[] numero)
        {
            bool booDuplicidade = SingApostas.Instance.VerificarDuplicidade(numero, Modelo);

            if (booDuplicidade)
            {
                throw new Exception(
                    string.Concat("Não é permitido apostar jogo repetido: ", string.Join(", ", numero)));
            }
        }

        public abstract List<Aposta> Sortear(string tipo, int id);
    }
}
