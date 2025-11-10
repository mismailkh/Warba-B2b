namespace WB.Shared.Dtos.UMS.ResponseDtos
{
    public class ListErrorLogResponseDto
    {
        public Guid Id { get; set; }
        public DateTime LogDate { get; set; }
        public string? Source { get; set; }
        public string Module { get; set; }
        public string Computer { get; set; }
        public string? CreatedBy { get; set; }
        public string? TerminalId { get; set; }
        public string? IPAddress { get; set; }
        public string? Operation { get; set; }
        public string Message { get; set; }
        public string? StackTrace { get; set; }
        public int TotalCount { get; set; }
    }
}
