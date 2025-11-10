
using WB.Shared.Dtos.General.RequestDtos;

namespace WB.Application.Interfaces.Services
{
    public interface IEmailService
    {
        Task<Tuple<bool, Exception?>> SendEmail(EmailRequestDto request);
    }
}
