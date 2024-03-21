using System;
using System.Collections.Generic;

namespace WatchMarketAPI.Models;

public partial class Comment
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public int UserId { get; set; }

    public string CommentText { get; set; } = null!;

    public DateTime DatePosted { get; set; }

    public virtual Watch Product { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
