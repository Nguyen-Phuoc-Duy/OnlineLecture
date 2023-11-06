using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace OnlineLecture.Models.Domain
{
    public class ApplicationUser:IdentityUser
    {
        public string Name { get; set; }
        public string? ProfilePicture { get; set; }
        //public  Guid Id{ get; set; }
        //[Required]
        //[Required]
        //public string UserName { get; set; }
        //[Required]
        //public string Email { get; set; }
    }
}
