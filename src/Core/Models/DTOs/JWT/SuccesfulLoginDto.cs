namespace Core.Models.DTOs.JWT
{
    public record SuccessfulLoginDto(string AccessToken, string RefreshToken);
}
