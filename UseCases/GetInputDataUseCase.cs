using System.Text.Json;

namespace pompadoc.UseCases;

public class GetInputDataUseCase
{
    private readonly string inputPath;

    public GetInputDataUseCase(string inputPath)
    {
        this.inputPath = inputPath;
    }

    public Dictionary<string, string> GetData()
    {
        var json = File.ReadAllText(this.inputPath);

        using JsonDocument doc = JsonDocument.Parse(json);
        var dic = new Dictionary<string, string>();
        foreach (var property in doc.RootElement.EnumerateObject())
        {
            var element = property.Value;
            if (element.ValueKind == JsonValueKind.String)
            {
                dic[property.Name] = element.GetString()!;
            }
            else
            {
                foreach (var subProperty in element.EnumerateObject())
                {
                    var subElement = subProperty.Value;
                    if (subElement.ValueKind == JsonValueKind.String)
                    {
                        dic[$"{property.Name}.{subProperty.Name}"] = subElement.GetString()!;
                    }
                }
            }
        }

        return dic;
    }
}