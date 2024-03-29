﻿//using AutoMapper;
//using Microsoft.AspNetCore.Mvc;
//using System.ComponentModel.DataAnnotations;
//using System.Threading.Tasks;
//using ToDoTask_SchedulerAppTest.Dto;
//using ToDoTask_SchedulerAppTest.Interfaces;
//using ToDoTask_SchedulerAppTest.Models;
//using ToDoTask_SchedulerAppTest.Repository;

//namespace ToDoTask_SchedulerAppTest.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class UsersController : Controller
//    {
//        private readonly IUsersRepository _usersRepository;
//        private readonly IMapper _mapper;
//        public UsersController(IUsersRepository usersRepository, IMapper mapper)
//        {
//            _usersRepository = usersRepository;
//            _mapper = mapper;
//        }

//        [HttpGet]
//        public IActionResult GetUsers()
//        {
//            var users = _mapper.Map<List<UsersDto>>(_usersRepository.GetUsers());

//            if (!ModelState.IsValid)
//                return BadRequest(ModelState);

//            if (users == null || !users.Any())
//                return Ok("No users found.");

//            return Ok(users);
//        }

//        [HttpGet("id/{uid}")]
//        public IActionResult GetUserById(int uid)
//        {
//            if (!_usersRepository.UserExistsById(uid))
//                return NotFound();

//            var user = _mapper.Map<UsersDto>(_usersRepository.GetUserById(uid));

//            if (!ModelState.IsValid)
//                return BadRequest(ModelState);

//            return Ok(user);
//        }

//        [HttpGet("UserName/{UserName}")]
//        public IActionResult GetUserByUsername(string UserName)
//        {
//            if (!_usersRepository.UserExistsByUsername(UserName))
//                return NotFound();

//            var user = _mapper.Map<UsersDto>(_usersRepository.GetUserByUsername(UserName));

//            if (!ModelState.IsValid)
//                return BadRequest(ModelState);

//            return Ok(user);
//        }

//        [HttpGet("FullName/{FullName}")]
//        public IActionResult GetUserByFullname(string FullName)
//        {
//            if (!_usersRepository.UserExistsByFullname(FullName))
//                return NotFound();

//            var user = _mapper.Map<UsersDto>(_usersRepository.GetUserByFullname(FullName));

//            if (!ModelState.IsValid)
//                return BadRequest(ModelState);

//            return Ok(user);
//        }

//        [HttpGet("tid/{tid}")]
//        public IActionResult GetUsersbyTid(int tid)
//        {
//            if (!_usersRepository.UserExistsByTid(tid))
//                return NotFound();

//            var users = _usersRepository.GetUsersByTid(tid).Select(t => _mapper.Map<UsersDto>(t)).ToList();

//            if (users == null || !users.Any())
//                return Ok("Task has no users assigned to it");

//            if (!ModelState.IsValid)
//                return BadRequest(ModelState);

//            return Ok(users);
//        }

//        [HttpPost("createuser/")]
//        public IActionResult CreateUser([FromBody, Required]UsersDto CreateUser)
//        {
//            var users = _usersRepository.GetUsers().Where(u => u.UserName.Trim().ToUpper()== CreateUser.UserName.Trim().ToUpper()).FirstOrDefault();

//            if (users != null)
//            {
//                ModelState.AddModelError("", "UserName already exists");
//                return StatusCode(422, ModelState);
//            }

//            if (!ModelState.IsValid)
//                return BadRequest(ModelState);

//            var usermap = _mapper.Map<Users>(CreateUser);

//            if (!_usersRepository.CreateUser(usermap))
//            {
//                ModelState.AddModelError("", "Something went wrong while saving");
//                return StatusCode(500, ModelState);
//            }

//            return Ok("Success");
//        }

//        [HttpPut("updateuser/")]
//        public IActionResult UpdateUser([FromBody, Required]UsersDto UpdatedUser)
//        {
//            if (UpdatedUser == null)
//                return BadRequest("Invalid user ID");

//            if (!_usersRepository.UserExistsById(UpdatedUser.Uid))
//                return NotFound("User not found");

//            if (!ModelState.IsValid)
//                return BadRequest(ModelState);

//            var user = _mapper.Map<Users>(UpdatedUser);

//            if (!_usersRepository.UpdateUser(user))
//                return StatusCode(500, ModelState);

//            return NoContent();
//        }

//        [HttpDelete("deleteuser/")]
//        public IActionResult DeleteUser([Required]int uid) 
//        {

//            if (!_usersRepository.UserExistsById(uid))
//                return NotFound();
      
//            var UserToDelete = _usersRepository.GetUserById(uid);

//            if (!ModelState.IsValid)
//                return BadRequest(ModelState);

//            if (!_usersRepository.DeleteUser(UserToDelete))
//            {
//                ModelState.AddModelError("", "Something went wrong deleting the user");
//            }
//            return NoContent();
//        }


//    }
//}
