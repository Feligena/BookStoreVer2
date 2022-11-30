using System;
using System.Collections.Generic;
using BookStoreVer2.Lib.DB;

namespace BookStoreVer2.Lib.Models
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
