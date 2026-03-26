using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace csharp_ui.Pages;

using csharp_ui.Helpers;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public string ApiBaseUrl { get; set; }

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
        ApiBaseUrl = ApiConfig.ApiBaseUrl;
    }
}
