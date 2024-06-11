using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System.Globalization;

public class IndexModel : PageModel
{
    private readonly IStringLocalizer<IndexModel> _localizer;
    private readonly ILogger<IndexModel> _logger;

    public string CurrentCulture { get; private set; }
    public string CurrentUICulture { get; private set; }

    public IndexModel(IStringLocalizer<IndexModel> localizer, ILogger<IndexModel> logger)
    {
        _localizer = localizer;
        _logger = logger;
    }

    public void OnGet()
    {
        CurrentCulture = CultureInfo.CurrentCulture.Name;
        CurrentUICulture = CultureInfo.CurrentUICulture.Name;

        _logger.LogInformation("Current Culture: {CurrentCulture}", CurrentCulture);
        _logger.LogInformation("Current UI Culture: {CurrentUICulture}", CurrentUICulture);
    }
}