using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OttooDo.Model.Entity.Base
{
    [Serializable]
    public class EntityBase
    {
        [BsonId]
        public string Id { get; set; } = Guid.NewGuid().ToString();
    }
}
