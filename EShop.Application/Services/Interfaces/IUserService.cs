using EShop.Data.DTOs.Account;
using EShop.Data.Entities.Account;

namespace EShop.Application.Services.Interfaces
{
    public interface IUserService : IAsyncDisposable
    {
        #region
        Task RegisterOrLoginUser(RegisterUserDTO dto);
        Task<bool> CheckUserExistByMobile(string mobile);
        Task<bool> CheckMobileAuthorization(MobileActivationDTO dto);
        Task<User?> GetUserByMobile(string mobile);
        Task<EditUserInfoDTO> GetEditUserDetail(long userId);
        Task EditUserDetail(EditUserInfoDTO dto);
        Task<UserDetailDTO> GetUserDetail(long userId);
        Task<bool> SendActivationSms(string mobile);
        #endregion
    }
}
