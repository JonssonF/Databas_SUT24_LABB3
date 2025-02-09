using System;
using System.Collections.Generic;

namespace FREDRIK_JONSSON_SUT24_LABB3.Models;

public partial class Role
{
    public int RoleId { get; set; }

    public string RoleName { get; set; } = null!;

    public virtual ICollection<Staff> Staff { get; set; } = new List<Staff>();
}
