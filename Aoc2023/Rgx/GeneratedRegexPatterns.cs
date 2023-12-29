
using System.Text.RegularExpressions;

namespace Aoc2023.Rgx;


public partial class GeneratedRegexPatterns
{
    //[GeneratedRegex(@"[#|$|/|=|*|%|-|@|&|+]")]
    [GeneratedRegex(@"[^0-9.]+")]
    public static partial Regex SignsRgx();


    [GeneratedRegex(@"[*]")]
    public static partial Regex StarRgx();


    [GeneratedRegex(@"\d+")]
    public static partial Regex NumRgx();


    [GeneratedRegex("[a-zA-Z]+")]
    public static partial Regex LetterRgx();
}
