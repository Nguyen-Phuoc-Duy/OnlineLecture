using System.ComponentModel.DataAnnotations;

namespace OnlineLecture.Models.DTO
{
    public class LoginModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
