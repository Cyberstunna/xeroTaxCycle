using System.Diagnostics;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using xeroTax.Models;
// using System.Collections.Generic;
// using System.Linq;

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
                // Add the rest of the properties here
                Machine = new MachineInfo
                {
                    // Populate MachineInfo properties
                }
            })
            .ToList();

        // Get Values for Bar Chart for Version Number

        // Get Values for Pie Chart for Exception Source

        return View(xmlFeedbackList);
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
