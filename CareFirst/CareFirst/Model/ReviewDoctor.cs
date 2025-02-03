using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CareFirst.Model;

[Table("ReviewDoctor")]
public partial class ReviewDoctor
{
    [Key]
    [Column("ReviewDoctorID")]
    public int ReviewDoctorId { get; set; }

    public int? Rating { get; set; }

    [StringLength(255)]
    public string? Comment { get; set; }

    [Column("Doctor_id")]
    public int DoctorId { get; set; }

    [Column("AppointmentDoctor_id")]
    public int AppointmentDoctorId { get; set; }

    [ForeignKey("AppointmentDoctorId")]
    [InverseProperty("ReviewDoctors")]
    public virtual AppointmentDoctor AppointmentDoctor { get; set; } = null!;

    [ForeignKey("DoctorId")]
    [InverseProperty("ReviewDoctors")]
    public virtual Doctor Doctor { get; set; } = null!;
}
