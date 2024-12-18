using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class UserProfile
{
    public int ProfileId { get; set; }

    public int UserId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? ProfilePictureUrl { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public string? Location { get; set; }

    public string? Bio { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual User User { get; set; } = null!;
}
