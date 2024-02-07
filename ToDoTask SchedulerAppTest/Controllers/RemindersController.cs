using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using ToDoTask_SchedulerAppTest.Dto;
using ToDoTask_SchedulerAppTest.Interfaces;
using ToDoTask_SchedulerAppTest.Models;
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

            return _remindersServices.CheckGetReminders(reminders, ModelState);
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
        public IActionResult GetRemindersByDate(DateTime date)
        {
            if (!_remindersRepository.RemindersExistsByDate(date))
                return NotFound();

            var reminder = _mapper.Map<List<RemindersDto>>(_remindersRepository.GetRemindersByDate(date));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reminder);
        }

        [HttpGet("uid/{uid}")]
        public IActionResult GetRemindersByUid(int uid)
        {
            if (!_remindersRepository.RemindersExistsByUid(uid))
                return NotFound();

            var reminder = _mapper.Map<List<RemindersDto>>(_remindersRepository.GetRemindersByUid(uid));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reminder);
        }

        [HttpPut("updatereminder/")]
        public IActionResult UpdateReminder([FromBody, Required] RemindersUpdateDto UpdatedReminder)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var (canCreate, ruidEntity, rtidEntity, errorMessage) = _remindersServices.CheckCreateUpdateReminder(UpdatedReminder.Ruid, UpdatedReminder.Rtid);

            if (!canCreate)
            {
                ModelState.AddModelError("", errorMessage);
                return BadRequest(ModelState);
            }

            var reminder = _mapper.Map<Reminders>(UpdatedReminder);

            if (!_remindersRepository.UpdateReminder(reminder, ruidEntity, rtidEntity))
                return StatusCode(500, ModelState);

            return NoContent();
        }

        [HttpPost("createreminder/")]
        public IActionResult CreateReminder([FromQuery, Required] int Ruid, [FromQuery, Required] int Rtid, [FromBody, Required] RemindersCreateDto CreateReminder)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var (canCreate, ruidEntity, rtidEntity, errorMessage) = _remindersServices.CheckCreateUpdateReminder(Ruid, Rtid);

            if (!canCreate)
            {
                ModelState.AddModelError("", errorMessage);
                return BadRequest(ModelState);
            }

            var reminder = _mapper.Map<Reminders>(CreateReminder);

            if (!_remindersRepository.CreateReminder(reminder, ruidEntity, rtidEntity))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Success");
        }

        [HttpDelete("deletereminder/")]
        public IActionResult DeleteReminder([Required] int rid)
        {

            if (!_remindersRepository.ReminderExistsById(rid))
                return NotFound();

            var ReminderToDelete = _remindersRepository.GetReminderById(rid);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_remindersRepository.DeleteReminder(ReminderToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting the reminder");
            }
            return NoContent();
        }
    }
}
