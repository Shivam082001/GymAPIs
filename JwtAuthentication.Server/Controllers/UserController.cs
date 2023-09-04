using System;
using System.Web.Http;
using Microsoft.AspNetCore.Mvc;
using JwtAuthentication.Server.Models;
using NuGet.Protocol.Plugins;
using System.Net.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

using System.Text;
using System.Linq;

namespace JwtAuthentication.Server.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/users")]
    public class UserController : ApiController
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private IUserService _userService;

        public UserController()
        {
            _userService = new UserService();
        }

        [AllowAnonymous]
        [Microsoft.AspNetCore.Mvc.HttpPost("authenticate")]
        public IHttpActionResult Authenticate([Microsoft.AspNetCore.Mvc.FromBody] LoginModel model)
        {
            var user = _userService.Authenticate(model.UserID, model.Password);

            if (user == null)
                return BadRequest("Username or password is incorrect");

            return Ok(user);
        }
        

        [Models.Authorize(Roles = Role.Admin)]
        [Microsoft.AspNetCore.Mvc.HttpGet]
        public IHttpActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        [System.Web.Http.Route("{id}")]
        [Models.Authorize]
        public IHttpActionResult GetById(int id)
        {
            var currentUser = (User)User.Identity;

            if (id != currentUser.UserId && !User.IsInRole(Role.Admin))
                return Unauthorized();

            var user = _userService.GetById(id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }
        
    }
}
