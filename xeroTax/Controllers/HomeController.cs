using System.Diagnostics;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using xeroTax.Models;

namespace xeroTax.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        var xmlFilePath = "wwwroot/DataFile.xml";

        var xmlDocument = XDocument.Load(xmlFilePath);
        var xmlFeedbackList = xmlDocument.Descendants("XmlFeedback")
            .Select(x => new XmlFeedback
            {
                Id = Guid.Parse(x.Element("Id")!.Value),
                SubmittedDateTimeUtc = DateTime.Parse(x.Element("SubmittedDateTimeUtc")!.Value),
                ProductName = x.Element("ProductName")?.Value,
                ProductVersion = x.Element("ProductVersion")?.Value,
                Type = x.Element("Type")?.Value,
                Email = x.Element("Email")?.Value,
                Exception = x.Element("Exception")?.Value,
                ExceptionType = x.Element("ExceptionType")?.Value,
                ExceptionSource = x.Element("ExceptionSource")?.Value,
                ExceptionDetails = x.Element("ExceptionDetails")?.Value,
            })
            .ToList();
        
        var vulnerableProductVersions = xmlFeedbackList
            .GroupBy(feedback => new { feedback.ProductName, feedback.ProductVersion })
            .Select(group => new
            {
                ProductName = group.Key.ProductName,
                ProductVersion = group.Key.ProductVersion,
                ExceptionCount = group.Count(),
            })
            .GroupBy(product => product.ProductName)
            .Select(group => new VulnerableProductInfo
            {
                ProductName = group.Key,
                MostVulnerableVersion = group.OrderByDescending(p => p.ExceptionCount).FirstOrDefault()?.ProductVersion,
                TotalExceptions = group.Sum(p => p.ExceptionCount),
            })
            .ToList();

        // Get Values for Bar Chart for Version Number
        var versionExceptionCounts = xmlFeedbackList?
            .GroupBy(x => x.ProductVersion!)
            .ToDictionary(
                group => group.Key,
                group => group.Count()
            );

        // Get Values for Pie Chart for Exception Source
        var sourceExceptionCounts = xmlFeedbackList?
            .GroupBy(x => x.ExceptionSource!)
            .ToDictionary(
                group => group.Key,
                group => group.Count()
            );

        var top3Sources = sourceExceptionCounts?
            .OrderByDescending(kv => kv.Value) // Order by count in descending order
            .Take(3) // Take the top 3 entries
            .ToArray();
    
        var modelData = new HomeModel {
            xmlFeedback = xmlFeedbackList,
            versionNumberChart = versionExceptionCounts,
            exceptionSourceChart = sourceExceptionCounts,
            topThreeSources = top3Sources,
            vulnerableProductVersions = vulnerableProductVersions,
        };

        return View(modelData);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
