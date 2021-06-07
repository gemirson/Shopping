using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopping.Core;

namespace Shopping.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        private readonly IClientService _clientService;
        private readonly IHttpCliente   _httpCliente;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IClientService clientService, IHttpCliente httpCliente)
        {
            _logger = logger;
            _clientService = clientService;
            _httpCliente = httpCliente;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("user")]
        public IActionResult GetUser()
        {
            return Ok(_clientService.GetAll());
        }

        [HttpGet("All")]
        public IActionResult GetAll()
        {
            return Ok(_httpCliente.Get());
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await _httpCliente.GetCliente());
        }
    }
}
