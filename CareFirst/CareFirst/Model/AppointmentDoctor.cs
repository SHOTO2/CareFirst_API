using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CareFirst.Model;

[Table("AppointmentDoctor")]
public partial class AppointmentDoctor
{
    [Key]
    [Column("AppointmentID")]
    public int AppointmentId { get; set; }

    public DateTime AppointmentDate { get; set; }

    [StringLength(255)]
    public string Status { get; set; } = null!;

    [Column("Doctor_id")]
    public int DoctorId { get; set; }

    [Column("User_id")]
    public int UserId { get; set; }

    [ForeignKey("DoctorId")]
    [InverseProperty("AppointmentDoctors")]
    public virtual Doctor Doctor { get; set; } = null!;

    [InverseProperty("AppointmentDoctor")]
    public virtual ICollection<ReviewDoctor> ReviewDoctors { get; set; } = new List<ReviewDoctor>();

    [ForeignKey("UserId")]
    [InverseProperty("AppointmentDoctors")]
    public virtual UserTable User { get; set; } = null!;
}
