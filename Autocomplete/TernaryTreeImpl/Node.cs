namespace Olive.Autocomplete.Core.TernaryTreeImpl
{
    class Node
    {
        public Node(char? character)
        {
            Character = character;
        }
        public char? Character { get; } = null;
        public int Weight { get; set; } = 0;
        public bool IsCompleteWord { get; set; } = false;
        public Node? Left { get; set; }
        public Node? Right { get; set; }
        public Node? Center { get; set; }

        public Node? Parent { get; set; }

        public bool IsLeaf()
        {
            return Left == null && Right == null && Center == null;   
        }

        internal IEnumerable<Node> GetChildNodes()
        {
            if (Center is not null)
                yield return Center;

            if (Left is not null)
                yield return Left;

            if (Right is not null)
                yield return Right;
        }

        public bool IsParentsCenter() => Parent!.Center == this;

        internal void SetCenter(Node node)
        {
            Center = node;
            node.Parent = this;
        }

        internal void SetLeft(Node node)
        {
            Left = node;
            node.Parent = this;
        }
        internal void SetRight(Node node)
        {
            Right = node;
            node.Parent = this;
        }
    }
}
