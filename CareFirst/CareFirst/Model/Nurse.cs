using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CareFirst.Model;

[Table("Nurse")]
public partial class Nurse
{
    [Key]
    [Column("NurseID")]
    public int NurseId { get; set; }

    [StringLength(255)]
    public string Name { get; set; } = null!;

    [StringLength(255)]
    public string Info { get; set; } = null!;

    public int Age { get; set; }

    [StringLength(255)]
    public string? ProfilePicture { get; set; }

    [Column("Department_Id")]
    public int? DepartmentId { get; set; }

    [Column("MedicalCenter_Id")]
    public int? MedicalCenterId { get; set; }

    [InverseProperty("Nurse")]
    public virtual ICollection<AppointmentNurse> AppointmentNurses { get; set; } = new List<AppointmentNurse>();

    [InverseProperty("Nurse")]
    public virtual ICollection<BookNurse> BookNurses { get; set; } = new List<BookNurse>();

    [ForeignKey("DepartmentId")]
    [InverseProperty("Nurses")]
    public virtual NurseDepartment? Department { get; set; }

    [ForeignKey("MedicalCenterId")]
    [InverseProperty("Nurses")]
    public virtual NurseMedicalCenter? MedicalCenter { get; set; }
}
