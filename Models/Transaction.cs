using System;
using System.Collections.Generic;

namespace WatchMarketAPI.Models;

public partial class Transaction
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public int SellerId { get; set; }

    public int BuyerId { get; set; }

    public DateTime TransactionDate { get; set; }

    public decimal Price { get; set; }

    public virtual User Buyer { get; set; } = null!;

    public virtual Watch Product { get; set; } = null!;

    public virtual User Seller { get; set; } = null!;
}
