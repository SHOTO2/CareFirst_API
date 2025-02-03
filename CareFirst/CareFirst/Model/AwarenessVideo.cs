using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CareFirst.Model;

[Table("AwarenessVideo")]
public partial class AwarenessVideo
{
    [Key]
    [Column("VideoID")]
    public int VideoId { get; set; }

    [StringLength(255)]
    public string Title { get; set; } = null!;

    public int? Duration { get; set; }

    [Column("category")]
    [StringLength(255)]
    public string? Category { get; set; }

    public DateOnly? UploadDate { get; set; }

    public int? ViewCount { get; set; }
}
