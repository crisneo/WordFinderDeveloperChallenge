using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordFinder.Logic;

namespace WordFinder.Tests
{
    public class UnitTests
    {
        private IEnumerable<string> _matrix;
        private IWordFinder _wFinder;

        [SetUp]
        public void Setup()
        {
            _matrix = new List<string>() { "windc", "cgwio", "ohill", "lqnsd", "dsnow" };
            _wFinder = new WordFinder.Logic.WordFinder(_matrix);
        }

        [Test]
        public void TestWordsFound()
        {
            var wordstream = new List<string>() { "cold", "wind", "snow", "chill" };
            var foundWords = _wFinder.Find(wordstream);
            Assert.IsTrue(foundWords.Contains("cold (2)"));
            Assert.IsTrue(foundWords.Contains("wind (1)"));
            Assert.IsTrue(foundWords.Contains("snow (1)"));

        }


        [Test]
        public void TestDuplicatesInWordStream()
        {
            var wordstream = new List<string>() { "cold", "wind", "snow", "chill", "cold", "wind" };
            var foundWords = _wFinder.Find(wordstream);
            Assert.IsTrue(foundWords.Contains("cold (2)"));
            Assert.IsTrue(foundWords.Contains("wind (1)"));
            Assert.IsTrue(foundWords.Contains("snow (1)"));
        }

        [Test]
        public void TestWordLongerThanMatrix()
        {
            var wordstream = new List<string>() { "colddsasdasdas", "wind", "snow", "chill", "cold", "wind" };
            Assert.Throws<ArgumentException>(() => _wFinder.Find(wordstream));
        }

        [Test]
        public void TestNoWordsFound()
        {
            var wordstream = new List<string>() { "seasx", "seasy" };
            var foundWords = _wFinder.Find(wordstream);
            Assert.IsTrue(foundWords.Count() == 0);
        }

        [Test]
        public void TestEmptyMatrix()
        {
            var emptyMatrix = new List<string>();
            var wordstream = new List<string>() { "seasonX", "seasonY" };
            var wFinder2 = new WordFinder.Logic.WordFinder(emptyMatrix);
            Assert.Throws<ArgumentException>(() => wFinder2.Find(wordstream));
        }

        [Test]
        public void TestMatrixWithDifferentSizes()
        {
            var wrongMatrix = new List<string>() { "a", "bb", "ccc", "dddd" };
            var wordstream = new List<string>() { "seasonX", "seasonY" };
            var wFinder2 = new WordFinder.Logic.WordFinder(wrongMatrix);
            Assert.Throws<ArgumentException>(() => wFinder2.Find(wordstream));
        }

        [Test]
        public void TestNoSquareMatrix()
        {
            var wrongMatrix = new List<string>() { "windc", "cgwio", "ohill" };
            var wordstream = new List<string>() { "cold", "wind", "snow", "chill" };
            var wFinder2 = new WordFinder.Logic.WordFinder(wrongMatrix);
            Assert.Throws<ArgumentException>(() => wFinder2.Find(wordstream));
        }

    }
}
