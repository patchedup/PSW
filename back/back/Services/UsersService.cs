using System.Collections.Generic;
using System.Threading.Tasks;
using back.Models;
using back.Repositories;
using Microsoft.EntityFrameworkCore;

namespace back.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _userRepository;

        public UsersService(IUsersRepository userRepository)
        {
            _userRepository = userRepository;
        
    }

        public Task<User> BlockUserAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<User> CreateUserAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteUserAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<User> FindUserByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> GetUsersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsFemaleAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<User> UnblockUserAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<User> UpdateUserAsync(long id, User user)
        {
            throw new NotImplementedException();
        }
    }
