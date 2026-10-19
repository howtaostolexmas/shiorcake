using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace DekBanshiCaptcha.Forms
{
    internal sealed class WordSearchResult
    {
        public char[,] Letters { get; set; }
        public HashSet<int> ValidCells { get; set; }
        public HashSet<int> WitwaCells { get; set; }
    }

    internal static class WordSearchBuilder
    {
        private const int Seed = 20240617;
        private const int Size = 10;
        private static readonly string[] ValidWords = { "LOVEBANSHI", "ACCOUNTING", "DEBIT", "CREDIT", "BUSINESS", "MARKETING", "STAT", "BBA" };
        private const string TrapWord = "LOVEWITWA";
        private static readonly int[,] Directions = { { -1, -1 }, { -1, 0 }, { -1, 1 }, { 0, -1 }, { 0, 1 }, { 1, -1 }, { 1, 0 }, { 1, 1 } };

        public static WordSearchResult Generate()
        {
            for (int attempt = 0; attempt < 10000; attempt++)
            {
                char[,] letters = new char[Size, Size];
                Dictionary<string, List<int>> intended = new Dictionary<string, List<int>>();
                HashSet<int> validCells = new HashSet<int>();
                HashSet<int> witwaCells = new HashSet<int>();
                List<string> ordered = new List<string>(ValidWords);
                ordered.Sort(delegate(string a, string b) { return b.Length.CompareTo(a.Length); });
                int row = 0;
                foreach (string word in ordered)
                {
                    PlaceHorizontal(letters, word, row, validCells, intended);
                    row++;
                }
                PlaceHorizontal(letters, TrapWord, row, witwaCells, intended);
                Random random = new Random(Seed + attempt);
                if (!FillRemaining(letters, intended, random))
                    continue;
                if (HasUnintendedWord(letters, intended))
                    continue;
                WordSearchResult result = new WordSearchResult { Letters = letters, ValidCells = validCells, WitwaCells = witwaCells };
#if DEBUG
                foreach (KeyValuePair<string, List<int>> placement in intended)
                {
                    for (int i = 0; i < placement.Value.Count; i++)
                    {
                        int cell = placement.Value[i];
                        Debug.Assert(letters[cell / Size, cell % Size] == placement.Key[i]);
                    }
                }
#endif
                return result;
            }
            throw new InvalidOperationException("Unable to generate a clean word-search grid.");
        }

        private static void PlaceHorizontal(char[,] letters, string word, int row, HashSet<int> cells, Dictionary<string, List<int>> intended)
        {
            List<int> coordinates = new List<int>();
            for (int column = 0; column < word.Length; column++)
            {
                letters[row, column] = word[column];
                int index = row * Size + column;
                cells.Add(index);
                coordinates.Add(index);
            }
            intended.Add(word, coordinates);
        }

        private static bool FillRemaining(char[,] letters, Dictionary<string, List<int>> intended, Random random)
        {
            for (int row = 0; row < Size; row++)
                for (int column = 0; column < Size; column++)
                {
                    if (letters[row, column] != '\0')
                        continue;
                    int[] choices = new int[26];
                    for (int i = 0; i < choices.Length; i++) choices[i] = i;
                    for (int i = choices.Length - 1; i > 0; i--)
                    {
                        int swap = random.Next(i + 1);
                        int value = choices[i]; choices[i] = choices[swap]; choices[swap] = value;
                    }
                    bool placed = false;
                    for (int i = 0; i < choices.Length; i++)
                    {
                        letters[row, column] = (char)('A' + choices[i]);
                        if (!HasUnintendedWord(letters, intended))
                        {
                            placed = true;
                            break;
                        }
                    }
                    if (!placed)
                    {
                        letters[row, column] = '\0';
                        return false;
                    }
                }
            return true;
        }

        private static bool HasUnintendedWord(char[,] letters, Dictionary<string, List<int>> intended)
        {
            foreach (KeyValuePair<string, List<int>> wordEntry in intended)
            {
                string word = wordEntry.Key;
                for (int row = 0; row < Size; row++)
                    for (int column = 0; column < Size; column++)
                        for (int direction = 0; direction < Directions.GetLength(0); direction++)
                        {
                            int endRow = row + Directions[direction, 0] * (word.Length - 1);
                            int endColumn = column + Directions[direction, 1] * (word.Length - 1);
                            if (endRow < 0 || endRow >= Size || endColumn < 0 || endColumn >= Size)
                                continue;
                            bool forward = true;
                            bool reverse = true;
                            List<int> coordinates = new List<int>();
                            for (int i = 0; i < word.Length; i++)
                            {
                                int r = row + Directions[direction, 0] * i;
                                int c = column + Directions[direction, 1] * i;
                                char letter = letters[r, c];
                                if (letter == '\0') { forward = false; reverse = false; }
                                else
                                {
                                    if (letter != word[i]) forward = false;
                                    if (letter != word[word.Length - 1 - i]) reverse = false;
                                }
                                coordinates.Add(r * Size + c);
                            }
                            if (forward || reverse)
                            {
                                coordinates.Sort();
                                List<int> expected = new List<int>(wordEntry.Value);
                                expected.Sort();
                                if (!SameCells(coordinates, expected))
                                    return true;
                            }
                        }
            }
            return false;
        }

        private static bool SameCells(List<int> first, List<int> second)
        {
            if (first.Count != second.Count) return false;
            for (int i = 0; i < first.Count; i++)
                if (first[i] != second[i]) return false;
            return true;
        }
    }
}
