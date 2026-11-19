using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KyrgyzTest.Application.Contracts.OldDbs.TestResults;

public class TestResultDto
{
    public int Id { get; set; }

    public int IdTest { get; set; }

    public int IdFacultet { get; set; }
    public string NameFacultet { get; set; } = null!;

    public int IdSemestr { get; set; }
    public string NameSemestr { get; set; } = null!;

    public int IdGroup { get; set; }
    public string NameGroup { get; set; } = null!;

    public int IdStudent { get; set; }
    public string NameStudent { get; set; } = null!;

    public int IdDiscipline { get; set; }
    public string NameDiscipline { get; set; } = null!;

    public int IdExamination { get; set; }
    public string NameExamination { get; set; } = null!;

    public int IdRegulation { get; set; }
    
    public string? ShRegulation { get; set; }

    public string TestType { get; set; } = null!;
    public string FormatTestType { get; set; } = null!;

    public int Attempt { get; set; }
    public int RightAnswers { get; set; }

    public decimal? Ball { get; set; }
    public decimal? BallForTextAnswers { get; set; }
    public decimal? BallForAudioAnswers { get; set; }

    public DateTime GenerateDate { get; set; }
    public DateTime? DeliveryDate { get; set; }

    public bool IsHandled { get; set; }
    public int CountQuest { get; set; }
    
    // public static TestResultDto AddRegulation() 
    // {
    //     
    // }
}

