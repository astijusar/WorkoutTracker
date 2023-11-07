using System.ComponentModel.DataAnnotations;

namespace Repository.Models.DTOs.User
{
    public record UserRegistrationDto(string UserName, string Email, string Password);
}
