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
        public bool IsDeleted { get; set; }                          //???????? Можно ли так вставлять или нужно снова подтягивать, а изменения делать в БД

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
