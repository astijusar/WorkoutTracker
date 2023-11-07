using System.ComponentModel.DataAnnotations;

namespace Repository.Models.DTOs.JWT
{
    public record RefreshAccessTokenDto(string RefreshToken);
}
