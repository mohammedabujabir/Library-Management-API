using Library_Management_API.BLL.DTOs.Identity;
using Library_Management_API.BLL.Helpers;
using Library_Management_API.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.EntityFrameworkCore.Metadata;
using Serilog;

namespace Library_Management_API.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Account : ControllerBase
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly AuthServices authService;

        public Account(UserManager<AppUser>userManager,SignInManager<AppUser>signInManager,RoleManager<IdentityRole>roleManager, AuthServices authService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.authService = authService;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDto register)
        {
            try
            {
                var user = new AppUser()
                {
                    FullName = register.FullName,
                    Email = register.Email,
                    PhoneNumber = register.PhoneNumber,
                    UserName = register.Email.Split("@")[0]
                };
                var result = await userManager.CreateAsync(user, register.Password);
                if (!result.Succeeded)
                {
                    Log.Information("Failed to Create User");
                    return BadRequest(400);
                }
                Log.Information("The user has been added successfully");
                return Ok("success");
            }
            catch (Exception ex) {
                Log.Error($"An error occurred while Create  user: {ex.Message}");
                return BadRequest("False:Failed to Create User");
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto login)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(login.Email);

                if (user is null)
                {
                    Log.Information("user not found");
                    return Unauthorized();
                }

                var result = await signInManager.CheckPasswordSignInAsync(user, login.Password, true);
                if (!result.Succeeded)
                {
                    Log.Information("The password entered is incorrect");
                    return Unauthorized();
                }
                var userResult = new UserDto()
                {
                    
                    Token = await authService.CreateToken(user, userManager)

                };
                Log.Information("The user has been logged in successfully");
                return Ok(userResult);
            }
            catch (Exception ex)
            {
                Log.Error($"An error occurred during user login: {ex.Message}");
                return BadRequest("False:login failed");
            }

        }
        [HttpPost("AddRole")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> AddRole(string role)
        {
            try
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    var result = await roleManager.CreateAsync(new IdentityRole(role));
                    if (result.Succeeded)
                    {
                        Log.Information("Role Added Successfully");
                        return Ok("Role Added Successfully");
                    }
                    Log.Information("Failed to create role");
                    return BadRequest(result.Errors);
                }
                Log.Information("Role Already Exists");
                return BadRequest("Role Already Exists");
            }
            catch (Exception ex)
            {
                Log.Error($"An error occurred while creating role: {ex.Message}");
                return BadRequest("False:Failed to create role");
            }

        }

        [HttpPost("AssignRole")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AssignRole(UserRoleDto userRole)
        {
            try
            {
                var user = await userManager.FindByNameAsync(userRole.UserName);
                if (user is null) {
                    Log.Information("user not found");
                    return BadRequest("user not found"); 
                }
                var result = await userManager.AddToRoleAsync(user, userRole.RoleName);
                if (result.Succeeded) {
                    Log.Information("role assigned Successfully");
                    return Ok("role assigned Successfully"); 
                }
                Log.Information("Failed to assign role");
                return BadRequest(result.Errors);
            }
            catch (Exception ex)
            {
                Log.Error($"An error occurred while assigning the role: {ex.Message}");
                return BadRequest("False:Failed to assign role");
            }


        }
    }
}
