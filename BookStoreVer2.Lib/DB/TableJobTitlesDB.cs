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
        /// <param name="nameTitle"></param>
        /// <returns></returns>
        public int SearchJobTitle(string nameTitle)
        {
            using (DbBookStore db = new DbBookStore())
            {
                var callTitle = from title in db.TableJobTitles
                                where title.NameTitle == nameTitle
                                select title.Id;

                var listTitle = callTitle.ToList();
                return listTitle[0];
            }
        }

        /// <summary>
        /// Добавление новой должности в таблицу БД
        /// </summary>
        public void AddJobTitle(string nameTitle)
        {
            using (DbBookStore db = new DbBookStore())
            {
                var tmp = db.TableJobTitles.First(t => t.NameTitle == nameTitle);
                if (tmp.NameTitle == nameTitle)
                    Console.WriteLine($" Должность {tmp.NameTitle} уже существует\n");
                else
                {
                    db.TableJobTitles.Add(new JobTitle { NameTitle = nameTitle });
                    db.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Удаление должности из таблицы
        /// </summary>
        /// <param name="nameTitle"></param>
        public void DeletedJobTitle(string nameTitle)
        {
            using (DbBookStore db = new DbBookStore())
            {
                db.TableJobTitles.Remove(new JobTitle { NameTitle = nameTitle });
                db.SaveChanges();
            }
        }
    }
}
