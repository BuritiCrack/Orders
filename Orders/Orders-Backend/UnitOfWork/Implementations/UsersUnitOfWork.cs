using Microsoft.AspNetCore.Identity;
using Orders_Backend.Repositories.Interfaces;
using Orders_Backend.UnitOfWork.Interfaces;
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

        public async Task CheckRoleAsync(string roleName)
            => await _usersRepository.CheckRoleAsync(roleName);

        public async Task<User> GetUserAsync(string email)
            => await _usersRepository.GetUserAsync(email);

        public async Task<bool> IsUserInRoleAsync(User user, string roleName)
            => await _usersRepository.IsUserInRoleAsync(user, roleName);
    }
}
