using BookStoreVer2.Lib.Models;
using System.Threading.Channels;

namespace BookStoreVer2.Lib.DB
{

    public enum KeyPablishingChang
    {
        namePublishingHous = 1,
        adress
    }

    public static class TablePublishingHousesDB
    {
        /// <summary>
        /// Добавляет нового издателя в таблицу БД
        /// </summary>
        /// <param name="name"></param>
        /// <param name="address"></param>
        /// <exception cref="Exception"></exception>
        public static void AddPublishere(string name, string address)
        {
            using (DbBookStore db = new DbBookStore())
            {
                var searchPublisher = db.TablePublishingHouses.Any(p => p.NamePubHouse == name
                                                 && p.Address == address && p.IsDeleted == false);
                if (!searchPublisher)                        // ?????????????????????????
                {
                    db.TablePublishingHouses.Add(new PublishingHouse { NamePubHouse = name, Address = address });
                    db.SaveChanges();
                }
                else throw new Exception();                  // ?????????????????????????
            }
        }

        /// <summary>
        /// Изменение имени или адреса издательства
        /// </summary>
        /// <param name="name"></param>
        /// <param name="address"></param>
        /// <param name="changes"></param>
        /// <param name="key"></param>
        /// <exception cref="Exception"></exception>
        public static void UbdatePublisher(string name, string address, string changes, KeyPablishingChang key)
        {
            using (DbBookStore db = new DbBookStore())
            {
                switch (key)
                {
                    case KeyPablishingChang.namePublishingHous:
                        var searchPublisher1 =db.TablePublishingHouses.ElementAt(SearchPablisherId(name, address));
                        searchPublisher1.NamePubHouse = changes;
                        db.SaveChanges();
                        break;
                    case KeyPablishingChang.adress:
                        var searchPublisher2 = db.TablePublishingHouses.ElementAt(SearchPablisherId(name, address));
                        searchPublisher2.Address = changes;
                        db.SaveChanges();
                        break;
                    default: throw new Exception();
                }
            }
        }

        /// <summary>
        /// Отмечает издательство как удаленное
        /// </summary>
        /// <param name="name"></param>
        /// <param name="address"></param>
        public static void DeletedPublisher(string name, string address)
        {
            using (DbBookStore db = new DbBookStore())
            {
                var publisher = db.TablePublishingHouses.ElementAt(SearchPablisherId(name, address));
                publisher.IsDeleted = true;
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Поиск издательства в таблице БД
        /// </summary>
        /// <param name="name"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        public static int SearchPablisherId(string name, string address)
        {
            using (DbBookStore db = new DbBookStore())
                return (db.TablePublishingHouses.FirstOrDefault(ph => ph.NamePubHouse == name 
                                                                  && ph.Address == address
                                                                  && ph.IsDeleted == false)).Id;
        }

        /// <summary>
        /// Поиск издательства в таблице БД
        /// </summary>
        /// <param name="name"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        public static int SearchPablisherId(string name)
        {
            using (DbBookStore db = new DbBookStore())
                return (db.TablePublishingHouses.FirstOrDefault(ph => ph.NamePubHouse == name
                                                                  && ph.IsDeleted == false)).Id;
        }
    }
}
