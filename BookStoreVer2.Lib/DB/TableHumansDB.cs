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
                return (db.TableHumans.First(h => h.FirstName == firstName
                && h.LastName == lastName
                && h.Patronymic == patronimic)).Id;
            }

        }

        /// <summary>
        /// Вносит изменения в существующую запись
        /// </summary>
        /// <param name="lastName"></param>
        /// <param name="firstName"></param>
        /// <param name="patronimic"></param>
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

        public void DeletedHuman(string lastName, string firstName, string patronimic)
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

        /// <summary>
        /// Поиск сотрудников по имени, фамилии, отчеству
        /// </summary>
        /// <param name="lastName"></param>
        /// <param name="firstName"></param>
        /// <param name="patronimic"></param>
        /// <returns></returns>
        public int SearchHuman(string lastName, string firstName, string patronimic)  
        {
            using (DbBookStore db = new DbBookStore())
            {
                var tmp = from human in db.TableHumans
                                where human.LastName == lastName
                                && human.FirstName == firstName
                                && human.Patronymic == patronimic
                                select human;

                var listHuman = tmp.ToList()[0];
                return listHuman.Id;
            }
        }

        /// <summary>
        /// Поиск сотрудников по имени, фамилии
        /// </summary>
        /// <param name="lastName"></param>
        /// <param name="firstName"></param>
        /// <returns></returns>
        public int SearchHuman(string lastName, string firstName)
        {

            return 0;
        }

        /// <summary>
        /// Поиск сотрудников по имени или фамилии
        /// </summary>
        /// <param name="lastName"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public int SearchHuman(string lastName,int key)
        {
            using (DbBookStore db = new DbBookStore())
            {
                switch (key)
                {
                    case 1:
                        break;
                    case 2:
                        break;
                    default: throw new ArgumentOutOfRangeException(nameof(key)); // прописать Эксепшены!
                }

                return 0;
            }
                
        }

    }
}
