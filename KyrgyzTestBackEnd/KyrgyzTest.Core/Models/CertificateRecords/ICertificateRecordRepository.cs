using KyrgyzTest.Core.Abstractions;

namespace KyrgyzTest.Core.Models.CertificateRecords;

public interface ICertificateRecordRepository : IRepository<CertificateRecord>
{
    Task<CertificateRecord> SearchByNameAsync(string name);
    
    Task<List<CertificateRecord>> GetAllByCertNumberAsync(string certificateNumber);
    
    Task<List<CertificateRecord>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
    
    Task AddRangeAsync(List<CertificateRecord> entities);
    
}
