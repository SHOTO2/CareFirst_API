using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CareFirst.Model;

[Table("Symptom")]
public partial class Symptom
{
    [Key]
    [Column("SymptomID")]
    public int SymptomId { get; set; }

    [StringLength(255)]
    public string Name { get; set; } = null!;

    [StringLength(255)]
    public string Symptoms { get; set; } = null!;

    [Column("Disease_id")]
    public int DiseaseId { get; set; }

    [ForeignKey("DiseaseId")]
    [InverseProperty("Symptoms")]
    public virtual Disease Disease { get; set; } = null!;
}
