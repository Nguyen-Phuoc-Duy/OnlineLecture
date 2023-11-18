using Humanizer.Localisation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineLecture.Models.Domain;
using OnlineLecture.Models.DTO;
using OnlineLecture.Repositories.Abstract;
using System.Security.Claims;

namespace OnlineLecture.Repositories.Implementation
{
    public class AdminService : IAdminService
    {
        private readonly DatabaseContext ctx;
        private readonly UserManager<ApplicationUser> userManager;

        public AdminService(DatabaseContext ctx, UserManager<ApplicationUser> userManager)
        {
            this.ctx = ctx;
            this.userManager = userManager;
        }
        public bool Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public ApplicationUser GetById(Guid id)
        {
            return ctx.ApplicationUser.Find(id.ToString());
        }

        //public async Task<bool> Update(Guid id, UserUpdateRequest request)
        //{
        //    try
        //    {
        //        var isNameInUse = await userManager.Users
        //            .Where(u => u.Id != id && u.Name == request.Name)
        //            .AnyAsync();

        //        if (isNameInUse)
        //        {
        //            return false;
        //        }

        //        var user = await userManager.FindByIdAsync(id.ToString());

        //        user.UserName = request.UserName;

        //        var result = await userManager.UpdateAsync(user);

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //}
        public IQueryable<ApplicationUser> List()
        {
            var data = ctx.ApplicationUser.AsQueryable();
            return data;
        }

        public bool Update(ApplicationUser model)
        {
            try
            {
                ctx.ApplicationUser.Update(model);
                ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
