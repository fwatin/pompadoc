using System.Text.Json;

namespace pompadoc.UseCases;

public class GetInputDataUseCase
{
    private readonly string inputPeoplePath;

    public GetInputDataUseCase(string inputPeoplePath)
    {
        this.inputPeoplePath = inputPeoplePath;
    }

    public Dictionary<string, string> GetData()
    {
        string json = File.ReadAllText(this.inputPeoplePath);
        using JsonDocument doc = JsonDocument.Parse(json);
        var dic = new Dictionary<string, string>();
        ParseJsonElement(doc.RootElement, dic, null);
        return dic;
    }

    private void ParseJsonElement(JsonElement element, Dictionary<string, string> dic, string? prefix)
    {
        foreach (var property in element.EnumerateObject())
        {
            var currentKey = prefix != null ? $"{prefix}.{property.Name}" : property.Name;
            if (property.Value.ValueKind == JsonValueKind.String)
            {
                dic[currentKey] = property.Value.GetString()!;
            }
            else if (property.Value.ValueKind == JsonValueKind.Object)
            {
                ParseJsonElement(property.Value, dic, currentKey);
            }
        }
    }
}