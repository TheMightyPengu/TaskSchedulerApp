using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using ToDoTask_SchedulerAppTest.Dto;
using ToDoTask_SchedulerAppTest.Interfaces;
using ToDoTask_SchedulerAppTest.Models;
using ToDoTask_SchedulerAppTest.Repository;

namespace ToDoTask_SchedulerAppTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : Controller
    {
        private readonly ITasksRepository _tasksRepository;
        private readonly IMapper _mapper;
        public TasksController(ITasksRepository tasksRepository, IMapper mapper)
        {
            _tasksRepository = tasksRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetTasks()
        {
            var tasks = _mapper.Map<List<TasksDto>>(_tasksRepository.GetTasks());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (tasks == null || !tasks.Any())
                return Ok("No tasks found.");

            return Ok(tasks);
        }

        [HttpGet("id/{tid}")]
        public IActionResult GetReminderById(int tid)
        {
            if (!_tasksRepository.TaskExistsById(tid))
                return NotFound();

            var task = _mapper.Map<TasksDto>(_tasksRepository.GetTaskById(tid));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(task);
        }

        [HttpGet("date/{date}")]
        public IActionResult GetTasksByDate(DateTime date)
        {
            if (!_tasksRepository.TasksExistsByDue(date))
                return NotFound();

            var task = _mapper.Map<List<TasksDto>>(_tasksRepository.GetTasksByDue(date));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(task);
        }

        [HttpGet("uid/{uid}")]
        public IActionResult GetTasksByUid(int uid)
        {
            if (!_tasksRepository.TasksExistsByUid(uid))
                return NotFound();

            //var tasks = _mapper.Map<TasksDto>(_tasksRepository.GetTasksByUid(uid));
            var tasks = _tasksRepository.GetTasksByUid(uid).Select(t => _mapper.Map<TasksDto>(t)).ToList();

            if (tasks == null || !tasks.Any())
                return Ok("User has no tasks");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(tasks);
        }

        [HttpPost("createtask/")]
        public IActionResult CreateTask([FromBody, Required]TasksDto CreateTask)
        {
            var tasks = _tasksRepository.GetTasks().Where(t => t.Description.Trim().ToUpper() == CreateTask.Description.Trim().ToUpper()).FirstOrDefault();

            if (tasks != null)
            {
                ModelState.AddModelError("", "Task already exists, change Description");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var taskmap = _mapper.Map<Tasks>(CreateTask);

            if (!_tasksRepository.CreateTask(taskmap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Success");
        }

    }
}
