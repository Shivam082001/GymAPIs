using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace JwtAuthentication.Server.Models
{
    public class LoginModel
    {
        [Key]
        public int Id { get; set; }
        public string UserID { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; }
    }
}
