using System;
using System.Collections.Generic;
using MunicipalForms.Models;

namespace MunicipalForms.Data
{
    internal class AVLNode
    {
        public int Key;
        public ServiceRequest Value;
        public AVLNode Left, Right;
        public int Height;
        public AVLNode(int k, ServiceRequest v) { Key = k; Value = v; Height = 1; }
    }

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

            // LL
            if (balance > 1 && key < node.Left!.Key) return RightRotate(node);
            // RR
            if (balance < -1 && key > node.Right!.Key) return LeftRotate(node);
            // LR
            if (balance > 1 && key > node.Left!.Key) { node.Left = LeftRotate(node.Left); return RightRotate(node); }
            // RL
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
