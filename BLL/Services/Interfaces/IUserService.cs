using Common.ViewModels;

namespace EcaFinance.BLL.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<UserViewModel>> GetUsers();
        Task EditAsync(UserViewModel model);
        Task RemoveAsync(int id);
        Task<UserViewModel> GetByIdAsync(int id);
        Task<UserViewModel> GetByEmailAsync(string email);
        Task ChangeUserDetailsAsync(UserViewModel model);

    }
}
