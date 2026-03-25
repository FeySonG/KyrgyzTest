using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace KyrgyzTest.OldDb.Models;

public partial class Statistic
{
    [Key]
    public int Id { get; set; }

    public int TestId { get; set; }

    public int NumQuest { get; set; }

    public int CountRightAnswers { get; set; }

    public int CountWrongAnswers { get; set; }

    [StringLength(50)]
    public string Category { get; set; } = null!;

    public int NumQuestInCategory { get; set; }

    [ForeignKey("TestId")]
    [InverseProperty("Statistics")]
    public virtual TestStorage Test { get; set; } = null!;
}
