using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CareFirst.Model;

[Table("SupperUser")]
public partial class SupperUser
{
    [Key]
    [Column("SupperUserID")]
    public int SupperUserId { get; set; }

    [StringLength(30)]
    public string? UserName { get; set; }

    [StringLength(255)]
    public string? Password { get; set; }

    [StringLength(50)]
    public string? Role { get; set; }
}
