using CryptoBull.Models;

namespace CryptoBull.Services
{
    public interface IDbService
    {
        Task <User> CreateUser(string firstName, string lastName, string email, string hashedPassword);

        Task<UserDto> LogIn(string email, string hashedPassword);
    }
}
