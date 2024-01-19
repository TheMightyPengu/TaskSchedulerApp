using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ToDoTask_SchedulerAppTest.Dto;
using ToDoTask_SchedulerAppTest.Interfaces;

namespace ToDoTask_SchedulerAppTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;
        public UsersController(IUsersRepository usersRepository, IMapper mapper)
        { 
            _usersRepository = usersRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _mapper.Map<List<UsersDto>>(_usersRepository.GetUsers());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(users);
        }

        [HttpGet("id/{uid}")]
        public IActionResult GetUserById(int uid)
        {
            if (!_usersRepository.UserExistsById(uid))
                return NotFound();

            var user = _mapper.Map<UsersDto>(_usersRepository.GetUserById(uid));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(user);
        }

        [HttpGet("username/{username}")]
        public IActionResult GetUserByUsername(string username)
        {
            if (!_usersRepository.UserExistsByUsername(username))
                return NotFound();

            var user = _mapper.Map<UsersDto>(_usersRepository.GetUserByUsername(username));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(user);
        }

        [HttpGet("fullname/{fullname}")]
        public IActionResult GetUserByFullname(string fullname)
        {
            if (!_usersRepository.UserExistsByFullname(fullname))
                return NotFound();

            var user = _mapper.Map<UsersDto>(_usersRepository.GetUserByFullname(fullname));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(user);
        }

    }
}
