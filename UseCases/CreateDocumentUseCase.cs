using HTMLQuestPDF.Extensions;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace pompadoc.UseCases;

public class CreateDocumentUseCase
{
    private readonly Dictionary<string,string> input;

    public CreateDocumentUseCase(Dictionary<string,string> input)
    {
        this.input = input;
    }

    public Document Create(string template)
    {
        string content = ReplaceVariablesInTemplate(template);
        
        return Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(2, Unit.Centimetre);
                page.Background(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(11));

                page.Content()
                    .PaddingVertical(1, Unit.Centimetre)
                    .Column(x =>
                    {
                        x.Spacing(20);
                        x.Item().HTML(descriptor => descriptor.SetHtml(content));
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
    }

    private string ReplaceVariablesInTemplate(string template)
    {
        string newContent = template;
        foreach (KeyValuePair<string, string> data in input)
        {
            newContent = newContent.Replace($"[{data.Key}]", data.Value);
        }
        return newContent;
    }
}