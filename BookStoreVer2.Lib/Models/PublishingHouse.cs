using System;
using System.Collections.Generic;

namespace BookStoreVer2.Lib.Models
{
    public partial class PublishingHouse
    {
        public PublishingHouse()
        {
            Books = new HashSet<Book>();
        }

        public int Id { get; set; }
        public string NamePubHouse { get; set; } = null!;
        public string Address { get; set; } = null!;
        public bool IsDeleted { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
