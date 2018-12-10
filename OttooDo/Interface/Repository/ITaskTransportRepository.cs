using OttooDo.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OttooDo.Interface.Repository
{
    public interface ITaskTransportRepository
    {
        Task Send(string processName, TaskElementDto task);
    }
}
