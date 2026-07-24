using EShop.Application.Services.Interfaces;
using EShop.Data.DTOs.Account;
using EShop.Data.Entities.Account;
using EShop.Data.Repository;
using Microsoft.EntityFrameworkCore;

namespace EShop.Application.Services.Implementations
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
                user.MobileActivationNumber = new Random().Next(10000, 99999).ToString();
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
        
        // Receive updated user information from the edit form and save the changes to the database.
        public async Task EditUserDetail(EditUserInfoDTO dto)
        {
            var user = await _userRepository.GetEntityById(dto.UserId);

            user.Address = dto.Address;
            user.Email = dto.Email;
            user.FullName = dto.FullName;
            user.PostCode = dto.PostCode;

            _userRepository.EditEntity(user);
            await _userRepository.SaveAsync();
        }

        // Get current user information from the database and return it as a DTO for the edit form.
        public async Task<EditUserInfoDTO> GetEditUserDetail(long userId)
        {
            var user = await _userRepository.GetEntityById(userId);
            return new EditUserInfoDTO
            {
                UserId = userId,
                Address = user.Address,
                Email = user.Email,
                FullName = user.FullName,
                PostCode = user.PostCode
            };
        }

        // Get complete user details only for display.
        public async Task<UserDetailDTO> GetUserDetail(long userId)
        {
            var user = await _userRepository.GetEntityById(userId);
            return new UserDetailDTO
            {
                Id = userId,
                Address = user.Address,
                Email = user.Email,
                FullName = user.FullName,
                PostCode = user.PostCode,
                MobileNumber = user.MobileNumber,
                CreateDate = user.CreateDate,
                LastUpdateDate = user.LastUpdateDate,
                IsDeleted = user.IsDeleted,
                MobileActivationNumber = user.MobileActivationNumber
            };
        }
        
        public Task<bool> SendActivationSms(string mobile)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CheckMobileAuthorization(MobileActivationDTO dto)
        {
            var user = await GetUserByMobile(dto.Mobile);
            if (user == null)
                return false;

            return dto.ActivationCode == user.MobileActivationNumber;
        }

        public async Task<User?> GetUserByMobile(string mobile)
        {
            return await _userRepository.GetQuery().FirstOrDefaultAsync(u => u.MobileNumber == mobile);
        }
        #endregion
    }
}
