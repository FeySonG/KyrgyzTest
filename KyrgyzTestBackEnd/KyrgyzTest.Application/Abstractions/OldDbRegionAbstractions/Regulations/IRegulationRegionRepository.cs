using KyrgyzTest.Application.Contracts.OldDbs.Regulations;

namespace KyrgyzTest.Application.Abstractions.OldDbRegionAbstractions.Regulations;

public interface IRegulationRegionRepository
{
    Task<RegulationDto?> GetByIdAsync(int id);
    public Task<List<RegulationDto>> GetAll();
}