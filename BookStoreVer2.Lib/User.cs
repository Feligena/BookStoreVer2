using System;
using System.Collections.Generic;

namespace BookStoreVer2.Lib
{
    public partial class User
    {
        public User()
        {
            BookReservations = new HashSet<BookReservation>();
        }

        public int Id { get; set; }
        public int IdHuman { get; set; }
        public int Phone { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Human IdHumanNavigation { get; set; } = null!;
        public virtual ICollection<BookReservation> BookReservations { get; set; }
    }
}
