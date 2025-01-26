using System;
using System.Collections.Generic;

namespace FREDRIK_JONSSON_SUT24_LABB3.Models;

public partial class Staff
{
    public int StaffId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public DateOnly? DoB { get; set; }

    public string? Role { get; set; }

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();

    public virtual ICollection<Subject> Subjects { get; set; } = new List<Subject>();
}
