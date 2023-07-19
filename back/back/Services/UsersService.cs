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

        public async Task<List<User>> GetUsersAsync()
        {
            return await _userRepository.GetUsersAsync();
        }

        public async Task<User> GetUserByIdAsync(long id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if(user == null)
            {
                throw new Exception("NOT FOUND");
            }
            return user;
        }

        public async Task<User> FindUserByEmailAsync(string email)
        {
            var user = await _userRepository.FindByEmailAsync(email);
            if (user == null)
            {
                throw new Exception("NOT FOUND");
            }
            return user;
        }

        public async Task<User> CreateUserAsync(User user)
        {
            User newUser = await _userRepository.CreateUserAsync(user);
            List<User> allUsers = await this.GetUsersAsync();
            foreach(User u in allUsers)
            {
                if(u.Role == "DOCTOR")
                {
                    newUser.AssignedGeneralPracticeDoctor = u;
                    newUser.AssignedGeneralPracticeDoctorId = u.Id;
                    break;
                }
            }
            return newUser;
        }

        public async Task<User> UpdateUserAsync(long id,User user)
        {
            var foundUser = await _userRepository.GetUserByIdAsync(id);
            if (foundUser == null)
            {
                throw new Exception("NOT FOUND");
            }
            await _userRepository.UpdateUserAsync(foundUser);
            return user;
        }

        public async Task<User> BlockUserAsync(long id)
        {
            var foundUser = await _userRepository.GetUserByIdAsync(id);
            if (foundUser == null)
            {
                throw new Exception("NOT FOUND");
            }

            if(foundUser.NumberOfPenalties < 3) {
                return foundUser;
            } 
            foundUser.IsBlocked = 1;
            await _userRepository.UpdateUserAsync(foundUser);
            return foundUser;
        }

        public async Task<User> UnblockUserAsync(long id)
        {
            var foundUser = await _userRepository.GetUserByIdAsync(id);
            if (foundUser == null)
            {
                throw new Exception("NOT FOUND");
            }

            if (foundUser.IsBlocked == 0)
            {
                return foundUser;
            }
            foundUser.IsBlocked = 0;
            foundUser.NumberOfPenalties = 0;
            await _userRepository.UpdateUserAsync(foundUser);
            return foundUser;
        }


        public async Task<bool> DeleteUserAsync(long id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                return false;
            }
            await _userRepository.DeleteUserAsync(user);
            return true;
        }

        public async Task<bool> IsFemaleAsync(long id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                throw new Exception("A");
            }
            return user.Is_female == 1;
        }
    }
}
