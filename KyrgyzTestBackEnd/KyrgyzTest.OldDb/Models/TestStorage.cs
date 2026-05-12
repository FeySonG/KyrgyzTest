using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace KyrgyzTest.OldDb.Models;

[Table("TestStorage")]
public partial class TestStorage
{
    [Key]
    public int TestId { get; set; }

    public string Discipline { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int CountOfQuestions { get; set; }

    public byte[] TestData { get; set; } = null!;

    public bool AllowAnonymousTesting { get; set; }

    public bool ForPrint { get; set; }

    [StringLength(20)]
    public string? Language { get; set; }

    [InverseProperty("TestId2Navigation")]
    public virtual ICollection<PGroup> PGroupTestId2Navigations { get; set; } = new List<PGroup>();

    [InverseProperty("Test")]
    public virtual ICollection<PGroup> PGroupTests { get; set; } = new List<PGroup>();

    [InverseProperty("Test")]
    public virtual ICollection<PTestSetting> PTestSettings { get; set; } = new List<PTestSetting>();

    [InverseProperty("Test")]
    public virtual ICollection<Statistic> Statistics { get; set; } = new List<Statistic>();
}
