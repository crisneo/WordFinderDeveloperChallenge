using System.Linq;
using System.Xml.Linq;
using System;
using System.Reflection.Metadata.Ecma335;
using System.Collections.Generic;
using WordFinder.Logic.Helpers;

namespace WordFinder.Logic
{
    public class WordFinder : IWordFinder
    {
        private IEnumerable<string> _matrix;
        private AlgorithmType _algorithm;

        public WordFinder(IEnumerable<string> matrix)
        {
            _matrix = matrix;
            _algorithm = AlgorithmType.Trie;
        }

        public IEnumerable<string> Find(IEnumerable<string> wordstream)
        {
            //ignore duplicates from stream
            wordstream = wordstream.Distinct().ToList();
            ValidateWordMatrix(_matrix, wordstream);
            var charArrays = _matrix.Select(x => x.ToCharArray());
            var mat = charArrays.ToArray();
            var dictRes = new Dictionary<string, int>();

            switch (_algorithm)
            {
                case AlgorithmType.Trie:
                    dictRes = SearchWordsUsingTrie(mat, wordstream.ToList());
                    break;
                case AlgorithmType.Linear:
                    dictRes = SearchWordsUsingLinearAlg(mat, wordstream.ToList());
                    break;
                default: break;
            }

            var items = from pair in dictRes orderby pair.Value descending select pair;
            return items.Select(x => $"{x.Key} ({x.Value})").Take(10).ToList();
        }

        /// <summary>
        /// search for the words using the simplest linear algorithm to find words horizontally or vertically
        /// it is enough for a small to medium list of words in a small to medium character matrix
        /// </summary>
        /// <param name="mat"></param>
        /// <param name="words"></param>
        /// <returns></returns>
        private Dictionary<string, int> SearchWordsUsingLinearAlg(char[][] mat, List<string> words)
        {
            var foundWords = new Dictionary<string, int>();
            foreach (var word in words)
            {
                FindWordOccurences(mat, word, foundWords);
            }
            return foundWords;
        }

        private void FindWordOccurences(char[][] mat, string word, Dictionary<string, int> findingsDict)
        {
            // search word from left to right
            for (int i = 0; i < mat.Length; i++)
            {
                string row = "";
                for (int j = 0; j < mat[i].Length; j++)
                {
                    row += mat[i][j];
                }
                if (row.Contains(word))
                {
                    //save finding
                    if (findingsDict.ContainsKey(word))
                    {
                        findingsDict[word] = findingsDict[word] + 1;
                    }
                    else
                    {
                        findingsDict.Add(word, 1);
                    }

                }
            }

            // search word from top to bottom
            for (int j = 0; j < mat.Length; j++)
            {
                string column = "";
                for (int i = 0; i < mat[j].Length; i++)
                {
                    column += mat[i][j];
                }
                if (column.Contains(word))
                {

                    if (findingsDict.ContainsKey(word))
                    {
                        findingsDict[word] = findingsDict[word] + 1;
                    }
                    else
                    {
                        findingsDict.Add(word, 1);
                    }
                }
            }

        }

        /// <summary>
        /// search word using the compressed trie algorithm that is optimal for a big numbers of words to search
        /// </summary>
        /// <param name="board"></param>
        /// <param name="words"></param>
        /// <returns></returns>
        private Dictionary<string, int> SearchWordsUsingTrie(char[][] board, List<string> words)
        {
            Trie trie = new Trie();
            foreach (string word in words)
            {
                trie.Insert(word);
            }

            var foundWords = new Dictionary<string, int>();

            // horizontal search
            for (int i = 0; i < board.Length; i++)
            {
                string row = "";
                for (int j = 0; j < board[i].Length; j++)
                {
                    row += board[i][j].ToString();
                }
                var subStrings = GetSubstrings(row);
                foreach (var sub in subStrings)
                {
                    var res = trie.Search(sub);
                    foreach (var word in res)
                    {
                        if (foundWords.ContainsKey(word))
                        {
                            foundWords[word] += 1;
                        }
                        else
                        {
                            foundWords.Add(word, 1);
                        }
                    }
                }
            }

            // vertical search
            for (int j = 0; j < board.Length; j++)
            {
                string col = "";
                for (int i = 0; i < board[j].Length; i++)
                {
                    col += board[i][j].ToString();
                }
                var subStrings = GetSubstrings(col);
                foreach (var sub in subStrings)
                {
                    var res = trie.Search(sub);
                    foreach (var word in res)
                    {
                        if (foundWords.ContainsKey(word))
                        {
                            foundWords[word] += 1;
                        }
                        else
                        {
                            foundWords.Add(word, 1);
                        }
                    }
                }
            }

            return foundWords;
        }

        private static List<string> GetSubstrings(string str)
        {
            List<string> subs = new List<string>();
            for (int i = 0; i < str.Length; i++)
            {
                for (int j = i; j < str.Length; j++)
                {
                    subs.Add(str.Substring(i, j - i + 1));
                }
            }
            return subs;
        }

        private void ValidateWordMatrix(IEnumerable<string> mat, IEnumerable<string> words)
        {
            if (mat.Count() == 0)
            {
                throw new ArgumentException("Empty matrix");
            }
            if (mat.DistinctBy(x => x.Length).Count() > 1)
            {
                throw new ArgumentException("All strings in the matrix should have the same size");
            }
            if (mat.Any(x => x.Length > 64))
            {
                throw new ArgumentException("the max length of words in stream in 64 chars");
            }
            if (mat.Count() != mat.First().Length)
            {
                throw new ArgumentException("please specify a square matrix");
            }
            if (words.Any(x => x.Length > mat.First().Length))
            {
                throw new ArgumentException("words to find should be at least the same size than the matrix");
            }
        }
    }

    public interface IWordFinder
    {
        IEnumerable<string> Find(IEnumerable<string> wordstream);

    }

    public enum AlgorithmType
    {
        Trie,
        Linear
    }
}
