using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace KyrgyzTest.OldDb.Models;

[Keyless]
public partial class VUserAnswersUpdate
{
    [Column("id")]
    public int Id { get; set; }

    [Column("ball", TypeName = "numeric(18, 2)")]
    public decimal? Ball { get; set; }
}
