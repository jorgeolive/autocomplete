using Olive.Autocomplete.Core.TrieImpl;
using Xunit;

namespace Olive.Autocomplete.Tests
{
    public class TrieTests
    {
        [Fact]
        public void SearchingCompleteWord_OnlyReturnsIt()
        {
            var trie = new Trie();
            trie.AddWord("gato");
            trie.AddWord("gatos");
            trie.AddWord("gata");

            var suggestions = trie.GetSuggestionsFor("gato");

            Assert.Contains(("gato", 0), suggestions);
            Assert.DoesNotContain(("gatos", 0), suggestions);
        }

        [Fact]
        public void CanGetTrieFirstLevelSuggestion()
        {
            var trie = new Trie();
            trie.AddWord("gato");
            trie.AddWord("gata");

            var suggestions = trie.GetSuggestionsFor("gat");

            Assert.Contains(("gata", 0), suggestions);
            Assert.Contains(("gato", 0), suggestions);
        }

        [Fact]
        public void CanGetTrieSecondLevelSuggestion()
        {
            var trie = new Trie();
            trie.AddWord("gatos");
            trie.AddWord("gatas");

            var suggestions = trie.GetSuggestionsFor("gat");

            Assert.Contains(("gatas", 0), suggestions);
            Assert.Contains(("gatos", 0), suggestions);
        }

        [Fact]
        public void DoesntSuggest_UnmatchingWorkds()
        {
            var trie = new Trie();
            trie.AddWord("gato");
            trie.AddWord("gata");

            var suggestions = trie.GetSuggestionsFor("perr");

            Assert.Empty(suggestions);
        }

        [Fact]
        public void CanSearch_ByNumbers()
        {
            var trie = new Trie();
            trie.AddWord("2");
            trie.AddWord("1080");

            var suggestions = trie.GetSuggestionsFor("2");

            Assert.Contains(("2", 0), suggestions);
        }

        [Fact]
        public void CanGetDeepSuggestion()
        {
            var trie = new Trie();

            trie.AddWord("gato");
            trie.AddWord("cartero");
            trie.AddWord("carteros");
            trie.AddWord("carromato");

            var suggestions = trie.GetSuggestionsFor("car");

            Assert.DoesNotContain(("gato", 0), suggestions);
            Assert.Contains(("cartero", 0), suggestions);
            Assert.Contains(("carteros", 0), suggestions);
            Assert.Contains(("carromato", 0), suggestions);
        }
    }
}