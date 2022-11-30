using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStoreVer2.Lib.Models;

namespace BookStoreVer2.Lib.DB
{
    public class TableAuthorizationsDB
    {
        /// <summary>
        /// ДОбавление авторизации сотрудников
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        public void AddEmployeeAuthorization(string lastName, string firstName, string patronimic, string login, string password, string jobTitle)
        {
            IdEmployee = new Employee().AddEmployee(lastName, firstName, patronimic, jobTitle);
            // Login = login;
            // Password = password;
            using (DbBookStore db = new DbBookStore())
            {

                db.SaveChanges();
            }

            //return this.Id;
        }
    }
}
