namespace pompadoc.UseCases;

public class GetInputPathUseCase
{
    public string GetPath()
    {
        Console.WriteLine("Entrer le chemin ou se trouve les jsons contenant les détails des défunts:");
        string? path = Console.ReadLine();
        if(path is null) throw new($"Path is null");
        return path!;
    }
}