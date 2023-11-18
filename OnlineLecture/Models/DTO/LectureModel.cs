using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OnlineLecture.Models.DTO
{
    public class LectureModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdLecture { get; set; }

        public string NameLecture { get; set; }

        public string? Description { get; set; }

        public string FileLecture { get; set; }
    }
}
