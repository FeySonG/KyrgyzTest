using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace KyrgyzTest.OldDb.Models;

[Table("TestResult")]
public partial class TestResult
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("id_test")]
    public int IdTest { get; set; }

    [Column("id_facultet")]
    public int IdFacultet { get; set; }

    [Column("name_facultet")]
    [StringLength(50)]
    public string NameFacultet { get; set; } = null!;

    [Column("id_semestr")]
    public int IdSemestr { get; set; }

    [Column("name_semestr")]
    [StringLength(20)]
    public string NameSemestr { get; set; } = null!;

    [Column("id_group")]
    public int IdGroup { get; set; }

    [Column("name_group")]
    [StringLength(50)]
    public string NameGroup { get; set; } = null!;

    [Column("id_student")]
    public int IdStudent { get; set; }

    [Column("name_student")]
    [StringLength(200)]
    public string NameStudent { get; set; } = null!;

    [Column("id_discipline")]
    public int IdDiscipline { get; set; }

    [Column("name_discipline")]
    [StringLength(300)]
    public string NameDiscipline { get; set; } = null!;

    [Column("id_examination")]
    public int IdExamination { get; set; }

    [Column("name_examination")]
    [StringLength(50)]
    public string NameExamination { get; set; } = null!;

    [Column("id_regulation")]
    public int IdRegulation { get; set; }

    [StringLength(10)]
    public string TestType { get; set; } = null!;

    public byte[] Questions { get; set; } = null!;

    public byte[]? Answers { get; set; }

    public int Attempt { get; set; }

    [StringLength(20)]
    public string Status { get; set; } = null!;

    public int RightAnsvers { get; set; }

    [Column(TypeName = "decimal(6, 2)")]
    public decimal? Ball { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime GenerateDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DeliveryDate { get; set; }

    [StringLength(50)]
    public string? IpAddress { get; set; }

    public byte[]? BlankImage { get; set; }

    public byte[]? CheckSum { get; set; }

    [StringLength(20)]
    public string FormatTestType { get; set; } = null!;

    [Column("isHandled")]
    public bool IsHandled { get; set; }

    public int CountQuest { get; set; }

    [Column(TypeName = "numeric(18, 2)")]
    public decimal? BallForTextAnswers { get; set; }

    [Column(TypeName = "numeric(18, 2)")]
    public decimal? BallForAudioAnswers { get; set; }

    [InverseProperty("TestResult")]
    public virtual ICollection<TestResultBallInfo> TestResultBallInfos { get; set; } = new List<TestResultBallInfo>();
}
