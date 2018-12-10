using OttooDo.Interface.Repository.Base;
using OttooDo.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OttooDo.Interface.Repository
{
    public interface ITaskRepository: IRepository<TaskElement>
    {
    }
}
