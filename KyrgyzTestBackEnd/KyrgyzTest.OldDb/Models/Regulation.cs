using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace KyrgyzTest.OldDb.Models;

[Table("Regulation")]
public partial class Regulation
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [StringLength(50)]
    public string? Name { get; set; }

    [Column("sh_Regulation")]
    [StringLength(50)]
    public string? ShRegulation { get; set; }

    public int? CountQuestions { get; set; }

    public int? MaxBall { get; set; }

    [Column("mm_pole")]
    public int? MmPole { get; set; }

    public int TestTime { get; set; }

    public string BallTable { get; set; } = null!;

    [Column("id_f_e")]
    public int IdFE { get; set; }

    public int CountAttempt { get; set; }

    public int ComplexityCountQuestions1 { get; set; }

    public int ComplexityCountQuestions2 { get; set; }

    public int ComplexityCountQuestions3 { get; set; }

    public int ComplexityCountQuestions4 { get; set; }

    public int ComplexityCountQuestions5 { get; set; }

    [StringLength(50)]
    public string? RegulationType { get; set; }

    [StringLength(500)]
    public string? BlockNames { get; set; }

    [StringLength(20)]
    public string FormatTestType { get; set; } = null!;

    [StringLength(100)]
    public string? AdvancedCountQuestions { get; set; }

    public byte[]? Parameters { get; set; }

    [InverseProperty("RegulationId2Navigation")]
    public virtual ICollection<PGroup> PGroupRegulationId2Navigations { get; set; } = new List<PGroup>();

    [InverseProperty("RegulationId3Navigation")]
    public virtual ICollection<PGroup> PGroupRegulationId3Navigations { get; set; } = new List<PGroup>();

    [InverseProperty("Regulation")]
    public virtual ICollection<PGroup> PGroupRegulations { get; set; } = new List<PGroup>();

    [InverseProperty("Regulation")]
    public virtual ICollection<PTestSetting> PTestSettings { get; set; } = new List<PTestSetting>();
}
