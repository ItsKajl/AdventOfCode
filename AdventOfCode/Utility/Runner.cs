
using System.Diagnostics;

namespace Utility;


internal class Runner(string basePath)
{
    private readonly string _basePath = basePath;

    private const string _inputFileName = "input.txt";


    public RunnerResult RunSolver(ISolver solver)
    {
        string year = solver.Year().ToString();
        string day = solver.Day().ToString();

        string inputFilePath = Path.Combine(_basePath, year, "Day" + day, _inputFileName);

        RunnerResult results = new($"AdventOfCode {year} Day {day}");

        Stopwatch sw = new();        
        
        try
        {
            sw.Start();
            var res = solver.PartOne(inputFilePath);
            sw.Stop();

            results.AddAnswer($"PartOne -> answer: {res}, time: {sw.Elapsed}");
        }
        catch(Exception ex)
        {
            results.AddError($"PartOne -> error: {ex}");
        }

        try
        {            
            sw.Restart();
            var res = solver.PartTwo(inputFilePath);
            sw.Stop();

            results.AddAnswer($"PartTwo -> answer: {res}, time: {sw.Elapsed}");
        }
        catch (Exception ex)
        {
            results.AddError($"PartTwo -> error: {ex}");
        }

        return results;
    }


}


internal class RunnerResult(string metaData)
{
    private readonly List<string> _answers = new List<string>(2);
    private readonly List<string> _errors = new List<string>(2);
    private readonly string _metaData = metaData;

    public void AddAnswer(string answer) => _answers.Add(answer);

    public void AddError(string error) => _errors.Add(error);

    public string GetMetaData() => _metaData;


    public IReadOnlyCollection<string> GetAnswers()
    {
        return _answers.AsReadOnly();
    }


    public IReadOnlyCollection<string> GetErrors()
    {
        return _errors.AsReadOnly();
    }


}


internal static class RunnerResultsExtension
{
    public static void ToConsole(this RunnerResult runnerResult)
    {
        Console.WriteLine(runnerResult.GetMetaData());

        foreach (var item in runnerResult.GetAnswers())
        {
            Console.WriteLine(item);
        }

        foreach (var item in runnerResult.GetErrors())
        {
            Console.WriteLine(item);
        }
    }
}