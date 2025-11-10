namespace WB.Shared.Dtos.UMS.RequestDtos
{
    public class AdminChangeUserPasswordRequestDto
    {
        public string UserId { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string CreatedBy { get; set; }
    }
}
