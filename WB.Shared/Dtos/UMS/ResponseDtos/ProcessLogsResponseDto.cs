namespace WB.Shared.Dtos.UMS.ResponseDtos
{
    public class ProcessLogsResponseDto
    {
        public string Process {  get; set; }
        public string? Description { get; set; }
        public DateTime LogDate { get; set; }
    }
}
