using System;
using System.Collections.Generic;

namespace JwtAuthentication.Server.Models;

public partial class Payment
{
    public int PaymentId { get; set; }

    public int? MemberId { get; set; }

    public DateTime? PaymentDate { get; set; }

    public decimal? Amount { get; set; }

    public string? PaymentType { get; set; }

    public virtual Member? Member { get; set; }
}
