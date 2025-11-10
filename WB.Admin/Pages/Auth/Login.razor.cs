using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Radzen;
using Radzen.Blazor;
using System.Text;
using System.Text.Json;
using WB.Admin.Pages.Auth.Dialogs;
using WB.Shared.Dtos;
using WB.Shared.Dtos.General.ResponseDtos;
using WB.Shared.Dtos.UMS.RequestDtos;

namespace WB.Admin.Pages.Auth
{
    public partial class Login : ComponentBase
    {
        #region Varaibles
        [CascadingParameter]
        private Task<AuthenticationState>? AuthenticationState { get; set; }
        private LoginRequestDto loginModel = new LoginRequestDto { CultureValue = Thread.CurrentThread.CurrentUICulture.Name };
        string isLoginFailed { get; set; } = "none";
        string loginFailedMessage { get; set; } = "";
        string errorMessage = "";
        bool IsDatabaseError, IsSpinnerVisible, showAdminCreation, showPassword, showLogin, showError = false;
        public bool ValidSubmit { get; set; } = true;
        List<string> DatabaseErrors { get; set; } = new List<string> { "Failed to connect", "28P01", "No such host", "3D000" };
        private CreateAdminRequestDto userDetail { get; set; } = new CreateAdminRequestDto();
        string listGroupClass = "d-none";
        RadzenTextBox textBox;
        RadzenPassword passwordBox;
        protected int selectedIndex { get; set; }
        protected RadzenTemplateForm<CreateAdminRequestDto> userForm { get; set; } = new RadzenTemplateForm<CreateAdminRequestDto>();
        protected RadzenSteps userSteps { get; set; } = new RadzenSteps();
        private LoginRequestFailedResponse loginFailedResponse { get; set; } = new LoginRequestFailedResponse();
        public WMBSCInitialSettings? BasicConfigurations;

        #endregion

        #region Parameters Set
        protected override async Task OnParametersSetAsync()
        {
            try
            {
                if (translationState.Translations == null)
                {
                    IsSpinnerVisible = true;
                    var responseUms = await apiHelper.SendGetAsync<TranslationUserCheckResponseDto>($"Translations/GetTranslationsListAndUserCheck", hasReturnData: true, isApiKeyAuth: true);
                    if (responseUms.IsSuccessStatusCode)
                    {
                        var result = (TranslationUserCheckResponseDto)responseUms.ResultData;
                        if (result.UserCount > 0)
                        {
                            showLogin = true;
                            translationState.Translations = result.Translations;
                        }
                        else
                        {
                            showAdminCreation = true;
                            translationState.FirstLaunchTranslations = result.Translations;
                        }
                        await InvokeAsync(StateHasChanged);
                        IsSpinnerVisible = false;
                    }
                    else
                    {
                        var error = (BadRequestResponse)responseUms.ResultData;
                        if (DatabaseErrors.Any(x => error != null && ((error.Message != null && error.Message.Contains(x)) || (error.InnerException != null && error.InnerException.Contains(x)))))
                            IsDatabaseError = true;
                        showError = true;
                        IsSpinnerVisible = false;
                    }
                }
                else
                {
                    showLogin = true;
                }
            }
            catch (Exception ex)
            {
            }
        }

        #endregion

        #region On Initialized

        protected override void OnInitialized()
        {
            BasicConfigurations = new WMBSCInitialSettings
            {
                arrows = true,
                dots = true,
                slidesToShow = 1,
                slidesToScroll = 1,
                infinite = true,
                autoplay = true,
                dotsClass = "slick-dots Dynamic-slick-dots",
                autoplaySpeed = 3000,
            };
        }
        #endregion

