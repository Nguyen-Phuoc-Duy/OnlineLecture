using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineLecture.Models.DTO
{
    public class SubjectModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdSubject { get; set; }
       
        public string NameSubject { get; set; }

        public string? Description { get; set; }
    }
}
