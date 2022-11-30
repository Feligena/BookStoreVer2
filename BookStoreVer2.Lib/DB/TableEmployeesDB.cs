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
        /// <param name="lastName"></param>
        /// <param name="firstName"></param>
        /// <param name="patronimic"></param>
        /// <param name="jobTitle"></param>
        /// <returns></returns>
        public int AddEmployee(string lastName, string firstName, string patronimic, string jobTitle)
        {
            var employee = new Employee();
            employee.IdHuman = new TableHumansDB().AddHuman(lastName, firstName, patronimic);
            employee.IdJobTitle = new TableJobTitlesDB().SearchJobTitle(jobTitle);
            using (DbBookStore db = new DbBookStore())
            {
                db.TableEmployees.Add(employee);
                db.SaveChanges();

                return SearchEmployee(lastName, firstName, patronimic, jobTitle);
            }
        }

        /// <summary>
        /// Поиск сотрудника по имени, фамилии, отчеству и должности
        /// </summary>
        /// <param name="lastName"></param>
        /// <param name="firstName"></param>
        /// <param name="patronimic"></param>
        /// <param name="jobTitle"></param>
        /// <returns></returns>
        public int SearchEmployee(string lastName, string firstName, string patronimic, string jobTitle)
        {
            using (DbBookStore db = new DbBookStore())
            {
                return (db.TableEmployees.First(e => 
                          e.IdHuman == (new TableHumansDB().SearchHuman(lastName, firstName, patronimic)) 
                          && e.IdJobTitle == (new TableJobTitlesDB().SearchJobTitle(jobTitle)))).Id;
            }
        }

        /// <summary>
        /// Поиск сотрудников по имени, фамилии, отчеству
        /// </summary>
        /// <param name="lastName"></param>
        /// <param name="firstName"></param>
        /// <param name="patronimic"></param>
        public void SearchEmployee(string lastName, string firstName, string patronimic)
        {
            using (DbBookStore db = new DbBookStore())
            {
                var search = db.TableEmployees.Where(e =>
                          e.IdHuman == (new TableHumansDB().SearchHuman(lastName, firstName, patronimic)));
            }
        }

        /// <summary>
        /// Поиск сотрудников по имени, фамилии
        /// </summary>
        /// <param name="lastName"></param>
        /// <param name="firstName"></param>
        /// <param name="patronimic"></param>
        public void SearchEmployee(string lastName, string firstName)
        {
            using (DbBookStore db = new DbBookStore())
            {
                var search = db.TableEmployees.Where(e =>
                          e.IdHuman == (new TableHumansDB().SearchHuman(lastName, firstName)));
            }
        }

        /// <summary>
        /// Поиск сотрудников по имени или фамилии
        /// </summary>
        /// <param name="lastName"></param>
        /// <param name="firstName"></param>
        /// <param name="patronimic"></param>
        public void SearchEmployee(string lastName, int key)
        {
            using (DbBookStore db = new DbBookStore())
            {
                var search = db.TableEmployees.Where(e =>
                          e.IdHuman == (new TableHumansDB().SearchHuman(lastName, key)));
            }
        }
    }
}
