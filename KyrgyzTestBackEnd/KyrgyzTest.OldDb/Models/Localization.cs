using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace KyrgyzTest.OldDb.Models;

[Table("Localization")]
public partial class Localization
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string ApplicationName { get; set; } = null!;

    [StringLength(100)]
    public string TextKey { get; set; } = null!;

    public string TextRu { get; set; } = null!;

    public string TextKg { get; set; } = null!;

    public string TextEn { get; set; } = null!;
}
