using System;
using System.Collections.Generic;

namespace FREDRIK_JONSSON_SUT24_LABB3.Models;

public partial class Subject
{
    public int SubjectId { get; set; }

    public string SubjectName { get; set; } = null!;

    public int TeacherId { get; set; }

    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();

    public virtual Staff Teacher { get; set; } = null!;
}
