namespace WB.Shared.Dtos.General.ResponseDtos
{
    public record LanguageListResponseDto
    {
        public string Name { get;set; }
        public string Culture { get;set; }
        public string Direction { get;set; }
        public string Flag { get;set; }
        public bool IsDefault { get; set; }
        public bool IsActive { get; set; }
    }
}
