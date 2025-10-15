namespace MunicipalForms.Models
{
    public class Issue
    {
        public string Location { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string AttachmentPath { get; set; }
    }

    public class IssueNode
    {
        public Issue Data { get; set; }
        public IssueNode Next { get; set; }
    }

    // Linked List class
    public class IssueLinkedList
    {
        private IssueNode head;

        public void Add(Issue issue)
        {
            IssueNode newNode = new IssueNode { Data = issue };
            if (head == null)
            {
                head = newNode;
            }
            else
            {
                IssueNode current = head;
                while (current.Next != null)
                    current = current.Next;

                current.Next = newNode;
            }
        }

        public IEnumerable<Issue> GetAll()
        {
            IssueNode current = head;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }
    }
}