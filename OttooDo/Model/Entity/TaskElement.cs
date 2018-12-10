using OttooDo.Model.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OttooDo.Model.Entity
{
    public class TaskElement : EntityBase
    {
        public string Name { get; set; } = "";
        public string Explanation { get; set; } = "";
        public int FavoriteCount { get; set; } = 0;
        public string CreatedBy { get; set; } = "";
        public DateTime LastUpdatedTime { get; set; } = DateTime.UtcNow;
    }
}
