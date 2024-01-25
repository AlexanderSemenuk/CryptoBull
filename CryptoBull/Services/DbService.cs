using CryptoBull.Data;
using CryptoBull.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CryptoBull.Services
{
    public class DbService : IDbService
    {
        private readonly AppDbContext _appDbContext;

        public DbService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<User> CreateUser(string firstName, string lastName, string email, string hashedPassword)
        {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(hashedPassword);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                string doubleHash = Convert.ToHexString(hashBytes).ToLower();
                User newUser = new User
                {
                    firstName = firstName,
                    lastName = lastName,
                    email = email,
                    hashPassword = doubleHash
                };
                _appDbContext.Users.Add(newUser);
                _appDbContext.SaveChanges();

                return newUser;
            }
        }
        public async Task<UserDto> LogIn(string email, string hashedPassword)
        {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                User user = _appDbContext.Users.FirstOrDefault(user => user.email == email);
                if (user != null)
                {
                    byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(hashedPassword);
                    byte[] hashBytes = md5.ComputeHash(inputBytes);
                    string doubleHash = Convert.ToHexString(hashBytes).ToLower();
                    if (user.hashPassword == doubleHash)
                    {
                        UserDto userDto = new UserDto
                        {
                            email = user.email,
                            firstName = user.firstName,
                            lastName = user.lastName,
                        };
                        return userDto;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            
        }
    }
}
