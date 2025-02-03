using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CareFirst.Model;

[Table("DoctorMedicalCenter")]
public partial class DoctorMedicalCenter
{
    [Key]
    [Column("DoctorMedicalCenterID")]
    public int DoctorMedicalCenterId { get; set; }

    [StringLength(100)]
    public string Name { get; set; } = null!;

    [StringLength(255)]
    public string Location { get; set; } = null!;

    [InverseProperty("MedicalCenter")]
    public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();
}
