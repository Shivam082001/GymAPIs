using System;
using System.Collections.Generic;

namespace JwtAuthentication.Server.Models;

public partial class Trainer
{
    public int TrainerId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public string? Gender { get; set; }

    public string? ContactNumber { get; set; }

    public string? Email { get; set; }

    public DateTime? HireDate { get; set; }

    public string? Specialization { get; set; }

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();
}
