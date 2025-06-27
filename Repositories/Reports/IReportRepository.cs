using ForumWebsite.Models;

namespace ForumWebsite.Repositories.Reports
{
    public interface IReportRepository
    {
        Task<ICollection<Report>> GetAllReport();
        Task<Report> GetReportById(int reportId);
        Task DeleteReport(int reportId);
        Task CreateReport(Report report);
        Task UpdateReport(Report report, int reportId);
    }
}