using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using LiteDB;

namespace Silver
{
    public class Repository
    {
        private string filename = "data.silver";

        public Repository()
        {
            // string filename = Assembly.GetExecutingAssembly().CodeBase;
            // filename = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            // filename += Path.DirectorySeparatorChar;
            // filename += "data.silver";

            // filename = "data.silver";

            // Open database (or create if doesn't exist)
            using (var db = new LiteDatabase(filename))
            {
                // Get a collection (or create, if doesn't exist)
                string TransactionType = typeof(Transaction).Name;
                var col = db.GetCollection<Transaction>(TransactionType);

                // Insert new customer document (Id will be auto-incremented)
                col.Delete(x => true);
                col.Insert(new Transaction { Id = 1, Amount = 120.50M, Comment = "Фрукты", IsChanged = false });
                col.Insert(new Transaction { Id = 2, Amount = 70.45M, Comment = "Кофе", IsChanged = false });
                col.Insert(new Transaction { Id = 3, Amount = 89.90M, Comment = "Печенье", IsChanged = false });
            }
        }

        public IEnumerable<T> GetAll<T>()
        {
            using (var db = new LiteDatabase(filename))
            {
                string EntityType = typeof(T).Name;
                var col = db.GetCollection<T>();
                return col.FindAll();
            }
        }

        public void Add<T>(T entity)
        {
            using (var db = new LiteDatabase(filename))
            {
                string EntityType = typeof(T).Name;
                var col = db.GetCollection<T>();
                col.Upsert(entity);
            }
        }
    }
}
