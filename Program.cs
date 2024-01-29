using pompadoc.UseCases;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;

Console.WriteLine("####Bienvenue sur Pompadoc####");

// GetInputPathUseCase inputPathUseCase = new();
 //string inputPath = inputPathUseCase.GetPath();
string inputPath = @"D:\Dev\MyProjects\pompadoc\Data\input-people-jason-burne.json";
//
Dictionary<string,string> input = new GetInputDataUseCase(inputPath).GetData();

QuestPDF.Settings.License = LicenseType.Community;
var document  = Document.Create(container =>
{
    container.Page(page =>
    {
        page.Size(PageSizes.A4);
        page.Margin(2, Unit.Centimetre);
        page.Background(Colors.White);
        page.DefaultTextStyle(x => x.FontSize(20));

        page.Header()
            .Text($"Données concernant {input["etatCivil.nom"]}")
            .SemiBold().FontSize(36).FontColor(Colors.Blue.Medium);

        page.Content()
            .PaddingVertical(1, Unit.Centimetre)
            .Column(x =>
            {
                x.Spacing(20);

                x.Item().Text(Placeholders.LoremIpsum());
                x.Item().Image(Placeholders.Image(200, 100));
            });

        page.Footer()
            .AlignCenter()
            .Text(x =>
            {
                x.Span("Page ");
                x.CurrentPageNumber();
            });
    });
});
//document.ShowInPreviewer();
document.GeneratePdf("hello.pdf");