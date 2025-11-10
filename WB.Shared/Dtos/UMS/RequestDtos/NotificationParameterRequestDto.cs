using WB.Shared.Dtos.General;
using System.ComponentModel.DataAnnotations.Schema;

namespace WB.Shared.Dtos.UMS.RequestDtos;

public class NotificationParameterRequestDto : EntityBaseDto
{
    public string Entity { get; set; } = string.Empty;

    public string ReceiverName { get; set; } = string.Empty;
    public string SenderName { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;

    public string StartDate { get; set; } = string.Empty;
    public string EndDate { get; set; } = string.Empty;
    public string NotificationReceiverId { get; set; } = string.Empty;

    public string Code { get; set; } = string.Empty;

    public string FolderName { get; set; } = string.Empty;
    public string ProjectName { get; set; } = string.Empty;
    public string RoleName { get; set; } = string.Empty;

    [NotMapped]
    public string SiteName { get; set; }
}
