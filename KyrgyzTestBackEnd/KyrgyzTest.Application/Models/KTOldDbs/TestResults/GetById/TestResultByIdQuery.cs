using KyrgyzTest.Application.Abstractions;
using KyrgyzTest.Application.Contracts.OldDbs.TestResults;
using KyrgyzTest.Application.Extensions.Result;

namespace KyrgyzTest.Application.Models.KTOldDbs.TestResults.GetById;

public record TestResultByIdQuery(int Id) : IQuery<Result<TestResultDto>>;
