using System.ComponentModel.DataAnnotations;

namespace OnlineLecture.Models.DTO
{
    public class SubjectModel
    {
        [Key]
        public int IdSubject { get; set; }
       
        public string NameSubject { get; set; }

        public string Description { get; set; }
    }
}
