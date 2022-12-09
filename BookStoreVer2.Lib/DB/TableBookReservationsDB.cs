using BookStoreVer2.Lib.Models;

namespace BookStoreVer2.Lib.DB
{
    public static class TableBookReservationsDB
    {
        /// <summary>
        /// Поставить резерв на книгу
        /// </summary>
        /// <param name="lastName"></param>
        /// <param name="firstName"></param>
        /// <param name="patronimic"></param>
        public static void AddReserve(string nameBook, string lastName, string firstName, string patronimic,
                                string namePubHouse, int yearPublishing, string nameGenre, int numPages, int amount,
                                int phone)
        {
            using (DbBookStore db = new DbBookStore())
            {
                db.TableBookReservations.Add(new BookReservation()
                {
                    IdBook = TableBooksDB.SearchBooksId(nameBook, lastName, firstName, patronimic, namePubHouse, 
                                                        yearPublishing, nameGenre, numPages),
                    IdUser = TableUsersDB.SearchUserId(phone),
                    Amount = amount
                });
                var book = db.TableBooks.ElementAt(TableBooksDB.SearchBooksId(nameBook, lastName, firstName, patronimic, namePubHouse,
                                                        yearPublishing, nameGenre, numPages));
                book.Amount -= amount;
                db.SaveChanges();
            }
        }

        
        /// <summary>
        /// Отмечает книгу как купленную
        /// </summary>
        public static void RemoveReserve(string nameBook, string lastName, string firstName, string patronimic,
                                string namePubHouse, int yearPublishing, string nameGenre, int numPages, int amount,
                                int phone)
        {
            using (DbBookStore db = new DbBookStore())
            {
                var reserve = db.TableBookReservations.First(r => r.IdBookNavigation.NameBook == nameBook
                                               && r.IdBookNavigation.IdAuthorNavigation.IdHumanNavigation.LastName == lastName
                                               && r.IdBookNavigation.IdAuthorNavigation.IdHumanNavigation.FirstName == firstName
                                               && r.IdBookNavigation.IdAuthorNavigation.IdHumanNavigation.Patronymic == patronimic
                                               && r.IdBookNavigation.IdPubHouseNavigation.NamePubHouse == namePubHouse
                                               && r.IdBookNavigation.YearPublishing == yearPublishing
                                               && r.IdBookNavigation.NumberPages == numPages
                                               && r.IdBookNavigation.IdGenreNavigation.NameGenre == nameGenre
                                               && r.Amount == amount
                                               && r.IdUserNavigation.Phone == phone
                                               );
                reserve.IsRedeem = true;
                db.SaveChanges();
            }
        }

        /*
        /// <summary>
        /// Отмечает книгу как купленную
        /// </summary>
        public static void RemoveReserve(int phone)
        {
            
        }
        */

        /// <summary>
        /// Убирает книгу из резерва
        /// </summary>
        public static void DeleteReserve(string nameBook, string lastName, string firstName, string patronimic,
                                string namePubHouse, int yearPublishing, string nameGenre, int numPages, int amount,
                                int phone)
        {
            using (DbBookStore db = new DbBookStore())
            {
                var reserve = db.TableBookReservations.First(r => r.IdBookNavigation.NameBook == nameBook
                                               && r.IdBookNavigation.IdAuthorNavigation.IdHumanNavigation.LastName == lastName
                                               && r.IdBookNavigation.IdAuthorNavigation.IdHumanNavigation.FirstName == firstName
                                               && r.IdBookNavigation.IdAuthorNavigation.IdHumanNavigation.Patronymic == patronimic
                                               && r.IdBookNavigation.IdPubHouseNavigation.NamePubHouse == namePubHouse
                                               && r.IdBookNavigation.YearPublishing == yearPublishing
                                               && r.IdBookNavigation.NumberPages == numPages
                                               && r.IdBookNavigation.IdGenreNavigation.NameGenre == nameGenre
                                               && r.Amount == amount
                                               && r.IdUserNavigation.Phone == phone
                                               );
                reserve.IsRedeem = true;
                var book = db.TableBooks.ElementAt(TableBooksDB.SearchBooksId(nameBook, lastName, firstName, patronimic,
                                                    namePubHouse, yearPublishing, nameGenre, numPages));
                book.Amount += amount;
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Поиск резерва по номеру пользователя
        /// </summary>
        public static IQueryable SearchReserve(int phone)
        {
            using (DbBookStore db = new DbBookStore())
            {
                var tmp = db.TableBookReservations.Where(r => r.IdUserNavigation.Phone == phone)
                                                  .Join(db.TableBooks, r => r.IdBook, b => b.Id, (r, b) => new
                                                  {
                                                      NameBook = b.NameBook,
                                                      Author = b.IdAuthorNavigation.IdHumanNavigation.LastName +
                                                               b.IdAuthorNavigation.IdHumanNavigation.FirstName +
                                                               b.IdAuthorNavigation.IdHumanNavigation.Patronymic,
                                                      User = r.IdUserNavigation.IdHumanNavigation.LastName +
                                                             r.IdUserNavigation.IdHumanNavigation.FirstName +
                                                             r.IdUserNavigation.IdHumanNavigation.Patronymic,
                                                      Phone = r.IdUserNavigation.Phone,
                                                      Amount = r.Amount
                                                  });
                return tmp;
            }
        }

        /// <summary>
        /// Поиск резерва по названию книги и фамилии автора
        /// </summary>
        public static IQueryable SearchReserve(string nameBook, string lastName)
        {
            using (DbBookStore db = new DbBookStore())
            {
                var tmp = db.TableBookReservations.Where(r => r.IdBookNavigation.NameBook == nameBook
                                     && r.IdBookNavigation.IdAuthorNavigation.IdHumanNavigation.LastName == lastName)
                                                  .Join(db.TableBooks, r => r.IdBook, b => b.Id, (r, b) => new
                                                  {
                                                      NameBook = b.NameBook,
                                                      Author = b.IdAuthorNavigation.IdHumanNavigation.LastName +
                                                               b.IdAuthorNavigation.IdHumanNavigation.FirstName +
                                                               b.IdAuthorNavigation.IdHumanNavigation.Patronymic,
                                                      User = r.IdUserNavigation.IdHumanNavigation.LastName +
                                                             r.IdUserNavigation.IdHumanNavigation.FirstName +
                                                             r.IdUserNavigation.IdHumanNavigation.Patronymic,
                                                      Phone = r.IdUserNavigation.Phone,
                                                      Amount = r.Amount
                                                  });
                return tmp;
            }
        }
    }
}
