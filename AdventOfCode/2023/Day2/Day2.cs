
using Rgx;
using System.Text.RegularExpressions;

namespace Aoc.Year2023.Day2;


public partial class Day2
{
    public static object SolvePartOne(object? inputFilePath, Dictionary<string, int> bagConfig)
    {
        if (inputFilePath is not string inputFilePath2)
        {
            throw new ArgumentException(null, nameof(inputFilePath));
        }

        string[] lines = File.ReadAllLines(inputFilePath2);

        Regex numRgx = GeneratedRegexPatterns.NumRgx();
        Regex colorRgx = GeneratedRegexPatterns.LetterRgx();

        List<int> possibleGames = [];

        int gameNumber = 0;
        foreach (string line in lines)
        {
            gameNumber++;
            string gameRes = line.Split(':').Last();

            int[] amounts = numRgx.Matches(gameRes)
                .Select(x => int.Parse(x.ToString()))
                .ToArray();
            string[] colors = colorRgx.Matches(gameRes)
                .Select(x => x.ToString())
                .ToArray();

            bool isPossible = true;
            for (int i = 0; i < colors.Length; i++)
            {
                if (!bagConfig.ContainsKey(colors[i]))
                {
                    isPossible = false;
                    break;
                }

                if (bagConfig[colors[i]] < amounts[i])
                {
                    isPossible = false;
                    break;
                }
            }

            if (isPossible)
            {
                possibleGames.Add(gameNumber);
            }

        }

        return possibleGames.Sum();
    }


    public static object SolvePartTwo(object? inputFilePath)
    {
        if (inputFilePath is not string inputFilePath2)
        {
            throw new ArgumentException(null, nameof(inputFilePath));
        }

        string[] lines = File.ReadAllLines(inputFilePath2);
        Regex numRgx = GeneratedRegexPatterns.NumRgx();
        Regex colorRgx = GeneratedRegexPatterns.LetterRgx();
        int sum = 0;
        int gameNumber = 0;

        foreach (string line in lines)
        {
            gameNumber++;

            Dictionary<string, int> minimunConfig = [];

            string gameRes = line.Split(':').Last();

            int[] amounts = numRgx.Matches(gameRes)
                .Select(x => int.Parse(x.ToString()))
                .ToArray();
            string[] colors = colorRgx.Matches(gameRes)
                .Select(x => x.ToString())
                .ToArray();

            for (int i = 0; i < colors.Length; i++)
            {
                if (!minimunConfig.ContainsKey(colors[i]))
                {
                    minimunConfig[colors[i]] = amounts[i];
                    continue;
                }

                if (minimunConfig[colors[i]] < amounts[i])
                {
                    minimunConfig[colors[i]] = amounts[i];
                }
            }

            sum += minimunConfig.Count switch
            {
                0 => 0,
                1 => minimunConfig.Values.First(),
                _ => minimunConfig.Values.Aggregate((x, y) => x * y)
            };

        }

        return sum;
    }

}
