using System;
using System.Collections.Generic;

namespace JwtAuthentication.Server.Models;

public partial class Class
{
    public int ClassId { get; set; }

    public string? ClassName { get; set; }

    public string? Description { get; set; }

    public string? Schedule { get; set; }

    public int? TrainerId { get; set; }

    public int? MaxCapacity { get; set; }

    public virtual ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();

    public virtual Trainer? Trainer { get; set; }
}
