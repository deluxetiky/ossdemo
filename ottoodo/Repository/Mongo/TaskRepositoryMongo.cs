using OttooDo.Interface.Repository;
using OttooDo.Model.Entity;
using OttooDo.Repository.Mongo.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OttooDo.Repository.Mongo
{
    public class TaskRepositoryMongo : MongoGenericRepository<TaskElement>, ITaskRepository
    {
        public TaskRepositoryMongo(string mongoUrl, string databaseName, string collectionName) : base(mongoUrl, databaseName, collectionName)
        {

        }
    }
}
