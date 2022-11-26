using System;
using System.Collections.Generic;

namespace BookStoreVer2.Lib
{
    public partial class JobTitle
    {
        public JobTitle()
        {
            Employees = new HashSet<Employee>();
        }

        public int Id { get; set; }
        public string NameTitle { get; set; } = null!;

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
