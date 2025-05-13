using Microsoft.AspNetCore.Identity;
using Orders_Backend.Repositories.Interfaces;
using Orders_Backend.UnitOfWork.Interfaces;
using Orders_Shared.DTOs;
using Orders_Shared.Entities;

namespace Orders_Backend.UnitOfWork.Implementations
{
    public class UsersUnitOfWork : IUsersUnitOfWork
    {
        private readonly IUsersRepository _usersRepository;

        public UsersUnitOfWork(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }
        public async Task<IdentityResult> AddUserAsync(User user, string password)
            => await _usersRepository.AddUserAsync(user, password);

        public async Task AddUserToRoleAsync(User user, string roleName)
            => await _usersRepository.AddUserToRoleAsync(user, roleName);

        public async Task<IdentityResult> ChangePasswordAsync(User user, string currentPassword, string newPssword)
            => await _usersRepository.ChangePasswordAsync(user, currentPassword, newPssword);

        public async Task CheckRoleAsync(string roleName)
            => await _usersRepository.CheckRoleAsync(roleName);

        public async Task<User> GetUserAsync(string email)
            => await _usersRepository.GetUserAsync(email);

        public async Task<User> GetUserAsync(Guid userId)
            => await GetUserAsync(userId);

        public async Task<bool> IsUserInRoleAsync(User user, string roleName)
            => await _usersRepository.IsUserInRoleAsync(user, roleName);

        public Task<SignInResult> LoginAsync(LoginDTO model)
            => _usersRepository.LoginAsync(model);

        public Task LogoutAsync()
            => _usersRepository.LogoutAsync();

        public async Task<IdentityResult> UpdateUserAsync(User user)
            => await _usersRepository.UpdateUserAsync(user);
    }
}
