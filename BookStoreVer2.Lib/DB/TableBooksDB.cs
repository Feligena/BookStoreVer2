using BookStoreVer2.Lib.Models;

namespace BookStoreVer2.Lib.DB
{
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

        public static void UpdateBook()
        {

        }


    }
}
