using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace KyrgyzTest.OldDb.Models;

[Keyless]
public partial class VStatistik
{
    public int TestId { get; set; }

    public string Description { get; set; } = null!;

    [StringLength(50)]
    public string Category { get; set; } = null!;

    [Column("num_vopr")]
    public int NumVopr { get; set; }

    [Column("kol_prav")]
    public int KolPrav { get; set; }

    [Column("kol_nepr")]
    public int KolNepr { get; set; }
}
