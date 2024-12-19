using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class UserRole
{
    public int RoleId { get; set; }

    public string RoleName { get; set; } = null!;

    public string? RoleDescription { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<UserRoleAssociation> UserRoleAssociations { get; set; } = new List<UserRoleAssociation>();
}
