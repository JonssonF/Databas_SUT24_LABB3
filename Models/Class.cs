using System;
using System.Collections.Generic;

namespace FREDRIK_JONSSON_SUT24_LABB3.Models;

public partial class Class
{
    public int ClassId { get; set; }

    public string? ClassName { get; set; }

    public int TeacherId { get; set; }

    public virtual Staff Teacher { get; set; } = null!;

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
