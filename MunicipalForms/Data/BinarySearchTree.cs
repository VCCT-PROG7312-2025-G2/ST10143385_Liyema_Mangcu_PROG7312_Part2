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
    // BST insert which can be exteneded for ordering
    public void Insert(T value) { }
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
        return default!;
    }
}

//  https://www.tutorialspoint.com/data_structures_algorithms/binary_search_tree.htm