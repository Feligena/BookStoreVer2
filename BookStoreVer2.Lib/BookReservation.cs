using System;
using System.Collections.Generic;

namespace BookStoreVer2.Lib
{
    public partial class BookReservation
    {
        public int Id { get; set; }
        public int IdBook { get; set; }
        public int IdUser { get; set; }
        public int Amount { get; set; }
        public bool IsRedeem { get; set; }

        public virtual Book IdBookNavigation { get; set; } = null!;
        public virtual User IdUserNavigation { get; set; } = null!;
    }
}
