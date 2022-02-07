using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rating.Domain;
using Rating.Infrastructure.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Rating.Application.Dto;
using Microsoft.AspNetCore.Authorization;
using Rating.Hub.Models;

namespace Rating.Hub.Services
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private RatingDbContext db;
        public AccountController(RatingDbContext context)
        {
            db = context;
        }
        [Route("Login")]
        [HttpPost]
        public async Task<ActionResult<UserDTO>> Login([FromBody] LoginModel model)
        {

            User? user = await db.Users.FirstOrDefaultAsync(u => u.Name == model.Name && u.Password == model.Password);
            if (user != null)
            {
                await Authenticate(model.Name);
                return new UserDTO(user);

            }
            else
            {
               return NotFound();
            }

        }
        [Route("Register")]
        [HttpPost]
        public async Task<ActionResult<UserDTO>> Register([FromBody] RegisterModel model)
        {
            User? user = await db.Users.FirstOrDefaultAsync(u => u.Name == model.Name);
            if (user == null)
            {
                
                db.Users.Add(new User(model.Name,model.Password,model.Email));
                await db.SaveChangesAsync();
                
                await Authenticate(model.Name);

                return new UserDTO(await db.Users.FirstAsync(u=>u.Name == model.Name));
            }
            else
            {
                return BadRequest($"User with name {model.Name} exists");
            }
            
            
        }
       
        [Route("Authorize")]
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDTO>> Authorize()
        {
            var user = await db.Users.FirstAsync(u => u.Name == User.Identity!.Name);
            return new UserDTO( user);
        }
       
        private async Task Authenticate(string userName)
            {
                // создаем один claim
                var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
                // создаем объект ClaimsIdentity
                ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                // установка аутентификационных куки

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
            }
            [Route("Logout")]
            public async Task<IActionResult> Logout()
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return Ok();
            }

        }
    }




