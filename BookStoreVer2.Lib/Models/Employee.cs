using System;
using System.Collections.Generic;
using BookStoreVer2.Lib.DB;
namespace BookStoreVer2.Lib.Models
{
    public partial class Employee
    {
        public Employee()
        {
            Authorizations = new HashSet<Authorization>();
            WriteOffs = new HashSet<WriteOff>();
        }

        public int Id { get; set; }
        public int IdHuman { get; set; }
        public int IdJobTitle { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Human IdHumanNavigation { get; set; } = null!;
        public virtual JobTitle IdJobTitleNavigation { get; set; } = null!;
        public virtual ICollection<Authorization> Authorizations { get; set; }
        public virtual ICollection<WriteOff> WriteOffs { get; set; }
    }
}
