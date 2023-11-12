using System.ComponentModel.DataAnnotations;

namespace OnlineLecture.Models.DTO
{
    public class RegisterSubjectModel
    {
        [Key]
        public int IdRegisterSubject { get; set; }
     
        public int IdSubject { get; set; }

        public int IdUser { get; set; }
    }
}
