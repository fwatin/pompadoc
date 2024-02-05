using Microsoft.AspNetCore.Components.Forms;
using pompadoc.Settings;
using pompadoc.UseCases;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using System.Text;
using Document = QuestPDF.Fluent.Document;

public class Processor
{
    private readonly List<IBrowserFile> deceasedFiles;
    private readonly List<IBrowserFile> templateFiles;

    public Processor(List<IBrowserFile> deceasedFiles, List<IBrowserFile> templateFiles)
    {
        this.deceasedFiles = deceasedFiles;
        this.templateFiles = templateFiles;
    }

    private async Task<string> ReadFileContent(IBrowserFile file)
    {
        long maxSize = 10 * 1024 * 1024; // Exemple de limite de 10 MB
        using var stream = file.OpenReadStream(maxSize);
        using var reader = new StreamReader(stream, Encoding.UTF8);
        string content = await reader.ReadToEndAsync();
        return content;
    }

    public async Task<List<MemoryStream>> Process()
    {
        QuestPDF.Settings.License = LicenseType.Community;
        List<MemoryStream> pdfStreams = new List<MemoryStream>();

        foreach (var deceasedFile in deceasedFiles)
        {
            string jsonContent = await ReadFileContent(deceasedFile);

            Dictionary<string, string> input = new GetInputDataUseCase(jsonContent).GetData();

            foreach (var templateFile in templateFiles)
            {
                string htmlTemplate = await ReadFileContent(templateFile);
                Document document = new CreateDocumentUseCase(input).Create(htmlTemplate);
                var memoryStream = new MemoryStream();
                document.GeneratePdf(memoryStream);
                memoryStream.Position = 0; // Reset the stream position for reading
                pdfStreams.Add(memoryStream);
            }
        }
        return pdfStreams;
    }
}