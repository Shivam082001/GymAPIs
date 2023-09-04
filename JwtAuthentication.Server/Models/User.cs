using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JwtAuthentication.Server.Models;

public partial class User
{
    [Key]
    [StringLength(5, ErrorMessage = "AdminID Length Cannot Exceeds 5 Character")]
    public int UserId { get; set; } = 0!;
    [Required(ErrorMessage = "User Name is Required")]
    [StringLength(int.MaxValue, ErrorMessage = "Length Exceeds")]
    public string Username { get; set; } = null!;
    [Required(ErrorMessage = "Password is Required")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;
    [Required(ErrorMessage = "Role Should be Defined")]
    [RegularExpression(@"^(Admin|Student|Faculty)$", ErrorMessage = "Invalid Role")]
    public string? Role { get; set; }
    public string Token { get; internal set; }
}

//using System.ComponentModel.DataAnnotations;

//namespace JwtAuthentication.Server.Models
//{
//    public class User
//    {
//        [Key]
//        public int UserId { get; set; }

//        [Required]
//        [MaxLength(50)]
//        public string UserName { get; set; }

//        [Required]
//        [MaxLength(100)]
//        public string Password { get; set; }
//    }
//}
