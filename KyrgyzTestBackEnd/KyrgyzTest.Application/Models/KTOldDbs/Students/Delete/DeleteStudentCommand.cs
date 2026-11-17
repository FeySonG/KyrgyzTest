using KyrgyzTest.Application.Abstractions;
using KyrgyzTest.Application.Contracts.OldDbs.Students;
using KyrgyzTest.Application.Extensions.Result;

namespace KyrgyzTest.Application.Models.KTOldDbs.Students.Delete;

public record DeleteStudentCommand(DeleteStudentArgs Args) : ICommand<Result<StudentDto>>;
