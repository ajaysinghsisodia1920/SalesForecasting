using System;
using System.Collections.Generic;

namespace SalesForecasting.Models;

public partial class OrderReturn
{
    public int ReturnId { get; set; }

    public string? OrderId { get; set; }

    public string? Comments { get; set; }

    public virtual Order? Order { get; set; }
}
