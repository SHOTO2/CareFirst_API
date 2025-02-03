using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CareFirst.Model;

[Table("BookDoctor")]
public partial class BookDoctor
{
    [Key]
    [Column("BookDoctorID")]
    public int BookDoctorId { get; set; }

    [StringLength(255)]
    public string Name { get; set; } = null!;

    [StringLength(255)]
    public string PhoneNumber { get; set; } = null!;

    public DateTime TimeBook { get; set; }

    [Column("Doctor_id")]
    public int DoctorId { get; set; }

    [Column("User_id")]
    public int UserId { get; set; }

    [ForeignKey("DoctorId")]
    [InverseProperty("BookDoctors")]
    public virtual Doctor Doctor { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("BookDoctors")]
    public virtual UserTable User { get; set; } = null!;
}
