
using Aoc2023.Day1;
using Aoc2023.Day2;
using Aoc2023.Day3;
using System.Diagnostics;
using System.Runtime.CompilerServices;


int day = 3;

switch (day)
{
    case 1:
        break;
    case 2:
        dayTwo();
        break;
    case 3:
        dayThree();
        break;
    default:
        Console.WriteLine("No door was chosen");
        break;
}

static void dayTwo()
{
    Dictionary<string, int> bagConfig = new()
    {
        ["red"] = 12,
        ["green"] = 13,
        ["blue"] = 14
    };

    var result = Day2.SolvePartOne(@"D:\Programmieren\_aoc\day1\2023AdventOfCoding\Aoc2023\Aoc2023\Day2\input.txt", bagConfig);
    var result2 = Day2.SolvePartTwo(@"D:\Programmieren\_aoc\day1\2023AdventOfCoding\Aoc2023\Aoc2023\Day2\input.txt");


    Debug.Assert(result is int);
    Debug.Assert(result2 is int);

    Console.WriteLine("Part One:", result);
    Console.WriteLine("Part Two:", result2);

}


static void dayThree()
{
    string inputFilePath = @"D:\Programmieren\_aoc\day1\2023AdventOfCoding\Aoc2023\Aoc2023\Day3\input.txt";
    //string inputFilePath = @"D:\Programmieren\_aoc\day1\2023AdventOfCoding\Aoc2023\Aoc2023\Day3\inputTest.txt";

    //var result1 = Day3.SolvePartOne(inputFilePath);
    var result2 = Day3.SolvePartTwo(inputFilePath);
    //var day3 = new Day3();
    //var result3 = day3.PartTwo(File.ReadAllText(inputFilePath));


    //Debug.Assert(result1 is int);
    //Debug.Assert(result2 is long);

    //Console.WriteLine(result1);
    Console.WriteLine(result2);
    //Console.WriteLine(result3);
}

