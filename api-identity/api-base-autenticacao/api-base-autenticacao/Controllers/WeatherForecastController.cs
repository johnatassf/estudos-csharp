
using infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Dominio.Entities;
using System;

namespace api_base_autenticacao.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ApiContext _apiContext;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,
            ApiContext apiContext)
        {
            _logger = logger;
            _apiContext = apiContext;
        }


        [HttpGet("summaries")]
        public IActionResult GetAllSumaries()
        {
           return Ok(Summaries);
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            var weatherForecas = await _apiContext.weatherForecasts.ToListAsync();

            return Ok(weatherForecas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var weatherForecas = await _apiContext.weatherForecasts.FindAsync(id);

            if (weatherForecas is null)
                return NotFound();

            return Ok(weatherForecas);

        }

        [HttpPost]
        public async Task<IActionResult> PostWeatherForecast(WeatherForecast weatherForecast)
        {
            try
            {
                //Need create a DTO Mapping

                weatherForecast.Id = 0;
                _apiContext.weatherForecasts.Add(weatherForecast);
                await _apiContext.SaveChangesAsync();


                return CreatedAtAction(nameof(GetById), new { id = weatherForecast.Id }, weatherForecast);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, ex.Message);

                return BadRequest("Erro ao salvar weatherForecast");

            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> PostWeatherForecast(int id)
        {
            try
            {
                var weatherForecas = await _apiContext.weatherForecasts.FindAsync(id);

                if (weatherForecas is null)
                    return NotFound();


                _apiContext.weatherForecasts.Remove(weatherForecas);
                _apiContext.SaveChanges();


                return NoContent();
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, ex.Message);

                return BadRequest("Erro ao ao deletar weatherForecast");

            }

        }
    }
}