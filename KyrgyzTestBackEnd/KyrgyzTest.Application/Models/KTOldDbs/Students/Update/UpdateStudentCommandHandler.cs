using KyrgyzTest.Application.Abstractions;
using KyrgyzTest.Application.Abstractions.OldDbAbstractions.Students;
using KyrgyzTest.Application.Abstractions.OldDbRegionAbstractions.Students;
using KyrgyzTest.Application.Contracts.Mailysearchs;
using KyrgyzTest.Application.Contracts.OldDbs.Students;
using KyrgyzTest.Application.Extensions.Result;

namespace KyrgyzTest.Application.Models.KTOldDbs.Students.Update;

public class UpdateStudentCommandHandler(
    IStudentRepository studentRepository,
    IStudentRegionRepository studentRegionRepository
    ) : ICommandHandler<UpdateStudentCommand, Result<StudentDto>>
{
    public async Task<Result<StudentDto>> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
    {
        var source = request.Args.Source;
        
        var studentId = request.Args.StudentId;
        
        var studentDto = MapStudent(request.Args);

        if (source == SearchSources.Region)
        {
             var response = await studentRegionRepository.UpdateStudentAsync(studentDto);
            if(!response) 
                return new Error(KtOldDbErrorCode.StudentNotFound, $"Student id:{studentId} not found");
        } 
        else if(source == SearchSources.OldDb)
        {
            var response = await studentRepository.UpdateStudentAsync(studentDto);
            if(!response) 
                return new Error(KtOldDbErrorCode.StudentNotFound, $"Student id:{studentId} not found");
        }
        else
        {
            return new Error(
                KtOldDbErrorCode.InvalidSource,
                $"Unknown source: {source}");
        }
        
        return studentDto;
        
    }

    private StudentDto MapStudent(UpdateStudentArgs newArgs)
    {
        var studentDto = new StudentDto
        {
            StudentId = newArgs.StudentId,
            Pin = newArgs.Pin,
            CertificateNumber = newArgs.CertificateNumber,
            FirstName = newArgs.FirstName,
            LastName = newArgs.LastName,
            MiddleName = newArgs.MiddleName,
        };
       
        return studentDto;
    }
}