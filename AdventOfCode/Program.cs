
using Aoc2023;
using Utility;


#if DEBUG

string basePath = @"D:\Programmieren\AdventOfCode\AdventOfCode\";

ISolver solver = new Day4();

Runner runner = new(basePath);

RunnerResult res = runner.RunSolver(solver);

res.ToConsole();

#else

// todo: Console Application for release build

#endif