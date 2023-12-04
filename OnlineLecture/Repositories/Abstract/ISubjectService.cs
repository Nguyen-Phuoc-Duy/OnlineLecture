using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using OnlineLecture.Models.DTO;

namespace OnlineLecture.Repositories.Abstract
{
    public interface ISubjectService
    {
        bool AddSubject(SubjectModel model);

        bool UpdateSubject(SubjectModel model);

        bool DeleteSubject(int idSubject);

        SubjectModel FindById(int id);

        IEnumerable<SubjectModel> GetAll();
        Task<List<SubjectModel>> SearchByNameAjax(string name);

    }
}
