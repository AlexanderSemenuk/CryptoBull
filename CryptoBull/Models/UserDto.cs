using System.ComponentModel.DataAnnotations;

namespace CryptoBull.Models
{
    public class UserDto
    {
        public string email {  get; set; }

        public string firstName { get; set; }

        public string lastName { get; set; }
    }
}
