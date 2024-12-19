using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class Review
{
    public int ReviewId { get; set; }

    public int UserId { get; set; }

    public int ProductId { get; set; }

    public int RatingValue { get; set; }

    public string? ReviewText { get; set; }

    public DateTime? ReviewDate { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual ICollection<ReviewReaction> ReviewReactions { get; set; } = new List<ReviewReaction>();

    public virtual User User { get; set; } = null!;
}
