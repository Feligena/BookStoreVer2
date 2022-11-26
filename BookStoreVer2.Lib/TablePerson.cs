using System;
using System.Collections.Generic;

namespace BookStoreVer2.Lib
{
    public partial class TablePerson
    {
        public int PersonId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public int Age { get; set; }
    }
}
