using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<ProductFavorite> ProductFavorites { get; set; } = new List<ProductFavorite>();

    public virtual ICollection<ReviewReaction> ReviewReactions { get; set; } = new List<ReviewReaction>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual UserProfile? UserProfile { get; set; }

    public virtual ICollection<UserRoleAssociation> UserRoleAssociations { get; set; } = new List<UserRoleAssociation>();
}
