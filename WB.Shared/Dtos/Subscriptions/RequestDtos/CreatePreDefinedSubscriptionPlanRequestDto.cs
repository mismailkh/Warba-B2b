namespace WB.Shared.Dtos.Subscriptions.RequestDtos
{
    public class CreatePreDefinedSubscriptionPlanRequestDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Sessions { get; set; }
        public int Duration { get; set; }
        public bool IsActive { get; set; } = true;
        public int VersionNumber { get; set; }
        public string CreatedBy { get; set; } = default!;
        public string? ModifiedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}
