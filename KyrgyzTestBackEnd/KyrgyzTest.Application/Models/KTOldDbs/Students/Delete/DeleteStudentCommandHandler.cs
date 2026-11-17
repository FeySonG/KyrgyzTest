using KyrgyzTest.Application.Abstractions;
using KyrgyzTest.Application.Abstractions.OldDbAbstractions.Students;
using KyrgyzTest.Application.Abstractions.OldDbRegionAbstractions.Students;
using KyrgyzTest.Application.Contracts.Mailysearchs;
using KyrgyzTest.Application.Contracts.OldDbs.Students;
using KyrgyzTest.Application.Extensions.Result;
using MediatR;

namespace KyrgyzTest.Application.Models.KTOldDbs.Students.Delete;

public class DeleteStudentCommandHandler(
        IStudentRepository studentRepository,
        IStudentRegionRepository studentRegionRepository
    ) : ICommandHandler<DeleteStudentCommand, Result<StudentDto>>
{
    public async Task<Result<StudentDto>> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
    {
        var source = request.Args.Source;

        StudentDto? response;
        
        if (source == SearchSources.OldDb)
        {
            response = await studentRepository.DeleteStudentAsync(request.Args.Id);
            
            if (response is null)
                    return new Error(KtOldDbErrorCode.StudentNotFound, "Student not found");
            
        } 
        else if(source == SearchSources.Region)
        {
            response = await studentRegionRepository.DeleteStudentAsync(request.Args.Id);
            
            if (response is null)
                return new Error(KtOldDbErrorCode.StudentNotFound, "Student not found");
        } 
        else
        {
            return new Error(
                KtOldDbErrorCode.InvalidSource,
                $"Unknown source: {source}");
        }

        return response;
    }
}