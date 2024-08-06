namespace TylerDM.StandardLibrary.System;

public static class LevenshteinDistance
{
    #region fields
    private static readonly char[] _chars = [.. "abcdefghijklmnopqrstuvwxyz"];
    #endregion

    #region methods
    public static bool ContainsSimilarWord(string text, string word)
    {
        var words = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        return ContainsSimilarWord(words, word);
    }

    public static bool ContainsSimilarWord(IEnumerable<string> words, string word)
    {
        if (word.Length < 3) return words.Any(x => x.Contains(word, StringComparison.InvariantCultureIgnoreCase));

        return words.Any(x => GetIsSimilar(x, word));
    }

    public static bool GetIsSimilar(string value1, string value2)
    {
        var tolerance = value1.Length / 3;
        tolerance = Math.Max(1, tolerance);
        var distance = GetDistance(value1, value2);
        if (distance is null) return false;
        return distance <= tolerance;
    }

    public static int? GetDistance(string value1, string value2)
    {
        if (value1 is null) return null;
        if (value2 is null) return null;

        value1 = value1.cleanseToLower();
        value2 = value2.cleanseToLower();

        if (value1.Length == 0) return null;
        if (value2.Length == 0) return null;

        return getDistance(value1, value2);
    }
    #endregion

    #region private methods
    private static int? getDistance(string value1, string value2)
    {
        var costs = new int[value2.Length];

        // Add indexing for insertion to first row
        for (int x = 0; x < costs.Length;)
            costs[x] = ++x;

        for (int x = 0; x < value1.Length; x++)
        {
            // cost of the first index
            int cost = x;
            int additionCost = x;

            // cache value for inner loop to avoid index lookup and bonds checking, profiled this is quicker
            char value1Char = value1[x];

            for (int y = 0; y < value2.Length; y++)
            {
                int insertionCost = cost;

                cost = additionCost;

                // assigning this here reduces the array reads we do, improvement over the old version
                additionCost = costs[y];

                if (value1Char != value2[y])
                {
                    if (insertionCost < cost)
                        cost = insertionCost;

                    if (additionCost < cost)
                        cost = additionCost;

                    ++cost;
                }

                costs[y] = cost;
            }
        }

        return costs[^1];
    }

    private static string cleanseToLower(this string input)
    {
        if (getIsNeeded(input) is false) return input;

        input = input.ToLowerInvariant();

        var array = new char[input.Length];
        var count = 0;
        foreach (var c in input)
        {
            if (getIsNotWhitelisted(c)) continue;

            array[count] = c;
            count++;
        }
        return new(array, 0, count);
    }

    private static bool getIsNotWhitelisted(char c) =>
        _chars.Contains(c) is false;

    private static bool getIsNeeded(string input)
    {
        for (int i = 0; i < input.Length; i++)
            if (getIsNotWhitelisted(input[i]))
                return true;
        return false;
    }
    #endregion
}