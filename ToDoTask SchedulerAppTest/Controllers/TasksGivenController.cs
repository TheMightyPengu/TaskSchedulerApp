using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using ToDoTask_SchedulerAppTest.Dto;
using ToDoTask_SchedulerAppTest.Interfaces;
using ToDoTask_SchedulerAppTest.Models;
using ToDoTask_SchedulerAppTest.Repository;
using ToDoTask_SchedulerAppTest.Services;


namespace ToDoTask_SchedulerAppTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksGivenController : Controller
    {
        private readonly ITasksGivenRepository _tasksgivenRepository;
        private readonly IMapper _mapper;
        private readonly TasksGivenServices _tasksgivenServices;

        public TasksGivenController(ITasksGivenRepository tasksgivenRepository, IMapper mapper, TasksGivenServices tasksgivenServices)
        {
            _tasksgivenRepository = tasksgivenRepository;
            _mapper = mapper;
            _tasksgivenServices = tasksgivenServices;
        }
        [HttpGet]
        public IActionResult GetTasksGiven()
        {
            var tasks = _mapper.Map<List<TasksGivenDto>>(_tasksgivenRepository.GetTasks());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (tasks == null || !tasks.Any())
                return Ok("No tasks found.");

            return Ok(tasks);
        }

        [HttpGet("uid/{uid}")]
        public IActionResult GetTasksByUid(int uid)
        {
            if (!_tasksgivenRepository.TasksExistsByUid(uid))
                return NotFound();

            var task = _mapper.Map<List<TasksGivenDto>>(_tasksgivenRepository.GetTasksByUid(uid));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(task);
        }

        [HttpGet("tid/{tid}")]
        public IActionResult GetUsersByTid(int tid)
        {
            if (!_tasksgivenRepository.UsersExistsByTid(tid))
                return NotFound();

            var task = _mapper.Map<List<UsersDto>>(_tasksgivenRepository.GetUsersByTid(tid));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(task);
        }

        [HttpPost("assigntask/")]
        public IActionResult AssignTask([FromQuery, Required]int Uid, [FromQuery, Required]int Tid)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var (CanCreate, UidEntity, TidEntity, ErrorMessage) = _tasksgivenServices.CheckAssignTask(Uid, Tid);
           
            if (!CanCreate)
            {
                ModelState.AddModelError("", ErrorMessage);
                return BadRequest(ModelState);
            }

           //var taskgivenmap = _mapper.Map<TasksGivenCreateDto>(AssignTask);

            if (!_tasksgivenRepository.AssignTask(UidEntity, TidEntity))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }   

            return Ok("Success");
        }

    }
}
