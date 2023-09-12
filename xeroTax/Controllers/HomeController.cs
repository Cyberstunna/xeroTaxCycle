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
    
        var modelData = new HomeModel {
            xmlFeedback = xmlFeedbackList,
            versionNumberChart = versionExceptionCounts,
            exceptionSourceChart = sourceExceptionCounts,
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
