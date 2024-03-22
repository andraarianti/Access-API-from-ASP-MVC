using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace APISolution.BLL.DTOs
{
    public class LoginDTO
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string? Token { get; set; }
    }
}
