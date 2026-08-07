using KyrgyzTest.Core.Models.CertificateRecords;
using KyrgyzTest.DAL.Services;
using Microsoft.EntityFrameworkCore;

namespace KyrgyzTest.DAL.Models.CertificateRecords;

public class CertificateRecordRepository(AppDbContext dbContext) : Repository<CertificateRecord>(dbContext), ICertificateRecordRepository
{
    public Task<CertificateRecord> SearchByNameAsync(string name)
    {
        
        throw new NotImplementedException();
    }

    public async Task<List<CertificateRecord>> GetAllByCertNumberAsync(string certificateNumber)
    {
        return await dbContext.CertificateRecords
            .AsNoTracking()
            .Where(x => x.CertificateNumber == certificateNumber.Trim())
            .ToListAsync();
    }

    public Task<List<CertificateRecord>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        return dbContext.CertificateRecords
            .AsNoTracking()
            .Where(x => x.IssueDate >= startDate && x.IssueDate < endDate)
            .OrderByDescending(x => x.IssueDate)
            .ToListAsync();
    }
    
    public async Task AddRangeAsync(List<CertificateRecord> entities)
    {
        await dbContext.CertificateRecords.AddRangeAsync(entities);
    }
    
}
