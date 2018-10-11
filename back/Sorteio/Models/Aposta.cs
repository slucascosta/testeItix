using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sorteio.Models
{
    public class Aposta
    {
        string modeloAposta;

        public Aposta(string modeloAposta)
        {
            this.modeloAposta = modeloAposta;
        }

        public int Id { get; set; }

        [JsonProperty("Data")]
        public string Registro
        {
            get
            {
                return this.Data.ToString("dd/MM/yyyy HH:mm");
            }
        }

        [JsonProperty("Numero")]
        public string DescNumero
        {
            get
            {
                return Join(this.Numero);
            }
        }

        public string ModeloAposta { get { return modeloAposta; } }

        [JsonIgnore]
        public DateTime Data { get; set; }

        [JsonIgnore]
        public int[] Numero { get; set; }

        public static string Join(IEnumerable<int> numero)
        {
            string join = string.Join(", ",
                numero.Select(x => x.ToString()));

            return join;
        }

        public bool Comparar(int[] numero)
        {
            var orderThis = this.Numero.OrderBy(x => x);
            var orderNum = numero.OrderBy(x => x);

            return Join(orderThis) == Join(orderNum);
        }
    }
}