using System;
using System.Collections.Generic;

namespace WatchMarketAPI.Models;

public partial class Watch
{
    public int Id { get; set; }

    public string Brand { get; set; } = null!;

    public string Model { get; set; } = null!;

    public int? Year { get; set; }

    public decimal Price { get; set; }

    public string? ImageUrl { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
