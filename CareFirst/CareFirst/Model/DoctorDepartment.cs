using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CareFirst.Model;

[Table("DoctorDepartment")]
public partial class DoctorDepartment
{
    [Key]
    [Column("DoctorAppointmentID")]
    public int DoctorAppointmentId { get; set; }

    [StringLength(100)]
    public string Name { get; set; } = null!;

    [StringLength(255)]
    public string? DepartmentPicture { get; set; }

    [InverseProperty("Department")]
    public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();
}
