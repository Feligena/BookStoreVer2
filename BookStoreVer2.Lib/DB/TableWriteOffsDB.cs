using BookStoreVer2.Lib.Models;

namespace BookStoreVer2.Lib.DB
{
    public static class TableWriteOffsDB //списания
    {
        /// <summary>
        /// Создание нового списания
        /// </summary>
        /// <param name="nameBook"></param>
        /// <param name="lastName"></param>
        /// <param name="firstName"></param>
        /// <param name="patronimic"></param>
        /// <param name="namePubHouse"></param>
        /// <param name="yearPublishing"></param>
        /// <param name="nameGenre"></param>
        /// <param name="numPages"></param>
        /// <param name="amount"></param>
        /// <param name="lastNameEmployee"></param>
        /// <param name="firstNameEmployee"></param>
        /// <param name="patronimicEmployee"></param>
        /// <param name="jobTitle"></param>
        public static void AddWriteOffs(string nameBook, string lastName, string firstName, string patronimic,
                                string namePubHouse, int yearPublishing, string nameGenre, int numPages, int amount,
                                string lastNameEmployee, string firstNameEmployee, string patronimicEmployee, string jobTitle)
        {
            using (DbBookStore db = new DbBookStore())
            {
                db.TableWriteOffs.Add(new WriteOff()
                {
                    IdBook = TableBooksDB.SearchBooksId(nameBook, lastName, firstName, patronimic,
                                                        namePubHouse, yearPublishing, nameGenre, numPages),
                    Amount = amount,
                    DateWriteOffs = DateTime.Now,
                    IdEmployee = TableEmployeesDB.SearchEmployeeId(lastNameEmployee, firstNameEmployee, 
                                                                   patronimicEmployee, jobTitle)
                });
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Поиск списаной книги по всем параметрам
        /// </summary>
        public static IQueryable<(string NameBook, string Author, string Publisher, int YearPublishing, int NumberPages, string Employee, string JobTitle)>
            SearchWriteOffs(string nameBook, string lastName, string firstName, string patronimic,
                                string namePubHouse, int yearPublishing, string nameGenre, int numPages,
                                string lastNameEmployee, string firstNameEmployee, string patronimicEmployee, string jobTitle)
        {
            using (DbBookStore db = new DbBookStore())
            {
                var tmp = db.TableWriteOffs.Where(w => w.IdBookNavigation.NameBook == nameBook
                                                     && w.IdBookNavigation.IdAuthorNavigation.IdHumanNavigation.LastName == lastName
                                                     && w.IdBookNavigation.IdAuthorNavigation.IdHumanNavigation.FirstName == firstName
                                                     && w.IdBookNavigation.IdAuthorNavigation.IdHumanNavigation.Patronymic == patronimic
                                                     && w.IdBookNavigation.IdPubHouseNavigation.NamePubHouse == namePubHouse
                                                     && w.IdBookNavigation.YearPublishing == yearPublishing
                                                     && w.IdBookNavigation.IdGenreNavigation.NameGenre == nameGenre
                                                     && w.IdBookNavigation.NumberPages == numPages
                                                     && w.IdEmployeeNavigation.IdHumanNavigation.LastName == lastNameEmployee
                                                     && w.IdEmployeeNavigation.IdHumanNavigation.FirstName == firstNameEmployee
                                                     && w.IdEmployeeNavigation.IdHumanNavigation.Patronymic == patronimicEmployee
                                                     && w.IdEmployeeNavigation.IdJobTitleNavigation.NameTitle == jobTitle)
                                            .Join(db.TableBooks, w => w.IdBook, b => b.Id, (w, b) => new
                                            {
                                                NameBook = b.NameBook,
                                                Author = b.IdAuthorNavigation.IdHumanNavigation.LastName +
                                                         b.IdAuthorNavigation.IdHumanNavigation.FirstName +
                                                         b.IdAuthorNavigation.IdHumanNavigation.Patronymic,
                                                Publisher = b.IdPubHouseNavigation.NamePubHouse,
                                                YearPublishing = b.YearPublishing,
                                                NumberPages = b.NumberPages,
                                                Employee = w.IdEmployee,
                                                JobTitle = w.IdEmployeeNavigation.IdJobTitle
                                            })
                                            .Join(db.TableEmployees, w => w.Employee, e => e.Id, (w, e) => new
                                            {
                                                NameBook = w.NameBook,
                                                Author = w.Author,
                                                Publisher = w.Publisher,
                                                YearPublishing = w.YearPublishing,
                                                NumberPages = w.NumberPages,
                                                Employee = e.IdHumanNavigation.LastName +
                                                           e.IdHumanNavigation.FirstName +
                                                           e.IdHumanNavigation.Patronymic,
                                                JobTitle = e.IdJobTitleNavigation.NameTitle
                                            });
                return (IQueryable<(string NameBook, string Author, string Publisher, int YearPublishing, int NumberPages, string Employee, string JobTitle)>)tmp;
            }
        }

        /// <summary>
        /// Поиск списаной книги по названию и автору
        /// </summary>
        public static IQueryable<(string NameBook, string Author, string Publisher, int YearPublishing, int NumberPages, string Employee, string JobTitle)>
            SearchWriteOffs(string nameBook, string lastName, string firstName, string patronimic)
        {
            using (DbBookStore db = new DbBookStore())
            {
                var tmp = db.TableWriteOffs.Where(w => w.IdBookNavigation.NameBook == nameBook
                                                     && w.IdBookNavigation.IdAuthorNavigation.IdHumanNavigation.LastName == lastName
                                                     && w.IdBookNavigation.IdAuthorNavigation.IdHumanNavigation.FirstName == firstName
                                                     && w.IdBookNavigation.IdAuthorNavigation.IdHumanNavigation.Patronymic == patronimic)
                                            .Join(db.TableBooks, w => w.IdBook, b => b.Id, (w, b) => new
                                            {
                                                NameBook = b.NameBook,
                                                Author = b.IdAuthorNavigation.IdHumanNavigation.LastName +
                                                         b.IdAuthorNavigation.IdHumanNavigation.FirstName +
                                                         b.IdAuthorNavigation.IdHumanNavigation.Patronymic,
                                                Publisher = b.IdPubHouseNavigation.NamePubHouse,
                                                YearPublishing = b.YearPublishing,
                                                NumberPages = b.NumberPages,
                                                Employee = w.IdEmployee,
                                                JobTitle = w.IdEmployeeNavigation.IdJobTitle
                                            })
                                            .Join(db.TableEmployees, w => w.Employee, e => e.Id, (w, e) => new
                                            {
                                                NameBook = w.NameBook,
                                                Author = w.Author,
                                                Publisher = w.Publisher,
                                                YearPublishing = w.YearPublishing,
                                                NumberPages = w.NumberPages,
                                                Employee = e.IdHumanNavigation.LastName +
                                                           e.IdHumanNavigation.FirstName +
                                                           e.IdHumanNavigation.Patronymic,
                                                JobTitle = e.IdJobTitleNavigation.NameTitle
                                            });
                return (IQueryable<(string NameBook, string Author, string Publisher, int YearPublishing, int NumberPages, string Employee, string JobTitle)>)tmp;
            }
        }


        /// <summary>
        /// Поиск списаной книги по сотруднику, проводившему списание
        /// </summary>
        public static IQueryable<(string NameBook, string Author, string Publisher, int YearPublishing, int NumberPages, string Employee, string JobTitle)> 
            SearchWriteOffs(string lastNameEmployee, string firstNameEmployee, string jobTitle)
        {
            using (DbBookStore db = new DbBookStore())
            {
                var tmp = db.TableWriteOffs.Where(w => w.IdEmployeeNavigation.IdHumanNavigation.LastName == lastNameEmployee
                                                     && w.IdEmployeeNavigation.IdHumanNavigation.FirstName == firstNameEmployee
                                                     && w.IdEmployeeNavigation.IdJobTitleNavigation.NameTitle == jobTitle)
                                            .Join(db.TableBooks, w => w.IdBook, b => b.Id, (w, b) => new
                                            {
                                                NameBook = b.NameBook,
                                                Author = b.IdAuthorNavigation.IdHumanNavigation.LastName +
                                                         b.IdAuthorNavigation.IdHumanNavigation.FirstName +
                                                         b.IdAuthorNavigation.IdHumanNavigation.Patronymic,
                                                Publisher = b.IdPubHouseNavigation.NamePubHouse,
                                                YearPublishing = b.YearPublishing,
                                                NumberPages = b.NumberPages,
                                                Employee = w.IdEmployee,
                                                JobTitle = w.IdEmployeeNavigation.IdJobTitle
                                            })
                                            .Join(db.TableEmployees, w => w.Employee, e => e.Id, (w, e) => new
                                            {
                                                NameBook = w.NameBook,
                                                Author = w.Author,
                                                Publisher = w.Publisher,
                                                YearPublishing = w.YearPublishing,
                                                NumberPages = w.NumberPages,
                                                Employee = e.IdHumanNavigation.LastName +
                                                           e.IdHumanNavigation.FirstName +
                                                           e.IdHumanNavigation.Patronymic,
                                                JobTitle = e.IdJobTitleNavigation.NameTitle
                                            });
                return (IQueryable<(string NameBook, string Author, string Publisher, int YearPublishing, int NumberPages, string Employee, string JobTitle)>)tmp;
            }
        }

        // можно добавить вывод всех списаний в txt, и/или за определенный период
    }
}
