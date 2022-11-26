using System;
using System.Collections.Generic;

namespace BookStoreVer2.Lib
{
    public partial class Author
    {
        public Author()
        {
            Books = new HashSet<Book>();
        }

        public int Id { get; set; }
        public int IdHuman { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Human IdHumanNavigation { get; set; } = null!;
        public virtual ICollection<Book> Books { get; set; }

        public void AddAuthor(string lastName, string firstName, string patronimic)
        {

        }
    }
}
