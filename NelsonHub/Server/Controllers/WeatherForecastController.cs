using NelsonHub.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using NelsonHub.Shared.NWS;
using NelsonHub.Server.Services;

namespace NelsonHub.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IWeatherService service;


        public WeatherForecastController(IWeatherService s)
        {
            service = s;
        }

        [HttpGet]
        public async Task<ActionResult<AwsPeriodModel[]>> Get()
        {
            var forcast = await service.GetWeather();

            return Ok(forcast);
        }
    }
}
