using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OnlineLecture.Models.DTO
{
    public class LectureModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdLecture { get; set; }
        public int IdSubject { get; set; }

        public string NameLecture { get; set; }

        public string? Description { get; set; }

        public string FileLecture { get; set; }

        [NotMapped]
        [Required]
        public List<int> Subjects { get; set; }

        [NotMapped]
        public IEnumerable<SelectListItem> SubjectList { get; set; }

        [NotMapped]
        public string SubjectNames { get; set; }
    }
}
