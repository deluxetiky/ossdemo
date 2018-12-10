using MongoDB.Driver;
using OttooDo.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OttooDo.Repository.Mongo.Base
{
    public class MongoGeneric<T> where T : class
    {
        protected IMongoClient _client;
        protected IMongoDatabase database;
        protected IMongoCollection<T> collection;
        protected RetryPolicyHandler<Exception> _retryHandler;

        protected FilterDefinitionBuilder<T> Builder => Builders<T>.Filter;
        public string ClientConnectionAppName { get; set; } = "";
        public MongoGeneric(string mongoUrl, string databaseName, string collectionName)
        {
            var settings = MongoClientSettings.FromUrl(MongoUrl.Create(mongoUrl));
            settings.MaxConnectionIdleTime = TimeSpan.FromMinutes(1);
            settings.SocketTimeout = TimeSpan.FromMinutes(1);
            settings.ApplicationName = ClientConnectionAppName;
            settings.ServerSelectionTimeout = TimeSpan.FromSeconds(3);
            _client = new MongoClient(settings);
            database = _client.GetDatabase(databaseName);
            collection = database.GetCollection<T>(collectionName);
            _retryHandler = new RetryPolicyHandler<Exception>(a =>
                a.Message.Contains("ExtendedSocketException") ||
                a.Message.Contains("An exception occurred while opening a connection to the server.")
            );
        }
    }
}
