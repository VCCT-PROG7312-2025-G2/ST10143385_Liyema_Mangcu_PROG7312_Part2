using System;

namespace MunicipalForms.DataStructures
{
    public enum NodeColor
    {
        Red,
        Black
    }

    public class RedBlackNode<T> where T : IComparable<T>
    {
        public T Data { get; set; }
        public NodeColor Color { get; set; }
        public RedBlackNode<T> Left { get; set; }
        public RedBlackNode<T> Right { get; set; }
        public RedBlackNode<T> Parent { get; set; }

        public RedBlackNode(T data)
        {
            Data = data;
            Color = NodeColor.Red;
            Left = null;
            Right = null;
            Parent = null;
        }
    }

    public class RedBlackTree<T> where T : IComparable<T>
    {
        private RedBlackNode<T> root;
        private RedBlackNode<T> nil; // Sentinel node

        public RedBlackTree()
        {
            nil = new RedBlackNode<T>(default(T));
            nil.Color = NodeColor.Black;
            root = nil;
        }

        // Left Rotate
        private void LeftRotate(RedBlackNode<T> x)
        {
            RedBlackNode<T> y = x.Right;
            x.Right = y.Left;
            if (y.Left != nil)
                y.Left.Parent = x;
            y.Parent = x.Parent;
            if (x.Parent == nil)
                root = y;
            else if (x == x.Parent.Left)
                x.Parent.Left = y;
            else
                x.Parent.Right = y;
            y.Left = x;
            x.Parent = y;
        }

        // Right Rotate
        private void RightRotate(RedBlackNode<T> x)
        {
            RedBlackNode<T> y = x.Left;
            x.Left = y.Right;
            if (y.Right != nil)
                y.Right.Parent = x;
            y.Parent = x.Parent;
            if (x.Parent == nil)
                root = y;
            else if (x == x.Parent.Right)
                x.Parent.Right = y;
            else
                x.Parent.Left = y;
            y.Right = x;
            x.Parent = y;
        }

        // Insert Fixup
        private void InsertFixup(RedBlackNode<T> z)
        {
            while (z.Parent.Color == NodeColor.Red)
            {
                if (z.Parent == z.Parent.Parent.Left)
                {
                    RedBlackNode<T> y = z.Parent.Parent.Right;
                    if (y.Color == NodeColor.Red)
                    {
                        z.Parent.Color = NodeColor.Black;
                        y.Color = NodeColor.Black;
                        z.Parent.Parent.Color = NodeColor.Red;
                        z = z.Parent.Parent;
                    }
                    else
                    {
                        if (z == z.Parent.Right)
                        {
                            z = z.Parent;
                            LeftRotate(z);
                        }
                        z.Parent.Color = NodeColor.Black;
                        z.Parent.Parent.Color = NodeColor.Red;
                        RightRotate(z.Parent.Parent);
                    }
                }
                else
                {
                    RedBlackNode<T> y = z.Parent.Parent.Left;
                    if (y.Color == NodeColor.Red)
                    {
                        z.Parent.Color = NodeColor.Black;
                        y.Color = NodeColor.Black;
                        z.Parent.Parent.Color = NodeColor.Red;
                        z = z.Parent.Parent;
                    }
                    else
                    {
                        if (z == z.Parent.Left)
                        {
                            z = z.Parent;
                            RightRotate(z);
                        }
                        z.Parent.Color = NodeColor.Black;
                        z.Parent.Parent.Color = NodeColor.Red;
                        LeftRotate(z.Parent.Parent);
                    }
                }
            }
            root.Color = NodeColor.Black;
        }

        // Insert
        public void Insert(T data)
        {
            RedBlackNode<T> z = new RedBlackNode<T>(data);
            RedBlackNode<T> y = nil;
            RedBlackNode<T> x = root;
            while (x != nil)
            {
                y = x;
                if (z.Data.CompareTo(x.Data) < 0)
                    x = x.Left;
                else
                    x = x.Right;
            }
            z.Parent = y;
            if (y == nil)
                root = z;
            else if (z.Data.CompareTo(y.Data) < 0)
                y.Left = z;
            else
                y.Right = z;
            z.Left = nil;
            z.Right = nil;
            z.Color = NodeColor.Red;
            InsertFixup(z);
        }

