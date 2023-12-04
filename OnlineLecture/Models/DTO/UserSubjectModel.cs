using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OnlineLecture.Models.DTO
{
    public class UserSubjectModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdUserSubject { get; set; }

        public string IdUser { get; set; }

        public int IdSubject { get; set; }
    }
}
