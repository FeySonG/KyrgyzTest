using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace KyrgyzTest.OldDb.Models;

[Table("author_test")]
public partial class AuthorTest
{
    [Key]
    [Column("id_author_test")]
    public int IdAuthorTest { get; set; }

    [Column("id_test")]
    public int? IdTest { get; set; }

    [Column("id_teacher")]
    public int? IdTeacher { get; set; }

    [Column("t_fio")]
    [StringLength(200)]
    [Unicode(false)]
    public string? TFio { get; set; }

    [StringLength(20)]
    public string FormatTestType { get; set; } = null!;
}
