using Microsoft.AspNetCore.SignalR.Client;
using OttooDo.Interface.Repository;
using OttooDo.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OttooDo.Repository.SignalR
{
    public class TaskTransportRepositorySignalR : ITaskTransportRepository
    {
        private readonly HubConnection _connection;

        public TaskTransportRepositorySignalR(string socketUrl)
        {
            _connection = new HubConnectionBuilder()
                .WithUrl($"{socketUrl}socket/task")
                .Build();
            _connection.StartAsync().Wait();
        }

        public async Task Send(string processName, TaskElementDto task)
        {
            await _connection.InvokeAsync("SendTaskProcess", processName, task);
        }
    }
}
