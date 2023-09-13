namespace xeroTax.Models;

public class HomeModel {
    public List<XmlFeedback>? xmlFeedback;

    public Dictionary<string, int>? versionNumberChart;

    public Dictionary<string, int>? exceptionSourceChart;

    public KeyValuePair<string, int>[]? topThreeSources;

    public List<VulnerableProductInfo>? vulnerableProductVersions;
}

public class VulnerableProductInfo {
    public string? ProductName;
    public string? MostVulnerableVersion;
    public int TotalExceptions;
}
