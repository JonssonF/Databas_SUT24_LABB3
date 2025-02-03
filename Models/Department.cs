using System;
using System.Collections.Generic;

namespace FREDRIK_JONSSON_SUT24_LABB3.Models;

public partial class Department
{
    public int DepartmentId { get; set; }

    public string DepartmentName { get; set; } = null!;

    public decimal Salary { get; set; }

    public virtual ICollection<Staff> Staff { get; set; } = new List<Staff>();
}