        // Transplant (helper for delete)
        private void Transplant(RedBlackNode<T> u, RedBlackNode<T> v)
        {
            if (u.Parent == nil)
                root = v;
            else if (u == u.Parent.Left)
                u.Parent.Left = v;
            else
                u.Parent.Right = v;
            v.Parent = u.Parent;
        }

        // Delete Fixup
        private void DeleteFixup(RedBlackNode<T> x)
        {
            while (x != root && x.Color == NodeColor.Black)
            {
                if (x == x.Parent.Left)
                {
                    RedBlackNode<T> w = x.Parent.Right;
                    if (w.Color == NodeColor.Red)
                    {
                        w.Color = NodeColor.Black;
                        x.Parent.Color = NodeColor.Red;
                        LeftRotate(x.Parent);
                        w = x.Parent.Right;
                    }
                    if (w.Left.Color == NodeColor.Black && w.Right.Color == NodeColor.Black)
                    {
                        w.Color = NodeColor.Red;
                        x = x.Parent;
                    }
                    else
                    {
                        if (w.Right.Color == NodeColor.Black)
                        {
                            w.Left.Color = NodeColor.Black;
                            w.Color = NodeColor.Red;
                            RightRotate(w);
                            w = x.Parent.Right;
                        }
                        w.Color = x.Parent.Color;
                        x.Parent.Color = NodeColor.Black;
                        w.Right.Color = NodeColor.Black;
                        LeftRotate(x.Parent);
                        x = root;
                    }
                }
                else
                {
                    RedBlackNode<T> w = x.Parent.Left;
                    if (w.Color == NodeColor.Red)
                    {
                        w.Color = NodeColor.Black;
                        x.Parent.Color = NodeColor.Red;
                        RightRotate(x.Parent);
                        w = x.Parent.Left;
                    }
                    if (w.Right.Color == NodeColor.Black && w.Left.Color == NodeColor.Black)
                    {
                        w.Color = NodeColor.Red;
                        x = x.Parent;
                    }
                    else
                    {
                        if (w.Left.Color == NodeColor.Black)
                        {
                            w.Right.Color = NodeColor.Black;
                            w.Color = NodeColor.Red;
                            LeftRotate(w);
                            w = x.Parent.Left;
                        }
                        w.Color = x.Parent.Color;
                        x.Parent.Color = NodeColor.Black;
                        w.Left.Color = NodeColor.Black;
                        RightRotate(x.Parent);
                        x = root;
                    }
                }
            }
            x.Color = NodeColor.Black;
        }

        // Delete
        public void Delete(T data)
        {
            RedBlackNode<T> z = Search(root, data);
            if (z == nil) return;

            RedBlackNode<T> y = z;
            RedBlackNode<T> x;
            NodeColor yOriginalColor = y.Color;
            if (z.Left == nil)
            {
                x = z.Right;
                Transplant(z, z.Right);
            }
            else if (z.Right == nil)
            {
                x = z.Left;
                Transplant(z, z.Left);
            }
            else
            {
                y = Minimum(z.Right);
                yOriginalColor = y.Color;
                x = y.Right;
                if (y.Parent == z)
                    x.Parent = y;
                else
                {
                    Transplant(y, y.Right);
                    y.Right = z.Right;
                    y.Right.Parent = y;
                }
                Transplant(z, y);
                y.Left = z.Left;
                y.Left.Parent = y;
                y.Color = z.Color;
            }
            if (yOriginalColor == NodeColor.Black)
                DeleteFixup(x);
        }

        // Helper: Search
        private RedBlackNode<T> Search(RedBlackNode<T> node, T data)
        {
            while (node != nil && data.CompareTo(node.Data) != 0)
            {
                if (data.CompareTo(node.Data) < 0)
                    node = node.Left;
                else
                    node = node.Right;
            }
            return node;
        }

        // Helper: Minimum
        private RedBlackNode<T> Minimum(RedBlackNode<T> node)
        {
            while (node.Left != nil)
                node = node.Left;
            return node;
        }

        // Example: InOrder Traversal (for testing)
        public void InOrderTraversal(Action<T> action)
        {
            InOrderTraversal(root, action);
        }

        private void InOrderTraversal(RedBlackNode<T> node, Action<T> action)
        {
            if (node != nil)
            {
                InOrderTraversal(node.Left, action);
                action(node.Data);
                InOrderTraversal(node.Right, action);
            }
        }
    }
}