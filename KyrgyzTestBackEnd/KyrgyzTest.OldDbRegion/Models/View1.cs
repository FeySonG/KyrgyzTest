using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace KyrgyzTest.OldDbRegion.Models;

[Keyless]
public partial class View1
{
    [Column("id")]
    public int Id { get; set; }

    [Column("name_group")]
    [StringLength(50)]
    public string NameGroup { get; set; } = null!;

    [Column("name_discipline")]
    [StringLength(300)]
    public string NameDiscipline { get; set; } = null!;

    [Column(TypeName = "decimal(6, 2)")]
    public decimal? Ball { get; set; }

    [Column("sh_Regulation")]
    [StringLength(50)]
    public string? ShRegulation { get; set; }

    [Column("name_student")]
    [StringLength(200)]
    public string NameStudent { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime GenerateDate { get; set; }
}
