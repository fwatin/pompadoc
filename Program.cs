using pompadoc.Settings;
using pompadoc.UseCases;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

Console.WriteLine("####Bienvenue sur Pompadoc####");

Configuration configuration = Configuration.GetConfiguration();
QuestPDF.Settings.License = LicenseType.Community;

string[] allPeople = Directory.EnumerateFiles(configuration.Path!.InputPeoplePath!).ToArray();
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
Console.WriteLine($"total of generated documents: {allPeople.Length * allTemplates.Length}");