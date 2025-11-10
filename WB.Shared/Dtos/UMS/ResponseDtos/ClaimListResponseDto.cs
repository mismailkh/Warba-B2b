namespace WB.Shared.Dtos.UMS.ResponseDtos
{
    public record ClaimListResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
        public int ModuleId { get; set; }
    }

    public class ClaimWithLinkedListResponseDto
    {
        public int Id { get; set; }
        public string RoleId { get; set; }
        public string Title { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
        public bool IsLinked { get; set; }
    }
}
