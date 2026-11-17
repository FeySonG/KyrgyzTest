using KyrgyzTest.Application.Contracts.OldDbs.Students;

namespace KyrgyzTest.Application.Abstractions.OldDbAbstractions.Students;

public interface IStudentRepository
{
    Task<StudentDto?> GetStudentByIdAsync(int studentId);

    Task<StudentDto?> GetStudentByPin(long studentPin);
    
    Task CreateStudentAsync(StudentDto studentDto);
    Task<bool> UpdateStudentAsync(StudentDto student);
    Task<StudentDto?> DeleteStudentAsync(int studentId);
    
    Task<bool> CheckStudentUnique(StudentDto student);
}