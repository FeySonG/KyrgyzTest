using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace KyrgyzTest.OldDb.Models;

[Keyless]
public partial class VBlank2
{
    [Column("id_faculty")]
    public int? IdFaculty { get; set; }

    [Column("id_semester")]
    public int? IdSemester { get; set; }

    [Column("p43")]
    [StringLength(20)]
    public string? P43 { get; set; }

    [StringLength(20)]
    public string FormatTestType { get; set; } = null!;
}
