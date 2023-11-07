namespace API.Models.DTOs.JWT
{
    public record SuccessfulLoginDto(string AccessToken, string RefreshToken);
}
