using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NoteBlog.Dtos.AccountDto;
using NoteBlog.Dtos.AppUserDtos;
using NoteBlog.QueryObjects;
using NoteBlog.Interfaces;
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
        private readonly IFileService _fileService;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IEmailSender emailSender, IFileService fileService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _fileService = fileService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid) return BadRequest();
            
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == loginDto.Email.ToLower());

            if (user == null) return Unauthorized("Invalid username!");

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) return Unauthorized("Username not found and/or password incorrect");

            var userRole = await _userManager.GetRolesAsync(user);

            var cookieData = new CookieData()
            {
                Surname = user.Surname,
                Name = user.Name,
                Id = user.Id,
                Nickname = user.UserName,
                Role = userRole,
                ProfileImageName = user.ProfileImage,
            };
            
            var cookieSettings = new CookieOptions()
            {
                HttpOnly = false,
                Secure = false,
                SameSite = SameSiteMode.Lax,
            };
            
            if(loginDto.IsRemember) cookieSettings.Expires = DateTimeOffset.Now.AddDays(30);
            
            var dataJson = JsonConvert.SerializeObject(cookieData);
            
            Response.Cookies.Append("SignIn", dataJson, cookieSettings);
            
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
                
                return Ok();
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

        [HttpGet("users")]
        public async Task<IActionResult> GetAllAuthors([FromQuery] AccountQueryObject query)
        {
            var users = (IQueryable<AppUser>)_userManager.Users.Include(u => u.Links);
            
            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                
                if (query.SortBy.Equals("CreatedBlogs", StringComparison.OrdinalIgnoreCase))
                {
                    users = query.IsDescending ?  users.OrderByDescending(u => u.CreatedBlogs) :  users.OrderBy(u => u.CreatedBlogs);
                }
            }
        
            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            var appUsersModel = await users.Skip(skipNumber).Take(query.PageSize).ToListAsync();

            var userDto = appUsersModel.Select(u => u.FromAppUserToUserDto());
            
            return Ok(userDto);
        }

        [HttpPut("createProfileImage/{id}")]
        public async Task<IActionResult> CreateProfileImage([FromRoute] string id, IFormFile file)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null) return NotFound();

            if (user.ProfileImage != null)
            {
                await _fileService.DeleteFile(user.ProfileImage);
            }

            user.ProfileImage = await _fileService.UploadFile(file);

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded) return StatusCode(500, "Server error");
            
            return Ok(user.FromAppUserToUserDto());
        }
    }
}
