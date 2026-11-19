using KyrgyzTest.Application.Abstractions;
using KyrgyzTest.Application.Contracts.OldDbs.Students;
using KyrgyzTest.Application.Contracts.OldDbs.TestResults;
using KyrgyzTest.Application.Extensions.Result;

namespace KyrgyzTest.Application.Models.KTOldDbs.TestResults.GetByStudentId;

public record GetStudentResultsQuery(GetByStudentIdArgs Args) : IQuery<Result<StudentResultResponse>>;
