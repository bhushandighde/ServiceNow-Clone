namespace ServiceNow.ServiceNow.Application.DTOs
{
    public class LoginResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

        public string Token { get; set; } // optional now, required when you add JWT
    }
}
