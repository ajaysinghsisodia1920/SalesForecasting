using System;
using System.Collections.Generic;

namespace SalesForecasting.Models;

public partial class Product
{
    public string? OrderId { get; set; }

    public string ProductId { get; set; } = null!;

    public string? Category { get; set; }

    public string? SubCategory { get; set; }

    public string? ProductName { get; set; }

    public decimal? Sales { get; set; }

    public int? Quantity { get; set; }

    public decimal? Discount { get; set; }

    public decimal? Profit { get; set; }

    public virtual Order? Order { get; set; }
}
