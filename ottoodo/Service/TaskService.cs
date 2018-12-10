using AutoMapper;
using OttooDo.Extensions;
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
        private readonly ITaskTransportRepository _taskTransportRepository;

        public TaskService(IMapper mapper,
                           ITaskRepository taskRepository,
                           ITaskTransportRepository taskTransportRepository)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
            _taskTransportRepository = taskTransportRepository;
        }

        public async Task<TaskElementDto> AddAsync(TaskElementDto taskElementDto)
        {
            var taskElement = new TaskElement();
            _mapper.Map(taskElementDto, taskElement);
            taskElement.LastUpdatedTime = DateTime.UtcNow;
            var taskElementDtoResult = _mapper.Map<TaskElementDto>(taskElement);
            await _taskTransportRepository.Send(SocketConstants.TaskAdd, taskElementDtoResult);
            await _taskRepository.InsertAsync(taskElement);
            return taskElementDtoResult;
        }

        public async Task<TaskElementDto> DeleteAsync(string id)
        {
            var task = await _taskRepository.FindAsync(id);
            task.LastUpdatedTime = DateTime.UtcNow;
            var taskElementDto = _mapper.Map<TaskElementDto>(task);
            await _taskTransportRepository.Send(SocketConstants.TaskDelete, taskElementDto);
            await _taskRepository.DeleteAsync(task);
            return taskElementDto;
        }

        public async Task<TaskElementDto> FindByIdAsync(string id)
        {
            var task = await _taskRepository.FindAsync(id);
            return _mapper.Map<TaskElementDto>(task);
        }

        public List<TaskElementDto> GetTasks()
        {
            var res = _taskRepository.GetQueryable().ToList();
            return _mapper.Map<List<TaskElementDto>>(res);
        }

        public async Task<TaskElementDto> AddFavorite(string id)
        {
            var taskDbElement = await _taskRepository.FindAsync(id);
            taskDbElement.FavoriteCount++;
            taskDbElement.LastUpdatedTime = DateTime.UtcNow;
            var taskDbElementDto = _mapper.Map<TaskElementDto>(taskDbElement);
            await _taskTransportRepository.Send(SocketConstants.TaskFavorite, taskDbElementDto);
            await _taskRepository.UpdateAsync(taskDbElement);
            return taskDbElementDto;
        }

        public async Task<TaskElementDto> UpdateAsync(TaskElementDto entity)
        {
            var taskDbElement = await _taskRepository.FindAsync(entity.Id);
            _mapper.Map(entity, taskDbElement);
            taskDbElement.LastUpdatedTime = DateTime.UtcNow;
            var taskElementDto = _mapper.Map<TaskElementDto>(taskDbElement);
            await _taskTransportRepository.Send(SocketConstants.TaskUpdate, taskElementDto);
            await _taskRepository.UpdateAsync(taskDbElement);
            return taskElementDto;
        }
    }
}
