using System.Text.Json;
using pompadoc.Settings;
using pompadoc.UseCases;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

Console.WriteLine("####Bienvenue sur Pompadoc####");

string configurationPath = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
Configuration configuration = JsonSerializer.Deserialize<Configuration>(File.ReadAllText(configurationPath))!;
QuestPDF.Settings.License = LicenseType.Community;

IEnumerable<string> allPeople = Directory.EnumerateFiles(configuration.Path!.InputPeoplePath!);
string[] allTemplates = Directory.EnumerateFiles(configuration.Path!.InputTemplatePath!).ToArray();
if (Directory.Exists(configuration.Path.OutputResultPath) is false)
    Directory.CreateDirectory(configuration.Path.OutputResultPath!);

foreach (string personFilePath in allPeople)
{
    Dictionary<string, string> input = new GetInputDataUseCase(personFilePath).GetData();

    foreach (string templatePath in allTemplates)
    {
        string htmlTemplate = File.ReadAllText(templatePath);
        string templateName = Path.GetFileNameWithoutExtension(templatePath);
        Document document = new CreateDocumentUseCase(input).Create(htmlTemplate);
        string outputPath = Path.Combine(configuration.Path.OutputResultPath!, $"{templateName}.pdf");
        document.GeneratePdf(outputPath);
        Console.WriteLine($"new file generated: {templateName}.pdf");
    }
}