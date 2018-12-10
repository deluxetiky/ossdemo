using OttooDo.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OttooDo.Interface.Service
{
    public interface ITaskService
    {
        Task<TaskElementDto> AddAsync(TaskElementDto taskElementDto);
        Task<TaskElementDto> DeleteAsync(string id);
        Task<TaskElementDto> FindByIdAsync(string id);
        List<TaskElementDto> GetTasks();
        Task<TaskElementDto> UpdateAsync(TaskElementDto entity);
    }
}
