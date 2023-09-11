namespace xeroTax.Models;

public class XmlFeedback
{
    public Guid Id { get; set; }
    public DateTime SubmittedDateTimeUtc { get; set; }
    public string? ProductName { get; set; }
    public string ProductVersion { get; set; }
    public string Type { get; set; }
    public string Attention { get; set; }
    public string Email { get; set; }
    public string Comments { get; set; }
    public string Exception { get; set; }
    public string ExceptionType { get; set; }
    public string ExceptionSource { get; set; }
    public string ExceptionDetails { get; set; }
    public MachineInfo Machine { get; set; }
}

public class MachineInfo
{
    public int DisplayCount { get; set; }
    public string DisplayResolutions { get; set; }
    public Guid MachineGuid { get; set; }
    public long MachineMemory { get; set; }
    public string NetFrameworksInstalled { get; set; }
    public string OperatingSystem { get; set; }
    public string OperatingSystemServicePack { get; set; }
    public int ProcessorCores { get; set; }
    public int ProcessorCount { get; set; }
    public string ProcessorName { get; set; }
}
