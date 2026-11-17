using KyrgyzTest.Application.Abstractions;
using KyrgyzTest.Application.Contracts.OldDbs.Students;
using KyrgyzTest.Application.Extensions.Result;

namespace KyrgyzTest.Application.Models.KTOldDbs.Students.Update;

public record UpdateStudentCommand(UpdateStudentArgs Args) : ICommand<Result<StudentDto>>;