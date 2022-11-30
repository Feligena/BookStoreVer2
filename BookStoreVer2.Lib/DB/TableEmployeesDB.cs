using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStoreVer2.Lib.Models;

namespace BookStoreVer2.Lib.DB
{
    public class TableEmployeesDB
    {
        /// <summary>
        /// Добавление сотрудника
        /// </summary>
        public int AddEmployee(string lastName, string firstName, string patronimic, string jobTitle)
        {
            IdJobTitle = new JobTitle().SearchJobTitle(jobTitle);
            using (DbBookStore db = new DbBookStore())
            {
                var addEmployee = from
            }

            return Id;
        }
    }
}
