using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace csharp_ui.Pages;

using csharp_ui.Helpers;

public record PriceRequest(decimal[] prices);

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

    public async Task<IActionResult> OnPostBestTimeToBuyOrSellStockAsync([FromBody] InputModel Prices)
    {
        var apiUrl = $"{ApiConfig.ApiBaseUrl}/besttimetobyorsellstock";
        var response = await _httpClient.PostAsJsonAsync(apiUrl, Prices.Prices);
        var result = await response.Content.ReadAsStringAsync();
        return Content(result, "application/json");
    }
}

public class InputModel
{
    public string Prices { get; set; }
}