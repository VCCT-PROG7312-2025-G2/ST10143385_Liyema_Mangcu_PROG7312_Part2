namespace MunicipalForms.Data;

public class BinarySearchTree<T> where T : IComparable<T>
{
    private class Node
    {
        public T Value;
        public Node Left, Right;
        public Node(T v) => Value = v;
    }

    private Node root;
    public void Insert(T value) { /* standard BST insert */ }
    public T Search(T value)
    {
        Node current = root;
        while (current != null)
        {
            int cmp = value.CompareTo(current.Value);
            if (cmp == 0)
                return current.Value;
            else if (cmp < 0)
                current = current.Left;
            else
                current = current.Right;
        }
        // Not found: throw or return default
        return default!;
    }
}