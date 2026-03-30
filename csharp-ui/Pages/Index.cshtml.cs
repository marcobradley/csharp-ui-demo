using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace csharp_ui.Pages;

using csharp_ui.Helpers;

public class IndexModel : PageModel
{

    private readonly ILogger<IndexModel> _logger;
    private readonly HttpClient _httpClient;

    public IndexModel(ILogger<IndexModel> logger, IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _httpClient = httpClientFactory.CreateClient();
    }

    public async Task<IActionResult> OnPostBestTimeToBuyOrSellStockAsync()
    {
        _logger.LogInformation("Received request for BestTimeToBuyOrSellStock handler");
        using var reader = new StreamReader(Request.Body);
        var body = await reader.ReadToEndAsync();
        _logger.LogInformation("Handler raw request body: {Body}", body);

        InputModel? request = null;
        try
        {
            request = System.Text.Json.JsonSerializer.Deserialize<InputModel>(body);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Deserialization failed");
            return BadRequest("Deserialization failed: " + ex.Message);
        }

        if (request == null)
        {
            _logger.LogWarning("Deserialized request is null");
            return BadRequest("Deserialized request is null");
        }
        if (request.Prices == null)
        {
            _logger.LogWarning("Prices property is null");
            return BadRequest("Prices property is null");
        }
        if (request.Prices.Length < 2)
        {
            _logger.LogWarning("Prices array has less than 2 elements");
            return BadRequest("Please provide at least two stock prices.");
        }

        var apiUrl = $"{CSharpApiConfig.ApiBaseUrl}/besttimetobyorsellstock";
        _logger.LogInformation("Forwarding to API: {ApiUrl} with payload: {@Payload}", apiUrl, request.Prices);
        var response = await _httpClient.PostAsJsonAsync(apiUrl, request);
        var result = await response.Content.ReadAsStringAsync();
        _logger.LogInformation("API response status: {StatusCode}, body: {Body}", response.StatusCode, result);
        return Content(result, "application/json");
    }

    public async Task<IActionResult> OnPostQuickSortAsync()
    {
        _logger.LogInformation("Received request for QuickSort handler");
        using var reader = new StreamReader(Request.Body);
        var body = await reader.ReadToEndAsync();
        _logger.LogInformation("Handler raw request body: {Body}", body);

        InputSortModel? request = null;
        try
        {
            request = System.Text.Json.JsonSerializer.Deserialize<InputSortModel>(body);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Deserialization failed");
            return BadRequest("Deserialization failed: " + ex.Message);
        }

        if (request == null)
        {
            _logger.LogWarning("Deserialized request is null");
            return BadRequest("Deserialized request is null");
        }
        if (request.array == null)
        {
            _logger.LogWarning("Array property is null");
            return BadRequest("Array property is null");
        }
        if (request.array.Length < 2)
        {
            _logger.LogWarning("Array has less than 2 elements");
            return BadRequest("Please provide at least two numbers.");
        }

        var apiUrl = $"{GoApiConfig.ApiBaseUrl}/quicksort";
        _logger.LogInformation("Forwarding to API: {ApiUrl} with payload: {@Payload}", apiUrl, request.array);
        var response = await _httpClient.PostAsJsonAsync(apiUrl, request);
        var result = await response.Content.ReadAsStringAsync();
        _logger.LogInformation("API response status: {StatusCode}, body: {Body}", response.StatusCode, result);
        return Content(result, "application/json");
    }

    public async Task<IActionResult> OnGetWeatherForecastAsync()
    {
        var apiUrl = $"{CSharpApiConfig.ApiBaseUrl}/weatherforecast";
        var response = await _httpClient.GetAsync(apiUrl);
        var result = await response.Content.ReadAsStringAsync();
        return Content(result, "application/json");
    }
}

public class InputModel
{
    public int[] Prices { get; set; }
}

public class InputSortModel
{
    public int[] array { get; set; }
}