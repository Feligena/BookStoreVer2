using System;
using System.Collections.Generic;

namespace BookStoreVer2.Lib.Models
{
    public partial class Book
    {
        public Book()
        {
            BookReservations = new HashSet<BookReservation>();
            WriteOffs = new HashSet<WriteOff>();
        }

        public int Id { get; set; }
        public string NameBook { get; set; } = null!;
        public int IdAuthor { get; set; }
        public int IdPubHouse { get; set; }
        public int YearPublishing { get; set; }
        public int IdGenre { get; set; }
        public int NumberPages { get; set; }
        public int CostPrice { get; set; }
        public int SellingPrice { get; set; }
        public int Amount { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Author IdAuthorNavigation { get; set; } = null!;
        public virtual Genre IdGenreNavigation { get; set; } = null!;
        public virtual PublishingHouse IdPubHouseNavigation { get; set; } = null!;
        public virtual ICollection<BookReservation> BookReservations { get; set; }
        public virtual ICollection<WriteOff> WriteOffs { get; set; }
    }
}
