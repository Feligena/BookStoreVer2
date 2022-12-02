using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public static void AddBook(string nameBook,
            string lastName, string firstName, string patronimic,
            string namePubHouse, string address, int yearPublishing,
            string nameGenre, int numPages, int costPrice, int sellingPrice, int amount)
        {

        }
    }
}
