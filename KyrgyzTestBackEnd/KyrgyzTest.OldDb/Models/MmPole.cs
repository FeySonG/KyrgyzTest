using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace KyrgyzTest.OldDb.Models;

[Table("mm_pole")]
public partial class MmPole
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("pole")]
    [StringLength(50)]
    public string? Pole { get; set; }
}
