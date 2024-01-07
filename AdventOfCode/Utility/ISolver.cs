
// idea from encse
// https://github.com/encse/adventofcode/blob/master/Lib/Runner.cs

namespace Utility;

[AttributeUsage(AttributeTargets.Class)]
internal class AocSolver(ushort year, ushort day, string name) : Attribute
{
    public readonly ushort Year = year;
    public readonly ushort Day = day;
    public readonly string Name = name;
}


internal interface ISolver
{
    public object PartOne(string input);

    public object PartTwo(string input);
}


internal static class ISolverExtension
{
    public static IEnumerable<object> Solve(this ISolver solver, string input)
    {
        yield return solver.PartOne(input);
        yield return solver.PartTwo(input);
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="solver"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static ushort Year(this ISolver solver)
    {
        return (Attribute.GetCustomAttribute(solver.GetType(), typeof(AocSolver)) as AocSolver)?.Year ?? throw new Exception();
    }


    public static ushort Day(this ISolver solver)
    {
        return (Attribute.GetCustomAttribute(solver.GetType(), typeof(AocSolver)) as AocSolver)?.Day ?? throw new Exception();
    }
}
