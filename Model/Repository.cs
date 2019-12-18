using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using LiteDB;

namespace Silver
{
    // public class Customer
    // {
    //     public int Id { get; set; }
    //     public string Name { get; set; }
    //     public string[] Phones { get; set; }
    //     public bool IsActive { get; set; }
    // }

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

                // Create your new customer instance
                // var transaction = new Transaction { Amount = 120.50M, Comment = "Фрукты", IsChanged = false };

                // Insert new customer document (Id will be auto-incremented)
                col.Delete(x => true);
                col.Insert(new Transaction { Amount = 120.50M, Comment = "Фрукты", IsChanged = false });
                col.Insert(new Transaction { Amount = 70.45M, Comment = "Кофе", IsChanged = false });
                col.Insert(new Transaction { Amount = 89.90M, Comment = "Печенье", IsChanged = false });

                // Update a document inside a collection
                // transaction.Name = "Joana Doe";
                // col.Update(transaction);

                // Index document using document Name property
                // col.EnsureIndex(x => x.Name);

                // Use LINQ to query documents
                // var results = col.Find(x => x.Name.StartsWith("Jo"));

                // Let's create an index in phone numbers (using expression). It's a multikey index
                // col.EnsureIndex(x => x.Phones, "$.Phones[*]");

                // and now we can query phones
                // var r = col.FindOne(x => x.Phones.Contains("8888-5555"));
                // var r = col.FindOne(x => (x.Phones as IList).Contains("8888-5555"));
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
    }
}
