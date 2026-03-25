using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace KyrgyzTest.OldDb.Models;

[Keyless]
public partial class VTest4Advanced
{
    [Column("id_group")]
    public int? IdGroup { get; set; }

    [Column("id_semester")]
    public int? IdSemester { get; set; }

    [Column("id_discipline")]
    public int? IdDiscipline { get; set; }

    [Column("id_examination")]
    public int? IdExamination { get; set; }

    [Column("id_teacher")]
    public int? IdTeacher { get; set; }

    [Column("t_fio")]
    [StringLength(200)]
    [Unicode(false)]
    public string? TFio { get; set; }

    [Column("id_student")]
    public int? IdStudent { get; set; }
}
