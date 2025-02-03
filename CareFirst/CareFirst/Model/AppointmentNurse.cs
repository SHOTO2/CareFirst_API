using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CareFirst.Model;

[Table("AppointmentNurse")]
public partial class AppointmentNurse
{
    [Key]
    [Column("AppointmentID")]
    public int AppointmentId { get; set; }

    public DateTime AppointmentDate { get; set; }

    [StringLength(255)]
    public string Status { get; set; } = null!;

    [Column("Nurse_id")]
    public int NurseId { get; set; }

    [Column("User_id")]
    public int UserId { get; set; }

    [ForeignKey("NurseId")]
    [InverseProperty("AppointmentNurses")]
    public virtual Nurse Nurse { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("AppointmentNurses")]
    public virtual UserTable User { get; set; } = null!;
}