        #region Login
        private async Task HandleLogin()
        {
            try
            {
                spinnerService.Show();
                loginModel.HasTranslations = translationState.Translations != null ? true : false;
                loginFailedResponse = new LoginRequestFailedResponse();
                var request = new HttpRequestMessage(HttpMethod.Post, config.GetValue<string>("ApiUrl") + "Auth/Login");
                request.Content = new StringContent(JsonSerializer.Serialize(loginModel), Encoding.UTF8, "application/json");
                var response = await new HttpClient().SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    spinnerService.Hide();
                    var data = await response.Content.ReadFromJsonAsync<UserSucessResponse>();
                    await authStateProvider.MarkUserAsAuthenticated(data.Token);
                    await localStorage.SetAsync("Token", data.Token);
                    await localStorage.SetAsync("RefreshToken", data.RefreshToken);
                    await localStorage.SetAsync("Username", data.UserDetail?.Email);
                    await localStorage.SetAsync("SecurityStamp", data.UserDetail?.SecurityStamp);
                    await localStorage.SetAsync("UserDetail", data.UserDetail);
                    await localStorage.SetAsync("ProfilePicUrl", data.ProfilePicUrl);
                    loginState.SetLoginAndClaims(data.UserDetail.UserName, data.UserDetail, true, true, data.UserClaims, data.Token, data.RefreshToken, "LoginForm", data.ProfilePicUrl);
                    if (loginModel.HasTranslations == false)
                    {
                        translationState.Translations = data.Translations.ToList();
                    }
                    navigationManager.NavigateTo("/dashboard", true);
                }
                else
                {
                    spinnerService.Hide();
                    showLogin = true;
                    loginFailedResponse = await response.Content.ReadFromJsonAsync<LoginRequestFailedResponse>();
                    if (loginFailedResponse != null)
                    {
                        if (!String.IsNullOrEmpty(loginFailedResponse.ErrorCode))
                        {
                            errorMessage = translationState.Translate(loginFailedResponse.ErrorCode);
                        }
                        else
                        {
                            errorMessage = translationState.Translate("LoginFailed");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                spinnerService.Hide();
            }
        }

        #endregion

        #region Create Admin
        protected async Task FormSubmitUser()
        {
            bool? dialogResponse = await dialogService.Confirm(
               translationState.Translate("Sure_Submit"),
               translationState.Translate("Confirm"),
               new ConfirmOptions()
               {
                   CancelButtonText = @translationState.Translate("Cancel"),
                   OkButtonText = @translationState.Translate("OK")
               });
            if (dialogResponse == true)
            {
                spinnerService.Show();
                var response = await apiHelper.SendAsync<CreateAdminRequestDto, object>(HttpMethod.Post, "Users/CreateAdminUser", userDetail, hasReturnData: false, isApiKeyAuth: true);
                if (response.IsSuccessStatusCode)
                {
                    spinnerService.Hide();
                    notificationService.Notify(new NotificationMessage()
                    {
                        Severity = NotificationSeverity.Success,
                        Detail = translationState.Translate("Admin_User_Created_Successfully")
                    });
                    translationState.FirstLaunchTranslations = null;
                    loginModel.Username = userDetail.Email;
                    loginModel.Password = userDetail.Password;
                    await HandleLogin();
                }
                else
                {
                    spinnerService.Hide();
                    await invalidRequestHandler.ReturnBadRequestNotification(response);
                }
            }
        }
        #endregion

        #region Events
        private async Task OpenForgetPasswordDialog()
        {
            await dialogService.OpenAsync<ForgetPasswordDialog>(translationState.Translate("Forgot_Password"),
                    null,
                    new Radzen.DialogOptions() { Width = "25%" });
        }
        #endregion

        #region Resend Confirmation Link
        protected async Task ResendConfirmationLink()
        {
            try
            {
                spinnerService.Show();
                var response = await apiHelper.SendGetAsync<object>("Auth/SendPasswordResetLink?email=" + loginModel.Username, hasReturnData: false, isApiKeyAuth: true);
                if (response.IsSuccessStatusCode)
                {
                    spinnerService.Hide();
                    invalidRequestHandler.Notify(NotificationSeverity.Success, translationState.Translate("Email_Confirmation_Link_Sent"));
                    errorMessage = "";
                    loginFailedResponse = new LoginRequestFailedResponse();
                }
                else
                {
                    spinnerService.Hide();
                    await invalidRequestHandler.ReturnBadRequestNotification(response);
                }
            }
            catch (Exception ex)
            {
                spinnerService.Hide();
                invalidRequestHandler.Notify(NotificationSeverity.Error);
            }
        }
        #endregion
    }
}
