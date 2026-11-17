using KyrgyzTest.Application.Abstractions;
using KyrgyzTest.Application.Contracts.OldDbs.Students;
using KyrgyzTest.Application.Extensions.Result;

namespace KyrgyzTest.Application.Models.KTOldDbs.Students.Create;

public record CreateStudentCommand(CreateStudentArgs Args) : ICommand<Result<StudentDto>>;