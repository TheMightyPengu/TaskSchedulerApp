using AutoMapper;
using ToDoTask_SchedulerAppTest.Dto;
using ToDoTask_SchedulerAppTest.Models;


namespace ToDoTask_SchedulerAppTest.AutoMapper;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Users, UsersDto>();
        CreateMap<UsersDto, Users>();

        CreateMap<Tasks, TasksDto>();
        CreateMap<TasksDto, Tasks>();

        CreateMap<Reminders, RemindersDto>();
        CreateMap<RemindersDto, Reminders>();
        CreateMap<RemindersCreateDto, Reminders>();
        CreateMap<RemindersUpdateDto, Reminders>().ForMember(dest => dest.Ruid, opt => opt.Ignore()).ForMember(dest => dest.Rtid, opt => opt.Ignore());

        CreateMap<TasksGiven, TasksGivenDto>();
        CreateMap<TasksGivenDto, TasksGiven>();
        CreateMap<TasksGivenUpdateDto, TasksGiven>();
    }
}
