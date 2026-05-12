using KyrgyzTest.Application.Abstractions;
using KyrgyzTest.Application.Abstractions.OldDbAbstractions.Regulations;
using KyrgyzTest.Application.Contracts.OldDbs.Regulations;
using KyrgyzTest.Application.Extensions.Result;

namespace KyrgyzTest.Application.Models.KTOldDbs.Regulations.GetByIdRegulation;

public class GetByIdRegulationQueryHandler(IRegulationRepository regulationRepository) : IQueryHandler<GetByIdRegulationQuery, Result<RegulationDto?>>
{
    public async Task<Result<RegulationDto?>> Handle(GetByIdRegulationQuery request, CancellationToken cancellationToken)
    {
        var testResult = await regulationRepository.GetByIdAsync(request.Id);
        if (testResult is null) 
        {
            return new Error(KtOldDbErrorCode.RegulationNotFound, $"Operation failed regulation id: {request.Id} not found");
        }
        
        return testResult;
    }
}