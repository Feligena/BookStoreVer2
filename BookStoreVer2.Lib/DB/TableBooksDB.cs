using BookStoreVer2.Lib.Models;

namespace BookStoreVer2.Lib.DB
{
    public enum KeyUpdateBook
    {
        name = 1,
        idAutor,
        idPublisher,
        yearPublishing,
        idGenre,
        numberPages,
        coastPrice,
        sellingPrice,
        amount
    };

    public enum KeySearchBook
    {
        nameBook = 1,
        lastNameAutor,
        firstNameAutor,
        patronimicAutor,
        namePublisher,
        nameGenre
    };

    public static class TableBooksDB
    {
        /// <summary>
        /// Добавление книги в таблицу
        /// </summary>
        /// <param name="name"></param>
        /// <param name="lastName"></param>
        /// <param name="firstName"></param>
        /// <param name="patronimic"></param>
        public static void AddBook(string nameBook, string lastName, string firstName, string patronimic,
                                   string namePubHouse, string address, int yearPublishing,
                                   string nameGenre, int numPages, int costPrice, int sellingPrice, int amount)
        {
            using (DbBookStore db = new DbBookStore())
            {
                var checkBook = db.TableBooks.Any(b => b.NameBook == nameBook && b.IdGenreNavigation.NameGenre == nameGenre
                                                    && b.IdAuthorNavigation.IdHumanNavigation.LastName == lastName
                                                    && b.IdAuthorNavigation.IdHumanNavigation.FirstName == firstName
                                                    && b.IdAuthorNavigation.IdHumanNavigation.Patronymic == patronimic
                                                    && b.IdPubHouseNavigation.NamePubHouse == namePubHouse
                                                    && b.IdPubHouseNavigation.Address == address
                                                    && b.YearPublishing == yearPublishing);
                if (!checkBook)
                {
                    db.TableBooks.Add(new Book
                    {
                        NameBook = nameBook,
                        IdAuthor = TableAuthorsDB.SearchAuthorId(lastName, firstName, patronimic),
                        IdPubHouse = TablePublishingHousesDB.SearchPablisherId(namePubHouse, address),
                        YearPublishing = yearPublishing,
                        IdGenre = TableGenresDB.SearchGenre(nameGenre),
                        NumberPages = numPages,
                        CostPrice = costPrice,
                        SellingPrice = sellingPrice,
                        Amount = amount
                    });
                }
                else throw new Exception();
            }
        }

