using Microsoft.AspNetCore.Mvc;
using OnlineLecture.Models.DTO;

namespace OnlineLecture.Repositories.Abstract
{
    public interface ILectureService
    {
        Task<bool> AddLecture(LectureModel model, IFormFile mFile);

        bool UpdateLecture(LectureModel model);

        bool DeleteLecture(int idLecture);

        LectureModel FindById(int id);

        IEnumerable<LectureModel> GetAll();

    }
}
