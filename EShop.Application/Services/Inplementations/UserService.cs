using EShop.Application.Services.Interfaces;
using EShop.Data.DTOs.Account;
using EShop.Data.Entities.Account;
using EShop.Data.Repository;
using Microsoft.EntityFrameworkCore;

namespace EShop.Application.Services.Inplementations
{
    public class UserService : IUserService
    {
        #region ctor
        private readonly IGenericRepository<User> _userRepository;
        public UserService(IGenericRepository<User> userReository)
        {
            _userRepository = userReository;
        }
        public async ValueTask DisposeAsync()
        {
            await _userRepository.DisposeAsync();
        }
        #endregion

        #region Register Methods
        public async Task RegisterOrLoginUser(RegisterUserDTO dto)
        {
            var checkUser = await CheckUserExistByMobile(dto.MobileNumber);
            if (checkUser)
            {
                //Login
                var user = await _userRepository.GetQuery().FirstAsync(u => u.MobileNumber == dto.MobileNumber);
                user.MobileActivationNumber = new Random().Next(10000,99999).ToString();
                await _userRepository.SaveAsync();
                return;
            }

            //Register
            var newUser = new User
            {
                MobileNumber = dto.MobileNumber,
                MobileActivationNumber = new Random().Next(10000, 99999).ToString()
            };
            await _userRepository.AddEntity(newUser);
            await _userRepository.SaveAsync();
        }

        public async Task<bool> CheckUserExistByMobile(string mobile)
        {
            return await _userRepository.GetQuery().AnyAsync(u => u.MobileNumber == mobile);
        }
        public Task EditUserDetail(EditUserInfoDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task<EditUserInfoDTO> GetEditUserDetail(long userId)
        {
            throw new NotImplementedException();
        }

        public Task<UserDetailDTO> GetUserDetail(long userId)
        {
            throw new NotImplementedException();
        }
        public Task<bool> SendActivationSms(string mobile)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
