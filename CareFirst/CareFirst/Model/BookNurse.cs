using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CareFirst.Model;

[Table("BookNurse")]
public partial class BookNurse
{
    [Key]
    [Column("BookNurseID")]
    public int BookNurseId { get; set; }

    [StringLength(255)]
    public string Name { get; set; } = null!;

    [StringLength(255)]
    public string PhoneNumber { get; set; } = null!;

    [Column("Nurse_id")]
    public int NurseId { get; set; }

    [Column("User_id")]
    public int UserId { get; set; }

    public DateTime TimeBook { get; set; }

    [ForeignKey("NurseId")]
    [InverseProperty("BookNurses")]
    public virtual Nurse Nurse { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("BookNurses")]
    public virtual UserTable User { get; set; } = null!;
}
