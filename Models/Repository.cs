using LiteDB;
using System;
using System.Collections.Generic;
using System.IO;

namespace Silver
{
    public class Repository
    {
        private LiteDatabase database;

        public Repository()
        {
            string directory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                Path.DirectorySeparatorChar + "liveinmegacity" +
                Path.DirectorySeparatorChar + "Silver";
            string fileName = directory + Path.DirectorySeparatorChar + "data.silver";

            Directory.CreateDirectory(directory);
            database = new LiteDatabase(fileName);
        }

        // private void Populate()
        // {
        //     string TransactionType = typeof(Transaction).Name;
        //     database.DropCollection(TransactionType);
        //     var collection = database.GetCollection<Transaction>();

        //     collection.Insert(new Transaction { Id = 1, Amount = 120.50M, Comment = "Фрукты", IsChanged = false });
        //     collection.Insert(new Transaction { Id = 2, Amount = 70.45M, Comment = "Кофе", IsChanged = false });
        //     collection.Insert(new Transaction { Id = 3, Amount = 89.90M, Comment = "Печенье", IsChanged = false });
        // }

        // public bool Open(string fileName)
        // {
        //     database?.Dispose();
        //     database = new LiteDatabase(fileName);
        //     return true;
        // }

        public IEnumerable<T> GetAll<T>()
        {
            var collection = database.GetCollection<T>();
            return collection.FindAll();
        }

        public void Add<T>(T entity)
        {
            var collection = database.GetCollection<T>();
            collection.Upsert(entity);
        }

        public void Remove<T>(T entity) where T : Entity, new()
        {
            var collection = database.GetCollection<T>();
            collection.Delete(entity.Id);
        }
    }
}
