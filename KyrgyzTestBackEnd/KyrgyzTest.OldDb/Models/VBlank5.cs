using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace KyrgyzTest.OldDb.Models;

[Keyless]
public partial class VBlank5
{
    [Column("id_faculty")]
    public int? IdFaculty { get; set; }

    [Column("id_semester")]
    public int? IdSemester { get; set; }

    [Column("id_teacher")]
    public int? IdTeacher { get; set; }

    [Column("id_discipline")]
    public int? IdDiscipline { get; set; }

    [Column("id_examination")]
    public int? IdExamination { get; set; }

    [Column("p30")]
    [StringLength(20)]
    public string? P30 { get; set; }

    [StringLength(20)]
    public string FormatTestType { get; set; } = null!;
}
