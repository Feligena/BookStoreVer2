using BookStoreVer2.Lib.Models;

namespace BookStoreVer2.Lib.DB
{
    public static class TableEmployeesDB
    {
        public enum KeyUbdateEmployee
        {
            firstName = 1,
            lastName,
            patronimic,
            jobTitle,
            isDelited
        };

        /// <summary>
        /// Добавление сотрудника
        /// </summary>
        /// <param name="lastName"></param>
        /// <param name="firstName"></param>
        /// <param name="patronimic"></param>
        /// <param name="jobTitle"></param>
        /// <returns></returns>
        public static int AddEmployee(string lastName, string firstName, string patronimic, string jobTitle)
        {
            var employee = new Employee();
            employee.IdHuman = TableHumansDB.AddHuman(lastName, firstName, patronimic);
            employee.IdJobTitle = TableJobTitlesDB.SearchJobTitle(jobTitle);
            using (DbBookStore db = new DbBookStore())
            {
                db.TableEmployees.Add(employee);                           // ????????
                db.SaveChanges();

                return SearchEmployeeId(lastName, firstName, patronimic, jobTitle);

            }
        }

        // пробуем через свои методы. + Методы расширения

        /// <summary>
        /// Изменение данных сотрудника по ключу
        /// </summary>
        /// <param name="lastName"></param>
        /// <param name="firstName"></param>
        /// <param name="patronimic"></param>
        /// <param name="jobTitle"></param>
        /// <param name="changeName"></param>
        public static void UpdateEmployee(string lastName, string firstName, string patronimic,
                                   string jobTitle, string changeName, KeyUbdateEmployee key)
        {
            using (DbBookStore db = new DbBookStore())
            {
                switch (key)
                {
                    case KeyUbdateEmployee.firstName://изменение имени
                        TableHumansDB.UpdateFirstName(lastName, firstName, patronimic, changeName);
                        db.SaveChanges();
                        break;
                    case KeyUbdateEmployee.lastName: //изменение фамилии
                        TableHumansDB.UpdateLastName(lastName, firstName, patronimic, changeName);
                        db.SaveChanges();
                        break;
                    case KeyUbdateEmployee.patronimic: //изменение отчества
                        TableHumansDB.UpdatePatronimic(lastName, firstName, patronimic, changeName);
                        db.SaveChanges();
                        break;
                    case KeyUbdateEmployee.jobTitle: //изменение должности
                        var employee4 = db.TableEmployees.ElementAt(SearchEmployeeId(lastName, firstName, patronimic, jobTitle));
                        employee4.IdJobTitle = TableJobTitlesDB.SearchJobTitle(jobTitle);
                        db.SaveChanges();
                        break;
                    case KeyUbdateEmployee.isDelited:
                        DeletedEmployee(lastName, firstName, patronimic, jobTitle);
                        break;
                    default:
                        break;
                }
            }
            
        }

        /// <summary>
        /// Отмечает сотрудника как удаленного
        /// </summary>
        /// <param name="lastName"></param>
        /// <param name="firstName"></param>
        /// <param name="patronimic"></param>
        /// <param name="jobTitle"></param>
        /// <returns></returns>
        public static void DeletedEmployee(string lastName, string firstName, string patronimic, string jobTitle)
        {
            using (DbBookStore db = new DbBookStore())
            {
                var employee5 = db.TableEmployees.ElementAt(SearchEmployeeId(lastName, firstName, patronimic, jobTitle));
                employee5.IsDeleted = true;
                db.SaveChanges();
            }
        }


        /// <summary>
        /// Поиск первого сотрудника в списке по имени, фамилии, отчеству и должности
        /// </summary>
        /// <param name="lastName"></param>
        /// <param name="firstName"></param>
        /// <param name="patronimic"></param>
        /// <param name="jobTitle"></param>
        /// <returns></returns>
        public static int SearchEmployeeId(string lastName, string firstName, string patronimic, string jobTitle)
        {
            using (DbBookStore db = new DbBookStore())
            {
                return (db.TableEmployees.First(e => 
                          e.IdHuman == TableHumansDB.SearchHumanId(lastName, firstName, patronimic) 
                          && e.IdJobTitle == TableJobTitlesDB.SearchJobTitle(jobTitle))).Id;
            }
        }
        /*
        /// <summary>
        /// Поиск первого сотрудника в списке по имени, фамилии, отчеству
        /// </summary>
        /// <param name="lastName"></param>
        /// <param name="firstName"></param>
        /// <param name="patronimic"></param>
        public static int SearchEmployeeId(string lastName, string firstName, string patronimic)
        {
            using (DbBookStore db = new DbBookStore())
            {
                return (db.TableEmployees.First(e => e.IdHuman == TableHumansDB.SearchHumanId(lastName, 
                          firstName, patronimic))).Id;
            }
        }

        /// <summary>
        /// Поиск первого сотрудника в списке по имени, фамилии
        /// </summary>
        /// <param name="lastName"></param>
        /// <param name="firstName"></param>
        /// <param name="patronimic"></param>
        public static int SearchEmployeeId(string lastName, string firstName)
        {
            using (DbBookStore db = new DbBookStore())
            {
                return (db.TableEmployees.First(e =>
                          e.IdHuman == TableHumansDB.SearchHumanId(lastName, firstName))).Id;
            }
        }

        /// <summary>
        /// Поиск первого сотрудника в списке по имени или фамилии
        /// </summary>
        /// <param name="lastName"></param>
        /// <param name="firstName"></param>
        /// <param name="patronimic"></param>
        public static int SearchEmployeeId(string lastName, KeyNameHuman key)
        {
            using (DbBookStore db = new DbBookStore())
            {
                return (db.TableEmployees.First(e =>
                          e.IdHuman == TableHumansDB.SearchHumanId(lastName, key))).Id;
            }
        }
        */

