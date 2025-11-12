using System;
using System.Collections.Generic;
using MunicipalForms.Models;

namespace MunicipalForms.Data
{
    // The node class used by the AVL tree
    internal class AVLNode
    {
        public int Key;                         // a unique key used for ordering, for example ServiceRequest ID
        public ServiceRequest Value;            // Stored data
        public AVLNode Left, Right;             // Child nodes
        public int Height;                      // The height of the node which is used for balancing
        public AVLNode(int k, ServiceRequest v) { Key = k; Value = v; Height = 1; }
    }

    // This is a self-balancing AVL Tree used for efficient insert or search by ID
    public class AVLTree
    {
        private AVLNode? root;

        public void Insert(int key, ServiceRequest value) => root = InsertInternal(root, key, value);

        private AVLNode InsertInternal(AVLNode? node, int key, ServiceRequest value)
        {
            if (node == null) return new AVLNode(key, value);

            if (key < node.Key) node.Left = InsertInternal(node.Left, key, value);
            else if (key > node.Key) node.Right = InsertInternal(node.Right, key, value);
            else { node.Value = value; return node; } // replace

            node.Height = 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));
            int balance = GetBalance(node);


            // perform rotations to maintain AVL balance
            if (balance > 1 && key < node.Left!.Key) return RightRotate(node);
            if (balance < -1 && key > node.Right!.Key) return LeftRotate(node);
            if (balance > 1 && key > node.Left!.Key) { node.Left = LeftRotate(node.Left); return RightRotate(node); }
            if (balance < -1 && key < node.Right!.Key) { node.Right = RightRotate(node.Right); return LeftRotate(node); }

            return node;
        }

        private AVLNode RightRotate(AVLNode y)
        {
            var x = y.Left!;
            var T2 = x.Right;
            x.Right = y;
            y.Left = T2;
            y.Height = Math.Max(GetHeight(y.Left), GetHeight(y.Right)) + 1;
            x.Height = Math.Max(GetHeight(x.Left), GetHeight(x.Right)) + 1;
            return x;
        }

        private AVLNode LeftRotate(AVLNode x)
        {
            var y = x.Right!;
            var T2 = y.Left;
            y.Left = x;
            x.Right = T2;
            x.Height = Math.Max(GetHeight(x.Left), GetHeight(x.Right)) + 1;
            y.Height = Math.Max(GetHeight(y.Left), GetHeight(y.Right)) + 1;
            return y;
        }

        private int GetHeight(AVLNode? n) => n?.Height ?? 0;
        private int GetBalance(AVLNode? n) => n == null ? 0 : GetHeight(n.Left) - GetHeight(n.Right);

        public ServiceRequest? Get(int key)
        {
            var node = root;
            while (node != null)
            {
                if (key == node.Key) return node.Value;
                node = key < node.Key ? node.Left : node.Right;
            }
            return null;
        }

        // return all the requests in sorted order
        public List<ServiceRequest> InOrder()
        {
            var list = new List<ServiceRequest>();
            InOrderRec(root, list);
            return list;
        }

        private void InOrderRec(AVLNode? node, List<ServiceRequest> list)
        {
            if (node == null) return;
            InOrderRec(node.Left, list);
            list.Add(node.Value);
            InOrderRec(node.Right, list);
        }
    }
}
// Balancing and rotation logic adapted from GeeksforGeeks (2024).
// Source: https://www.geeksforgeeks.org/avl-tree-set-1-insertion/