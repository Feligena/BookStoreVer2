﻿using System;
using System.Collections.Generic;

namespace BookStoreVer2.Lib.Models
{
    public partial class Genre
    {
        public Genre()
        {
            Books = new HashSet<Book>();
        }

        public int Id { get; set; }
        public string NameGenre { get; set; } = null!;
        public bool IsDeleted { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
