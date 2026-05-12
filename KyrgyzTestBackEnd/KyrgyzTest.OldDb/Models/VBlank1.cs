using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace KyrgyzTest.OldDb.Models;

[Keyless]
public partial class VBlank1
{
    [Column("p23-1")]
    [StringLength(20)]
    public string? P231 { get; set; }

    [Column("id_faculty")]
    public int? IdFaculty { get; set; }

    [StringLength(20)]
    public string FormatTestType { get; set; } = null!;
}
