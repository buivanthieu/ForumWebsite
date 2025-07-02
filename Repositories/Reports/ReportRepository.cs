using ForumWebsite.Datas;
using ForumWebsite.Models;
using Microsoft.EntityFrameworkCore;

namespace ForumWebsite.Repositories.Reports
{
    public class ReportRepository : IReportRepository
    {
        private readonly ApplicationDbContext _context;
        public ReportRepository(ApplicationDbContext context) 
        {
            _context = context;       
        }
        public async Task CreateReport(Report report)
        {
            await _context.Reports.AddAsync(report);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteReport(int reportId)
        {
            var report = await _context.Reports.FindAsync(reportId)
                ?? throw new KeyNotFoundException("key is null");
            _context.Reports.Remove(report);
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<Report>> GetAllReport()
        {
            return await _context.Reports.ToListAsync();
        }

        public async Task<Report> GetReportById(int reportId)
        {
            var report = await _context.Reports.FindAsync(reportId)
                ?? throw new KeyNotFoundException("key is null");
            return report;
        }

        public async Task UpdateReport(Report report, int reportId)
        {
            var existingReport = await _context.Reports.FindAsync(reportId)
                ?? throw new KeyNotFoundException("key is null");
            _context.Entry(existingReport).CurrentValues.SetValues(report);
            await _context.SaveChangesAsync();
        }
    }
}
