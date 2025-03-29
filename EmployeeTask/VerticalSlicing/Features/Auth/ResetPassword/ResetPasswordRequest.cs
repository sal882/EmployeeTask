namespace EmployeeTask.VerticalSlicing.Features.Auth.ResetPassword
{
    public class ResetPasswordRequest
    {
        public string Email { get; set; }
        public string OTP { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
