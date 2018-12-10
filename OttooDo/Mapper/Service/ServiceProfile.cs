using AutoMapper;
using OttooDo.Model.Dto;
using OttooDo.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OttooDo.Mapper.Service
{
    public class ServiceProfile : Profile
    {
        public ServiceProfile()
        {
            CreateMap<TaskElement, TaskElementDto>();
        }
    }
}
