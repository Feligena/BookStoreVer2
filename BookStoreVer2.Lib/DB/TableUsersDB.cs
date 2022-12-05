using BookStoreVer2.Lib.Models;

namespace BookStoreVer2.Lib.DB
{

    public enum KeyUpdateUser
    {
        firstName = 1,
        lastName,
        patronimic
    };

    public static class TableUsersDB
    {
        /// <summary>
        /// Создание нового пользователя
        /// </summary>
        /// <param name="lastName"></param>
        /// <param name="firstName"></param>
        /// <param name="patronimic"></param>
        /// <param name="phone"></param>
        public static void AddUser(string lastName, string firstName, string patronimic, int phone)
        {
            using (DbBookStore db = new DbBookStore())
            {
                var searchPhone = db.TableUsers.Any(u => u.Phone == phone);
                if (!searchPhone)
                {
                    db.TableUsers.Add(new User
                    {
                        IdHuman = TableHumansDB.AddHuman(lastName, firstName, patronimic),
                        Phone = phone
                    });
                    db.SaveChanges();
                }
                else throw new Exception();                  // ????????????????????
            }
        }

        /// <summary>
        /// Изменение имени, фамилии или отчества пользователя
        /// </summary>
        /// <param name="phone"></param>
        public static void UpdateUserData(int phone, string changes, KeyUpdateUser key)
        {
            using (DbBookStore db = new DbBookStore())
            {
                switch (key)
                {
                    case KeyUpdateUser.firstName:
                        var user1 = db.TableUsers.ElementAt(SearchUserId(phone));
                        user1.IdHumanNavigation.FirstName = changes;
                        db.SaveChanges();
                        break;
                    case KeyUpdateUser.lastName:
                        var user2 = db.TableUsers.ElementAt(SearchUserId(phone));
                        user2.IdHumanNavigation.LastName = changes;
                        db.SaveChanges();
                        break;
                    case KeyUpdateUser.patronimic:
                        var user3 = db.TableUsers.ElementAt(SearchUserId(phone));
                        user3.IdHumanNavigation.Patronymic = changes;
                        db.SaveChanges();
                        break;
                    default: throw new Exception();              // ??????????????????????????????
                }
            }
        }

        /// <summary>
        /// Изменение номера телефона
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="changes"></param>
        public static void UpdateUserPhone(int phone, int changPhone)
        {
            using (DbBookStore db = new DbBookStore())
            {
                var searchPhone = db.TableUsers.Any(u => u.Phone == changPhone);
                if (!searchPhone)
                {
                    var user = db.TableUsers.ElementAt(SearchUserId(phone));
                    user.Phone = changPhone;
                    db.SaveChanges();
                }
                else throw new Exception();                  // ????????????????????????????
            }
        }

        /// <summary>
        /// Отмечает пользователя как удаленного
        /// </summary>
        /// <param name="phone"></param>
        public static void DeletedUser(int phone)
        {
            using (DbBookStore db = new DbBookStore())
            {
                var user = db.TableUsers.ElementAt(SearchUserId(phone));
                user.IsDeleted = true;
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Поиск id пользователя по номеру телефона
        /// </summary>
        /// <param name="phone"></param>
        public static int SearchUserId(int phone)
        {
            using (DbBookStore db = new DbBookStore())
                return db.TableUsers.FirstOrDefault(u => u.Phone == phone && u.IsDeleted == false).Id;
        }

    }
}
