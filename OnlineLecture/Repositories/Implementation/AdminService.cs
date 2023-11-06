using Humanizer.Localisation;
using Microsoft.AspNetCore.Identity;
using OnlineLecture.Models.Domain;
using OnlineLecture.Models.DTO;
using OnlineLecture.Repositories.Abstract;
using System.Security.Claims;

namespace OnlineLecture.Repositories.Implementation
{
    public class AdminService : IAdminService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AdminService(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;

        }


        public IQueryable<ApplicationUser> List()
        {
            var data = userManager.Users.AsQueryable();
            return data;
        }

        public bool Update(ApplicationUser model)
        {
            throw new NotImplementedException();
        }

        public ApplicationUser GetById(int id)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        Task<bool> IAdminService.Update(ApplicationUser model)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationUser> GetById(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
