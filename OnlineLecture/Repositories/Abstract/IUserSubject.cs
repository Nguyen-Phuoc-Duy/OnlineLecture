using OnlineLecture.Models.DTO;

namespace OnlineLecture.Repositories.Abstract
{
    public interface IUserSubject
    {
        bool AddUserSubject(UserSubjectModel model);

        bool UpdateUserSubject(UserSubjectModel model);

        bool DeleteUserSubject(int idUserSubject);

        UserSubjectModel FindById(int id);

        IEnumerable<UserSubjectModel> GetAll();

    }
}
