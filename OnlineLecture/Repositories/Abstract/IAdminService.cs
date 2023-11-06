using Humanizer.Localisation;
using OnlineLecture.Models.Domain;
using OnlineLecture.Models.DTO;

namespace OnlineLecture.Repositories.Abstract
{
    public interface IAdminService
    {
        Task<bool> Update(ApplicationUser model);
        Task<ApplicationUser> GetById(Guid id);
        bool Delete(int id);
        IQueryable<ApplicationUser> List();
    }
}
