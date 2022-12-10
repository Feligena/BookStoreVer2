using BookStoreVer2.Lib;
using BookStoreVer2.Lib.DB;

namespace BookStoreVer2.App
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //var db = new DbBookStore();

            string login;
            string password;
            int answer = 0;
            do
            {
                Console.WriteLine(" Введите логин");
                login = Console.ReadLine();
                Console.WriteLine(" Введите пароль");
                password = Console.ReadLine();

                if (TableAuthorizationsDB.CheckAuthorizatoin(login, password))
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
                else 
                    Console.WriteLine(" Не верный логин или пароль. Попробуйте снова");
               
            } while (login == "out");


            //пример работы с анонимным классом
            var employeeShow = TableEmployeesDB.SearchEmployee("Anonim", "anonimus", "Anonimov");

            foreach (var item in employeeShow)
            {
                Console.WriteLine($" {item.LastName}, {item.FirstName}, {item.Patronimic}, {item.JobTitle}");
            }

        }

    }
}