using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sorteio.Domain.Business;
using Sorteio.Domain.Interface;
using System.Collections.Concurrent;
using Sorteio.Models;
using System.Threading;

namespace Sorteio.Domain.Singleton
{
    public class SingApostas
    {
        private static int _id = 0;
        private static readonly Lazy<SingApostas> lazy = new Lazy<SingApostas>(() => new SingApostas());

        public static SingApostas Instance
        {
            get { return lazy.Value; }
        }

        private ConcurrentBag<Aposta> _apostas = new ConcurrentBag<Aposta>();

        public Aposta Registrar(int[] numero, string modeloAposta)
        {
            Aposta aposta = new Aposta(modeloAposta)
            {
                Data = DateTime.Now,
                Id = Interlocked.Increment(ref _id),
                Numero = numero
            };

            _apostas.Add(aposta);

            return aposta;
        }

        public bool VerificarDuplicidade(int[] numero, string modeloAposta)
        {
            Aposta aposta = _apostas
                .FirstOrDefault(x => x.Comparar(numero) && x.ModeloAposta == modeloAposta);

            return aposta != null;
        }

        public List<Aposta> ObterApostas(string modeloAposta)
        {
            List<Aposta> apostas = _apostas
                .Where(x => x.ModeloAposta == modeloAposta)
                .OrderByDescending(x => x.Data)
                .ToList();

            return apostas;
        }

        public Aposta Sortear(string modeloAposta)
        {
            Random ran = new Random();
            var apostas = _apostas.Where(x => x.ModeloAposta == modeloAposta.ToUpper());
            int qtd = apostas.Count();
            int pos = ran.Next(qtd - 1);

            Aposta aposta = apostas.ElementAt(pos);

            return aposta;
        }

        public List<Aposta> ObterDezenasIguais(string modeloAposta, int id, int quantidade)
        {
            Aposta apostaSorteada = _apostas.FirstOrDefault(x => x.Id == id && x.ModeloAposta == modeloAposta);
            IEnumerable<Aposta> apostasModelo = _apostas.Where(x => x.ModeloAposta == modeloAposta);
            List<Aposta> apostas = new List<Aposta>();

            foreach (Aposta ap in apostasModelo)
            {
                int quant = 0;

                foreach (int dez in ap.Numero)
                {
                    if (apostaSorteada.Numero.Contains(dez))
                    {
                        quant++;
                    }
                }

                if (quant == quantidade)
                {
                    apostas.Add(ap);
                }
            }

            return apostas;
        }
    }
}