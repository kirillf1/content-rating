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
using Rating.Domain.Interfaces;

namespace Rating.Hub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService<UserDTO> userService;

        public AccountController(IUserService<UserDTO> userService)
        {
            this.userService = userService;
        }
        [Route("Login")]
        [HttpPost]
        public async Task<ActionResult<UserDTO>> Login([FromBody] LoginModel model)
        {
            var user = await userService.GetUser(model.Name, model.Password);
            if (user != null)
            {
                await Authenticate(user.Name);
                return user;

            }
            else
            {
                return BadRequest("can't find user");
            }

        }
        [Route("Register")]
        [HttpPost]
        public async Task<ActionResult<UserDTO>> Register([FromBody] RegisterModel model)
        {
            var user = await userService.Register(model.Name, model.Password, model.Email);
            if (user != null)
            {
                await Authenticate(model.Name);
                return user;
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
            var user = await userService.GetUserByName(User.Identity!.Name!);
            if (user != null)
                return user;
            else
                return BadRequest("can't find user");
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
        [HttpGet]
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok();
        }

    }
}




