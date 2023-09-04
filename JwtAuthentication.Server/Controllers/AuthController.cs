//using JwtAuthentication.Server.Models;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;
////using System.Linq; // Add this using statement for LINQ
///*using JwtAuthentication.Server.Data;*/ // Add this using statement for your DbContext

//namespace JwtAuthentication.Server.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class AuthController : ControllerBase
//    {
//        private readonly GymManagementSystemContext _dbContext;

//        public AuthController(GymManagementSystemContext dbContext)
//        {
//            _dbContext = dbContext;
//        }

//        [HttpPost("login")]
//        public IActionResult Login([FromBody] LoginModel user)
//        {
//            if (user is null)
//            {
//                return BadRequest("Invalid client request");
//            }

//            if (user.UserName == "shivam08" && user.Password == "abc@123")
//            {
//                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
//                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

//                var claims = new List<Claim>
//                {
//                    new Claim(ClaimTypes.Name, user.UserName),
//                    new Claim(ClaimTypes.Role, "Manager")
//                };

//                var tokeOptions = new JwtSecurityToken(
//                    issuer: "https://localhost:5001",
//                    audience: "https://localhost:5001",
//                    claims: claims,
//                    expires: DateTime.Now.AddMinutes(5),
//                    signingCredentials: signinCredentials
//                );

//                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);

//                return Ok(new AuthenticatedResponse { Token = tokenString });
//            }

//            // Retrieve user data from the database
//            var dbUser = _dbContext.Members.SingleOrDefault(u => u.Email == user.UserName && u.Password == user.Password);

//            if (dbUser != null)
//            {
//                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
//                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

//                var claims = new List<Claim> 
//                { 
//                    new Claim(ClaimTypes.Name, dbUser.Email), 
//                    new Claim(ClaimTypes.Role, "Manager") 
//                };

//                var tokenOptions = new JwtSecurityToken(
//                    issuer: "https://localhost:5001",
//                    audience: "https://localhost:5001",
//                    claims: claims,
//                    expires: DateTime.Now.AddMinutes(5),
//                    signingCredentials: signinCredentials
//                );

//                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

//                return Ok(new AuthenticatedResponse { Token = tokenString });
//            }

//            return Unauthorized();
//        }
//    }
//}


//using JwtAuthentication.Server.Models;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;

//namespace JwtAuthentication.Server.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class AuthController : ControllerBase
//    {
//        [HttpPost("login")]
//        public IActionResult Login([FromBody] LoginModel user)
//        {
//            if (user is null)
//            {
//                return BadRequest("Invalid client request");
//            }

//if (user.UserName == "shivam08" && user.Password == "abc@123")
//{
//    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
//    var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

//    var claims = new List<Claim>
//                {
//                    new Claim(ClaimTypes.Name, user.UserName),
//                    new Claim(ClaimTypes.Role, "Manager")
//                };

//    var tokeOptions = new JwtSecurityToken(
//        issuer: "https://localhost:5001",
//        audience: "https://localhost:5001",
//        claims: claims,
//        expires: DateTime.Now.AddMinutes(5),
//        signingCredentials: signinCredentials
//    );

//    var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);

//    return Ok(new AuthenticatedResponse { Token = tokenString });
//}

//            return Unauthorized();
//        }
//    }
//}
