using System.Text.Json;

namespace pompadoc.UseCases;

public class GetInputDataUseCase
{
    private readonly string inputPath;

    public GetInputDataUseCase(string inputPath)
    {
        this.inputPath = inputPath;
    }

    public Dictionary<string,string> GetData()
    {
        var json = File.ReadAllText(this.inputPath);
        
        using (JsonDocument doc = JsonDocument.Parse(json))
        {
            var dic = new Dictionary<string, string>();
            foreach (JsonProperty element in doc.RootElement.EnumerateObject())
            {
                dic[element.Name] = element.Value.ToString();
            }
            return dic;
        }
    }
}