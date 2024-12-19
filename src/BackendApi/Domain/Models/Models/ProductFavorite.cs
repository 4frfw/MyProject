using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class ProductFavorite
{
    public int FavoriteId { get; set; }

    public int UserId { get; set; }

    public int ProductId { get; set; }

    public DateTime? AddedAt { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
