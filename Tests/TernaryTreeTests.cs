using Olive.Autocomplete.Core.TernaryTreeImpl;
using Xunit;

namespace Olive.Autocomplete.Tests
{
    public class TernaryTreeTests
    {
        [Fact]
        public void SearchingCompleteWord_OnlyReturnsIt()
        {
            var ternaryTree = new TernaryTree();
            ternaryTree.AddWord("gato");
            ternaryTree.AddWord("gatos");
            ternaryTree.AddWord("gata");

            var suggestions = ternaryTree.GetSuggestionsFor("gato");

            Assert.Contains(("gato", 0), suggestions);
            Assert.DoesNotContain(("gatos", 0), suggestions);
        }

        [Fact]
        public void CanGetternaryTreeFirstLevelSuggestion()
        {
            var ternaryTree = new TernaryTree();
            ternaryTree.AddWord("gata");
            ternaryTree.AddWord("gato");
            ternaryTree.AddWord("mago");

            var suggestions = ternaryTree.GetSuggestionsFor("gat");

            Assert.Contains(("gata", 0), suggestions);
            Assert.Contains(("gato", 0), suggestions);
        }

        [Fact]
        public void CanGetternaryTreeSecondLevelSuggestion()
        {
            var ternaryTree = new TernaryTree();
            ternaryTree.AddWord("gatos");
            ternaryTree.AddWord("gatas");

            var suggestions = ternaryTree.GetSuggestionsFor("gat");

            Assert.Contains(("gatas", 0), suggestions);
            Assert.Contains(("gatos", 0), suggestions);
        }

        [Fact]
        public void DoesntSuggest_UnmatchingWorkds()
        {
            var ternaryTree = new TernaryTree();
            ternaryTree.AddWord("gato");
            ternaryTree.AddWord("gata");

            var suggestions = ternaryTree.GetSuggestionsFor("perr");

            Assert.Empty(suggestions);
        }

        [Fact]
        public void CanSearch_SingleCharacterString()
        {
            var ternaryTree = new TernaryTree();
            ternaryTree.AddWord("2");
            ternaryTree.AddWord("1080");

            var suggestions = ternaryTree.GetSuggestionsFor("2");

            Assert.Contains(("2", 0), suggestions);
        }

        [Fact]
        public void CanSearch_ExactWord()
        {
            var ternaryTree = new TernaryTree();
            ternaryTree.AddWord("20");

            var suggestions = ternaryTree.GetSuggestionsFor("20");

            Assert.Contains(("20", 0), suggestions);
        }

        [Fact]
        public void CanGetDeepSuggestion()
        {
            var ternaryTree = new TernaryTree();

            ternaryTree.AddWord("gato");
            ternaryTree.AddWord("cartero");
            ternaryTree.AddWord("carteros");
            ternaryTree.AddWord("carromato");

            var suggestions = ternaryTree.GetSuggestionsFor("car");

            Assert.DoesNotContain(("gato", 0), suggestions);
            Assert.Contains(("cartero", 0), suggestions);
            Assert.Contains(("carteros", 0), suggestions);
            Assert.Contains(("carromato", 0), suggestions);
        }
    }
}
