using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CareFirst.Model;

[Table("UserTable")]
[Index("PhoneNumber", Name = "UQ__UserTabl__85FB4E386B84F6E2", IsUnique = true)]
[Index("Email", Name = "UQ__UserTabl__A9D10534B2810CE1", IsUnique = true)]
public partial class UserTable
{
    [Key]
    [Column("UserID")]
    public int UserId { get; set; }

    [StringLength(255)]
    public string Email { get; set; } = null!;

    [StringLength(255)]
    public string PhoneNumber { get; set; } = null!;

    public int Age { get; set; }

    [StringLength(255)]
    public string Name { get; set; } = null!;

    [StringLength(255)]
    public string PasswordHash { get; set; } = null!;

    public int Gender { get; set; }

    [StringLength(255)]
    public string? ProfilePicture { get; set; }

    [StringLength(200)]
    public string? VerificationToken { get; set; }

    public DateTime? VerifiedAt { get; set; }

    [StringLength(200)]
    public string? PasswordResetToken { get; set; }

    public DateTime? ResetTokenExpires { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<AppointmentDoctor> AppointmentDoctors { get; set; } = new List<AppointmentDoctor>();

    [InverseProperty("User")]
    public virtual ICollection<AppointmentNurse> AppointmentNurses { get; set; } = new List<AppointmentNurse>();

    [InverseProperty("User")]
    public virtual ICollection<BookDoctor> BookDoctors { get; set; } = new List<BookDoctor>();

    [InverseProperty("User")]
    public virtual ICollection<BookNurse> BookNurses { get; set; } = new List<BookNurse>();
}
