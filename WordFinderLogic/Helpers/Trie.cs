using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordFinder.Logic.Helpers
{
    public class Trie
    {
        private TrieNode root;

        public Trie()
        {
            root = new TrieNode();
        }

        public void Insert(string word)
        {
            var node = root;
            foreach (char c in word)
            {
                if (!node.Children.ContainsKey(c))
                {
                    node.Children.Add(c, new TrieNode());
                }
                node = node.Children[c];
            }
            node.Words.Add(word);
        }

        public List<string> Search(string str)
        {
            var node = root;
            foreach (char c in str)
            {
                if (!node.Children.ContainsKey(c))
                {
                    return new List<string>();
                }
                node = node.Children[c];
            }
            return node.Words;
        }
    }
}
