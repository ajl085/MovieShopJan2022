using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace Infrastructure.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _userRepository;

        public AccountService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> CreateUser(RegisterModel model)
        {
            // check whether user has registered with same email
            // go to user repository and get user record from user table by email

            var dbUser = await _userRepository.GetUserByEmail(model.Email);

            if (dbUser != null)
            {
                throw new Exception("Email already registered, try to login");
            }

            // continue with registration
            // generate a random salt
            // hash the password with salt



        }

        public Task<bool> ValidateUser(string email, string password)
        {
            throw new NotImplementedException();
        }

        private string GetRandomSalt()
        {
            var randomBytes = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }

            return Convert.ToBase64String(randomBytes);
        }

        private string GetHashedPassword(string password, string salt)
        {
            var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password,
            Convert.FromBase64String(salt),
            KeyDerivationPrf.HMACSHA512,
            10000,
            256 / 8));
            
            return hashed;
        }
    }
}
