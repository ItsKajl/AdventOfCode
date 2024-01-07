
using System.Text.RegularExpressions;
using Utility;

namespace Aoc2023;


[AocSolver(2023, 4, "Scratchcards")]
public class Day4 : ISolver
{
    public object PartOne(string input)
    {
        string[] inputs = File.ReadAllLines(input);

        int points = 0;        
        foreach (string card in inputs)
        {
            points += evaluateCardPoints(card);
        }

        return points;
    }


    private int evaluateCardPoints(string card)
    {        
        int amountOfWinners = Day4.winnersPerCard(card);
        return amountOfWinners > 0
            ? (int)Math.Pow(2, amountOfWinners - 1)
            : 0;
    }


    public object PartTwo(string input)
    {        
        string[] cards = File.ReadAllLines(input);
        Dictionary<int, int> copiesAcquiredPerCard = [];

        int points = 0;
        for (int cardNumber = cards.Length - 1; cardNumber >= 0; cardNumber--)
        {
            points += aggregatedPointsForCard(copiesAcquiredPerCard, cards, cardNumber);
        }

        return points + cards.Length;
    }


    private int aggregatedPointsForCard(Dictionary<int, int> copiesAcquiredPerCard, string[] cards, int cardNumber)
    {
        if (copiesAcquiredPerCard.TryGetValue(cardNumber, out int value))
        {
            return value;
        }

        int amountOfWinners = winnersPerCard(cards[cardNumber]);
        int amountOfWinners2 = amountOfWinners;
        for (int i = 1; i <= amountOfWinners; i++)
        {
            amountOfWinners2 += aggregatedPointsForCard(copiesAcquiredPerCard, cards, cardNumber + i);
        }

        copiesAcquiredPerCard[cardNumber] = amountOfWinners2;

        return amountOfWinners2;
    }


    private static int winnersPerCard(string card)
    {
        int cardNumberSeperatorIndex = card.IndexOf(':');
        int partSeperatorIndex = card.IndexOf('|');

        string winningPart = card.Substring(cardNumberSeperatorIndex + 1, partSeperatorIndex - cardNumberSeperatorIndex);

        Regex numRgx = Rgx.GeneratedRegexPatterns.NumRgx();

        Dictionary<string, int> winningNumbers = [];

        foreach (Match match in numRgx.Matches(winningPart).Cast<Match>())
        {
            winningNumbers[match.Value] = 0;
        }

        foreach (Match match in numRgx.Matches(card, partSeperatorIndex + 1).Cast<Match>())
        {
            if (!winningNumbers.ContainsKey(match.Value))
            {
                continue;
            }

            winningNumbers[match.Value]++;
        }

        return winningNumbers.Values.Count(x => x > 0);
    }
}
