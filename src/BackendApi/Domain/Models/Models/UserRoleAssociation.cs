using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class UserRoleAssociation
{
    public int AssociationId { get; set; }

    public int UserId { get; set; }

    public int RoleId { get; set; }

    public virtual UserRole Role { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
