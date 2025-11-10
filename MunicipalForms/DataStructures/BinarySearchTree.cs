namespace MunicipalForms.DataStructures
{
    public class BinarySearchTree
    {
        public TreeNode? Root { get; private set; }

        public void Insert(int requestId, string description, string status)
        {
            Root = InsertRec(Root, requestId, description, status);
        }

        private TreeNode InsertRec(TreeNode? root, int id, string description, string status)
        {
            if (root == null)
                return new TreeNode(id, description, status);

            if (id < root.RequestId)
                root.Left = InsertRec(root.Left, id, description, status);
            else if (id > root.RequestId)
                root.Right = InsertRec(root.Right, id, description, status);

            return root;
        }

        public TreeNode? Search(int id)
        {
            return SearchRec(Root, id);
        }

        private TreeNode? SearchRec(TreeNode? root, int id)
        {
            if (root == null || root.RequestId == id)
                return root;

            if (id < root.RequestId)
                return SearchRec(root.Left, id);
            else
                return SearchRec(root.Right, id);
        }

        public List<TreeNode> InOrder()
        {
            List<TreeNode> nodes = new();
            InOrderRec(Root, nodes);
            return nodes;
        }

        private void InOrderRec(TreeNode? root, List<TreeNode> nodes)
        {
            if (root != null)
            {
                InOrderRec(root.Left, nodes);
                nodes.Add(root);
                InOrderRec(root.Right, nodes);
            }
        }
    }
}
