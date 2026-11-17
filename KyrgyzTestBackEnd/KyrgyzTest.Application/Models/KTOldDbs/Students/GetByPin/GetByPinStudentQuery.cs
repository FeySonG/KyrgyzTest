using KyrgyzTest.Application.Abstractions;
using KyrgyzTest.Application.Contracts.OldDbs.Students;
using KyrgyzTest.Application.Extensions.Result;

namespace KyrgyzTest.Application.Models.KTOldDbs.Students.GetByPin;

public record GetByPinStudentQuery(GetStudentByPinArgs Args) : IQuery<Result<StudentDto>>;