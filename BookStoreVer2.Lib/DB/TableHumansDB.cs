using BookStoreVer2.Lib.Models;

namespace BookStoreVer2.Lib.DB
{
    public enum KeyNameHuman
    {
        firstName = 1,
        lastName
    };

    public static class TableHumansDB
    {
        /// <summary>
        /// Добавляем человека в таблицу БД
        /// </summary>
        /// <param name="lastName"></param>
        /// <param name="firstName"></param>
        /// <param name="patronimic"></param>
        /// <returns></returns>
        public static int AddHuman(string lastName, string firstName, string patronimic)
        {
            using (DbBookStore db = new DbBookStore())
            {
                db.TableHumans.Add(new Human()
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Patronymic = patronimic
                });
                db.SaveChanges();

                return (db.TableHumans.Last(h => h.FirstName == firstName
                                              && h.LastName == lastName
                                              && h.Patronymic == patronimic
                                              && h.IsDeleted == false)).Id;
            }

        }
        /*
        /// <summary>
        /// Вносит изменение имени существующую запись
        /// </summary>
        /// <param name="lastName"></param>
        /// <param name="firstName"></param>
        /// <param name="patronimic"></param>
        public void UpdateHuman(string lastName, string firstName, string patronimic)
        {
            using (DbBookStore db = new DbBookStore())
            {
                var searchHuman = SearchHuman(lastName, firstName, patronimic);
                
                var callTitle = from title in db.TableHumans
                                where title.FirstName == firstName
                                select title;

                var Title = callTitle.ToList()[0];
                Title.FirstName = "Vasa";
                
                
                db.SaveChanges();
            }
        }
        */

