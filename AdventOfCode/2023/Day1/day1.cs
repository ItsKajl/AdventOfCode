
using System.Buffers;
using System.Text.RegularExpressions;
using Utility;

namespace Aoc2023;


[AocSolver(2023, 1, "Trebuchet?!")]
internal class Day1 : ISolver
{
    public object PartOne(string inputFilePath)
    {
        if (inputFilePath is not string inputFilePath2)
        {
            throw new ArgumentException(null, nameof(inputFilePath));
        }

        char[] buffer = { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
        Span<char> searchPattern = new(buffer);
        SearchValues<char> soptions = SearchValues.Create(searchPattern);

        string[] inputs = File.ReadAllLines(inputFilePath2);

        int sum = 0;
        int currentValue;
        foreach (string input in inputs)
        {
            var inputAsSpan = input.AsSpan();
            var i = inputAsSpan.IndexOfAny(soptions);

            if (i == -1)
            {
                continue;
            }

            currentValue = 0;

            currentValue += (input[i] - '0') * 10;

            i = inputAsSpan.LastIndexOfAny(soptions);

            currentValue += input[i] - '0';
            
            sum += currentValue;
        }

        return sum;
    }


    public object PartTwo(string inputFilePath)
    {
        if (inputFilePath is not string inputFilePath2)
        {
            throw new ArgumentException(null, nameof(inputFilePath));
        }

        string[] digits = ["0", "1", "2", "3", "4", "5", "6", "7", "8", "9"];
        string[] words = ["zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine"];

        string pattern = @"\d|one|two|three|four|five|six|seven|eight|nine";

        Regex regex = new(pattern);
        Regex regex2 = new(pattern, RegexOptions.RightToLeft);

        string[] inputs = File.ReadAllLines(inputFilePath2);

        int[] codes = new int[inputs.Length];
        int sum = 0;
        int currentValue = 0;
        int i = 0;

        int index = -1;
        foreach (string input in inputs)
        {
            index++;
            MatchCollection matches = regex.Matches(input);
            MatchCollection matches2 = regex2.Matches(input);

            if (matches.Count == 0)
            {
                continue;
            }

            i = Utility.Test(matches.First().ToString(), digits, words);
            currentValue = i * 10;

            i = Utility.Test(matches2.First().ToString(), digits, words);
            currentValue += i;

            codes[index] = currentValue;
            sum += currentValue;
        }

        return sum;

    }
}


public class Utility
{
    public static int Test(string input, string[] matchForDigit, string[] matchForWord)
    {
        if (input.Length == 1)
        {
            return Array.IndexOf(matchForDigit, input);
        }

        return Array.IndexOf(matchForWord, input);

    }
}