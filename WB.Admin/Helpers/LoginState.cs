using System.Runtime.InteropServices;
using WB.Shared.Dtos;
using WB.Shared.Dtos.UMS.ResponseDtos;

namespace WB.Admin.Helpers
{
    public class LoginState
    {
        public bool IsLoggedIn { get; set; }
        public bool IsSSOAthenticated { get; set; }
        public bool IsStateChecked { get; set; }
        public string Token { get; set; }
        public string Username { get; set; }
        public UserDetailLoginResponseDto UserDetail { get; set; }
        public IEnumerable<UserAssignedRoleResponseDto> UserRoles { get; set; }
        public string RefreshToken { get; set; }
        public string ProfilePicUrl { get; set; }
        public int PageSize { get; set; } = 10;
        public int ModuleId { get; set; }
        public IEnumerable<ClaimSucessResponse> ClaimList { get; set; }
        public event Action OnChange;

        public void SetLoginAndClaims(string username, UserDetailLoginResponseDto userDetail, bool login, bool stateCheck, IEnumerable<ClaimSucessResponse> claims, string token, string refreshToken, [Optional] string form, string profilePicUrl)
        {
            Username = username;
            IsLoggedIn = login;
            IsStateChecked = stateCheck;
            ClaimList = claims;
            Token = token;
            UserDetail = userDetail;
            RefreshToken = refreshToken;
            ProfilePicUrl = profilePicUrl;
            if (form != "LoginForm")
            {
                NotifyStateChanged();
            }
        }

        public void SetLogout(bool login)
        {
            Username = String.Empty;
            IsLoggedIn = login;
            IsStateChecked = false;
            ClaimList = null;
            Token = string.Empty;
            RefreshToken = string.Empty;
            ProfilePicUrl = string.Empty;
            NotifyStateChanged();
        }

        public void NotifyStateChanged()
        {
            OnChange?.Invoke();
        }
    }
}
