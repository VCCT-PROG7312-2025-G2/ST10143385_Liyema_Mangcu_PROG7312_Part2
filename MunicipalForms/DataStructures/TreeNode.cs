namespace MunicipalForms.DataStructures
{
    public class TreeNode
    {
        public int RequestId { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public TreeNode? Left { get; set; }
        public TreeNode? Right { get; set; }

        public TreeNode(int requestId, string description, string status)
        {
            RequestId = requestId;
            Description = description;
            Status = status;
        }
    }
}
