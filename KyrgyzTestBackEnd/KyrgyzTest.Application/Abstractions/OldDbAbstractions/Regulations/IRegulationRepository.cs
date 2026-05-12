using KyrgyzTest.Application.Contracts.OldDbs.Regulations;

namespace KyrgyzTest.Application.Abstractions.OldDbAbstractions.Regulations;

public interface IRegulationRepository
{
    Task<RegulationDto?> GetByIdAsync(int id);
    public Task<List<RegulationDto>> GetAll();
}