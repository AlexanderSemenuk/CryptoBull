using Microsoft.AspNetCore.Mvc;
using CryptoBull.Services;
using CryptoBull.Models;

namespace CryptoBull.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class DbController : Controller
    {
        private readonly IDbService _dbService;
        
        public DbController(IDbService dbService)
        {
            _dbService = dbService;
        }

        [HttpPost("Create")] 
        public async Task<IActionResult> CreateUser(string firstName, string lastName, string email, string hashPassword)
        {
            try
            {
                User newUser = await _dbService.CreateUser(firstName, lastName, email, hashPassword);
                return Ok(newUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("Login")]

        public async Task<IActionResult> LogIn(string email, string password)
        {
            var user = await _dbService.LogIn(email, password);

            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return BadRequest("Invalid email or password");
            }
        }
    }
}
