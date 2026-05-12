using KyrgyzTest.Application.Abstractions.OldDbAbstractions.Regulations;
using KyrgyzTest.Application.Contracts.OldDbs.Regulations;
using KyrgyzTest.Application.Contracts.OldDbs.TestResults;
using KyrgyzTest.OldDb.Models;
using Microsoft.EntityFrameworkCore;

namespace KyrgyzTest.OldDb.Repositories.Regulations;

public class RegulationRepository(LegacyDbContext dbContext) : IRegulationRepository
{
    public async Task<RegulationDto?> GetByIdAsync(int id)
    {
        var response =  await dbContext.Regulations.FirstOrDefaultAsync(e => e.Id == id);
        if (response is null) return null;
        
        return MapResponseDto(response);
    }

    public async Task<List<RegulationDto>> GetAll()
    {
        var response =  await dbContext.Regulations.ToListAsync();
        if (response.Count == 0) return new List<RegulationDto>();
        
        return MapDtos(response);
    }

    private List<RegulationDto> MapDtos(List<Regulation> regulations)
    {
            var listResult = new List<RegulationDto>();
            
            if (regulations.Count == 0) return listResult;
        
            foreach (var item in regulations)
            {
                listResult.Add(MapResponseDto(item));
            }
        
            return listResult;
        
    } 
    private RegulationDto MapResponseDto(Regulation regulation)
    {
        return new RegulationDto
        {
            Id = regulation.Id,
            Name = regulation.Name,
            ShRegulation = regulation.ShRegulation,
        };
    }
}