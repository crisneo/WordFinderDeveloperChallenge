// See https://aka.ms/new-console-template for more information



using WordFinder.Logic;

// sample chart matrix (in order to check more cases you can review unit tests
var matrix = new List<string>() { "windc", "cgwio", "ohill", "lqnsd", "dsnow" };
var words = new List<string>() {"cold", "wind", "snow","chill", "cold" };
IWordFinder wFinder = new WordFinder.Logic.WordFinder(matrix);
var res  = wFinder.Find(words);
foreach(var t in res)
{
    Console.WriteLine(t);
}


