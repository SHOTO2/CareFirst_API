using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CareFirst.Model;

[Table("NurseMedicalCenter")]
public partial class NurseMedicalCenter
{
    [Key]
    [Column("NurseMedicalCenterID")]
    public int NurseMedicalCenterId { get; set; }

    [StringLength(100)]
    public string Name { get; set; } = null!;

    [StringLength(255)]
    public string Location { get; set; } = null!;

    [InverseProperty("MedicalCenter")]
    public virtual ICollection<Nurse> Nurses { get; set; } = new List<Nurse>();
}
