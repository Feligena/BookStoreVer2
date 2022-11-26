using System;
using System.Collections.Generic;

namespace BookStoreVer2.Lib
{
    public partial class Human
    {
        public Human()
        {
            Authors = new HashSet<Author>();
            Employees = new HashSet<Employee>();
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Patronymic { get; set; } = null!;

        public virtual ICollection<Author> Authors { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<User> Users { get; set; }

        public void AddHuman(string lastName, string firstName, string patronimic)
        {
            /*
            FirstName = firstName;
            LastName = lastName;    
            Patronymic = patronimic;
            */
        }
    }
}
