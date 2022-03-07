using System.Text;

namespace Olive.Autocomplete.Core.TernaryTreeImpl
{
    public class TernaryTree
    {
        private Node? _masterNode;

        public void AddWord(string word)
        {
            if (word.Contains(" "))
                return;

            var characters = word.ToCharArray();
            Node? current = _masterNode;

            
            for (int i = 0; i < characters.Length; i++)
            {
                if (current == null)
                {
                    _masterNode = new Node(characters[i]);
                    current = _masterNode;

                    if (i == characters.Length - 1)
                        current!.IsCompleteWord = true;

                    continue;
                }

                bool transversing = true;

                while (transversing)
                {
                    if (current!.IsLeaf() && i!= 0 && characters[i - 1] == current.Character)
                    {
                        current.SetCenter(new Node(characters[i]));
                        current = current.Center;
                        transversing = false;
                        continue;
                    }

                    if ((int)characters[i] < (int)current.Character!)
                    {
                        if (current.Left is null)
                        {
                            current.SetLeft(new Node(characters[i]));
                            transversing = false;
                        }

                        current = current.Left;
                    }
                    else

                    if ((int)characters[i] > (int)current.Character!)
                    {
                        if (current.Right is null)
                        {
                            current.SetRight(new Node(characters[i]));
                            transversing = false;
                        }

                        current = current.Right;
                    }
                    else
                    if ((int)characters[i] == (int)current.Character!)
                    {
                        if(current.Center is not null) 
                        {
                            current = current.Center;
                        }

                        transversing = false;
                    }
                }

                if (i == characters.Length - 1)
                    current!.IsCompleteWord = true;
            }
        }

        public IEnumerable<(string, int)> GetSuggestionsFor(string prefix)
        {
            Node? prefixNode = GetPrefixNode(prefix);

            var result = new List<(string, int)>();

            if (prefixNode == null)
                return result;

            if (prefixNode.IsCompleteWord)
                return new List<(string, int)> { (prefix, prefixNode.Weight) };

            return DiscoverFullWords(prefixNode, prefix);
        }

        //}
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

            foreach (var child in current.GetChildNodes())
            {
                ExploreChild(
                    child.IsParentsCenter() ? currentWord + child.Character : currentWord.Remove(currentWord.Length - 1) +child.Character,
                    child,
                    weightedSuggestions);
            }
        }

        private Node? GetPrefixNode(string prefix)
        {
            var result = new List<(string, int)>();

            var characters = prefix.ToCharArray();
            Node? current = _masterNode;

            for (int i = 0; i < characters.Length; i++)
            {
                bool transversing = true;

                while (transversing)
                {
                    if (current is null)
                        return null;

                    if ((int)characters[i] < (int)current.Character!)
                    {
                        current = current.Left;
                        continue;
                    }

                    if ((int)characters[i] > (int)current.Character!)
                    {
                        current = current.Right;
                        continue;
                    }

                    if ((int)characters[i] == (int)current.Character!)
                    {
                        transversing = false;

                        if(i == characters.Length - 1)
                        {
                            return current;
                        }

                        current = current.Center;
                    }
                }
            }

            return current.Parent;
        }
    }
}
