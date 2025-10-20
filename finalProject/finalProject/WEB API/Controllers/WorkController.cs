using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace finalProject.WEB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> IsWork()
        {
            string apiUrlEUR = "https://boi.org.il/PublicApi/GetExchangeRate?key=EUR";
            string apiUrlUSD = "https://boi.org.il/PublicApi/GetExchangeRate?Key=USD";
            string apiUrlInterest = "https://boi.org.il/PublicApi/GetInterest";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage responseEUR = await client.GetAsync(apiUrlEUR);
                HttpResponseMessage responseUSD = await client.GetAsync(apiUrlUSD);
                HttpResponseMessage responseInterest = await client.GetAsync(apiUrlInterest);

                if (responseEUR.IsSuccessStatusCode && responseUSD.IsSuccessStatusCode && responseInterest.IsSuccessStatusCode)
                {
                    string responseBodyEUR = await responseEUR.Content.ReadAsStringAsync();
                    string responseBodyUSD = await responseUSD.Content.ReadAsStringAsync();
                    string responseBodyInterest = await responseInterest.Content.ReadAsStringAsync();

                    // Parse JSON responses
                    var jsonEUR = JsonDocument.Parse(responseBodyEUR).RootElement;
                    var jsonUSD = JsonDocument.Parse(responseBodyUSD).RootElement;
                    var jsonInterest = JsonDocument.Parse(responseBodyInterest).RootElement;

                    // Extract specific fields
                    var exchangeRateEUR = jsonEUR.GetProperty("currentExchangeRate").GetDecimal();
                    var exchangeRateUSD = jsonUSD.GetProperty("currentExchangeRate").GetDecimal();
                    var interestRate = jsonInterest.GetProperty("currentInterest").GetDecimal();

                    // Return only the required fields
                    var result = new
                    {
                        currentExchangeRateEUR = exchangeRateEUR,
                        currentExchangeRateUSD = exchangeRateUSD,
                        currentInterest = interestRate
                    };

                    return Ok(result);
                }
                else
                {
                    return BadRequest($"Failed to call the API. Status code: {responseEUR.StatusCode}, {responseUSD.StatusCode}, {responseInterest.StatusCode}");
                }
            }
        }
    }
}
