using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace KyrgyzTest.OldDb.Models;

[Keyless]
public partial class VBlank4
{
    [Column("id_faculty")]
    public int? IdFaculty { get; set; }

    [Column("id_semester")]
    public int? IdSemester { get; set; }

    [Column("id_teacher")]
    public int? IdTeacher { get; set; }

    [Column("id_discipline")]
    public int? IdDiscipline { get; set; }

    [Column("p34")]
    [StringLength(300)]
    public string? P34 { get; set; }

    [StringLength(20)]
    public string FormatTestType { get; set; } = null!;
}
