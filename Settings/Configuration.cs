using System.Text.Json;

namespace pompadoc.Settings;

public class Configuration
{
    public Paths? Path { get; set; }

    public static Configuration GetConfiguration()
    {
        string configurationPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
        Configuration? config = JsonSerializer.Deserialize<Configuration>(File.ReadAllText(configurationPath));
        
        if (config is null) throw new("Configuration is null after deserialisation");
        if (config.Path is null) throw new("Expected Path attribute in appsettings but Path is null");
        if (config.Path!.InputPeoplePath is null) throw new("Expected InputPeoplePath attribute in appsettings.Path but InputPeoplePath is null");
        if (config.Path!.InputTemplatePath is null) throw new("Expected InputTemplatePath attribute in appsettings.Path but InputTemplatePath is null");
        if (config.Path!.OutputResultPath is null) throw new("Expected OutputResultPath attribute in appsettings.Path but OutputResultPath is null");
        
        return config!;
    }
}

public class Paths  
{
    public string? InputPeoplePath { get; set; }
    public string? InputTemplatePath { get; set; }
    public string? OutputResultPath { get; set; }
}