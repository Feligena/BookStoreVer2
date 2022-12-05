using BookStoreVer2.Lib.Models;

namespace BookStoreVer2.Lib.DB
{
    public static class TableAuthorizationsDB
    {
        /// <summary>
        /// ДОбавление авторизации сотрудников
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        public static void AddEmployeeAuthorization(string lastName, string firstName, string patronimic, 
                                                string login, string password, string jobTitle)
        {
            using (DbBookStore db = new DbBookStore())
            {
                var searchAuthorization = db.TableAuthorizations.Any(a => a.Login == login && a.IsDeleted == false);
                if (!searchAuthorization)
                {
                    var authorization = new Authorization();
                    authorization.IdEmployee = TableEmployeesDB.AddEmployee(lastName, firstName, patronimic, jobTitle);
                    authorization.Login = login;
                    authorization.Password = password;
                    db.TableAuthorizations.Add(authorization);
                    db.SaveChanges();
                }
                else
                    new Exception();                     //????????????????????????
            }
        }

        /// <summary>
        /// Отмечает данные авторизации как не действительные
        /// </summary>
        /// <param name="lastName"></param>
        /// <param name="firstName"></param>
        /// <param name="patronimic"></param>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <param name="jobTitle"></param>
        public static void DeletedEmployeeAuthorization(string login)
        {
            using (DbBookStore db = new DbBookStore())
            {
                var authorization = db.TableAuthorizations.ElementAt(SearchAuthorizationId(login));       // ??????????
                if (authorization.Id != 0)
                {
                    authorization.IsDeleted = true;
                    db.SaveChanges();
                }
                else
                    new Exception();                           // ????????????????????
            }
        }

        /// <summary>
        /// Поиск id авторизации в БД
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public static int SearchAuthorizationId(string login)
        {
            using (DbBookStore db = new DbBookStore())
                return db.TableAuthorizations.FirstOrDefault(a => a.Login == login 
                                                                && a.IsDeleted == false).Id;
        }
    }
}
