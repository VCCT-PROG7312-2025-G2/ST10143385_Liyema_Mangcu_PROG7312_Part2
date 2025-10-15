namespace MunicipalForms.Models
{
    public class Report
    {
        public string Location { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string? AttachmentPath { get; set; }
        public DateTime SubmittedAt { get; set; } = DateTime.Now;
    }
}
