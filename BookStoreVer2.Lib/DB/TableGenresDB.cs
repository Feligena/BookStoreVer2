using BookStoreVer2.Lib.Models;

namespace BookStoreVer2.Lib.DB
{
    public static class TableGenresDB
    {
        /// <summary>
        /// Добавляет новый жанр в таблицу БД
        /// </summary>
        /// <param name="nameGenre"></param>
        /// <exception cref="Exception"></exception>
        public static void AddGenre(string nameGenre)
        {
            using (DbBookStore db = new DbBookStore())
            {
                var checkGenre = db.TableGenres.Any(g => g.NameGenre == nameGenre && g.IsDeleted == false);
                if (!checkGenre)
                {
                    db.TableGenres.Add(new Genre { NameGenre = nameGenre });
                    db.SaveChanges();
                }
                else throw new Exception();
            }
        }
        
        /// <summary>
        /// Помечает жанр как удаленный
        /// </summary>
        /// <param name="nameGenre"></param>
        public static void DeletedGenre(string nameGenre)
        {
            using (DbBookStore db = new DbBookStore())
            {
                var genre = db.TableGenres.ElementAt(SearchGenre(nameGenre));
                genre.IsDeleted = true;
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Поиск id жанра в таблицах БД
        /// </summary>
        /// <param name="nameGenre"></param>
        /// <returns></returns>
        public static int SearchGenre(string nameGenre)
        {
            using (DbBookStore db = new DbBookStore())
                return (db.TableGenres.FirstOrDefault(g => g.NameGenre == nameGenre && g.IsDeleted == false)).Id;
        }
    }
}
