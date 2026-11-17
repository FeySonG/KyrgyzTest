using KyrgyzTest.Application.Abstractions;
using KyrgyzTest.Application.Abstractions.OldDbAbstractions.Regulations;
using KyrgyzTest.Application.Abstractions.OldDbAbstractions.Students;
using KyrgyzTest.Application.Abstractions.OldDbAbstractions.TestResults;
using KyrgyzTest.Application.Abstractions.OldDbRegionAbstractions.Regulations;
using KyrgyzTest.Application.Abstractions.OldDbRegionAbstractions.Students;
using KyrgyzTest.Application.Abstractions.OldDbRegionAbstractions.TestResults;
using KyrgyzTest.Application.Contracts.Mailysearchs;
using KyrgyzTest.Application.Contracts.OldDbs.Regulations;
using KyrgyzTest.Application.Contracts.OldDbs.Students;
using KyrgyzTest.Application.Contracts.OldDbs.TestResults;
using KyrgyzTest.Application.Extensions.Result;

namespace KyrgyzTest.Application.Models.KTOldDbs.Students.Create;

public class CreateStudentCommandHandler(
    ITestResultRepository testResultRepository,
    ITestResultRegionRepository testResultRegionRepository,
    IRegulationRepository regulationRepository,
    IRegulationRegionRepository regulationRegionRepository,
    IStudentRepository studentRepository,
    IStudentRegionRepository studentRegionRepository
    ) : ICommandHandler<CreateStudentCommand, Result<StudentDto>>
{
    public async Task<Result<StudentDto>> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
    {
        var source = request.Args.Source;
        
        var student = new StudentDto
        {
            StudentId = request.Args.StudentId,
            Pin = request.Args.Pin,
            CertificateNumber = request.Args.CertificateNumber,
            FirstName = request.Args.FirstName,
            LastName = request.Args.LastName,
            MiddleName = request.Args.MiddleName,
        };
        
        if(source == SearchSources.Region)
        {
            await studentRegionRepository.CheckStudentUnique(student);
            await studentRegionRepository.CreateStudentAsync(student);
        } 
        else if (source == SearchSources.OldDb)
        {
            await studentRepository.CheckStudentUnique(student);
            await studentRepository.CreateStudentAsync(student);
        }
        else
        {
            return new Error(
                KtOldDbErrorCode.InvalidSource,
                $"Unknown source: {source}");
        }
        
        return student;
    }

    private List<TestResultDto> AddRegulation(List<RegulationDto> regulations, List<TestResultDto> results)
    {
        var regulationDict = regulations
            .Where(r => r.Id != 0)
            .ToDictionary(r => r.Id, r => r.ShRegulation);

        foreach (var test in results)
        {
            if (regulationDict.TryGetValue(test.IdRegulation, out var shReg))
            {
                test.ShRegulation = shReg;
            }
        }
        
        return results;
    }
}