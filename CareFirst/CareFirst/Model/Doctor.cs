using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CareFirst.Model;

[Table("Doctor")]
public partial class Doctor
{
    [Key]
    [Column("DoctorID")]
    public int DoctorId { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;

    public string? Info { get; set; }

    public int Rating { get; set; }

    [StringLength(255)]
    public string? Location { get; set; }

    [StringLength(255)]
    public string? ProfilePicture { get; set; }

    [Column("Department_Id")]
    public int DepartmentId { get; set; }

    [Column("MedicalCenter_Id")]
    public int MedicalCenterId { get; set; }

    [InverseProperty("Doctor")]
    public virtual ICollection<AppointmentDoctor> AppointmentDoctors { get; set; } = new List<AppointmentDoctor>();

    [InverseProperty("Doctor")]
    public virtual ICollection<BookDoctor> BookDoctors { get; set; } = new List<BookDoctor>();

    [ForeignKey("DepartmentId")]
    [InverseProperty("Doctors")]
    public virtual DoctorDepartment Department { get; set; } = null!;

    [InverseProperty("Doctor")]
    public virtual ICollection<DoctorsAvailableTime> DoctorsAvailableTimes { get; set; } = new List<DoctorsAvailableTime>();

    [ForeignKey("MedicalCenterId")]
    [InverseProperty("Doctors")]
    public virtual DoctorMedicalCenter MedicalCenter { get; set; } = null!;

    [InverseProperty("Doctor")]
    public virtual ICollection<ReviewDoctor> ReviewDoctors { get; set; } = new List<ReviewDoctor>();
}
