using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.JSInterop;
using Radzen;
using WB.Shared.Configs;
using WB.Shared.Dtos;
using WB.Shared.Dtos.UMS.ResponseDtos;

namespace WB.Admin.Pages.Common
{
    public partial class NotificationDrawer : ComponentBase
    {
        #region Variables
        private HubConnection? _hubConnectionUMS;
        public List<NotificationResponseDto> BellNotifications { get; set; } = new List<NotificationResponseDto>();
        protected int? notificationCount;
        protected Dictionary<string, List<NotificationResponseDto>> GroupedNotifications = new Dictionary<string, List<NotificationResponseDto>>();
        protected Dictionary<string, int> KeyOrderDictionary = new Dictionary<string, int>
        {
            { "Today", 5 },
            { "Yesterday", 4 },
            { "This_Month", 3 },
            { "Last_Month", 2 },
            { "Older", 1 }
        };

        #endregion

        #region On Load
        protected override async Task OnInitializedAsync()
        {
            spinnerService.Show();
            await Load();
            spinnerService.Hide();
            await InitiateNotificationHubConnection();
        }

        public async ValueTask DisposeAsync()
        {
            try
            {
                if (_hubConnectionUMS != null)
                {
                    await _hubConnectionUMS.DisposeAsync();
                }
            }
            catch (Exception ex)
            {
            }
        }
        public async Task InitializeKeyOrderDictionary()
        {
            KeyOrderDictionary = new Dictionary<string, int>
        {
            { translationState.Translate("Today"), 5 },
            { translationState.Translate("Yesterday"), 4 },
            { translationState.Translate("This_Month"), 3 },
            { translationState.Translate("Last_Month"), 2 },
            { translationState.Translate("Older"), 1 }
        };
        }
        public async Task InitiateNotificationHubConnection()
        {
            try
            {
                var notificationHubUrl = config.GetValue<string>("ums_notification_hub_url");
                _hubConnectionUMS = new HubConnectionBuilder()
                    .WithUrl(config.GetValue<string>("ums_notification_hub_url"),
                o => o.AccessTokenProvider = () => Task.FromResult<string>(loginState.Token))
                .Build();

                _hubConnectionUMS.On<NotificationResponseDto>("SendNotification", async notification =>
                {
                    if (BellNotifications == null)
                    {
                        BellNotifications = new List<NotificationResponseDto>();
                    }
                    notification.NotificationMessage = Thread.CurrentThread.CurrentUICulture.Name == "en-US" ? notification.NotificationMessage.Split('|')[0] : notification.NotificationMessage.Split('|')[1];
                    notification.SenderName = Thread.CurrentThread.CurrentUICulture.Name == "en-US" ? notification.SenderName.Split('/')[0] : notification.SenderName.Split('/')[1];
                    notification.ReceiverName = Thread.CurrentThread.CurrentUICulture.Name == "en-US" ? notification.ReceiverName.Split('/')[0] : notification.ReceiverName.Split('/')[1];
                    BellNotifications.Insert(0, notification);
                    notificationCount = BellNotifications.Count();
                    BellNotifications = (from item in BellNotifications select item).Take(200).ToList();
                    GroupedNotifications = BellNotifications
                        .GroupBy(notification => GetGroupingKey(notification.CreatedDate))
                        .OrderByDescending(x => KeyOrderDictionary[x.Key])
                        .ToDictionary(x => x.Key, x => x.ToList());
                    await jsInterop.InvokeVoidAsync("recalculateSimplebar", "notification-scroll-div");
                });

                _hubConnectionUMS.On<string>("NotifyClaimsRolesUpdated", async (userId) =>
                {
                    await FetchLatestUserClaimsAndRoles(userId);
                });
                await _hubConnectionUMS.StartAsync();
            }
            catch (Exception ex)
            {
                //notificationService.Notify(new NotificationMessage()
                //{
                //    Severity = NotificationSeverity.Error,
                //    Detail = translationState.Translate("Something_went_wrong_Please_try_again"),
                //});
            }
        }

        protected async Task Load()
        {
            try
            {
                var response = await apiHelper.SendGetAsync<List<NotificationResponseDto>>($"Notification/GetUnreadNotifications?"
                    + $"culture={Thread.CurrentThread.CurrentUICulture.Name}"
                    + $"&userId={loginState.UserDetail.UserId}");
                if (response.IsSuccessStatusCode)
                {
                    BellNotifications = (List<NotificationResponseDto>)response.ResultData;
                    notificationCount = BellNotifications.Count();
                    BellNotifications = (from item in BellNotifications select item).Take(200).ToList();
                    GroupedNotifications = BellNotifications
                        .GroupBy(notification => GetGroupingKey(notification.CreatedDate))
                        .OrderByDescending(x => KeyOrderDictionary[x.Key])
                        .ToDictionary(x => x.Key, x => x.ToList());

                    await InvokeAsync(StateHasChanged);
                }
                else
                {
                    await invalidRequestHandler.ReturnBadRequestNotification(response);
                }
            }
            catch (Exception ex)
            {
                notificationService.Notify(new NotificationMessage()
                {
                    Severity = NotificationSeverity.Error,
                    Detail = translationState.Translate("Something_went_wrong_Please_try_again"),
                });
            }
        }
        #endregion

