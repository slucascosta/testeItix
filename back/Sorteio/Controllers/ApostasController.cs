using Newtonsoft.Json;
using Sorteio.Domain;
using Sorteio.Domain.Business;
using Sorteio.Domain.Interface;
using Sorteio.Domain.Singleton;
using Sorteio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Sorteio.Controllers
{
    [RoutePrefix("{modeloAposta}")]
    public class ApostasController : ApiController
    {
        [Route("GerarNumero")]
        [HttpGet]
        public IHttpActionResult GerarNumero(string modeloAposta)
        {
            if (string.IsNullOrWhiteSpace(modeloAposta))
            {
                return NotFound();
            }

            try
            {
                ModeloAposta tipoAposta = Instanciador.GerarIntancia(modeloAposta);
                List<int> numero = tipoAposta.GerarNumero();

                return Ok(numero);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("Apostar")]
        [HttpOptions]
        public IHttpActionResult Apostar()
        {
            return Ok();
        }

        [Route("Apostar")]
        [HttpPost]
        public IHttpActionResult Apostar([FromBody] int[] numero, string modeloAposta)
        {
            if (string.IsNullOrWhiteSpace(modeloAposta))
            {
                return NotFound();
            }

            try
            {
                ModeloAposta tipoAposta = Instanciador.GerarIntancia(modeloAposta);
                Aposta aposta = tipoAposta.Registrar(numero);

                return Ok(aposta);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("ObterApostas")]
        [HttpGet]
        public IHttpActionResult ObterApostas(string modeloAposta)
        {
            if (string.IsNullOrWhiteSpace(modeloAposta))
            {
                return NotFound();
            }

            try
            {
                ModeloAposta tipoAposta = Instanciador.GerarIntancia(modeloAposta);
                List<Aposta> apostas = tipoAposta.ObterApostas();

                return Ok(apostas);
            }catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("Sortear")]
        [HttpGet]
        public IHttpActionResult Sortear(string modeloAposta)
        {
            if (string.IsNullOrWhiteSpace(modeloAposta))
            {
                return NotFound();
            }

            Aposta sorteada = SingApostas.Instance.Sortear(modeloAposta);

            return Ok(sorteada);
        }

        [Route("Sortear/{tipo}/{id}")]
        [HttpGet]
        public IHttpActionResult SortearQuina(string modeloAposta, string tipo, int id)
        {
            if (string.IsNullOrWhiteSpace(modeloAposta))
            {
                return NotFound();
            }

            try
            {
                ModeloAposta tipoAposta = Instanciador.GerarIntancia(modeloAposta);
                List<Aposta> apostas = tipoAposta.Sortear(tipo, id);

                return Ok(apostas);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
