using DDD.App.Api.Applicationses.Queries;
using DDD.Domain.OrderAggregate;
using DDD.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DDD.App.Api.Controllers
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
        private readonly DomainContext _dbContext;
        private readonly IMediator _mediator;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, DomainContext dbContext, IMediator mediator)
        {
            _logger = logger;
            _dbContext = dbContext;
            _mediator = mediator;
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

        [HttpGet("getLatestOrder")]
        public virtual IActionResult GetLatestOrder(CancellationToken cancellationToken)
        {
            var order = _dbContext.Orders.OrderByDescending(n => n.Id).FirstOrDefault();
            return Ok(order);
        }

        [HttpPost("createOrder")]
        public virtual async Task<IActionResult> CreateOrder(CancellationToken cancellationToken)
        {
            var address = new Address("street04", "city04", "zipCode04");
            var order = new Order("444", "wangwu", 10, address);
            await _dbContext.AddAsync(order);
            await _dbContext.SaveEntitiesAsync(cancellationToken);

            return Ok(order);
        }

        [HttpPost("queryOrder")]
        public virtual async Task<IActionResult> QueryOrder([FromBody]OrderQuery @params, CancellationToken cancellationToken)
        {
            var list = await _mediator.Send(@params);

            return Ok(list);
        }
    }
}