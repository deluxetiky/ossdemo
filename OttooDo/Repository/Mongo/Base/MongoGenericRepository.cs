using MongoDB.Driver;
using OttooDo.Interface.Repository.Base;
using OttooDo.Model.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OttooDo.Repository.Mongo.Base
{
    public abstract class MongoGenericRepository<T> : MongoGeneric<T>, IRepository<T> where T : EntityBase
    {
        public MongoGenericRepository(string mongoUrl, string databaseName, string collectionName) : base(mongoUrl, databaseName, collectionName)
        {
            ClientConnectionAppName = "WebApis";
        }

        public Task<bool> DeleteAsync(T model)
        {
            return _retryHandler.DoWithRetry(async () =>
            {
                var res = await collection.DeleteOneAsync(a => a.Id == model.Id);
                return res.DeletedCount > 0;
            });
        }

        public IQueryable<T> GetQueryable()
        {
            return collection.AsQueryable();
        }

        public Task InsertAsync(T entity)
        {
            return _retryHandler.DoWithRetry(async () => await collection.InsertOneAsync(entity));
        }

        public Task<bool> UpdateAsync(T entity)
        {
            return _retryHandler.DoWithRetry(async () => {
                var filter = Builders<T>.Filter.Eq(a => a.Id, entity.Id);
                var res = await collection.ReplaceOneAsync(filter, entity);
                return res.ModifiedCount == 1;
            });
        }

        public Task<T> FindAsync(string Id)
        {
            return _retryHandler.DoWithRetry(async () => await collection.Find(a => a.Id == Id).FirstOrDefaultAsync());
        }
    }
}
