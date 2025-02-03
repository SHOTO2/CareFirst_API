using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CareFirst.Model;

[Table("Disease")]
public partial class Disease
{
    [Key]
    [Column("DiseaseID")]
    public int DiseaseId { get; set; }

    [StringLength(255)]
    public string Name { get; set; } = null!;

    [InverseProperty("Disease")]
    public virtual ICollection<Symptom> Symptoms { get; set; } = new List<Symptom>();
}
