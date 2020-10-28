using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using NelsonHub.Shared.NWS;

namespace NelsonHub.Server.Services
{
    public interface IWeatherService
    {
        Task<AwsPeriodModel[]> GetWeather();
    }

    public class WeatherService : IWeatherService
    {
        private readonly HttpClient HttpClient;

        public WeatherService(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }

        public async Task<AwsPeriodModel[]> GetWeather()
        {
            var response = await HttpClient.GetAsync("https://api.weather.gov/gridpoints/SLC/93,163/forecast");            

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();

                var awsResponse = JsonSerializer.Deserialize<AwsResponseModel>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return awsResponse.Properties.Periods;
            }
            else
            {
                return null;
            }


        }
    }
}
