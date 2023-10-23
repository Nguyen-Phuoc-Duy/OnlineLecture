using OnlineLecture.Models.DTO;

namespace OnlineLecture.Repositories.Abstract
{
    public interface IUserAuthenticationService
    {
        Task<Status> LoginAsync(LoginModel model);
        Task<Status> RegisterAsync(RegistrationModel model);

        Task LogoutAsync();
    }
}
