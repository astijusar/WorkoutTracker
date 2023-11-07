using System.ComponentModel.DataAnnotations;

namespace Repository.Models.DTOs.User
{
    public record UserLoginDto(string UserName, string Password);
}
