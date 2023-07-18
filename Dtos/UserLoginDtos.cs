namespace SampleAPI.Dtos
{
    public class LoginDtos
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
    }

    public class UserLoginDtos
    {
        public Guid? UserId { get; set; }
        public string? Username { get; set; }
        public string? AccessToken { get; set; }
        public string? Audience { get; set; }
        public string? Issuer { get; set; }
        public string? IssueAt { get; set; }
        public long? ExpiresIn { get; set; }
        public string? ExpireDate { get; set; }
    }
}
