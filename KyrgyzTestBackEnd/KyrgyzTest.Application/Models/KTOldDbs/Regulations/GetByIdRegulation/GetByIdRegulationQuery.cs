using KyrgyzTest.Application.Abstractions;
using KyrgyzTest.Application.Contracts.OldDbs.Regulations;
using KyrgyzTest.Application.Extensions.Result;

namespace KyrgyzTest.Application.Models.KTOldDbs.Regulations.GetByIdRegulation;

public record GetByIdRegulationQuery(int Id) : IQuery<Result<RegulationDto?>>;