        // пробуем через навигационные поля

        /// <summary>
        /// Поиск всех сотрудников по имени, фамилии и отчеству
        /// </summary>
        /// <param name="lastName"></param>
        /// <param name="firstName"></param>
        /// <param name="patronimic"></param>
        /// <returns></returns>
        public static IQueryable SearchEmployee(string lastName, string firstName, string patronimic)
        {
            using(DbBookStore db = new DbBookStore())
            {
                var tmp = db.TableEmployees.Where(e => e.IdHumanNavigation.LastName == lastName
                                                    && e.IdHumanNavigation.FirstName == firstName 
                                                    && e.IdHumanNavigation.Patronymic == patronimic 
                                                    && e.IsDeleted == false)
                                           .Join(db.TableHumans,
                                           e => e.IdHuman, h => h.Id,
                                           (e, h) => new
                                           {
                                               FirstName = h.FirstName,
                                               LastName = h.LastName,
                                               Patronymic = h.Patronymic,
                                               JobTitle = e.IdJobTitle
                                           })
                                           .Join(db.TableJobTitles,
                                           e => e.JobTitle, j => j.Id,
                                           (e, j) => new {
                                               FirstName = e.FirstName,
                                               LastName = e.LastName,
                                               Patronymic = e.Patronymic,
                                               JobTitle = j.NameTitle
                                           });
                return tmp;
            }
        }

        /// <summary>
        /// Поиск всех сотрудников по имени, фамилии
        /// </summary>
        /// <param name="lastName"></param>
        /// <param name="firstName"></param>
        /// <param name="patronimic"></param>
        /// <returns></returns>
        public static IQueryable SearchEmployee(string lastName, string firstName)
        {
            using (DbBookStore db = new DbBookStore())
            {
                var tmp = db.TableEmployees.Where(e => e.IdHumanNavigation.LastName == lastName
                                                    && e.IdHumanNavigation.FirstName == firstName 
                                                    && e.IsDeleted == false)
                                           .Join(db.TableHumans,
                                           e => e.IdHuman, h => h.Id,
                                           (e, h) => new
                                           {
                                               FirstName = h.FirstName,
                                               LastName = h.LastName,
                                               Patronymic = h.Patronymic,
                                               JobTitle = e.IdJobTitle
                                           })
                                           .Join(db.TableJobTitles,
                                           e => e.JobTitle, j => j.Id,
                                           (e, j) => new {
                                               FirstName = e.FirstName,
                                               LastName = e.LastName,
                                               Patronymic = e.Patronymic,
                                               JobTitle = j.NameTitle
                                           });
                return tmp;
            }
        }

        /// <summary>
        /// Поиск всех сотрудников по имени или фамилии
        /// </summary>
        /// <param name="lastName"></param>
        /// <param name="firstName"></param>
        /// <param name="patronimic"></param>
        /// <returns></returns>
        public static IQueryable SearchEmployee(string name, KeyNameHuman key)
        {
            using (DbBookStore db = new DbBookStore())
            {
                switch (key)
                {
                    case KeyNameHuman.firstName:
                        var tmpFirstName = db.TableEmployees.Where(e => e.IdHumanNavigation.FirstName == name
                                                                     && e.IsDeleted == false)
                                                            .Join(db.TableHumans,
                                                            e => e.IdHuman, h => h.Id,
                                                            (e, h) => new
                                                            {
                                                                FirstName = h.FirstName,
                                                                LastName = h.LastName,
                                                                Patronymic = h.Patronymic,
                                                                JobTitle = e.IdJobTitle
                                                            })
                                                            .Join(db.TableJobTitles, 
                                                            e => e.JobTitle, j => j.Id, 
                                                            (e, j) => new {
                                                                FirstName = e.FirstName,
                                                                LastName = e.LastName,
                                                                Patronymic = e.Patronymic,
                                                                JobTitle = j.NameTitle });
                                                
                        return tmpFirstName;                        // ??????????????????????????????????

                    case KeyNameHuman.lastName:
                        var tmpLastName = db.TableEmployees.Where(e => e.IdHumanNavigation.LastName == name 
                                                                    && e.IsDeleted == false)
                                                           .Join(db.TableHumans,
                                                           e => e.IdHuman, h => h.Id,
                                                           (e, h) => new
                                                           {
                                                               FirstName = h.FirstName,
                                                               LastName = h.LastName,
                                                               Patronymic = h.Patronymic,
                                                               JobTitle = e.IdJobTitle
                                                           })
                                                           .Join(db.TableJobTitles,
                                                           e => e.JobTitle, j => j.Id,
                                                           (e, j) => new {
                                                               FirstName = e.FirstName,
                                                               LastName = e.LastName,
                                                               Patronymic = e.Patronymic,
                                                               JobTitle = j.NameTitle
                                                           });
                        return tmpLastName;                        // ?????????????????????????????????????
                    default: throw new ArgumentOutOfRangeException(nameof(key));
                }
            }
        }
    }
}
