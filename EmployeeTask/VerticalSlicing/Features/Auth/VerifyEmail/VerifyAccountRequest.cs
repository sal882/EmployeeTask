namespace EmployeeTask.VerticalSlicing.Features.Auth.VerifyEmail
{
    public class VerifyAccountRequest
    {
        public string Email { get; set; }
        public string OTP { get; set; }
    }
}
