using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ToDoTask_SchedulerAppTest.Dto;
using ToDoTask_SchedulerAppTest.Interfaces;
using ToDoTask_SchedulerAppTest.Repository;


namespace ToDoTask_SchedulerAppTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RemindersController : Controller
    {
        private readonly IRemindersRepository _remindersRepository;
        private readonly IMapper _mapper;
        public RemindersController(IRemindersRepository remindersRepository, IMapper mapper)
        {
            _remindersRepository = remindersRepository;
            _mapper = mapper;
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


    }
}
