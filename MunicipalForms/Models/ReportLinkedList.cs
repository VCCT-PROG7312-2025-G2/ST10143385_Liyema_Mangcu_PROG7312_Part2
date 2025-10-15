using MunicipalForms.Models;

public class ReportLinkedList
{
    private static LinkedList<Report> reports = new LinkedList<Report>();

    public static void AddReport(Report report)
    {
        reports.AddLast(report);
    }

    public static IEnumerable<Report> GetReports()
    {
        return reports;
    }
}

