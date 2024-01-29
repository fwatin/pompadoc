using pompadoc.UseCases;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

Console.WriteLine("####Bienvenue sur Pompadoc####");

// GetInputPathUseCase inputPathUseCase = new();
 //string inputPath = inputPathUseCase.GetPath();
string inputPeoplePath = @"D:\Dev\MyProjects\pompadoc\Data\input-people-jason-burne.json";
string inputTemplatePath = @"D:\Dev\MyProjects\pompadoc\Data\input-template-resiliation-banque.html";

Dictionary<string,string> input = new GetInputDataUseCase(inputPeoplePath).GetData();
string htmlTemplate = File.ReadAllText(inputTemplatePath);

QuestPDF.Settings.License = LicenseType.Community;
var document  = new CreateDocumentUseCase(input).Create(htmlTemplate);
//document.ShowInPreviewer();

document.GeneratePdf("resiliation-banque.pdf");

