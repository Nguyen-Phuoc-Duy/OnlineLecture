using Humanizer.Localisation;
using OnlineLecture.Models.Domain;
using OnlineLecture.Models.DTO;

namespace OnlineLecture.Repositories.Abstract
{
    public interface IAdminService
    {
        //Task<bool> Update(Guid id, UserUpdateRequest request);
        ApplicationUser GetById(Guid id);
        bool Delete(Guid id);
        IQueryable<ApplicationUser> List();
        bool Update(ApplicationUser model);
    }
}
