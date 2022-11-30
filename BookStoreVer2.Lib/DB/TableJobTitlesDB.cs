using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using BookStoreVer2.Lib.Models;

namespace BookStoreVer2.Lib.DB
{
    public class TableJobTitlesDB
    {
        /// <summary>
        /// Поиск должности в БД
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int SearchJobTitle(string name)
        {
            using (DbBookStore db = new DbBookStore())
            {
                var callTitle = from title in db.TableJobTitles
                                where title.NameTitle == name
                                select title.Id;

                var listTitle = callTitle.ToList();
                return listTitle[0];
            }
        }

        /// <summary>
        /// Добавление нового жанра в таблицу БД
        /// </summary>
        public void AddJobTitle()
        {

        }
    }
}
