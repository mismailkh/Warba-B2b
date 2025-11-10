using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using WB.Web.Data;
using WB.Web.Helpers;
using Radzen;
using System.Net;
using WB.Shared.Dtos;

namespace WB.Web.Services
{
    public class InvalidRequestHandlerService
    {
        #region Variables
        private readonly NotificationService _notificationService;
        private readonly TranslationState _translationState;
        private readonly LoginState _loginState;
        private readonly ProtectedLocalStorage _localStorage;
        private readonly NavigationManager _navigationManager;
        private readonly CustomAuthenticationStateProvider _authStateProvider;

        #endregion

        #region Injection thru Constructor
        public InvalidRequestHandlerService(NotificationService notificationService, TranslationState translationState, LoginState loginState, ProtectedLocalStorage localStorage, NavigationManager navigationManager, CustomAuthenticationStateProvider authStateProvider)
        {
            _notificationService = notificationService;
            _translationState = translationState;
            _loginState = loginState;
            _localStorage = localStorage;
            _navigationManager = navigationManager;
            _authStateProvider = authStateProvider;
        }
        #endregion 

        #region Badrequest Notification
        /// <summary>
        /// Handles the API Bad Request response and provides appropriate msg
        /// </summary>
        /// <param name="response">The received API response</param> 
        /// <param name="alreadyDataExistMessage">Translation key for custom messages (e,g "UserName_Already_Exist"), when you are expecting exception of duplicate data entry</param> 
        public async Task ReturnBadRequestNotification(ApiCallResponse response, string alreadyDataExistMessage = null)
        {
            try
            {
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    Notify(NotificationSeverity.Error, "Token_Expired");
                    await Task.Delay(3000);
                    await _localStorage.DeleteAsync("Token");
                    await _localStorage.DeleteAsync("RefreshToken");
                    await _localStorage.DeleteAsync("Username");
                    await _localStorage.DeleteAsync("UserDetail");
                    await _localStorage.DeleteAsync("SecurityStamp");
                    await _localStorage.DeleteAsync("ProfilePicUrl");
                    await ((CustomAuthenticationStateProvider)_authStateProvider).MarkUserAsLoggedOut();
                    _loginState.SetLogout(false);
                    _navigationManager.NavigateTo("/login", true);
                }
                else if (response.StatusCode == HttpStatusCode.NotFound || response.StatusCode == HttpStatusCode.NoContent)
                {
                    Notify(NotificationSeverity.Error, "No_record_found");
                }
                else
                {
                    var badRequestResponse = (BadRequestResponse)response.ResultData;
                    if (badRequestResponse?.InnerException != null && badRequestResponse.InnerException.ToLower().Contains("violation of unique key"))
                    {
                        Notify(NotificationSeverity.Error, string.IsNullOrEmpty(alreadyDataExistMessage) ? "Save_failed_duplicate_data_detected" : alreadyDataExistMessage);
                    }
                    else
                    {
                        Notify(NotificationSeverity.Error, alreadyDataExistMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.Source != "Microsoft.JSInterop")
                    Notify(NotificationSeverity.Error);
            }
        }
        #endregion

        #region Radzen Notification Message
        public void Notify(NotificationSeverity severity, string? message = null) =>
            _notificationService.Notify(new NotificationMessage
            {
                Severity = severity,
                Detail = _translationState.Translate(!string.IsNullOrWhiteSpace(message) ? message : severity switch
                {
                    NotificationSeverity.Success => "Save_Changes_Successfully",
                    NotificationSeverity.Info => "Default_Info_Message",
                    NotificationSeverity.Warning => "Default_Warning_Message",
                    _ => "Something_went_wrong_Please_try_again"
                })
            });
        #endregion
    }
}
