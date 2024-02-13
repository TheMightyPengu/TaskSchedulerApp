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
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_tasksRepository.TaskExistsById(tid))
                return NotFound();

            var task = _mapper.Map<TasksDto>(_tasksRepository.GetTaskById(tid));

            return Ok(task);
        }

        [HttpGet("date/{date}")]
        public IActionResult GetTasksByDate(DateTime date)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_tasksRepository.TasksExistsByDue(date))
                return NotFound();

            var task = _mapper.Map<List<TasksDto>>(_tasksRepository.GetTasksByDue(date));

            return Ok(task);
        }

        [HttpGet("uid/{uid}")]
        public IActionResult GetTasksByUid(string uid)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_tasksRepository.TasksExistsByUid(uid))
                return NotFound();

            //var tasks = _mapper.Map<TasksDto>(_tasksRepository.GetTasksByUid(uid));
            var tasks = _tasksRepository.GetTasksByUid(uid).Select(t => _mapper.Map<TasksDto>(t)).ToList();

            if (tasks == null || !tasks.Any())
                return Ok("User has no tasks");

            return Ok(tasks);
        }

        [HttpPost("createtask")]
        public IActionResult CreateTask([FromBody, Required] TasksDto newTask)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingTask = _tasksRepository.GetTasks()
                .FirstOrDefault(t => t.Description.Trim().ToUpper() == newTask.Description.Trim().ToUpper());

            if (existingTask != null)
            {
                ModelState.AddModelError("", "Task already exists, change Description");
                return BadRequest(ModelState);
            }

            var taskMap = _mapper.Map<Tasks>(newTask);

            if (!_tasksRepository.CreateTask(taskMap))
            {
                ModelState.AddModelError("", "Unable to save the task");
                return StatusCode(500, ModelState);
            }

            return Ok("Success");
        }


        [HttpPut("updatetask/")]
        public IActionResult UpdateTask([FromBody, Required] TasksDto UpdatedTask)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (UpdatedTask == null)
                return BadRequest("Invalid task ID");

            if (!_tasksRepository.TaskExistsById(UpdatedTask.Tid))
                return NotFound("Task not found");

            var task = _mapper.Map<Tasks>(UpdatedTask);

            if (!_tasksRepository.UpdateTask(task))
                return StatusCode(500, ModelState);

            return NoContent();
        }

        [HttpDelete("deletetask/")]
        public IActionResult DeleteTask([Required] int tid)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_tasksRepository.TaskExistsById(tid))
                return NotFound();

            var TaskToDelete = _tasksRepository.GetTaskById(tid);

            if (!_tasksRepository.DeleteTask(TaskToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting the task");
            }
            return NoContent();
        }

    }
}
