namespace Repository.Models.DTOs.JWT
{
    public record SuccessfulLoginDto(string AccessToken, string RefreshToken);
}
