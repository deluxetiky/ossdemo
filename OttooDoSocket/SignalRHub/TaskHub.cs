using Microsoft.AspNetCore.SignalR;
using OttooDoSocket.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OttooDoSocket.SignalRHub
{
    public class TaskHub : Hub
    {
        public async Task SendTaskProcess(string processName, TaskElementDto task)
        {
            await Clients.All.SendAsync("TaskProcess", processName, task);
        }
    }
}
