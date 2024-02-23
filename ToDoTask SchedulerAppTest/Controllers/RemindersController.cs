using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;
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

            return _remindersServices.ValidateGetReminders(reminders, ModelState);
        }

        [HttpGet("id/{rid}")]
        public IActionResult GetReminderById(int rid)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_remindersRepository.ReminderExistsById(rid))
                return NotFound();

            var reminder = _mapper.Map<RemindersDto>(_remindersRepository.GetReminderById(rid));

            return Ok(reminder);
        }

        [HttpGet("date/{date}")]
        public IActionResult GetRemindersByDate(DateTime date)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_remindersRepository.RemindersExistsByDate(date))
                return NotFound();

            var reminder = _mapper.Map<List<RemindersDto>>(_remindersRepository.GetRemindersByDate(date));

            return Ok(reminder);
        }

        [HttpGet("uid/{uid}")]
        public IActionResult GetRemindersByUid(string uid)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_remindersRepository.RemindersExistsByUid(uid))
                return NotFound();

            var reminder = _mapper.Map<List<RemindersDto>>(_remindersRepository.GetRemindersByUid(uid));

            return Ok(reminder);
        }

        [HttpGet("tid/{tid}")]
        public IActionResult GetRemindersByTid(int tid)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_remindersRepository.RemindersExistsByTid(tid))
                return NotFound();

            var reminder = _mapper.Map<List<RemindersDto>>(_remindersRepository.GetRemindersByTid(tid));

            return Ok(reminder);
        }
        
        [HttpPost("createreminder")]
        public IActionResult CreateReminder([FromBody, Required] RemindersCreateDto newReminder)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var (canCreate, errorMessage) = _remindersServices.ValidateReminderEntities(newReminder.Rauid, newReminder.Rtid, newReminder.ReminderDate);
            if (!canCreate)
            {
                ModelState.AddModelError("", errorMessage);
                return BadRequest(ModelState);
            }

            var reminder = _mapper.Map<Reminders>(newReminder);

            reminder.Rauid = newReminder.Rauid;
            reminder.Rtid = newReminder.Rtid;

            if (!_remindersRepository.CreateReminder(reminder))
            {
                ModelState.AddModelError("", "Unable to save the reminder");
                return StatusCode(500, ModelState);
            }

            return Ok("Success");
        }

        [HttpPut("updatereminder/")]
        public IActionResult UpdateReminder([FromQuery, Required]int rid, [FromBody, Required]RemindersUpdateDto newReminder)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var reminderToUpdate = _remindersRepository.GetReminderById(rid);
            if (reminderToUpdate == null)
                return NotFound($"The reminder you want to change({rid}) does not exist.");

            var (canUpdate, errorMessage) = _remindersServices.ValidateReminderEntities(newReminder.Rauid, newReminder.Rtid, newReminder.ReminderDate);
            if (!canUpdate)
            {
                ModelState.AddModelError("", errorMessage);
                return BadRequest(ModelState);
            }

            _mapper.Map(newReminder, reminderToUpdate);

            reminderToUpdate.Rauid = newReminder.Rauid;
            reminderToUpdate.Rtid = newReminder.Rtid;

            if (!_remindersRepository.UpdateReminder(reminderToUpdate))
            {
                ModelState.AddModelError("", "Something went wrong while updating the reminder.");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("deletereminder/")]
        public IActionResult DeleteReminder([Required] int rid)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_remindersRepository.ReminderExistsById(rid))
                return NotFound();

            var ReminderToDelete = _remindersRepository.GetReminderById(rid);

            if (!_remindersRepository.DeleteReminder(ReminderToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting the reminder");
            }
            return NoContent();
        }

    }
}
