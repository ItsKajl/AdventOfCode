
using Rgx;
using System.Buffers;
using System.Collections;
using System.Text.RegularExpressions;

namespace Aoc2023.Day3;


public partial class Day3
{

    private class EnginePartCoords
    {
        public readonly int Index;
        public readonly int Value;
        public readonly int Length;


        public EnginePartCoords(int index, int value, int length)
        {
            Index = index;
            Value = value;
            Length = length;
        }
    }


    private class EnginePartsToCheck()
    {
        private EnginePartCoords[]? _before;
        private EnginePartCoords[]? _current;
        private EnginePartCoords[]? _next;


        public EnginePartsToCheck(
            EnginePartCoords[]? current,
            EnginePartCoords[]? next,
            EnginePartCoords[]? before)
            :this()
        {
            _before = before;
            _current = current;
            _next= next;
        }


        public void ShiftEntries(EnginePartCoords[]? next = null)
        {
            this._before = this._current;
            this._current = this._next;
            this._next = next;
        }


        public int CheckForMatches(int[] signIndeces)
        {
            return checkForMatch(_before, signIndeces)
                + checkForMatch(_current, signIndeces)
                + checkForMatch(_next, signIndeces);
        }


        public long GetGearRatios(int[] signIndeces)
        {
            long sum = 0;
            IEnumerator s = signIndeces.GetEnumerator();

            IEnumerator b = _before.GetEnumerator();
            b.MoveNext();
            IEnumerator c = _current.GetEnumerator();
            c.MoveNext();
            IEnumerator n = _next.GetEnumerator();
            n.MoveNext();

            List<int> useForGearRatio = [];

            while (s.MoveNext())
            {
                useForGearRatio.Clear();

                if (moveGearPartsForGearRatio(b, (int)s.Current))
                {
                    useForGearRatio.Add(((EnginePartCoords)b.Current).Value);
                }

                if (moveGearPartsForGearRatio(c, (int)s.Current))
                {
                    useForGearRatio.Add(((EnginePartCoords)c.Current).Value);
                }

                if (moveGearPartsForGearRatio(n, (int)s.Current))
                {
                    useForGearRatio.Add(((EnginePartCoords)n.Current).Value);
                }

                if (useForGearRatio.Count == 2)
                {
                    sum += useForGearRatio[0] * useForGearRatio[1];
                    Console.WriteLine(useForGearRatio[0] * useForGearRatio[1]);
                }
            }

            return sum;
        }


        private bool moveGearPartsForGearRatio(IEnumerator enumerator, int currentSign)
        {
            try
            {                
                if (((EnginePartCoords)enumerator.Current).Index > currentSign + 1)
                {
                    return false;
                }

                for (int i = 0; i < ((EnginePartCoords)enumerator.Current).Length; i++)
                {
                    if (((EnginePartCoords)enumerator.Current).Index + i >= currentSign - 1
                        && ((EnginePartCoords)enumerator.Current).Index + i <= currentSign + 1)
                    {
                        return true;
                    }
                }

                while (enumerator.MoveNext())
                {
                    if (((EnginePartCoords)enumerator.Current).Index > currentSign + 1)
                    {
                        return false;
                    }

                    for (int i = 0; i < ((EnginePartCoords)enumerator.Current).Length; i++)
                    {
                        if (((EnginePartCoords)enumerator.Current).Index + i >= currentSign - 1
                            && ((EnginePartCoords)enumerator.Current).Index + i <= currentSign + 1)
                        {
                            return true;
                        }
                    }
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }


        private int checkForMatch(EnginePartCoords[]? engineParts, int[] signIdeces)
        {
            if (engineParts is null)
            {
                return 0;
            }

            IEnumerator einginePartsEnumerator = engineParts.GetEnumerator();
            IEnumerator s = signIdeces.GetEnumerator();
            if (!s.MoveNext())
            {
                return 0;
            }

            int sum = 0;
            while (einginePartsEnumerator.MoveNext())
            {
                EnginePartCoords currentEnginePart = (EnginePartCoords)einginePartsEnumerator.Current;

                while (currentEnginePart.Index > (int)s.Current + 1)
                {
                    if (!s.MoveNext())
                    {
                        return sum;
                    }
                }
                int currentSign = (int)s.Current;

                for (int i = 0; i < currentEnginePart.Length; i++)
                {
                    if(currentEnginePart.Index+i >= currentSign -1
                        && currentEnginePart.Index + i <= currentSign + 1)
                    {
                        sum += currentEnginePart.Value;
                        break;
                    }
                }
            }

            return sum;
        }
    }


    public static object SolvePartOne(object? inputFilePath)
    {
        if (inputFilePath is not string inputFilePath2)
        {
            throw new ArgumentException(null, nameof(inputFilePath));
        }

        string[] lines = File.ReadAllLines(inputFilePath2);

        Regex signRgx = GeneratedRegexPatterns.SignsRgx();

        Regex numberRgx = GeneratedRegexPatterns.NumRgx();

        EnginePartsToCheck enginePartsToCheck = new(
            current: numberRgx.Matches(lines[0]).Select(x => new EnginePartCoords(x.Index, int.Parse(x.ToString()), x.ToString().Length)).ToArray(),
            before: null,
            next: numberRgx.Matches(lines[1]).Select(x => new EnginePartCoords(x.Index, int.Parse(x.ToString()), x.ToString().Length)).ToArray());

        int sum = 0;
        for (int i = 1; i < lines.Length - 1; i++)
        {
            EnginePartCoords[] t = numberRgx.Matches(lines[i + 1]).Select(x => new EnginePartCoords(x.Index, int.Parse(x.ToString()), x.ToString().Length)).ToArray();
            enginePartsToCheck.ShiftEntries(t);
            int[] signIndeces = signRgx.Matches(lines[i]).Select(x => x.Index).ToArray();

            sum += enginePartsToCheck.CheckForMatches(signIndeces); ;
        }

        return sum;
    }


    public static object SolvePartTwo(object? inputFilePath)
    {
        if (inputFilePath is not string inputFilePath2)
        {
            throw new ArgumentException(null, nameof(inputFilePath));
        }

        string[] lines = File.ReadAllLines(inputFilePath2);

        Regex signRgx = GeneratedRegexPatterns.StarRgx();

        Regex numberRgx = GeneratedRegexPatterns.NumRgx();

        List<Part> signs = [];
        List<Part> numbers = [];

        for (int i = 0; i < lines.Length; i++)
        {
            signs.AddRange(signRgx.Matches(lines[i]).Select(x => new Part(x.Value, i, x.Index)));
            numbers.AddRange(numberRgx.Matches(lines[i]).Select(x => new Part(x.Value, i, x.Index)));
        }

        long sum = 0;
        List<int> useForRatio = [];

        foreach (Part s in signs)
        {
            useForRatio.Clear();
            foreach (Part n in numbers)
            {
                if (n.Irow < s.Irow - 1)
                {
                    continue;
                }

                if (n.Irow > s.Irow + 1)
                {
                    break;
                }

                if (s.Icol <= n.Icol + n.Text.Length
                    && n.Icol <= s.Icol + s.Text.Length)
                {
                    useForRatio.Add(n.Int);
                }
            }

            if (useForRatio.Count == 2)
            {
                sum += useForRatio[0] * useForRatio[1];
            }
        }

        return sum;
        
    }
    
}


record Part(string Text, int Irow, int Icol)
{
    public int Int => int.Parse(Text);
}



