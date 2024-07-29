using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NoteBlog.Dtos.AccountDto;
using NoteBlog.Dtos.AppUserDtos;
using NoteBlog.mappers;
using NoteBlog.Models;

namespace NoteBlog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == loginDto.Email.ToLower());

            if (user == null) return Unauthorized("Invalid username!");

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) return Unauthorized("Username not found and/or password incorrect");
            
            return Ok();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                var appUser = new AppUser()
                {
                    Surname = registerDto.Surname,
                    Name = registerDto.Name,
                    Email = registerDto.Email,
                    UserName = registerDto.Username
                };

                var createdUser = await _userManager.CreateAsync(appUser, registerDto.Password);

                if (!createdUser.Succeeded)
                    return StatusCode(500, createdUser.Errors);

                var roleResult = await _userManager.AddToRoleAsync(appUser, "User");

                if (!roleResult.Succeeded)
                    return StatusCode(500, roleResult.Errors);

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var user = await _userManager.Users.Include(u=>u.Links).FirstOrDefaultAsync(u => Equals(u.Id, id));

                if (user == null) return NotFound();

                return Ok(user.FromAppUserToAppUserDetailsDto());
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDetails([FromRoute] string id, [FromBody] AppUserUpdateDto appUserUpdateDto)
        {
            try
            {
                var user = await _userManager.Users.FirstOrDefaultAsync(u => Equals(u.Id, id));

                if (user == null) return NotFound();

                user.Name = appUserUpdateDto.Name;
                user.Surname = appUserUpdateDto.Surname;
                user.Bio = appUserUpdateDto.Bio;
                user.Email = appUserUpdateDto.Email;
                user.UserName = appUserUpdateDto.UserName;

                await _userManager.UpdateAsync(user);

                return Ok(user.FromAppUserToAppUserDetailsDto());
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }   
        }
    }
}
