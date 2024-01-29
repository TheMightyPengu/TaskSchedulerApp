using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using ToDoTask_SchedulerAppTest.Dto;
using ToDoTask_SchedulerAppTest.Interfaces;
using ToDoTask_SchedulerAppTest.Models;
using ToDoTask_SchedulerAppTest.Repository;
using ToDoTask_SchedulerAppTest.Services;


namespace ToDoTask_SchedulerAppTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RemindersController : Controller
    {
        private readonly IRemindersRepository _remindersRepository;
        private readonly IMapper _mapper;
        private readonly RemindersServices _remindersServices;
        public RemindersController(IRemindersRepository remindersRepository, IMapper mapper, RemindersServices remindersServices)
        {
            _remindersRepository = remindersRepository;
            _mapper = mapper;
            _remindersServices = remindersServices;
        }

        [HttpGet]
        public IActionResult GetReminders()
        {
            var reminders = _mapper.Map<List<RemindersDto>>(_remindersRepository.GetReminders());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reminders);
        }

        [HttpGet("id/{rid}")]
        public IActionResult GetReminderById(int rid)
        {
            if (!_remindersRepository.ReminderExistsById(rid))
                return NotFound();

            var reminder = _mapper.Map<RemindersDto>(_remindersRepository.GetReminderById(rid));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reminder);
        }

        [HttpGet("date/{date}")]
        public IActionResult GetReminderByDate(DateTime date)
        {
            if (!_remindersRepository.ReminderExistsByDate(date))
                return NotFound();

            var reminder = _mapper.Map<RemindersDto>(_remindersRepository.GetReminderByDate(date));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reminder);
        }

        [HttpGet("uid/{uid}")]
        public IActionResult GetReminderByUid(int uid)
        {
            if (!_remindersRepository.ReminderExistsByUid(uid))
                return NotFound();

            var reminder = _mapper.Map<RemindersDto>(_remindersRepository.GetReminderByUid(uid));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reminder);
        }

        [HttpPost("createreminder/")]
        public IActionResult CreateReminder([FromQuery, Required]int Rtid, [FromQuery, Required]int Ruid, [FromBody, Required]RemindersDto CreateReminder)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var (canCreate, ruidEntity, rtidEntity, errorMessage) = _remindersServices.CheckCreateReminder(Rtid, Ruid);

            if (!canCreate)
            {
                ModelState.AddModelError("", errorMessage);
                return BadRequest(ModelState);
            }

            var remindermap = _mapper.Map<Reminders>(CreateReminder);
            var success = _remindersRepository.CreateReminder(remindermap, ruidEntity, rtidEntity);

            if (!success)
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Success");
        }
    }
}
