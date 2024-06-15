using System;
using System.Collections.Generic;

namespace SalesForecasting.Models;

public partial class Order
{
    public string OrderId { get; set; } = null!;

    public string? OrderDate { get; set; }

    public string? ShipDate { get; set; }

    public string? ShipMode { get; set; }

    public string? CustomerId { get; set; }

    public string? CustomerName { get; set; }

    public string? Segment { get; set; }

    public string? Country { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public int? PostalCode { get; set; }

    public string? Region { get; set; }

    public virtual ICollection<OrderReturn> OrderReturns { get; set; } = new List<OrderReturn>();

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
