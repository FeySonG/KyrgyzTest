using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace KyrgyzTest.OldDb.Models;

public partial class BlankTestVariant
{
    [Key]
    public int VariantId { get; set; }

    public int TestId { get; set; }

    public byte[] Questions { get; set; } = null!;

    public bool IsProcessed { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime GenTime { get; set; }

    public int? StudentId { get; set; }

    [StringLength(50)]
    public string? StudentFio { get; set; }

    public int? FacultetId { get; set; }

    [StringLength(50)]
    public string? FacultetName { get; set; }

    public int GroupId { get; set; }

    [StringLength(50)]
    public string? GroupName { get; set; }

    public double? Ball { get; set; }

    public byte[]? Ansvers { get; set; }

    public byte[]? Blank { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ScanDate { get; set; }
}
