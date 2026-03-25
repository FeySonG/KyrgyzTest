using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace KyrgyzTest.OldDb.Models;

public partial class UserAnswer
{
    [Key]
    public int UserAnswerId { get; set; }

    public int TestResultId { get; set; }

    public byte[] Quest { get; set; } = null!;

    public byte[] Answer { get; set; } = null!;

    [StringLength(20)]
    public string AnswerType { get; set; } = null!;

    [Column(TypeName = "numeric(18, 2)")]
    public decimal? Ball { get; set; }

    [Column("AVN_date", TypeName = "datetime")]
    public DateTime? AvnDate { get; set; }

    public int NumQuest { get; set; }
}
