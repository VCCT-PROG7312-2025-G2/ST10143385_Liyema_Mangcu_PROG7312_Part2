namespace MunicipalForms.Models
{
    public class ServiceRequest
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public RequestStatus Status { get; set; } = RequestStatus.Pending;
        public string Location { get; set; }
        public DateTime SubmittedAt { get; set; } = DateTime.Now;
        public int Priority { get; set; }
    }

    public enum RequestStatus
    {
        Pending,
        InProgress,
        Completed
    }
}
