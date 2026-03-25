using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace KyrgyzTest.OldDb.Models;

[Table("DatabaseVersion")]
public partial class DatabaseVersion
{
    [Key]
    [StringLength(20)]
    public string DatabaseType { get; set; } = null!;

    [StringLength(50)]
    public string Version { get; set; } = null!;
}
