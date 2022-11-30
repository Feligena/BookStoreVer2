using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;
using BookStoreVer2.Lib.Models;

namespace BookStoreVer2.Lib.DB
{
    public class TableHumansDB
    {
        /// <summary>
        /// Добавляем человека в таблицу БД
        /// </summary>
        /// <param name="lastName"></param>
        /// <param name="firstName"></param>
        /// <param name="patronimic"></param>
        /// <returns></returns>
        public int AddHuman(string lastName, string firstName, string patronimic)
        {
            using (DbBookStore db = new DbBookStore())
            {
                db.TableHumans.Add(new Human
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Patronymic = patronimic
                });
                db.SaveChanges();

                /*
                 return (получаем строку в коллекции, соотв. условиям лямбды)
                         .обращаемся к полям этой коллекции;
                */
                return db.TableHumans.First(h => h.FirstName == firstName
                && h.LastName == lastName
                && h.Patronymic == patronimic).Id;
            }

        }

        public void UpdateHuman(string lastName, string firstName, string patronimic)
        {
            using (DbBookStore db = new DbBookStore())
            {
                var callTitle = from title in db.TableHumans
                                where title.FirstName == firstName
                                select title;

                var Title = callTitle.ToList()[0];
                Title.FirstName = "Vasa";

                db.SaveChanges();


            }

        }
    }
}
