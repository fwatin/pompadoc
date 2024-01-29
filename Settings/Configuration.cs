namespace pompadoc.Settings;

public class Configuration
{
    public Paths? Path { get; set; }
}

public class Paths  
{
    public string? InputPeoplePath { get; set; }
    public string? InputTemplatePath { get; set; }
    public string? OutputResultPath { get; set; }
}