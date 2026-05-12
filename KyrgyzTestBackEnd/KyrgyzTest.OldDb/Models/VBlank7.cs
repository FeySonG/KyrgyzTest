using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace KyrgyzTest.OldDb.Models;

[Keyless]
public partial class VBlank7
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

    [Column("id_group")]
    public int? IdGroup { get; set; }

    [Column("id_test")]
    public int? IdTest { get; set; }

    [Column("test")]
    public string? Test { get; set; }

    [Column("id_regulation")]
    public int? IdRegulation { get; set; }

    [Column("Count_of_questions")]
    public int? CountOfQuestions { get; set; }
}
