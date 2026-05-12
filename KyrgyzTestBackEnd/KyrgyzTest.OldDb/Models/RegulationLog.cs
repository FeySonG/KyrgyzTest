using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace KyrgyzTest.OldDb.Models;

[Table("RegulationLog")]
public partial class RegulationLog
{
    [Key]
    public int RegulationLogId { get; set; }

    [Column("AVN_login")]
    [StringLength(200)]
    public string AvnLogin { get; set; } = null!;

    public int RegulationId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime LogTime { get; set; }

    [StringLength(200)]
    public string LogType { get; set; } = null!;
}
