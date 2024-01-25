using CryptoBull.Models;
using CryptoBull.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace CryptoBull.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CryptoController
    {
        private readonly ICryptoService _cryptoService;

        public CryptoController(ICryptoService cryptoService)
        {
            _cryptoService = cryptoService;
        }

        [HttpGet("getAll")] 
        public async Task<ActionResult<object>> GetAll()
        {
            var cryptoData = await _cryptoService.GetCryptoData();
            var result = new
            {
                data = cryptoData
            };

            return result;
        }

        [HttpGet("getCryptocurrency/{id}")]
        public async Task<ActionResult> GetCryptocurrency(string id)
        {
            var cryptoData = await _cryptoService.GetCryptocurrency(id);
            if (cryptoData != null)
            {
                return new JsonResult(cryptoData);
            }
            else
            {
                return new JsonResult($"Cryptocurrency with id {id} not found.");
            }

        }
        [HttpGet("getTopMovers")]
        public async Task<JsonResult> GetTopMovers()
        {
            var cryptoData = await _cryptoService.GetTopMovers();

            var result = new
            {
                data = cryptoData
            };

            // Возвращаем объект в формате JSON
            return new JsonResult(result);


        }
    }
}
