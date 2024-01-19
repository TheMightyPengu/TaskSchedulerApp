using AutoMapper;
using ToDoTask_SchedulerAppTest.Dto;
using ToDoTask_SchedulerAppTest.Models;


namespace ToDoTask_SchedulerAppTest.AutoMapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Users, UsersDto>();
            CreateMap<Reminders, RemindersDto>();
        }
    }
}
