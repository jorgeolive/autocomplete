namespace Olive.Autocomplete.Core.TrieImpl
{
    class Node
    {
        public Node(char? character)
        {
            Character = character;
            Children = new();
        }
        public char? Character { get; } = null;
        public int Weight { get; set; } = 0;
        public bool IsCompleteWord { get; set; } = false;
        public Dictionary<char, Node> Children { get; } = new();
    }
}
