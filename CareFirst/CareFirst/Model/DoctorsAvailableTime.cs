using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CareFirst.Model;

[Table("DoctorsAvailableTime")]
public partial class DoctorsAvailableTime
{
    [Key]
    [Column("AvailableTimeID")]
    public int AvailableTimeId { get; set; }

    [StringLength(255)]
    public string TimeAvailable { get; set; } = null!;

    [Column("Doctor_id")]
    public int DoctorId { get; set; }

    [ForeignKey("DoctorId")]
    [InverseProperty("DoctorsAvailableTimes")]
    public virtual Doctor Doctor { get; set; } = null!;
}
