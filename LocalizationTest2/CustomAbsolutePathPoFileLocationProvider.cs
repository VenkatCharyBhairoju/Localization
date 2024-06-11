using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.FileProviders.Physical;
using Microsoft.Extensions.Logging;
using OrchardCore.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

public class CustomAbsolutePathPoFileLocationProvider : ILocalizationFileLocationProvider
{
    private readonly string _absolutePath;
    private readonly ILogger<CustomAbsolutePathPoFileLocationProvider> _logger;

    public CustomAbsolutePathPoFileLocationProvider(string absolutePath, ILogger<CustomAbsolutePathPoFileLocationProvider> logger)
    {
        _absolutePath = absolutePath ?? throw new ArgumentNullException(nameof(absolutePath));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public IEnumerable<IFileInfo> GetLocations(string cultureName)
    {
        var cultureInfo = new CultureInfo(cultureName);
        var searchPath = Path.Combine(_absolutePath, cultureInfo.Name);

        _logger.LogInformation("Searching for PO files in {Path}", searchPath);

        if (!Directory.Exists(searchPath))
        {
            _logger.LogWarning("Directory not found: {Path}", searchPath);
            yield break;
        }

        foreach (var file in Directory.EnumerateFiles(searchPath, "*.po", SearchOption.TopDirectoryOnly))
        {
            _logger.LogInformation("Found PO file: {FilePath}", file);
            yield return new PhysicalFileInfo(new FileInfo(file));
        }
    }

    public IEnumerable<IFileInfo> GetLocations()
    {
        _logger.LogInformation("Searching for PO files in {Path}", _absolutePath);

        if (!Directory.Exists(_absolutePath))
        {
            _logger.LogWarning("Directory not found: {Path}", _absolutePath);
            yield break;
        }

        foreach (var file in Directory.EnumerateFiles(_absolutePath, "*.po", SearchOption.AllDirectories))
        {
            _logger.LogInformation("Found PO file: {FilePath}", file);
            yield return new PhysicalFileInfo(new FileInfo(file));
        }
    }
}