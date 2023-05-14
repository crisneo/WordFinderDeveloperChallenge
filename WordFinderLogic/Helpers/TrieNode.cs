using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordFinder.Logic.Helpers
{
    public class TrieNode
    {
        public Dictionary<char, TrieNode> Children;
        public List<string> Words;

        public TrieNode()
        {
            Children = new Dictionary<char, TrieNode>();
            Words = new List<string>();
        }
    }
}
