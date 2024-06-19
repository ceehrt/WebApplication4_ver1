using Microsoft.AspNetCore.Antiforgery;
using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Models
{
    public class SAuthViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string LastName { get; set; }

        public string UserEmail { get; set; }

        [StringLength(50,MinimumLength = 6,ErrorMessage = "Password must be at least 6 characters.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string Role { get; set; }

    }
}
