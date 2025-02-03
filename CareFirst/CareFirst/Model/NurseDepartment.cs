using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CareFirst.Model;

[Table("NurseDepartment")]
public partial class NurseDepartment
{
    [Key]
    [Column("NurseAppointmentID")]
    public int NurseAppointmentId { get; set; }

    [StringLength(255)]
    public string Name { get; set; } = null!;

    public string Info { get; set; } = null!;

    [StringLength(255)]
    public string? DepartmentPicture { get; set; }

    [InverseProperty("Department")]
    public virtual ICollection<Nurse> Nurses { get; set; } = new List<Nurse>();
}
