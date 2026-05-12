using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace KyrgyzTest.OldDb.Models;

[Table("rrnk_test")]
public partial class RrnkTest
{
    [Key]
    [Column("id_rrnk_test")]
    public int IdRrnkTest { get; set; }

    [Column("vid_rrnk")]
    public int? VidRrnk { get; set; }

    [Column("id_rrnk")]
    [StringLength(50)]
    [Unicode(false)]
    public string? IdRrnk { get; set; }

    [Column("id_test")]
    public int? IdTest { get; set; }

    [Column("AVNDATE", TypeName = "datetime")]
    public DateTime? Avndate { get; set; }

    [Column("id_a_year")]
    public int? IdAYear { get; set; }

    [Column("p32")]
    [StringLength(20)]
    public string? P32 { get; set; }

    [Column("id_semester")]
    public int? IdSemester { get; set; }

    [Column("p43")]
    [StringLength(20)]
    public string? P43 { get; set; }

    [Column("id_faculty")]
    public int? IdFaculty { get; set; }

    [Column("p23-1")]
    [StringLength(20)]
    public string? P231 { get; set; }

    [Column("id_discipline")]
    public int? IdDiscipline { get; set; }

    [Column("p34")]
    [StringLength(300)]
    public string? P34 { get; set; }

    [Column("id_examination")]
    public int? IdExamination { get; set; }

    [Column("p30")]
    [StringLength(20)]
    public string? P30 { get; set; }

    [Column("id_group")]
    public int? IdGroup { get; set; }

    [Column("p20")]
    [StringLength(20)]
    public string? P20 { get; set; }

    [Column("begin_date", TypeName = "datetime")]
    public DateTime? BeginDate { get; set; }

    [Column("end_date", TypeName = "datetime")]
    public DateTime? EndDate { get; set; }

    [Column("kol_popitki")]
    public int? KolPopitki { get; set; }

    [Column("id_regulation")]
    public int? IdRegulation { get; set; }

    [Column("id_student")]
    public int? IdStudent { get; set; }

    [StringLength(20)]
    public string FormatTestType { get; set; } = null!;

    [Column("AVN_login")]
    [StringLength(200)]
    public string AvnLogin { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime EditTime { get; set; }
}