        /// <summary>
        /// Изменение автора книги
        /// </summary>
        /// <param name="nameBook"></param>
        /// <param name="lastName"></param>
        /// <param name="firstName"></param>
        /// <param name="patronimic"></param>
        /// <param name="namePubHouse"></param>
        /// <param name="yearPublishing"></param>
        /// <param name="nameGenre"></param>
        /// <param name="numPages"></param>
        /// <param name="changeFirstName"></param>
        /// <param name="changeLastName"></param>
        /// <param name="changePatronimic"></param>
        public static void UpdateBook(string nameBook, string lastName, string firstName, string patronimic,
                                   string namePubHouse, int yearPublishing, string nameGenre, int numPages,
                                   string changeLastName, string changeFirstName, string changePatronimic)
        {
            using (DbBookStore db = new DbBookStore())
            {
                var book = db.TableBooks.ElementAt(SearchBooksId(nameBook, lastName, firstName,
                                                    patronimic, namePubHouse, yearPublishing, nameGenre, numPages));
                book.IdAuthor = TableAuthorsDB.SearchAuthorId(changeLastName, changeFirstName, changePatronimic);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Изменяет данные о книге
        /// </summary>
        public static void UpdateBook(string nameBook, string lastName, string firstName, string patronimic,
                                   string namePubHouse, int yearPublishing, string nameGenre, int numPages, 
                                   string strChange, KeyUpdateBook key)
        {
            using (DbBookStore db = new DbBookStore())
            {
                switch (key)
                {
                    case KeyUpdateBook.name:
                        var book1 = db.TableBooks.ElementAt(SearchBooksId(nameBook, lastName, firstName,
                                                    patronimic, namePubHouse, yearPublishing, nameGenre, numPages));
                        book1.NameBook = strChange;
                        db.SaveChanges();
                        break;

                    case KeyUpdateBook.idPublisher:
                        var book2 = db.TableBooks.ElementAt(SearchBooksId(nameBook, lastName, firstName,
                                                    patronimic, namePubHouse, yearPublishing, nameGenre, numPages));
                        book2.IdPubHouse = TablePublishingHousesDB.SearchPablisherId(strChange);
                        db.SaveChanges();
                        break;

                    case KeyUpdateBook.idGenre:
                        var book3 = db.TableBooks.ElementAt(SearchBooksId(nameBook, lastName, firstName,
                                                    patronimic, namePubHouse, yearPublishing, nameGenre, numPages));
                        book3.IdGenre = TableGenresDB.SearchGenre(strChange);
                        db.SaveChanges();
                        break;
                        default: throw new Exception();                   // ????????????????????????
                }
            }
        }

        /// <summary>
        /// Изменяет данные о книге
        /// </summary>
        public static void UpdateBook(string nameBook, string lastName, string firstName, string patronimic,
                                   string namePubHouse, int yearPublishing, string nameGenre, int numPages,
                                   int strChange, KeyUpdateBook key)
        {
            using (DbBookStore db = new DbBookStore())
            {
                switch (key)
                {
                    case KeyUpdateBook.yearPublishing:
                        var book1 = db.TableBooks.ElementAt(SearchBooksId(nameBook, lastName, firstName,
                                                    patronimic, namePubHouse, yearPublishing, nameGenre, numPages));
                        book1.YearPublishing = strChange;
                        db.SaveChanges();
                        break;

                    case KeyUpdateBook.numberPages:
                        var book2 = db.TableBooks.ElementAt(SearchBooksId(nameBook, lastName, firstName,
                                                    patronimic, namePubHouse, yearPublishing, nameGenre, numPages));
                        book2.NumberPages = strChange;
                        db.SaveChanges();
                        break;

                    case KeyUpdateBook.coastPrice:
                        var book3 = db.TableBooks.ElementAt(SearchBooksId(nameBook, lastName, firstName,
                                                    patronimic, namePubHouse, yearPublishing, nameGenre, numPages));
                        book3.CostPrice = strChange;
                        db.SaveChanges();
                        break;

                    case KeyUpdateBook.sellingPrice:
                        var book4 = db.TableBooks.ElementAt(SearchBooksId(nameBook, lastName, firstName,
                                                    patronimic, namePubHouse, yearPublishing, nameGenre, numPages));
                        book4.SellingPrice = strChange;
                        db.SaveChanges();
                        break;

                    case KeyUpdateBook.amount:
                        var book5 = db.TableBooks.ElementAt(SearchBooksId(nameBook, lastName, firstName,
                                                    patronimic, namePubHouse, yearPublishing, nameGenre, numPages));
                        book5.Amount = strChange;
                        db.SaveChanges();
                        break;
                        default: throw new Exception();                 // ????????????????????????
                }
            }
        }

        /// <summary>
        /// Отмечает книгу как удаленную
        /// </summary>
        /// <param name="nameBook"></param>
        /// <param name="lastName"></param>
        /// <param name="firstName"></param>
        /// <param name="patronimic"></param>
        /// <param name="namePubHouse"></param>
        /// <param name="yearPublishing"></param>
        /// <param name="nameGenre"></param>
        /// <param name="numPages"></param>
        public static void DeletedBook(string nameBook, string lastName, string firstName, string patronimic,
                                   string namePubHouse, int yearPublishing, string nameGenre, int numPages)
        {
            using (DbBookStore db = new DbBookStore())
            {
                var book = db.TableBooks.ElementAt(SearchBooksId(nameBook, lastName, firstName, patronimic,
                                   namePubHouse, yearPublishing, nameGenre, numPages));
                book.IsDeleted = true;
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Поиск книги по названию, автору, издательству, году издания, жанру и количеству страниц
        /// </summary>
        /// <param name="nameBook"></param>
        /// <param name="lastName"></param>
        /// <param name="firstName"></param>
        /// <param name="patronimic"></param>
        /// <param name="namePubHouse"></param>
        /// <param name="yearPublishing"></param>
        /// <param name="nameGenre"></param>
        /// <param name="numPages"></param>
        /// <returns></returns>
        public static int SearchBooksId(string nameBook, string lastName, string firstName, string patronimic,
                                   string namePubHouse, int yearPublishing, string nameGenre, int numPages)
        {
            using (DbBookStore db = new DbBookStore())
            {
                return (db.TableBooks.First(b => b.NameBook == nameBook
                                                && b.IdAuthorNavigation.IdHumanNavigation.LastName == lastName
                                                && b.IdAuthorNavigation.IdHumanNavigation.FirstName == firstName
                                                && b.IdAuthorNavigation.IdHumanNavigation.Patronymic == patronimic
                                                && b.IdPubHouseNavigation.NamePubHouse == namePubHouse
                                                && b.YearPublishing == yearPublishing
                                                && b.IdGenreNavigation.NameGenre == nameGenre
                                                && b.NumberPages == numPages
                                                && b.IsDeleted == false)).Id;
            }
        }

        /// <summary>
        /// Поиск книги по названию, автору, издательству, году издания, жанру и количеству страниц
        /// </summary>
        /// <param name="nameBook"></param>
        /// <param name="lastName"></param>
        /// <param name="firstName"></param>
        /// <param name="patronimic"></param>
        /// <param name="namePubHouse"></param>
        /// <param name="yearPublishing"></param>
        /// <param name="nameGenre"></param>
        /// <param name="numPages"></param>
        /// <returns></returns>
        public static IQueryable SearchBooks(string nameBook, string lastName, string firstName, string patronimic,
                                   string namePubHouse, int yearPublishing, string nameGenre, int numPages)
        {
            using (DbBookStore db = new DbBookStore())
            {
                var tmp = db.TableBooks.Where(b => b.NameBook == nameBook
                                                && b.IdAuthorNavigation.IdHumanNavigation.LastName == lastName
                                                && b.IdAuthorNavigation.IdHumanNavigation.FirstName == firstName
                                                && b.IdAuthorNavigation.IdHumanNavigation.Patronymic == patronimic
                                                && b.IdPubHouseNavigation.NamePubHouse == namePubHouse
                                                && b.YearPublishing == yearPublishing
                                                && b.IdGenreNavigation.NameGenre == nameGenre
                                                && b.NumberPages == numPages
                                                && b.IsDeleted == false)
                                        .Join(db.TableAuthors, b => b.IdAuthor, a => a.Id,
                                              (b, a) => new
                                              {
                                                  NameBook = b.NameBook,
                                                  Author = a.IdHuman,
                                                  Publisher = b.IdPubHouse,
                                                  YearPublishing = b.YearPublishing,
                                                  Genre = b.IdGenre,
                                                  NumPages = b.NumberPages
                                              })
                                        .Join(db.TableHumans, b => b.Author, h => h.Id,
                                              (b, h) => new
                                              {
                                                  NameBook = b.NameBook,
                                                  Author = h.LastName + h.FirstName + h.Patronymic,
                                                  Publisher = b.Publisher,
                                                  YearPublishing = b.YearPublishing,
                                                  Genre = b.Genre,
                                                  NumPages = b.NumPages,
                                              })
                                        .Join(db.TablePublishingHouses, b => b.Publisher, p => p.Id,
                                              (b, p) => new
                                              {
                                                  NameBook = b.NameBook,
                                                  Author = b.Author,
                                                  Publisher = p.NamePubHouse,
                                                  YearPublishing = b.YearPublishing,
                                                  Genre = b.Genre,
                                                  NumPages = b.NumPages,
                                              })
                                        .Join(db.TableGenres, b => b.Genre, g => g.Id,
                                              (b, g) => new
                                              {
                                                  NameBook = b.NameBook,
                                                  Author = b.Author,
                                                  Publisher = b.Publisher,
                                                  YearPublishing = b.YearPublishing,
                                                  Genre = g.NameGenre,
                                                  NumPages = b.NumPages,
                                              });
                return tmp;
            }
        }


        /// <summary>
        /// Поиск книги по названию, автору, издательству, году издания и жанру
        /// </summary>
        /// <param name="nameBook"></param>
        /// <param name="lastName"></param>
        /// <param name="firstName"></param>
        /// <param name="patronimic"></param>
        /// <param name="namePubHouse"></param>
        /// <param name="yearPublishing"></param>
        /// <param name="nameGenre"></param>
        /// <param name="numPages"></param>
        /// <returns></returns>
        public static IQueryable SearchBooks(string nameBook, string lastName, string firstName, string patronimic,
                                   string namePubHouse, int yearPublishing, string nameGenre)
        {
            using (DbBookStore db = new DbBookStore())
            {
                var tmp = db.TableBooks.Where(b => b.NameBook == nameBook
                                                && b.IdAuthorNavigation.IdHumanNavigation.LastName == lastName
                                                && b.IdAuthorNavigation.IdHumanNavigation.FirstName == firstName
                                                && b.IdAuthorNavigation.IdHumanNavigation.Patronymic == patronimic
                                                && b.IdPubHouseNavigation.NamePubHouse == namePubHouse
                                                && b.YearPublishing == yearPublishing
                                                && b.IdGenreNavigation.NameGenre == nameGenre
                                                && b.IsDeleted == false)
                                        .Join(db.TableAuthors, b => b.IdAuthor, a => a.Id,
                                              (b, a) => new
                                              {
                                                  NameBook = b.NameBook,
                                                  Author = a.IdHuman,
                                                  Publisher = b.IdPubHouse,
                                                  YearPublishing = b.YearPublishing,
                                                  Genre = b.IdGenre,
                                                  NumPages = b.NumberPages
                                              })
                                        .Join(db.TableHumans, b => b.Author, h => h.Id,
                                              (b, h) => new
                                              {
                                                  NameBook = b.NameBook,
                                                  Author = h.LastName + h.FirstName + h.Patronymic,
                                                  Publisher = b.Publisher,
                                                  YearPublishing = b.YearPublishing,
                                                  Genre = b.Genre,
                                                  NumPages = b.NumPages,
                                              })
                                        .Join(db.TablePublishingHouses, b => b.Publisher, p => p.Id,
                                              (b, p) => new
                                              {
                                                  NameBook = b.NameBook,
                                                  Author = b.Author,
                                                  Publisher = p.NamePubHouse,
                                                  YearPublishing = b.YearPublishing,
                                                  Genre = b.Genre,
                                                  NumPages = b.NumPages,
                                              })
                                        .Join(db.TableGenres, b => b.Genre, g => g.Id,
                                              (b, g) => new
                                              {
                                                  NameBook = b.NameBook,
                                                  Author = b.Author,
                                                  Publisher = b.Publisher,
                                                  YearPublishing = b.YearPublishing,
                                                  Genre = g.NameGenre,
                                                  NumPages = b.NumPages,
                                              });
                return tmp;
            }
        }


        /// <summary>
        /// Поиск книги по названию, автору, издательству, году издания и количеству страниц
        /// </summary>
        /// <param name="nameBook"></param>
        /// <param name="lastName"></param>
        /// <param name="firstName"></param>
        /// <param name="patronimic"></param>
        /// <param name="namePubHouse"></param>
        /// <param name="yearPublishing"></param>
        /// <param name="nameGenre"></param>
        /// <param name="numPages"></param>
        /// <returns></returns>
        public static IQueryable SearchBooks(string nameBook, string lastName, string firstName, string patronimic,
                                   string namePubHouse, int yearPublishing, int numPages)
        {
            using (DbBookStore db = new DbBookStore())
            {
                var tmp = db.TableBooks.Where(b => b.NameBook == nameBook
                                                && b.IdAuthorNavigation.IdHumanNavigation.LastName == lastName
                                                && b.IdAuthorNavigation.IdHumanNavigation.FirstName == firstName
                                                && b.IdAuthorNavigation.IdHumanNavigation.Patronymic == patronimic
                                                && b.IdPubHouseNavigation.NamePubHouse == namePubHouse
                                                && b.YearPublishing == yearPublishing
                                                && b.NumberPages == numPages
                                                && b.IsDeleted == false)
                                        .Join(db.TableAuthors, b => b.IdAuthor, a => a.Id,
                                              (b, a) => new
                                              {
                                                  NameBook = b.NameBook,
                                                  Author = a.IdHuman,
                                                  Publisher = b.IdPubHouse,
                                                  YearPublishing = b.YearPublishing,
                                                  Genre = b.IdGenre,
                                                  NumPages = b.NumberPages
                                              })
                                        .Join(db.TableHumans, b => b.Author, h => h.Id,
                                              (b, h) => new
                                              {
                                                  NameBook = b.NameBook,
                                                  Author = h.LastName + h.FirstName + h.Patronymic,
                                                  Publisher = b.Publisher,
                                                  YearPublishing = b.YearPublishing,
                                                  Genre = b.Genre,
                                                  NumPages = b.NumPages,
                                              })
                                        .Join(db.TablePublishingHouses, b => b.Publisher, p => p.Id,
                                              (b, p) => new
                                              {
                                                  NameBook = b.NameBook,
                                                  Author = b.Author,
                                                  Publisher = p.NamePubHouse,
                                                  YearPublishing = b.YearPublishing,
                                                  Genre = b.Genre,
                                                  NumPages = b.NumPages,
                                              })
                                        .Join(db.TableGenres, b => b.Genre, g => g.Id,
                                              (b, g) => new
                                              {
                                                  NameBook = b.NameBook,
                                                  Author = b.Author,
                                                  Publisher = b.Publisher,
                                                  YearPublishing = b.YearPublishing,
                                                  Genre = g.NameGenre,
                                                  NumPages = b.NumPages,
                                              });
                return tmp;
            }
        }


        /// <summary>
        /// Поиск книги по названию, автору, издательству, жанру и количеству страниц
        /// </summary>
        /// <param name="nameBook"></param>
        /// <param name="lastName"></param>
        /// <param name="firstName"></param>
        /// <param name="patronimic"></param>
        /// <param name="namePubHouse"></param>
        /// <param name="yearPublishing"></param>
        /// <param name="nameGenre"></param>
        /// <param name="numPages"></param>
        /// <returns></returns>
        public static IQueryable SearchBooks(string nameBook, string lastName, string firstName, string patronimic,
                                   string namePubHouse, string nameGenre, int numPages)
        {
            using (DbBookStore db = new DbBookStore())
            {
                var tmp = db.TableBooks.Where(b => b.NameBook == nameBook
                                                && b.IdAuthorNavigation.IdHumanNavigation.LastName == lastName
                                                && b.IdAuthorNavigation.IdHumanNavigation.FirstName == firstName
                                                && b.IdAuthorNavigation.IdHumanNavigation.Patronymic == patronimic
                                                && b.IdPubHouseNavigation.NamePubHouse == namePubHouse
                                                && b.IdGenreNavigation.NameGenre == nameGenre
                                                && b.NumberPages == numPages
                                                && b.IsDeleted == false)
                                        .Join(db.TableAuthors, b => b.IdAuthor, a => a.Id,
                                              (b, a) => new
                                              {
                                                  NameBook = b.NameBook,
                                                  Author = a.IdHuman,
                                                  Publisher = b.IdPubHouse,
                                                  YearPublishing = b.YearPublishing,
                                                  Genre = b.IdGenre,
                                                  NumPages = b.NumberPages
                                              })
                                        .Join(db.TableHumans, b => b.Author, h => h.Id,
                                              (b, h) => new
                                              {
                                                  NameBook = b.NameBook,
                                                  Author = h.LastName + h.FirstName + h.Patronymic,
                                                  Publisher = b.Publisher,
                                                  YearPublishing = b.YearPublishing,
                                                  Genre = b.Genre,
                                                  NumPages = b.NumPages,
                                              })
                                        .Join(db.TablePublishingHouses, b => b.Publisher, p => p.Id,
                                              (b, p) => new
                                              {
                                                  NameBook = b.NameBook,
                                                  Author = b.Author,
                                                  Publisher = p.NamePubHouse,
                                                  YearPublishing = b.YearPublishing,
                                                  Genre = b.Genre,
                                                  NumPages = b.NumPages,
                                              })
                                        .Join(db.TableGenres, b => b.Genre, g => g.Id,
                                              (b, g) => new
                                              {
                                                  NameBook = b.NameBook,
                                                  Author = b.Author,
                                                  Publisher = b.Publisher,
                                                  YearPublishing = b.YearPublishing,
                                                  Genre = g.NameGenre,
                                                  NumPages = b.NumPages,
                                              });
                return tmp;
            }
        }

        /// <summary>
        /// Поиск книги по названию, автору, году издания, жанру и количеству страниц
        /// </summary>
        /// <param name="nameBook"></param>
        /// <param name="lastName"></param>
        /// <param name="firstName"></param>
        /// <param name="patronimic"></param>
        /// <param name="namePubHouse"></param>
        /// <param name="yearPublishing"></param>
        /// <param name="nameGenre"></param>
        /// <param name="numPages"></param>
        /// <returns></returns>
        public static IQueryable SearchBooks(string nameBook, string lastName, string firstName, string patronimic,
                                    int yearPublishing, string nameGenre, int numPages)
        {
            using (DbBookStore db = new DbBookStore())
            {
                var tmp = db.TableBooks.Where(b => b.NameBook == nameBook
                                                && b.IdAuthorNavigation.IdHumanNavigation.LastName == lastName
                                                && b.IdAuthorNavigation.IdHumanNavigation.FirstName == firstName
                                                && b.IdAuthorNavigation.IdHumanNavigation.Patronymic == patronimic
                                                && b.YearPublishing == yearPublishing
                                                && b.IdGenreNavigation.NameGenre == nameGenre
                                                && b.NumberPages == numPages
                                                && b.IsDeleted == false)
                                        .Join(db.TableAuthors, b => b.IdAuthor, a => a.Id,
                                              (b, a) => new
                                              {
                                                  NameBook = b.NameBook,
                                                  Author = a.IdHuman,
                                                  Publisher = b.IdPubHouse,
                                                  YearPublishing = b.YearPublishing,
                                                  Genre = b.IdGenre,
                                                  NumPages = b.NumberPages
                                              })
                                        .Join(db.TableHumans, b => b.Author, h => h.Id,
                                              (b, h) => new
                                              {
                                                  NameBook = b.NameBook,
                                                  Author = h.LastName + h.FirstName + h.Patronymic,
                                                  Publisher = b.Publisher,
                                                  YearPublishing = b.YearPublishing,
                                                  Genre = b.Genre,
                                                  NumPages = b.NumPages,
                                              })
                                        .Join(db.TablePublishingHouses, b => b.Publisher, p => p.Id,
                                              (b, p) => new
                                              {
                                                  NameBook = b.NameBook,
                                                  Author = b.Author,
                                                  Publisher = p.NamePubHouse,
                                                  YearPublishing = b.YearPublishing,
                                                  Genre = b.Genre,
                                                  NumPages = b.NumPages,
                                              })
                                        .Join(db.TableGenres, b => b.Genre, g => g.Id,
                                              (b, g) => new
                                              {
                                                  NameBook = b.NameBook,
                                                  Author = b.Author,
                                                  Publisher = b.Publisher,
                                                  YearPublishing = b.YearPublishing,
                                                  Genre = g.NameGenre,
                                                  NumPages = b.NumPages,
                                              });
                return tmp;
            }
        }

        /// <summary>
        /// Поиск книги по названию, издательству, году издания, жанру и количеству страниц
        /// </summary>
        /// <param name="nameBook"></param>
        /// <param name="lastName"></param>
        /// <param name="firstName"></param>
        /// <param name="patronimic"></param>
        /// <param name="namePubHouse"></param>
        /// <param name="yearPublishing"></param>
        /// <param name="nameGenre"></param>
        /// <param name="numPages"></param>
        /// <returns></returns>
        public static IQueryable SearchBooks(string nameBook, string namePubHouse, int yearPublishing, string nameGenre, int numPages)
        {
            using (DbBookStore db = new DbBookStore())
            {
                var tmp = db.TableBooks.Where(b => b.NameBook == nameBook
                                                && b.IdPubHouseNavigation.NamePubHouse == namePubHouse
                                                && b.YearPublishing == yearPublishing
                                                && b.IdGenreNavigation.NameGenre == nameGenre
                                                && b.NumberPages == numPages
                                                && b.IsDeleted == false)
                                        .Join(db.TableAuthors, b => b.IdAuthor, a => a.Id,
                                              (b, a) => new
                                              {
                                                  NameBook = b.NameBook,
                                                  Author = a.IdHuman,
                                                  Publisher = b.IdPubHouse,
                                                  YearPublishing = b.YearPublishing,
                                                  Genre = b.IdGenre,
                                                  NumPages = b.NumberPages
                                              })
                                        .Join(db.TableHumans, b => b.Author, h => h.Id,
                                              (b, h) => new
                                              {
                                                  NameBook = b.NameBook,
                                                  Author = h.LastName + h.FirstName + h.Patronymic,
                                                  Publisher = b.Publisher,
                                                  YearPublishing = b.YearPublishing,
                                                  Genre = b.Genre,
                                                  NumPages = b.NumPages,
                                              })
                                        .Join(db.TablePublishingHouses, b => b.Publisher, p => p.Id,
                                              (b, p) => new
                                              {
                                                  NameBook = b.NameBook,
                                                  Author = b.Author,
                                                  Publisher = p.NamePubHouse,
                                                  YearPublishing = b.YearPublishing,
                                                  Genre = b.Genre,
                                                  NumPages = b.NumPages,
                                              })
                                        .Join(db.TableGenres, b => b.Genre, g => g.Id,
                                              (b, g) => new
                                              {
                                                  NameBook = b.NameBook,
                                                  Author = b.Author,
                                                  Publisher = b.Publisher,
                                                  YearPublishing = b.YearPublishing,
                                                  Genre = g.NameGenre,
                                                  NumPages = b.NumPages,
                                              });
                return tmp;
            }
        }

        /// <summary>
        /// Поиск книги по названию, автору, издательству, жанру
        /// </summary>
        /// <param name="nameBook"></param>
        /// <param name="lastName"></param>
        /// <param name="firstName"></param>
        /// <param name="patronimic"></param>
        /// <param name="namePubHouse"></param>
        /// <param name="yearPublishing"></param>
        /// <param name="nameGenre"></param>
        /// <param name="numPages"></param>
        /// <returns></returns>
        public static IQueryable SearchBooks(string nameBook, string lastName, string firstName, string patronimic,
                                   string namePubHouse, string nameGenre)
        {
            using (DbBookStore db = new DbBookStore())
            {
                var tmp = db.TableBooks.Where(b => b.NameBook == nameBook
                                                && b.IdAuthorNavigation.IdHumanNavigation.LastName == lastName
                                                && b.IdAuthorNavigation.IdHumanNavigation.FirstName == firstName
                                                && b.IdAuthorNavigation.IdHumanNavigation.Patronymic == patronimic
                                                && b.IdPubHouseNavigation.NamePubHouse == namePubHouse
                                                && b.IdGenreNavigation.NameGenre == nameGenre
                                                && b.IsDeleted == false)
                                        .Join(db.TableAuthors, b => b.IdAuthor, a => a.Id,
                                              (b, a) => new
                                              {
                                                  NameBook = b.NameBook,
                                                  Author = a.IdHuman,
                                                  Publisher = b.IdPubHouse,
                                                  YearPublishing = b.YearPublishing,
                                                  Genre = b.IdGenre,
                                                  NumPages = b.NumberPages
                                              })
                                        .Join(db.TableHumans, b => b.Author, h => h.Id,
                                              (b, h) => new
                                              {
                                                  NameBook = b.NameBook,
                                                  Author = h.LastName + h.FirstName + h.Patronymic,
                                                  Publisher = b.Publisher,
                                                  YearPublishing = b.YearPublishing,
                                                  Genre = b.Genre,
                                                  NumPages = b.NumPages,
                                              })
                                        .Join(db.TablePublishingHouses, b => b.Publisher, p => p.Id,
                                              (b, p) => new
                                              {
                                                  NameBook = b.NameBook,
                                                  Author = b.Author,
                                                  Publisher = p.NamePubHouse,
                                                  YearPublishing = b.YearPublishing,
                                                  Genre = b.Genre,
                                                  NumPages = b.NumPages,
                                              })
                                        .Join(db.TableGenres, b => b.Genre, g => g.Id,
                                              (b, g) => new
                                              {
                                                  NameBook = b.NameBook,
                                                  Author = b.Author,
                                                  Publisher = b.Publisher,
                                                  YearPublishing = b.YearPublishing,
                                                  Genre = g.NameGenre,
                                                  NumPages = b.NumPages,
                                              });
                return tmp;
            }
        }

        /// <summary>
        /// Поиск книги по названию, фамилии автора и жанру
        /// </summary>
        /// <param name="nameBook"></param>
        /// <param name="lastName"></param>
        /// <param name="firstName"></param>
        /// <param name="patronimic"></param>
        /// <param name="namePubHouse"></param>
        /// <param name="yearPublishing"></param>
        /// <param name="nameGenre"></param>
        /// <param name="numPages"></param>
        /// <returns></returns>
        public static IQueryable SearchBooks(string nameBook, string lastName, string nameGenre)
        {
            using (DbBookStore db = new DbBookStore())
            {
                var tmp = db.TableBooks.Where(b => b.NameBook == nameBook
                                                && b.IdAuthorNavigation.IdHumanNavigation.LastName == lastName
                                                && b.IdGenreNavigation.NameGenre == nameGenre
                                                && b.IsDeleted == false)
                                        .Join(db.TableAuthors, b => b.IdAuthor, a => a.Id,
                                              (b, a) => new
                                              {
                                                  NameBook = b.NameBook,
                                                  Author = a.IdHuman,
                                                  Publisher = b.IdPubHouse,
                                                  YearPublishing = b.YearPublishing,
                                                  Genre = b.IdGenre,
                                                  NumPages = b.NumberPages
                                              })
                                        .Join(db.TableHumans, b => b.Author, h => h.Id,
                                              (b, h) => new
                                              {
                                                  NameBook = b.NameBook,
                                                  Author = h.LastName + h.FirstName + h.Patronymic,
                                                  Publisher = b.Publisher,
                                                  YearPublishing = b.YearPublishing,
                                                  Genre = b.Genre,
                                                  NumPages = b.NumPages,
                                              })
                                        .Join(db.TablePublishingHouses, b => b.Publisher, p => p.Id,
                                              (b, p) => new
                                              {
                                                  NameBook = b.NameBook,
                                                  Author = b.Author,
                                                  Publisher = p.NamePubHouse,
                                                  YearPublishing = b.YearPublishing,
                                                  Genre = b.Genre,
                                                  NumPages = b.NumPages,
                                              })
                                        .Join(db.TableGenres, b => b.Genre, g => g.Id,
                                              (b, g) => new
                                              {
                                                  NameBook = b.NameBook,
                                                  Author = b.Author,
                                                  Publisher = b.Publisher,
                                                  YearPublishing = b.YearPublishing,
                                                  Genre = g.NameGenre,
                                                  NumPages = b.NumPages,
                                              });
                return tmp;
            }
        }

        /// <summary>
        /// Поиск книги по названию и жанру
        /// </summary>
        /// <param name="nameBook"></param>
        /// <param name="lastName"></param>
        /// <param name="firstName"></param>
        /// <param name="patronimic"></param>
        /// <param name="namePubHouse"></param>
        /// <param name="yearPublishing"></param>
        /// <param name="nameGenre"></param>
        /// <param name="numPages"></param>
        /// <returns></returns>
        public static IQueryable SearchBooks(string nameBook, string nameGenre)
        {
            using (DbBookStore db = new DbBookStore())
            {
                var tmp = db.TableBooks.Where(b => b.NameBook == nameBook
                                                && b.IdGenreNavigation.NameGenre == nameGenre
                                                && b.IsDeleted == false)
                                        .Join(db.TableAuthors, b => b.IdAuthor, a => a.Id,
                                              (b, a) => new
                                              {
                                                  NameBook = b.NameBook,
                                                  Author = a.IdHuman,
                                                  Publisher = b.IdPubHouse,
                                                  YearPublishing = b.YearPublishing,
                                                  Genre = b.IdGenre,
                                                  NumPages = b.NumberPages
                                              })
                                        .Join(db.TableHumans, b => b.Author, h => h.Id,
                                              (b, h) => new
                                              {
                                                  NameBook = b.NameBook,
                                                  Author = h.LastName + h.FirstName + h.Patronymic,
                                                  Publisher = b.Publisher,
                                                  YearPublishing = b.YearPublishing,
                                                  Genre = b.Genre,
                                                  NumPages = b.NumPages,
                                              })
                                        .Join(db.TablePublishingHouses, b => b.Publisher, p => p.Id,
                                              (b, p) => new
                                              {
                                                  NameBook = b.NameBook,
                                                  Author = b.Author,
                                                  Publisher = p.NamePubHouse,
                                                  YearPublishing = b.YearPublishing,
                                                  Genre = b.Genre,
                                                  NumPages = b.NumPages,
                                              })
                                        .Join(db.TableGenres, b => b.Genre, g => g.Id,
                                              (b, g) => new
                                              {
                                                  NameBook = b.NameBook,
                                                  Author = b.Author,
                                                  Publisher = b.Publisher,
                                                  YearPublishing = b.YearPublishing,
                                                  Genre = g.NameGenre,
                                                  NumPages = b.NumPages,
                                              });
                return tmp;
            }
        }

        /// <summary>
        /// Поиск книги по названию и автору
        /// </summary>
        /// <param name="nameBook"></param>
        /// <param name="lastName"></param>
        /// <param name="firstName"></param>
        /// <param name="patronimic"></param>
        /// <param name="namePubHouse"></param>
        /// <param name="yearPublishing"></param>
        /// <param name="nameGenre"></param>
        /// <param name="numPages"></param>
        /// <returns></returns>
        public static IQueryable SearchBooks(string nameBook, string lastName, string firstName, string patronimic)
        {
            using (DbBookStore db = new DbBookStore())
            {
                var tmp = db.TableBooks.Where(b => b.NameBook == nameBook
                                                && b.IdAuthorNavigation.IdHumanNavigation.LastName == lastName
                                                && b.IdAuthorNavigation.IdHumanNavigation.FirstName == firstName
                                                && b.IdAuthorNavigation.IdHumanNavigation.Patronymic == patronimic
                                                && b.IsDeleted == false)
                                        .Join(db.TableAuthors, b => b.IdAuthor, a => a.Id,
                                              (b, a) => new
                                              {
                                                  NameBook = b.NameBook,
                                                  Author = a.IdHuman,
                                                  Publisher = b.IdPubHouse,
                                                  YearPublishing = b.YearPublishing,
                                                  Genre = b.IdGenre,
                                                  NumPages = b.NumberPages
                                              })
                                        .Join(db.TableHumans, b => b.Author, h => h.Id,
                                              (b, h) => new
                                              {
                                                  NameBook = b.NameBook,
                                                  Author = h.LastName + h.FirstName + h.Patronymic,
                                                  Publisher = b.Publisher,
                                                  YearPublishing = b.YearPublishing,
                                                  Genre = b.Genre,
                                                  NumPages = b.NumPages,
                                              })
                                        .Join(db.TablePublishingHouses, b => b.Publisher, p => p.Id,
                                              (b, p) => new
                                              {
                                                  NameBook = b.NameBook,
                                                  Author = b.Author,
                                                  Publisher = p.NamePubHouse,
                                                  YearPublishing = b.YearPublishing,
                                                  Genre = b.Genre,
                                                  NumPages = b.NumPages,
                                              })
                                        .Join(db.TableGenres, b => b.Genre, g => g.Id,
                                              (b, g) => new
                                              {
                                                  NameBook = b.NameBook,
                                                  Author = b.Author,
                                                  Publisher = b.Publisher,
                                                  YearPublishing = b.YearPublishing,
                                                  Genre = g.NameGenre,
                                                  NumPages = b.NumPages,
                                              });
                return tmp;
            }
        }

        /// <summary>
        ///  Поиск книг по одному параметру
        /// </summary>
        /// <param name="nameBook"></param>
        /// <param name="lastName"></param>
        /// <param name="nameGenre"></param>
        /// <returns></returns>
        public static IQueryable SearchBooks(string strSearch, KeySearchBook key)
        {
            using (DbBookStore db = new DbBookStore())
            {
                switch (key)
                {
                    case KeySearchBook.nameBook:
                        var tmp1 = db.TableBooks.Where(b => b.NameBook == strSearch
                                                && b.IsDeleted == false)
                                        .Join(db.TableAuthors, b => b.IdAuthor, a => a.Id,
                                              (b, a) => new
                                              {
                                                  NameBook = b.NameBook,
                                                  Author = a.IdHuman,
                                                  Publisher = b.IdPubHouse,
                                                  YearPublishing = b.YearPublishing,
                                                  Genre = b.IdGenre,
                                                  NumPages = b.NumberPages
                                              })
                                        .Join(db.TableHumans, b => b.Author, h => h.Id,
                                              (b, h) => new
                                              {
                                                  NameBook = b.NameBook,
                                                  Author = h.LastName + h.FirstName + h.Patronymic,
                                                  Publisher = b.Publisher,
                                                  YearPublishing = b.YearPublishing,
                                                  Genre = b.Genre,
                                                  NumPages = b.NumPages,
                                              })
                                        .Join(db.TablePublishingHouses, b => b.Publisher, p => p.Id,
                                              (b, p) => new
                                              {
                                                  NameBook = b.NameBook,
                                                  Author = b.Author,
                                                  Publisher = p.NamePubHouse,
                                                  YearPublishing = b.YearPublishing,
                                                  Genre = b.Genre,
                                                  NumPages = b.NumPages,
                                              })
                                        .Join(db.TableGenres, b => b.Genre, g => g.Id,
                                              (b, g) => new
                                              {
                                                  NameBook = b.NameBook,
                                                  Author = b.Author,
                                                  Publisher = b.Publisher,
                                                  YearPublishing = b.YearPublishing,
                                                  Genre = g.NameGenre,
                                                  NumPages = b.NumPages,
                                              });
                        return tmp1;
                        
                    case KeySearchBook.firstNameAutor:
                        var tmp2 = db.TableBooks.Where(b =>b.IdAuthorNavigation.IdHumanNavigation.FirstName == strSearch
                                                && b.IsDeleted == false)
                                        .Join(db.TableAuthors, b => b.IdAuthor, a => a.Id,
                                              (b, a) => new
                                              {
                                                  NameBook = b.NameBook,
                                                  Author = a.IdHuman,
                                                  Publisher = b.IdPubHouse,
                                                  YearPublishing = b.YearPublishing,
                                                  Genre = b.IdGenre,
                                                  NumPages = b.NumberPages
                                              })
                                        .Join(db.TableHumans, b => b.Author, h => h.Id,
                                              (b, h) => new
                                              {
                                                  NameBook = b.NameBook,
                                                  Author = h.LastName + h.FirstName + h.Patronymic,
                                                  Publisher = b.Publisher,
                                                  YearPublishing = b.YearPublishing,
                                                  Genre = b.Genre,
                                                  NumPages = b.NumPages,
                                              })
                                        .Join(db.TablePublishingHouses, b => b.Publisher, p => p.Id,
                                              (b, p) => new
                                              {
                                                  NameBook = b.NameBook,
                                                  Author = b.Author,
                                                  Publisher = p.NamePubHouse,
                                                  YearPublishing = b.YearPublishing,
                                                  Genre = b.Genre,
                                                  NumPages = b.NumPages,
                                              })
                                        .Join(db.TableGenres, b => b.Genre, g => g.Id,
                                              (b, g) => new
                                              {
                                                  NameBook = b.NameBook,
                                                  Author = b.Author,
                                                  Publisher = b.Publisher,
                                                  YearPublishing = b.YearPublishing,
                                                  Genre = g.NameGenre,
                                                  NumPages = b.NumPages,
                                              });
                        return tmp2;
                        
                    case KeySearchBook.lastNameAutor:
                        var tmp3 = db.TableBooks.Where(b => b.IdAuthorNavigation.IdHumanNavigation.LastName == strSearch
                                                && b.IsDeleted == false)
                                        .Join(db.TableAuthors, b => b.IdAuthor, a => a.Id,
                                              (b, a) => new
                                              {
                                                  NameBook = b.NameBook,
                                                  Author = a.IdHuman,
                                                  Publisher = b.IdPubHouse,
                                                  YearPublishing = b.YearPublishing,
                                                  Genre = b.IdGenre,
                                                  NumPages = b.NumberPages
                                              })
                                        .Join(db.TableHumans, b => b.Author, h => h.Id,
                                              (b, h) => new
                                              {
                                                  NameBook = b.NameBook,
                                                  Author = h.LastName + h.FirstName + h.Patronymic,
                                                  Publisher = b.Publisher,
                                                  YearPublishing = b.YearPublishing,
                                                  Genre = b.Genre,
                                                  NumPages = b.NumPages,
                                              })
                                        .Join(db.TablePublishingHouses, b => b.Publisher, p => p.Id,
                                              (b, p) => new
                                              {
                                                  NameBook = b.NameBook,
                                                  Author = b.Author,
                                                  Publisher = p.NamePubHouse,
                                                  YearPublishing = b.YearPublishing,
                                                  Genre = b.Genre,
                                                  NumPages = b.NumPages,
                                              })
                                        .Join(db.TableGenres, b => b.Genre, g => g.Id,
                                              (b, g) => new
                                              {
                                                  NameBook = b.NameBook,
                                                  Author = b.Author,
                                                  Publisher = b.Publisher,
                                                  YearPublishing = b.YearPublishing,
                                                  Genre = g.NameGenre,
                                                  NumPages = b.NumPages,
                                              });
                        return tmp3;
                        
                    case KeySearchBook.patronimicAutor:
                        var tmp4 = db.TableBooks.Where(b => b.IdAuthorNavigation.IdHumanNavigation.Patronymic == strSearch
                                                && b.IsDeleted == false)
                                        .Join(db.TableAuthors, b => b.IdAuthor, a => a.Id,
                                              (b, a) => new
                                              {
                                                  NameBook = b.NameBook,
                                                  Author = a.IdHuman,
                                                  Publisher = b.IdPubHouse,
                                                  YearPublishing = b.YearPublishing,
                                                  Genre = b.IdGenre,
                                                  NumPages = b.NumberPages
                                              })
                                        .Join(db.TableHumans, b => b.Author, h => h.Id,
                                              (b, h) => new
                                              {
                                                  NameBook = b.NameBook,
                                                  Author = h.LastName + h.FirstName + h.Patronymic,
                                                  Publisher = b.Publisher,
                                                  YearPublishing = b.YearPublishing,
                                                  Genre = b.Genre,
                                                  NumPages = b.NumPages,
                                              })
                                        .Join(db.TablePublishingHouses, b => b.Publisher, p => p.Id,
                                              (b, p) => new
                                              {
                                                  NameBook = b.NameBook,
                                                  Author = b.Author,
                                                  Publisher = p.NamePubHouse,
                                                  YearPublishing = b.YearPublishing,
                                                  Genre = b.Genre,
                                                  NumPages = b.NumPages,
                                              })
                                        .Join(db.TableGenres, b => b.Genre, g => g.Id,
                                              (b, g) => new
                                              {
                                                  NameBook = b.NameBook,
                                                  Author = b.Author,
                                                  Publisher = b.Publisher,
                                                  YearPublishing = b.YearPublishing,
                                                  Genre = g.NameGenre,
                                                  NumPages = b.NumPages,
                                              });
                        return tmp4;
                        
                    case KeySearchBook.namePublisher:
                        var tmp5 = db.TableBooks.Where(b => b.IdPubHouseNavigation.NamePubHouse == strSearch
                                                && b.IsDeleted == false)
                                        .Join(db.TableAuthors, b => b.IdAuthor, a => a.Id,
                                              (b, a) => new
                                              {
                                                  NameBook = b.NameBook,
                                                  Author = a.IdHuman,
                                                  Publisher = b.IdPubHouse,
                                                  YearPublishing = b.YearPublishing,
                                                  Genre = b.IdGenre,
                                                  NumPages = b.NumberPages
                                              })
                                        .Join(db.TableHumans, b => b.Author, h => h.Id,
                                              (b, h) => new
                                              {
                                                  NameBook = b.NameBook,
                                                  Author = h.LastName + h.FirstName + h.Patronymic,
                                                  Publisher = b.Publisher,
                                                  YearPublishing = b.YearPublishing,
                                                  Genre = b.Genre,
                                                  NumPages = b.NumPages,
                                              })
                                        .Join(db.TablePublishingHouses, b => b.Publisher, p => p.Id,
                                              (b, p) => new
                                              {
                                                  NameBook = b.NameBook,
                                                  Author = b.Author,
                                                  Publisher = p.NamePubHouse,
                                                  YearPublishing = b.YearPublishing,
                                                  Genre = b.Genre,
                                                  NumPages = b.NumPages,
                                              })
                                        .Join(db.TableGenres, b => b.Genre, g => g.Id,
                                              (b, g) => new
                                              {
                                                  NameBook = b.NameBook,
                                                  Author = b.Author,
                                                  Publisher = b.Publisher,
                                                  YearPublishing = b.YearPublishing,
                                                  Genre = g.NameGenre,
                                                  NumPages = b.NumPages,
                                              });
                        return tmp5;
                        
                    case KeySearchBook.nameGenre:
                        var tmp6 = db.TableBooks.Where(b => b.IdGenreNavigation.NameGenre == strSearch
                                                && b.IsDeleted == false)
                                        .Join(db.TableAuthors, b => b.IdAuthor, a => a.Id,
                                              (b, a) => new
                                              {
                                                  NameBook = b.NameBook,
                                                  Author = a.IdHuman,
                                                  Publisher = b.IdPubHouse,
                                                  YearPublishing = b.YearPublishing,
                                                  Genre = b.IdGenre,
                                                  NumPages = b.NumberPages
                                              })
                                        .Join(db.TableHumans, b => b.Author, h => h.Id,
                                              (b, h) => new
                                              {
                                                  NameBook = b.NameBook,
                                                  Author = h.LastName + h.FirstName + h.Patronymic,
                                                  Publisher = b.Publisher,
                                                  YearPublishing = b.YearPublishing,
                                                  Genre = b.Genre,
                                                  NumPages = b.NumPages,
                                              })
                                        .Join(db.TablePublishingHouses, b => b.Publisher, p => p.Id,
                                              (b, p) => new
                                              {
                                                  NameBook = b.NameBook,
                                                  Author = b.Author,
                                                  Publisher = p.NamePubHouse,
                                                  YearPublishing = b.YearPublishing,
                                                  Genre = b.Genre,
                                                  NumPages = b.NumPages,
                                              })
                                        .Join(db.TableGenres, b => b.Genre, g => g.Id,
                                              (b, g) => new
                                              {
                                                  NameBook = b.NameBook,
                                                  Author = b.Author,
                                                  Publisher = b.Publisher,
                                                  YearPublishing = b.YearPublishing,
                                                  Genre = g.NameGenre,
                                                  NumPages = b.NumPages,
                                              });
                        return tmp6;

                    default: throw new Exception();
                }
            }
        }

    }
}
