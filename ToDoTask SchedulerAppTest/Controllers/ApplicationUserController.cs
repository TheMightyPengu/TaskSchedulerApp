using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ToDoTask_SchedulerAppTest.Models;
using ToDoTask_SchedulerAppTest.Dto;
using AutoMapper;
using ToDoTask_SchedulerAppTest.Services;

namespace ToDoTask_SchedulerAppTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMapper _mapper;
        private readonly ApplicationUserServices _applicationUserServices;

        public ApplicationUserController(UserManager<ApplicationUser> userManager, IMapper mapper, SignInManager<ApplicationUser> signInManager, ApplicationUserServices applicationUserServices)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _applicationUserServices = applicationUserServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = _userManager.Users.ToList();
            var userDtos = _mapper.Map<List<GetUsersDto>>(users);
            return Ok(userDtos);
        }

        [HttpGet("{auid}")]
        public async Task<IActionResult> GetUserById(string auid)
        {
            var user = await _userManager.FindByIdAsync(auid);
            if (user == null)
            {
                return NotFound();
            }
            var userDto = _mapper.Map<GetUsersDto>(user);
            return Ok(userDto);
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = new ApplicationUser {
                UserName = model.UserName,
                Email = model.Email,
                FullName = model.FullName
            };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return Ok(new { Message = "User registered successfully" });
            }
            else {
                return BadRequest(result.Errors);
            }

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return Unauthorized(new { Message = "Invalid email" });

            var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, model.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
                return Ok(new { Message = "User logged in successfully" });
            else
                return Unauthorized(new { Message = "Invalid password" });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok(new { Message = "User logged out successfully" });
        }

        [HttpPut("edituser/{userId}")]
        public async Task<IActionResult> EditUser(string uid, [FromBody] GetUsersDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByIdAsync(uid);
            if (user == null)
                return NotFound($"User with ID {uid} not found.");

            user.Email = model.Email;
            user.UserName = model.UserName;
            user.FullName = model.FullName;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
                return Ok(new { Message = "User updated successfully" });
            else
                return BadRequest(new { Message = "User could not be updated", Errors = result.Errors });
        }

        [HttpDelete("deleteuser/{uid}")]
        public async Task<IActionResult> DeleteUser(string uid)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByIdAsync(uid);
            if (user == null)
                return NotFound($"User with ID {uid} not found.");

             await _applicationUserServices.DeleteReminderAndTaskGiven(uid);

                var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
                return Ok(new { Message = "User deleted successfully" });
            else
                return BadRequest(new { Message = "User could not be deleted", Errors = result.Errors });
        }
    }
}
