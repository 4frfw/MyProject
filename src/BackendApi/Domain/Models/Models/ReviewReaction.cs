using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class ReviewReaction
{
    public int ReactionId { get; set; }

    public int ReviewId { get; set; }

    public int UserId { get; set; }

    public bool ReactionType { get; set; }

    public string? ReactionText { get; set; }

    public DateTime? ReactedAt { get; set; }

    public virtual Review Review { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
