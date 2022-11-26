using System;
using System.Collections.Generic;

namespace BookStoreVer2.Lib
{
    public partial class Genre
    {
        public Genre()
        {
            Books = new HashSet<Book>();
        }

        public int Id { get; set; }
        public string NameGenre { get; set; } = null!;

        public virtual ICollection<Book> Books { get; set; }

        public void AddGenre(string nameGenre)
        {

        }
    }
}
