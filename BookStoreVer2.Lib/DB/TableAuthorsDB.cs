using BookStoreVer2.Lib.Models;

namespace BookStoreVer2.Lib.DB
{
    public static class TableAuthorsDB
    {
        /// <summary>
        /// Добавление нового Автора в БД
        /// </summary>
        /// <param name="lastName"></param>
        /// <param name="firstName"></param>
        /// <param name="patronimic"></param>
        public static void AddAuthor(string lastName, string firstName, string patronimic)
        {
            using (DbBookStore db = new DbBookStore())
            {
                if (SearchAuthor(lastName, firstName, patronimic) == null)
                {
                    db.TableAuthors.Add(new Author { IdHuman = TableHumansDB.AddHuman(lastName, firstName, patronimic) });
                    db.SaveChanges();
                }
                else throw new Exception();          // ??????????????????????????????
                
            }
        }

        /// <summary>
        /// Отмечает автора как удаленного
        /// </summary>
        public static void DeletedAuthor(string lastName, string firstName, string patronimic)
        {
            using (DbBookStore db = new DbBookStore())
            {
                var author = db.TableAuthors.ElementAt(SearchAuthorId(lastName, firstName, patronimic));
                author.IsDeleted = true;
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Поиск id автора по имени, фамилии и отчеству
        /// </summary>
        /// <param name="lastName"></param>
        /// <param name="firstName"></param>
        /// <param name="patronimic"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static int SearchAuthorId(string lastName, string firstName, string patronimic)
        {
            using (DbBookStore db = new DbBookStore())
            {
                return db.TableAuthors.First(a => a.IdHumanNavigation.LastName == lastName
                                         && a.IdHumanNavigation.FirstName == firstName
                                         && a.IdHumanNavigation.Patronymic == patronimic 
                                         && a.IdHumanNavigation.IsDeleted == false
                                         && a.IsDeleted == false).Id;
            }
        }

        /// <summary>
        /// Поиск авторов по имени, Фамилии и отчеству
        /// </summary>
        /// <param name="lastName"></param>
        /// <param name="firstName"></param>
        /// <param name="patronimic"></param>
        /// <returns></returns>
        public static IQueryable SearchAuthor(string lastName, string firstName, string patronimic)
        {
                return TableHumansDB.SearchHuman(lastName, firstName, patronimic);             // ????????????
        }

        /// <summary>
        /// Поиск авторов по имени и фамилии
        /// </summary>
        /// <param name="lastName"></param>
        /// <param name="firstName"></param>
        /// <returns></returns>
        public static IQueryable SearchAuthor(string lastName, string firstName)
        {
            return TableHumansDB.SearchHuman(lastName, firstName);
        }

        /// <summary>
        /// Поиск авторов по имени или фамилии
        /// </summary>
        /// <param name="changeName"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static IQueryable SearchAuthor(string changeName, KeyNameHuman key)
        {
            return TableHumansDB.SearchHuman(changeName, key);
        }
    }
}
