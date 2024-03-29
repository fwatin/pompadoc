﻿using System.Text.Json;

namespace pompadoc.UseCases;

public class GetInputDataUseCase
{
    private readonly string jsonContent;

    public GetInputDataUseCase(string jsonContent)
    {
        this.jsonContent = jsonContent;
    }

    public Dictionary<string, string> GetData()
    {
        using JsonDocument doc = JsonDocument.Parse(jsonContent);
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