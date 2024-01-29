using System.Text.Json;

namespace pompadoc.Settings;

public class Configuration
{
    public Paths? Path { get; set; }

    public static Configuration GetConfiguration()
    {
        string configurationPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
        Configuration? config = JsonSerializer.Deserialize<Configuration>(File.ReadAllText(configurationPath));
        
        if (config is null) throw new("Configuration is null after deserialization");
        if (config.Path is null) throw new("Expected Path attribute in appsettings but Path is null");
        
        VerifyPath("InputPeoplePath",config.Path!.InputPeoplePath,configurationPath);
        VerifyPath("InputTemplatePath",config.Path!.InputTemplatePath,configurationPath);
        VerifyPath("OutputResultPath",config.Path!.OutputResultPath,configurationPath);
        
        return config!;
    }

    private static void VerifyPath(string name, string? path, string configurationPath)
    {
        if (path is null) throw new($"Expected {name} attribute in appsettings.Path but {name} is null");
        if (File.Exists(path) is false)
            throw new(
                $"File supposedly in {path} doesnt exist. Verify configuration file (appsettings.json) to update your paths in {configurationPath}");
    }
}

public class Paths  
{
    public string? InputPeoplePath { get; set; }
    public string? InputTemplatePath { get; set; }
    public string? OutputResultPath { get; set; }
}