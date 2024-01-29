using pompadoc.UseCases;

Console.WriteLine("####Bienvenue sur Pompadoc####");

GetInputPathUseCase inputPathUseCase = new();
string inputPath = inputPathUseCase.GetPath();

Dictionary<string,string> input = new GetInputDataUseCase(inputPath).GetData();