        #region Functions
        protected string GetGroupingKey(DateTime date)
        {
            var today = DateTime.Today;
            var yesterday = today.AddDays(-1);
            var thisMonth = new DateTime(today.Year, today.Month, 1);
            var lastMonth = thisMonth.AddMonths(-1);

            if (date.Date == today)
            {
                return "Today";
            }
            if (date.Date == yesterday)
            {
                return "Yesterday";
            }
            else if (date >= thisMonth)
            {
                return "This_Month";
            }
            else if (date >= lastMonth)
            {
                return "Last_Month";
            }
            else
            {
                return "Older";
            }
        }
        protected async Task ReadAllAsync()
        {
            if (BellNotifications.Any())
            {
                if (await dialogService.Confirm(translationState.Translate("Sure_Want_To_Mark_All_Read"),
                    translationState.Translate("Read")) == true)
                {
                    try
                    {
                        var notificationIds = new List<Guid>();
                        spinnerService.Show();
                        foreach (NotificationResponseDto bellNotification in BellNotifications)
                        {
                            notificationIds.Add(bellNotification.NotificationId);
                        }

                        var result = await apiHelper.SendAsync<List<Guid>, ApiCallResponse>(HttpMethod.Post, "Notification/MarkAllNotificationAsRead", notificationIds, false);
                        if (result.IsSuccessStatusCode)
                        {
                            await Load();
                        }
                        else
                        {
                            notificationService.Notify(new NotificationMessage()
                            {
                                Severity = NotificationSeverity.Error,
                                Detail = translationState.Translate("Notification_Updation"),
                                Style = "position: fixed !important; left: 0; margin: auto;"
                            });
                        }
                        spinnerService.Hide();
                    }
                    catch (Exception)
                    {
                        notificationService.Notify(new NotificationMessage()
                        {
                            Severity = NotificationSeverity.Error,
                            Detail = translationState.Translate("Something_went_wrong_Please_try_again"),
                        });
                    }
                }
            }
            else
            {
                await dialogService.Alert(translationState.Translate("No_Unread_Notifications"),
                    translationState.Translate("No_Unread_Notifications"));
            }
        }
        protected async Task ReadAsync(Guid id)
        {
            if (await dialogService.Confirm(translationState.Translate("Sure_Want_To_Mark_As_Read"),
                    translationState.Translate("Read")) == true)
            {
                try
                {
                    spinnerService.Show();
                    var result = await apiHelper.SendAsync<Guid, ApiCallResponse>(HttpMethod.Post, "Notification/MarkNotificationAsRead", id, false);
                    if (result.IsSuccessStatusCode)
                    {
                        await Load();
                    }
                    else
                    {
                        notificationService.Notify(new NotificationMessage()
                        {
                            Severity = NotificationSeverity.Error,
                            Detail = translationState.Translate("Notification_Updation"),
                            Style = "position: fixed !important; left: 0; margin: auto;"
                        });
                    }
                    spinnerService.Hide();
                }
                catch (Exception)
                {
                    notificationService.Notify(new NotificationMessage()
                    {
                        Severity = NotificationSeverity.Error,
                        Detail = translationState.Translate("Something_went_wrong_Please_try_again"),
                    });
                }
            }
        }
        protected async Task ViewDetailAsync(NotificationResponseDto notification)
        {
            try
            {
                var chunks = notification.NotificationURL.Split('/');
                var itemId = Guid.Parse(chunks[2]);
                await apiHelper.SendGetAsync<ApiCallResponse>("Notification/MarkNotificationAsRead?notificationId=" + $"{notification.NotificationId}", false);
                switch (chunks[1])
                {
                    case var action when action == "shared-item":
                        break;
                    case var action when action == "add-project-member":
                        navigationManager.NavigateTo("project-drive/");
                        break;
                    case var action when action == "role-assignment":
                        break;
                    default:
                        break;
                }
                await Load();
            }
            catch (Exception ex)
            {
                notificationService.Notify(new NotificationMessage()
                {
                    Severity = NotificationSeverity.Error,
                    Detail = translationState.Translate("Something_went_wrong_Please_try_again"),
                });
            }
        }
        protected async Task FetchLatestUserClaimsAndRoles(string userId)
        {
            try
            {
                var response2 = await apiHelper.SendGetAsync<UserClaimsRolesResponseDto>($"Auth/GetUserLatestClaimAndRoles?userId={userId}");
                if (response2.IsSuccessStatusCode)
                {
                    var result = (UserClaimsRolesResponseDto)response2.ResultData;
                    loginState.ClaimList = result.Claims;
                    loginState.UserRoles = result.Roles;
                    loginState.NotifyStateChanged();

                    var currentpath = new Uri(navigationManager.Uri.ToLower()).AbsolutePath.ToLower();
                    if (PermissionPatterns.routesAndClaims.TryGetValue(currentpath, out var oldClaim))
                    {
                        bool hasAccess = loginState.ClaimList.Any(claim => claim.Value == oldClaim);
                        if (!hasAccess)
                        {
                            notificationService.Notify(new NotificationMessage()
                            {
                                Severity = NotificationSeverity.Info,
                                Detail = translationState.Translate("Access_Removed_Redirecting"),
                            });
                            await Task.Delay(1000);
                            navigationManager.NavigateTo("/unauthorized");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                notificationService.Notify(new NotificationMessage()
                {
                    Severity = NotificationSeverity.Error,
                    Detail = translationState.Translate("Something_went_wrong_Please_try_again"),
                });
            }
        }
        #endregion

    }
}
