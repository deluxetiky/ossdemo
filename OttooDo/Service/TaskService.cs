using AutoMapper;
using OttooDo.Interface.Repository;
using OttooDo.Interface.Service;
using OttooDo.Model.Dto;
using OttooDo.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OttooDo.Service
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;

        public TaskService(IMapper mapper,
                            ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
        }


        public async Task<TaskElementDto> AddAsync(TaskElementDto taskElementDto)
        {
            var taskElement = new TaskElement();
            _mapper.Map(taskElementDto, taskElement);
            await _taskRepository.InsertAsync(taskElement);
            return _mapper.Map<TaskElementDto>(taskElement);
        }

        public async Task<TaskElementDto> DeleteAsync(string id)
        {
            var task = await _taskRepository.FindAsync(id);
            //if (task == null)
            //    throw new ApplicationCodedException(ErrorCodes.AppError1015);//Driver couldn't found.
            await _taskRepository.DeleteAsync(task);
            return _mapper.Map<TaskElementDto>(task);
        }

        public async Task<TaskElementDto> FindByIdAsync(string id)
        {
            var task = await _taskRepository.FindAsync(id);
            //if (record == null)
            //    throw new ApplicationCodedException(ErrorCodes.AppError1015);//Driver couldn't found.
            return _mapper.Map<TaskElementDto>(task);
        }

        public List<TaskElementDto> GetTasks()
        {
            var res = _taskRepository.GetQueryable().ToList();
            return _mapper.Map<List<TaskElementDto>>(res);
        }

        public async Task<TaskElementDto> UpdateAsync(TaskElementDto entity)
        {
            var taskDbElement = await _taskRepository.FindAsync(entity.Id);
            //if (taskDbElement == null)
            //{
            //    throw new ApplicationCodedException(ErrorCodes.AppError1015);//Driver couldn't found.
            //}
            _mapper.Map(entity, taskDbElement);
            var res = await _taskRepository.UpdateAsync(taskDbElement);
            return _mapper.Map<TaskElementDto>(taskDbElement);
        }
    }
}
