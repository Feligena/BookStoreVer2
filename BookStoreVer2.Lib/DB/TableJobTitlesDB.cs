
using BookStoreVer2.Lib.Models;

namespace BookStoreVer2.Lib.DB
{
    public static class TableJobTitlesDB
    {
        /// <summary>
        /// Поиск должности в БД
        /// </summary>
        /// <param name="nameTitle"></param>
        /// <returns></returns>
        public static int SearchJobTitle(string nameTitle)
        {
            using (DbBookStore db = new DbBookStore())
            {
                var callChack = from title in db.TableJobTitles
                                where title.NameTitle == nameTitle
                                select title;
                if (callChack.Count() > 1)
                    return -1;
                else
                {
                    var tmp = callChack.ToList()[0];
                    return tmp.Id;
                }
                /*
                    var callTitle = from title in db.TableJobTitles
                                    where title.NameTitle == nameTitle
                                    && title.IsDeleted == false
                                    select title.Id;

                    var listTitle = callTitle.ToList();
                    return listTitle[0];
                    */
            }
        }

        /// <summary>
        /// Добавление новой должности в таблицу БД
        /// </summary>
        public static void AddJobTitle(string nameTitle)
        {
            using (DbBookStore db = new DbBookStore())
            {
                var tmp = db.TableJobTitles.First(t => t.NameTitle == nameTitle);
                if (tmp.NameTitle == nameTitle)
                    new Exception(nameTitle); // эксепшн дописать
                else
                {
                    db.TableJobTitles.Add(new JobTitle { NameTitle = nameTitle });
                    db.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Помечает должность как удаленную
        /// </summary>
        /// <param name="nameTitle"></param>
        public static void DeletedJobTitle(string nameTitle)
        {
            using (DbBookStore db = new DbBookStore())
            {
                var title = db.TableJobTitles.ElementAt(SearchJobTitle(nameTitle));
                title.IsDeleted = true;
                db.SaveChanges();
            }
        }
    }
}
