using KyrgyzTest.Application.Abstractions.OldDbAbstractions.Students;
using KyrgyzTest.Application.Contracts.OldDbs.Students;
using KyrgyzTest.OldDb.Models;
using Microsoft.EntityFrameworkCore;

namespace KyrgyzTest.OldDb.Repositories.Students;

public class StudentRepository(LegacyDbContext dbContext) : IStudentRepository
{
    public async Task<StudentDto?> GetStudentByIdAsync(int studentId)
    {
        var response =  await dbContext.Students.FirstOrDefaultAsync(e => e.StudentId == studentId);
        if (response is null) return null;
        return MapResponse(response);
    }

    public async Task<StudentDto?> GetStudentByPin(long studentPin)
    {
        var response =  await dbContext.Students.FirstOrDefaultAsync(e => e.Pin == studentPin);
        if (response is null) return null;
        return MapResponse(response);
    }

    public async Task CreateStudentAsync(StudentDto studentDto)
    {
        var student = new Student
        {
            StudentId = studentDto.StudentId,
            Pin = studentDto.Pin,
            CertificateNumber = studentDto.CertificateNumber,
            FirstName = studentDto.FirstName,
            LastName = studentDto.LastName,
            MiddleName = studentDto.MiddleName,
        };

        await dbContext.Students.AddAsync(student);
        await dbContext.SaveChangesAsync();
    }

    public async Task<bool> UpdateStudentAsync(StudentDto studentDto)
    {
        var student = await dbContext.Students
            .FirstOrDefaultAsync(s => s.StudentId == studentDto.StudentId);

        if (student == null)
            return false;

        student.Pin = studentDto.Pin;
        student.CertificateNumber = studentDto.CertificateNumber;
        student.FirstName = studentDto.FirstName;
        student.LastName = studentDto.LastName;
        student.MiddleName = studentDto.MiddleName;

        await dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<StudentDto?> DeleteStudentAsync(int studentId)
    {
        var student = await dbContext.Students
            .FirstOrDefaultAsync(e => e.StudentId == studentId);

        if (student is null)
            return null;

        dbContext.Students.Remove(student);
        await dbContext.SaveChangesAsync();

        return MapResponse(student);
    }

    public async Task<bool> CheckStudentUnique(StudentDto student) 
        => await dbContext.Students.AnyAsync(u => u.StudentId == student.StudentId);
    

    private StudentDto MapResponse(Student student)
    {
        return new StudentDto
        {
            Id = student.Id,
            StudentId = student.StudentId,
            Pin = student.Pin,
            CertificateNumber = student.CertificateNumber,
            FirstName = student.FirstName,
            LastName = student.LastName,
            MiddleName = student.MiddleName,
        };
    }
}