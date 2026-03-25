using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace KyrgyzTest.OldDb.Models;

[Table("TestResultBallInfo")]
public partial class TestResultBallInfo
{
    [Key]
    public int TestResultBallInfoId { get; set; }

    public int TestResultId { get; set; }

    [Column("Cate ry")]
    [StringLength(100)]
    public string CateRy { get; set; } = null!;

    public int CountQuest { get; set; }

    public int CountRightAnswers { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    public decimal Ball { get; set; }

    [ForeignKey("TestResultId")]
    [InverseProperty("TestResultBallInfos")]
    public virtual TestResult TestResult { get; set; } = null!;
}
