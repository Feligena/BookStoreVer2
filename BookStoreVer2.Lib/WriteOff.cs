using System;
using System.Collections.Generic;

namespace BookStoreVer2.Lib
{
    public partial class WriteOff
    {
        public int Id { get; set; }
        public int IdBook { get; set; }
        public int Amount { get; set; }
        public DateTime DateWriteOffs { get; set; }
        public int IdEmployee { get; set; }

        public virtual Book IdBookNavigation { get; set; } = null!;
        public virtual Employee IdEmployeeNavigation { get; set; } = null!;
    }
}
