using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace csharp_ui.Pages;

using csharp_ui.Helpers;

public class IndexModel : PageModel
{

    private readonly ILogger<IndexModel> _logger;
    private readonly HttpClient _httpClient;
    public string ApiBaseUrl { get; set; }

    public IndexModel(ILogger<IndexModel> logger, IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _httpClient = httpClientFactory.CreateClient();
    }

    public void OnGet()
    {
        ApiBaseUrl = ApiConfig.ApiBaseUrl;
    }

    [IgnoreAntiforgeryToken]
    public async Task<IActionResult> OnPostBestTimeToBuyOrSellStockAsync()
    {
        using var reader = new StreamReader(Request.Body);
        var body = await reader.ReadToEndAsync();
        _logger.LogInformation("Raw request body: {Body}", body);

        var request = System.Text.Json.JsonSerializer.Deserialize<InputModel>(body);
        if (request == null || request.Prices == null || request.Prices.Length < 2)
            return BadRequest("Invalid or missing Prices array.");

        var apiUrl = $"{ApiConfig.ApiBaseUrl}/besttimetobyorsellstock";
        _logger.LogInformation("Forwarding to API: {ApiUrl} with payload: {@Payload}", apiUrl, request.Prices);
        var response = await _httpClient.PostAsJsonAsync(apiUrl, request);
        var result = await response.Content.ReadAsStringAsync();
        _logger.LogInformation("API response status: {StatusCode}, body: {Body}", response.StatusCode, result);
        return Content(result, "application/json");
    }

    public async Task<IActionResult> OnGetWeatherForecastAsync()
    {
        var apiUrl = $"{ApiConfig.ApiBaseUrl}/weatherforecast";
        var response = await _httpClient.GetAsync(apiUrl);
        var result = await response.Content.ReadAsStringAsync();
        return Content(result, "application/json");
    }
}

public class InputModel
{
    public int[] Prices { get; set; }
}