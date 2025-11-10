namespace WB.Shared.Dtos.General.ResponseDtos
{
    public record LookupResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public Dictionary<string, object> ExtraData { get; set; } = new();
    }
}
