using System;
using System.Collections.Generic;

namespace JwtAuthentication.Server.Models;

public partial class Subscription
{
    public int SubscriptionId { get; set; }

    public int? MemberId { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string? SubscriptionType { get; set; }

    public decimal? AmountPaid { get; set; }

    public virtual Member? Member { get; set; }
}
