using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OnlineLecture.Models.DTO
{
    public class SubjectLectureModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdSubjectLecture { get; set; }

        public int IdSubject { get; set; }

        public int IdLecture { get; set; }
    }
}
