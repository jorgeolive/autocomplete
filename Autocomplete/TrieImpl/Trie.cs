namespace Olive.Autocomplete.Core.TrieImpl
{
    public class Trie
    {
        private Node _masterNode;

        public Trie()
        {
            _masterNode = new Node(null);
        }

        public IEnumerable<(string, int)> GetSuggestionsFor(string prefix)
        {
            var prefixNode = GetPrefixNode(prefix);

            var result = new List<(string, int)>();

            if (prefixNode == null)
                return result;

            if (prefixNode.IsCompleteWord)
                return new List<(string, int)> { (prefix, prefixNode.Weight) };

            return DiscoverFullWords(prefixNode, prefix);
        }

        public void AddHit(string word)
        {
            var node = GetPrefixNode(word);

            if(node is not null)
                node.Weight++;
        }

        public void AddWord(string word)
        {
            if (word.Contains(" "))
                return;

            var characters = word.ToCharArray();
            Node current = _masterNode;

            for (int i = 0; i < characters.Length; i++)
            {
                Node? child;
                current.Children.TryGetValue(characters[i], out child);

                if (child is null)
                {
                    var newNode = new Node(characters[i]);
                    current.Children.Add(characters[i], newNode);
                    current = newNode;
                }
                else
                {
                    current = child;
                }

                if (i == characters.Length - 1)
                    current.IsCompleteWord = true;
            }
        }

        private List<(string, int)> DiscoverFullWords(Node from, string prefix)
        {
            var results = new List<(string, int)>();

            ExploreChild(prefix, from, results);

            return results;
        }

        private void ExploreChild(string currentWord, Node current, List<(string, int)> weightedSuggestions)
        {
            
            if (current.IsCompleteWord)
            {
                weightedSuggestions.Add((currentWord, current.Weight));
            }

            foreach (var child in current.Children)
            {
                ExploreChild(currentWord + child.Key, child.Value, weightedSuggestions);
            }
        }

        private Node? GetPrefixNode(string word)
        {
            var characters = word.ToCharArray();
            Node? current = _masterNode;

            for (int i = 0; i < characters.Length; i++)
            {
                current?.Children.TryGetValue(characters[i], out current);

                if (current == null)
                    return null;
            }

            return current;
        }
    }
}
