
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

using Microsoft.AspNetCore.Mvc;


using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

using Microsoft.EntityFrameworkCore;
using System.Web.Http.Results;
using NuGet.Common;
using System.Net.Http;
using NuGet.Protocol.Plugins;
using JwtAuthentication.Server.Controllers;

namespace JwtAuthentication.Server.Models
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
        IEnumerable<User> GetAll();
        User GetById(int id);
    }

    public class UserService : IUserService
    {
        private List<User> _users = new List<User>
        {
            new User { UserId = 1, Username = "admin", Password = "admin", Role = Role.Admin },
            new User { UserId = 2, Username = "user", Password = "user", Role = Role.User }
        };

        public List<User> _usersList = new List<User>
        {
            new User { UserId = 1, Username = "admin", Password = "admin", Role = Role.Admin },
            new User { UserId = 2, Username = "user", Password = "user", Role = Role.User }
        };
        private readonly HttpClient _httpClient = new HttpClient();


        public IHttpActionResult GetAllMembers()
        {
            IList<Member> member = null;
            using (var ctx = new GymManagementSystemContext())
            {
                member = ctx.Members.Select(m => new Member()
                {
                    Email = m.Email,
                    Password = m.Password,
                    FirstName = m.FirstName

                }).ToList();
            }
            return (IHttpActionResult)member;
        }


        public User Authenticate(string username, string password)
        {
            var apiLink = "https://localhost:5001/api/members";
            HttpResponseMessage response =  _httpClient.GetAsync(apiLink).Result;
            if (response != null)
            {
                response.EnsureSuccessStatusCode();

                 List<Member> members =  response.Content.ReadAsAsync<List<Member>>().Result;

            if (members.SingleOrDefault(y => y.Email == username) != null)
            {
                _users.Add(new User { UserId = 4, Username = "test", Password = "test", Role = Role.User });
                Console.WriteLine(_users);
            }
            
            Member matchingMember = members.FirstOrDefault(
            m => m.Email == username && m.Password == password);
            if (matchingMember!=null)
            {
                _users.Add(new User {Username=matchingMember.Email ,Password=matchingMember.Password ,Role=Role.User});
                _users.Add(new User { UserId = 3, Username = "test", Password = "test", Role = Role.User });
            }
                
            }

            Member mem= new Member();
            mem= (Member)GetAllMembers();
            if (mem.Email == username)
            {
                _users.Add(new User { UserId = 4, Username = mem.Email, Password = mem.Password, Role = Role.User });
            }


            var user = _users.SingleOrDefault(x => x.Username == username && x.Password == password);

            if (user == null)
                return null;

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role)
                };

            var tokenOptions = new JwtSecurityToken(
                issuer: "https://localhost:5001",
                audience: "https://localhost:5001",
                claims: claims,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: signinCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            user.Token = tokenString;

            return user;

            //return Ok(new AuthenticatedResponse { Token = tokenString });

            //var tokenHandler = new JwtSecurityTokenHandler();
            //var key = Encoding.ASCII.GetBytes("superSecretKey@345"); // Replace with your secret key
            //var tokenDescriptor = new SecurityTokenDescriptor
            //{
            //    Subject = new ClaimsIdentity(new Claim[]
            //    {
            //        new Claim(ClaimTypes.Name, user.Username.ToString()),
            //        new Claim(ClaimTypes.Role, user.Role)
            //    }),
            //    Expires = DateTime.UtcNow.AddMinutes(30),
            //    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            //};
            //var token = tokenHandler.CreateToken(tokenDescriptor);
            //user.Token = tokenHandler.WriteToken(token);

            //user.Password = null; // Remove the password for security reasons
            //return user;
        }

        public User UserAuthenticate(string username, string password)
        {
            var user = _users.SingleOrDefault(x => x.Username == username && x.Password == password);

            if (user == null)
                return null;

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role)
                };

            var tokenOptions = new JwtSecurityToken(
                issuer: "https://localhost:5001",
                audience: "https://localhost:5001",
                claims: claims,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: signinCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            user.Token = tokenString;

            return user;
        }

            public IEnumerable<User> GetAll()
        {
            return _users.Select(x =>
            {
                x.Password = null;
                return x;
            });
        }

        public User GetById(int id)
        {
            var user = _users.FirstOrDefault(x => x.UserId == id);
            if (user != null)
            {
                user.Password = null;
            }
            return user;
        }
    }
}
