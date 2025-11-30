namespace ECommerceAPI.Services;

public interface IReportService
{
    public void GenerateProductReport(Stream stream);
}