        /// <summary>
        /// Вносит изменение имени в существующую запись таблицы
        /// </summary>
        /// <param name="lastName"></param>
        /// <param name="firstName"></param>
        /// <param name="patronimic"></param>
        /// <param name="changeName"></param>
        public static void UpdateFirstName(string lastName, string firstName, string patronimic, string changeName)
        {
            using (DbBookStore db = new DbBookStore())
            {
                var human = db.TableHumans.ElementAt(SearchHumanId(lastName, firstName, patronimic));
                human.FirstName = changeName;
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Вносит изменение фамилии в существующую запись таблицы
        /// </summary>
        /// <param name="lastName"></param>
        /// <param name="firstName"></param>
        /// <param name="patronimic"></param>
        /// <param name="changeName"></param>
        public static void UpdateLastName(string lastName, string firstName, string patronimic, string changeName)
        {
            using (DbBookStore db = new DbBookStore())
            {
                var human = db.TableHumans.ElementAt(SearchHumanId(lastName, firstName, patronimic));
                human.LastName = changeName;
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Вносит изменение отчества в существующую запись таблицы
        /// </summary>
        /// <param name="lastName"></param>
        /// <param name="firstName"></param>
        /// <param name="patronimic"></param>
        /// <param name="changeName"></param>
        public static void UpdatePatronimic(string lastName, string firstName, string patronimic, string changeName)
        {
            using (DbBookStore db = new DbBookStore())
            {
                var human = db.TableHumans.ElementAt(SearchHumanId(lastName, firstName, patronimic));
                human.Patronymic = changeName;
                db.SaveChanges();
            }
        }

        /*
        /// <summary>
        /// Удаляет запись о человеке из таблицы
        /// </summary>
        /// <param name="lastName"></param>
        /// <param name="firstName"></param>
        /// <param name="patronimic"></param>
        public static void DropHuman(string lastName, string firstName, string patronimic)
        {
            using (DbBookStore db = new DbBookStore())
            {
                db.TableHumans.Remove(db.TableHumans.ElementAt(SearchHumanId(lastName, firstName, patronimic)));      //????????
                db.SaveChanges();
            }
        }
        */

        /// <summary>
        /// Помечает запись о человеке как удаленную
        /// </summary>
        /// <param name="lastName"></param>
        /// <param name="firstName"></param>
        /// <param name="patronimic"></param>
        public static void DeletedHuman(string lastName, string firstName, string patronimic)
        {
            using (DbBookStore db = new DbBookStore())
            {
                var human = db.TableHumans.ElementAt(SearchHumanId(lastName, firstName, patronimic));
                human.IsDeleted = true;
                db.SaveChanges();
            }
                 
        }

        /// <summary>
        /// Поиск первого человека в списке по имени, фамилии, отчеству
        /// </summary>
        /// <param name="lastName"></param>
        /// <param name="firstName"></param>
        /// <param name="patronimic"></param>
        /// <returns></returns>
        public static int SearchHumanId(string lastName, string firstName, string patronimic)  
        {
            using (DbBookStore db = new DbBookStore())
            {
                var tmp = from human in db.TableHumans
                                where human.LastName == lastName
                                && human.FirstName == firstName
                                && human.Patronymic == patronimic
                                && human.IsDeleted == false
                          select human;
                if(tmp.Count() > 1)
                    return -1; /* проверка, что получили. Если коллекция, то вызвать другой метод,
                                 через эксепшн, что несколько таких человек есть*/
                else
                {
                    var listHuman = tmp.ToList()[0];
                    return listHuman.Id;
                }
            }
        }
        /*
        /// <summary>
        /// Поиск первого человека в списке по имени, фамилии
        /// </summary>
        /// <param name="lastName"></param>
        /// <param name="firstName"></param>
        /// <returns></returns>
        public static int SearchHumanId(string lastName, string firstName)
        {
            using (DbBookStore db = new DbBookStore())
            {
                var tmp = from human in db.TableHumans
                          where human.LastName == lastName
                          && human.FirstName == firstName
                          select human;

                var listHuman = tmp.ToList()[0];
                return listHuman.Id;
            }
        }

        /// <summary>
        /// Поиск первого человека в списке по имени или фамилии
        /// </summary>
        /// <param name="lastName"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static int SearchHumanId(string name,KeyNameHuman key)
        {
            using (DbBookStore db = new DbBookStore())
            {
                switch (key)
                {
                    case KeyNameHuman.firstName: // ищем по имени
                        var tmp2 = from human in db.TableHumans
                                   where human.FirstName == name
                                   select human;
                        var listHuman2 = tmp2.ToList()[0];
                        return listHuman2.Id;
                    case KeyNameHuman.lastName:// ищем по фамилии 
                        var tmp1 = from human in db.TableHumans
                                  where human.LastName == name
                                  select human;
                        var listHuman1 = tmp1.ToList()[0];
                        return listHuman1.Id;

                    default: throw new ArgumentOutOfRangeException(nameof(key)); // прописать Эксепшены!
                }
            }
        }
        */

        /// <summary>
        /// Поиск всех существующих людей по имени, фамилии и отчеству
        /// </summary>
        /// <param name="lastName"></param>
        /// <param name="firstName"></param>
        /// <param name="patronimic"></param>
        /// <returns></returns>
        public static IEnumerable<Human> SearchHuman(string lastName, string firstName, string patronimic)
        {
            using (DbBookStore db = new DbBookStore())
            {
                var tmp = from human in db.TableHumans
                          where human.LastName == lastName
                          && human.FirstName == firstName
                          && human.Patronymic == patronimic
                          && human.IsDeleted == false
                          select human;

                return tmp.AsEnumerable<Human>();    
            }
        }

        /// <summary>
        /// Поиск всех существующих людей по имени, фамилии
        /// </summary>
        /// <param name="lastName"></param>
        /// <param name="firstName"></param>
        /// <returns></returns>
        public static IEnumerable<Human> SearchHuman(string lastName, string firstName)
        {
            using (DbBookStore db = new DbBookStore())
            {
                var tmp = from human in db.TableHumans
                          where human.LastName == lastName
                          && human.FirstName == firstName
                          && human.IsDeleted == false
                          select human;

                return tmp.AsEnumerable<Human>();
            }
        }

        /// <summary>
        /// Поиск всех существующих людей по имени или фамилии
        /// </summary>
        /// <param name="lastName"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static IEnumerable<Human> SearchHuman(string name, KeyNameHuman key)
        {
            using (DbBookStore db = new DbBookStore())
            {
                switch (key)
                {
                    case KeyNameHuman.lastName:// ищем по фамилии 
                        var tmp1 = from human in db.TableHumans
                                   where human.LastName == name
                                   && human.IsDeleted == false
                                   select human;
                        
                        return tmp1.AsEnumerable<Human>();

                    case KeyNameHuman.firstName: // ищем по имени
                        var tmp2 = from human in db.TableHumans
                                   where human.FirstName == name
                                   && human.IsDeleted == false
                                   select human;
                        
                        return tmp2.AsEnumerable<Human>();

                    default: throw new ArgumentOutOfRangeException(nameof(key)); // прописать Эксепшены!
                }
            }

        }

    }
}
