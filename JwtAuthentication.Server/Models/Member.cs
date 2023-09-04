using System;
using System.Collections.Generic;

namespace JwtAuthentication.Server.Models;

public partial class Member
{
    public int MemberId { get; set; }
     
    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public string? Gender { get; set; }

    public string? ContactNumber { get; set; }

    public string? Email { get; set; }

    public DateTime? JoinDate { get; set; }

    public string? Password { get; set; }


    public virtual ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
}
