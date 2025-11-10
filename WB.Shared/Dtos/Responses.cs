using WB.Shared.Dtos.General.ResponseDtos;
using WB.Shared.Dtos.UMS.ResponseDtos;
using System.Net;

namespace WB.Shared.Dtos
{
    public class AuthenticationResult
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public string? ProfilePicUrl { get; set; }
        public bool Success { get; set; }
        public dynamic User { get; set; }
        public UserDetailLoginResponseDto? UserDetail { get; set; }
        public string ErrorCode { get; set; }
        public IEnumerable<ClaimSucessResponse> ClaimsResultList { get; set; }
        public IEnumerable<TranslationResponseDto> Translations { get; set; }
    }
    public class ClaimSucessResponse
    {
        public string Type { get; set; }
        public string Value { get; set; }
    }

    public class LoginRequestFailedResponse
    {
        public string ErrorCode { get; set; }
    }
    public class UserSucessResponse
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public string ProfilePicUrl { get; set; }
        public dynamic User { get; set; }
        public UserDetailLoginResponseDto? UserDetail { get; set; }
        public IEnumerable<ClaimSucessResponse> UserClaims { get; set; }
        public IEnumerable<TranslationResponseDto> Translations { get; set; }
    }
    public class Pagination
    {
        public int length { get; set; }
        public int size { get; set; }
        public int page { get; set; }
        public int lastPage { get; set; }
        public int startIndex { get; set; }
        public int endIndex { get; set; }

    }
    public class GenericResponseForCreateUpdate
    {
        public Guid Id { get; set; }
    }

    public class ApiResponse<T>
    {
        public int Count { get; set; }
        public IEnumerable<T> Value { get; set; }
    }

    public class WorkflowActivityResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
    public class ApiCallResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccessStatusCode { get; set; }
        public dynamic ResultData { get; set; }
        public string Message { get; set; }
    }
    public class BadRequestResponse
    {
        public string Message { get; set; }
        public string InnerException { get; set; }
        //[NotMapped]
        //public ErrorLog? errorLog { get; set; }
    }
    public class FileUploadSuccessResponse
    {
        public string StoragePath { get; set; }
        public int AttachementId { get; set; }
    }
}
