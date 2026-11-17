using KyrgyzTest.Application.Abstractions;
using KyrgyzTest.Application.Contracts.OldDbs.Students;
using KyrgyzTest.Application.Extensions.Result;

namespace KyrgyzTest.Application.Models.KTOldDbs.Students.GetById;

public record GetByIdStudentQuery(GetByStudentIdArgs Args) : IQuery<Result<StudentDto>>;