using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Models
{
    public class AdminViewModel
    {

            public int Id { get; set; }
            public string AdUsername { get; set; }

            [StringLength(50, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters.")]
            [DataType(DataType.Password)]
            public string AdPassword { get; set; }

    }
}
