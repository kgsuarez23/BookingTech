using Api.Manager.Application.Entities;
using Api.Manager.Application.Mediator.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Api.GestionHotel.Controllers
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
        private readonly IMediator _mediator;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet("GetListHotelsQuery")]
        public async Task<ActionResult<ListaHoteles>> GetListHotelsQuery()
        {
            var req = new TestQuery();
            var res = await _mediator.Send(req);
            return Ok(res);
        }

        [HttpGet("testeo2")]
        public async Task<IActionResult> Testeo2([FromBody] TestQuery request)
        {
            var req = new TestQuery();
            var res = await _mediator.Send(request);
            return Ok(res);
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {


            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
