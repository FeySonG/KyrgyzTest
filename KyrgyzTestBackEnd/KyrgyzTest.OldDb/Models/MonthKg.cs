using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace KyrgyzTest.OldDb.Models;

[Keyless]
[Table("month_kg")]
public partial class MonthKg
{
    [Column("id_month")]
    public int IdMonth { get; set; }

    [Column("RU")]
    [StringLength(200)]
    public string? Ru { get; set; }

    [Column("KG")]
    [StringLength(200)]
    public string? Kg { get; set; }
}
