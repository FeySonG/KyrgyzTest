using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace KyrgyzTest.OldDb.Models;

[Keyless]
public partial class OtchetBlank
{
    [Column("id_a_year")]
    public int IdAYear { get; set; }

    [Column("p32")]
    [StringLength(10)]
    [Unicode(false)]
    public string P32 { get; set; } = null!;

    [Column("id_faculty")]
    public int IdFaculty { get; set; }

    [StringLength(50)]
    public string? FacultetName { get; set; }

    [Column("p23-2")]
    [StringLength(100)]
    [Unicode(false)]
    public string P232 { get; set; } = null!;

    [Column("id_w_s")]
    public int IdWS { get; set; }

    [Column("ws_sort")]
    public short? WsSort { get; set; }

    [Column("p42")]
    [StringLength(24)]
    [Unicode(false)]
    public string P42 { get; set; } = null!;

    [Column("id_semester")]
    public int IdSemester { get; set; }

    [Column("semestr")]
    [StringLength(30)]
    [Unicode(false)]
    public string Semestr { get; set; } = null!;

    [Column("id_rate")]
    public int IdRate { get; set; }

    [Column("p22")]
    [StringLength(50)]
    [Unicode(false)]
    public string P22 { get; set; } = null!;

    [Column("id_student")]
    public int IdStudent { get; set; }

    [StringLength(50)]
    public string? StudentFio { get; set; }

    [Column("id_group")]
    public int IdGroup { get; set; }

    [StringLength(50)]
    public string? GroupName { get; set; }

    public int TestId { get; set; }

    public string Discipline { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int VariantId { get; set; }

    public bool IsProcessed { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime GenTime { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ScanDate { get; set; }

    public double? Ball { get; set; }
}
