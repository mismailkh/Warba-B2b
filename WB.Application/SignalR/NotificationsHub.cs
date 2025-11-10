using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using WB.Shared.Dtos.UMS.ResponseDtos;

namespace WB.Application.SignalR
{
    public class CustomUserIdProvider : IUserIdProvider
    {
        public string GetUserId(HubConnectionContext connection)
        {
            return connection.User?.FindFirst("Id")?.Value
                   ?? connection.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value
                   ?? connection.User?.Identity?.Name
                   ?? "unknown";
        }
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class NotificationsHub : Hub<INotificationClient>
    {
        public override async Task OnConnectedAsync()
        {
            var claims = Context.User?.Claims?.Select(c => $"{c.Type}: {c.Value}");
            Console.WriteLine("Claims: " + string.Join(", ", claims));
            await base.OnConnectedAsync();
        }
    }

    public interface INotificationClient
    {
        Task SendNotification(NotificationResponseDto notification);
        Task NotifyClaimsRolesUpdated(string userId);
    }
}
