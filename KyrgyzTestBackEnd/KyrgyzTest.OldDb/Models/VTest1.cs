using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace KyrgyzTest.OldDb.Models;

[Keyless]
public partial class VTest1
{
    [Column("id_group")]
    public int? IdGroup { get; set; }

    [Column("id_semester")]
    public int? IdSemester { get; set; }

    [Column("p43")]
    [StringLength(20)]
    public string? P43 { get; set; }

    [Column("id_student")]
    public int? IdStudent { get; set; }

    [StringLength(20)]
    public string FormatTestType { get; set; } = null!;
}
