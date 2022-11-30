using BookStoreVer2.Lib;

namespace BookStoreVer2.App
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var db = new DbBookStore();

            

            /*
            foreach (var book in db.TableBooks)
            {
                Console.WriteLine($"{book.NameBook} " +
                    $"{book.IdAuthorNavigation.IdHumanNavigation.LastName} " +
                    $"{book.IdAuthorNavigation.IdHumanNavigation.FirstName}");
            }
            */

            string login;
            string password;
            int answer = 0;
            do
            {
                Console.WriteLine(" Введите логин");
                login = Console.ReadLine();
                var ansLogin = db.TableAuthorizations.FirstOrDefault().Login.ToString();
                // написать проверку на поиск совподения логина и пароля в базе
                if (login == ansLogin)
                {
                    Console.WriteLine(" Введите пароль");
                    password = Console.ReadLine();
                    var ansPassword = db.TableAuthorizations.FirstOrDefault().Login.ToString();

                    if (password == ansPassword)
                    {
                        do
                        {
                            Console.WriteLine("  Выберите пункт меню:\n" +
                                " 1 - Добавить книгу\n" +
                                " 2 - Удалить книгу\n" +
                                " 3 - Найти книгу\n" +
                                " 4 - Редактировать параметры книги\n" +
                                " 5 - Новинки\n" +
                                " 6 - Самые продаваемые книги\n" +
                                " 7 - Самые популярные авторы\n" +
                                " 8 - Сымые популярные жанры\n");

                        } while (answer == 0);
                    }
                }
            } while (login == "out");

            Console.WriteLine("Hello, World!");
        }
    }
}