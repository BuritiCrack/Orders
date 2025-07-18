﻿using Microsoft.AspNetCore.Identity;
using Orders_Backend.Repositories.Interfaces;
using Orders_Backend.UnitOfWork.Interfaces;
using Orders_Shared.DTOs;
using Orders_Shared.Entities;
using Orders_Shared.Responses;

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

        public async Task<IdentityResult> ChangePasswordAsync(User user, string currentPassword, string newPassword)
            => await _usersRepository.ChangePasswordAsync(user, currentPassword, newPassword);

        public async Task CheckRoleAsync(string roleName)
            => await _usersRepository.CheckRoleAsync(roleName);

        public async Task<IdentityResult> ConfirmEmailAsync(User user, string token)
            => await _usersRepository.ConfirmEmailAsync(user, token);

        public async Task<string> GenerateEmailConfirmationTokenAsync(User user)
            => await _usersRepository.GenerateEmailConfirmationTokenAsync(user);

        public async Task<string> GeneratePasswordResetTokenAsync(User user)
            => await _usersRepository.GeneratePasswordResetTokenAsync(user);

        public async Task<User> GetUserAsync(string email)
            => await _usersRepository.GetUserAsync(email);

        public async Task<User> GetUserAsync(Guid userId)
            => await _usersRepository.GetUserAsync(userId);

        public async Task<bool> IsUserInRoleAsync(User user, string roleName)
            => await _usersRepository.IsUserInRoleAsync(user, roleName);

        public Task<SignInResult> LoginAsync(LoginDTO model)
            => _usersRepository.LoginAsync(model);

        public Task LogoutAsync()
            => _usersRepository.LogoutAsync();

        public async Task<IdentityResult> ResetPasswordAsync(User user, string token, string password)
            => await _usersRepository.ResetPasswordAsync(user, token, password);

        public async Task<IdentityResult> UpdateUserAsync(User user)
            => await _usersRepository.UpdateUserAsync(user);

        public async Task<ActionResponse<IEnumerable<User>>> GetAsync(PaginationDTO pagination)
            => await _usersRepository.GetAsync(pagination);

        public async Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination)
            => await _usersRepository.GetTotalPagesAsync(pagination);
    }
}