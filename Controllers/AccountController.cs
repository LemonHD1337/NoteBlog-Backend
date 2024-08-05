using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
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
        private readonly IEmailSender _emailSender;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid) return BadRequest();
            
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == loginDto.Email.ToLower());

            if (user == null) return Unauthorized("Invalid username!");

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) return Unauthorized("Username not found and/or password incorrect");
            
            return Ok();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            if (!ModelState.IsValid) return BadRequest();
            
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
            if (!ModelState.IsValid) return BadRequest();
            
            try
            {
                var user = await _userManager.Users.FirstOrDefaultAsync(u => Equals(u.Id, id));

                if (user == null) return NotFound();

                user.Name = appUserUpdateDto.Name;
                user.Surname = appUserUpdateDto.Surname;
                user.Bio = appUserUpdateDto.Bio;
                user.Email = appUserUpdateDto.Email;
                user.UserName = appUserUpdateDto.UserName;

                var result = await _userManager.UpdateAsync(user);

                if (!result.Succeeded)
                {
                    Console.WriteLine(result.Errors);
                    return StatusCode(500, "Server error");
                }
                
                return Ok(user.FromAppUserToAppUserDetailsDto());
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }   
        }

        [HttpPost("forgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto forgotPasswordDto)
        {
            if (!ModelState.IsValid) return BadRequest();
            
            try
            {
                var appUser = await _userManager.FindByEmailAsync(forgotPasswordDto.Email);

                if (appUser == null) return BadRequest("Invalid email");

                string token = await _userManager.GeneratePasswordResetTokenAsync(appUser);

                //TODO send email with link 
                
                return Ok(new
                {
                    token = token,
                    id = appUser.Id,
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, "Server error");
            }
        }

        [HttpPost("resetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
        {
            if (!ModelState.IsValid) return BadRequest();
            
            try
            {
                var appUser = await _userManager.FindByIdAsync(resetPasswordDto.Id);

                if (appUser == null) return NotFound("User doesn't exists");

                var result = await _userManager.ResetPasswordAsync(appUser, resetPasswordDto.Token, resetPasswordDto.Password);

                if (!result.Succeeded)
                {
                    return StatusCode(500, result.Errors);
                }

                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
