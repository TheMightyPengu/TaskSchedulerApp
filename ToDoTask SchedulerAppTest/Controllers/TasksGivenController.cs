using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
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
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var tasks = _mapper.Map<List<TasksGivenDto>>(_tasksgivenRepository.GetTasksGiven());

            if (tasks == null || !tasks.Any())
                return Ok("No tasks found.");

            return Ok(tasks);
        }

        [HttpGet("uid/{uid}")]
        public IActionResult GetTasksByUid(string TGauid)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_tasksgivenRepository.TasksGivenExistsByUid(TGauid))
                return NotFound();

            return Ok(_mapper.Map<List<TasksGivenDto>>(_tasksgivenRepository.GetTasksGivenByUid(TGauid)));
        }

        [HttpGet("id/{uid}/{tid}")]
        public IActionResult GetTaskGivenByUidAndTid(String TGauid, int TGtid)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_tasksgivenRepository.TaskGivenExistsByUidAndTid(TGauid, TGtid))
                return NotFound();

            return Ok(_mapper.Map<TasksGivenDto>(_tasksgivenRepository.GetTaskGivenByUidAndTid(TGauid, TGtid)));
        }

        [HttpGet("tid/{tid}")]
        public IActionResult GetUsersByTid(int TGtid)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_tasksgivenRepository.UsersExistsByTid(TGtid))
                return NotFound();

            var usersDto = _mapper.Map<List<UsersDto>>(_tasksgivenRepository.GetUsersByTid(TGtid));

            return Ok(usersDto);
        }

        [HttpPost("assigntask/")]
        public IActionResult CreateTaskGiven([FromBody, Required] TasksGivenUpdateDto newTaskGiven, [FromQuery, Required]string newTGauid, [FromQuery, Required]int newTGtid)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var (canCreate, errorMessage) = _tasksgivenServices.ValidateTaskGivenEntities(newTGauid, newTGtid, newTaskGiven);
           
            if (!canCreate)
            {
                ModelState.AddModelError("", errorMessage);
                return BadRequest(ModelState);
            }

            //var taskgivenmap = _mapper.Map<TasksGivenCreateDto>(CreateTaskGiven);

            if (!_tasksgivenRepository.CreateTaskGiven(newTGauid, newTGtid))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }   

            return Ok("Success");
        }

        [HttpPut("updatetaskgiven/")]
        public IActionResult UpdateTaskGiven([FromBody, Required]TasksGivenUpdateDto newTaskGiven, [FromQuery, Required]string TGauid, [FromQuery, Required]int TGtid)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var (canUpdate, errorMessage) = _tasksgivenServices.ValidateTaskGivenEntities(TGauid, TGtid, newTaskGiven);
            if (!canUpdate)
            {
                ModelState.AddModelError("", errorMessage);
                return BadRequest(ModelState);
            }

            var taskgiven = _mapper.Map<TasksGiven>(newTaskGiven);

            if (!_tasksgivenRepository.UpdateTaskGiven(taskgiven, TGauid, TGtid))
                return StatusCode(500, ModelState);

            return NoContent();
        }

        [HttpDelete("deletetaskgiven/")]
        public IActionResult DeleteTaskGiven([Required]string TGauid, [Required]int TGtid)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_tasksgivenRepository.TasksGivenExistsByUid(TGauid) || !_tasksgivenRepository.UsersExistsByTid(TGtid))
                return NotFound();

            var TaskGivenToDelete = _tasksgivenRepository.GetTaskGivenByUidAndTid(TGauid, TGtid);
            if (TaskGivenToDelete == null)
                return NotFound();

            if (!_tasksgivenRepository.DeleteTaskGiven(TaskGivenToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting the given task");
            }
            return NoContent();
        }

    }
}
