using Microsoft.AspNetCore.SignalR.Client;
using OttooDo.Interface.Repository;
using OttooDo.Model.Dto;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OttooDo.Repository.SignalR
{
    public class TaskTransportRepositorySignalR : ITaskTransportRepository
    {
        private readonly HubConnection _connection;
        private bool _isConnected = false;

        public TaskTransportRepositorySignalR(string socketUrl)
        {
            _connection = new HubConnectionBuilder()
                .WithUrl($"{socketUrl}socket/task")
                .Build();
            Log.Information("Socket Connection {@url}", $"{socketUrl}socket/task");

            _connection.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await _connection.StartAsync();
            };
        }

        public async Task Send(string processName, TaskElementDto task)
        {
            if (!_isConnected)
            {
                await _connection.StartAsync();
                _isConnected = true;
            }
            await _connection.InvokeAsync("SendTaskProcess", processName, task);
        }
    }
}
