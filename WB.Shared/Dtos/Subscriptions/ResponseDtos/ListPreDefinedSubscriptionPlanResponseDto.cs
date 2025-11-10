namespace WB.Shared.Dtos.Subscriptions.ResponseDtos
{
    public class ListPreDefinedSubscriptionPlanResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Sessions { get; set; }
        public int Duration { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedBy { get; set; } = default!;
    }
}
