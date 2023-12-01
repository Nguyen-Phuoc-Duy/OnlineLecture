using Microsoft.AspNetCore.Mvc;
using OnlineLecture.Models.DTO;

namespace OnlineLecture.Repositories.Abstract
{
    public interface ILectureService
    {
        Task<bool> AddLecture(LectureModel model, IFormFile mFile);

        Task<bool> UpdateLecture(LectureModel model, IFormFile mFile);

        bool DeleteLecture(int idLecture);

        LectureModel FindById(int id);

        IEnumerable<LectureModel> GetAll();

        LectureListVm FilterList();

        List<int> GetSubjectByLectureId(int idLecture);

    }
}
