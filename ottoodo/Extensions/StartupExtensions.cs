using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OttooDo.Interface.Repository;
using OttooDo.Interface.Service;
using OttooDo.Mapper.Service;
using OttooDo.Repository.Mongo;
using OttooDo.Repository.SignalR;
using OttooDo.Service;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OttooDo.Extensions
{
    public static class StartupExtensions
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            var mongoUrl = configuration.GetValue<string>("MongoService:Host");
            var mongoDb = configuration.GetValue<string>("MongoService:DbName");
            var socketHost = configuration.GetValue<string>("Socket:Host");

            Log.Information("Mongo {@url}, {@db}", mongoUrl, mongoDb);
            Log.Information("Socket {@url}", socketHost);

            services.AddSingleton<ITaskService, TaskService>();
            services.AddSingleton<ITaskRepository>((c) => new TaskRepositoryMongo(mongoUrl, mongoDb, "Task"));
            services.AddSingleton<ITaskTransportRepository>((c) => new TaskTransportRepositorySignalR(socketHost));
        }

        public static void AutoMapperRegister(this IServiceCollection services, params Profile[] profiles)
        {
            var config = new MapperConfiguration(cfg =>
            {
                foreach (var profile in profiles)
                {
                    cfg.AddProfile(profile);
                }
            });
            var mapper = config.CreateMapper();
            mapper.ConfigurationProvider.AssertConfigurationIsValid();
            services.AddSingleton((a) => mapper);//Register mapper.
        }
    }
}
