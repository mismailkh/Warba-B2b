namespace WB.Shared.Dtos.Assessment
{
    public class CreateAssessmentRequestDto
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public int StatusId { get; set; }
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public string CreatedBy { get; set; }
        public bool IsDefault { get; set; }
        public bool IsDeleted { get; set; }

        public Guid? DefaultAssessmentId { get; set; }
        public IEnumerable<int>? subcategoryUsersId { get; set; }
        public List<QuestionDto> Questions { get; set; } = new List<QuestionDto>();

    }
    public class QuestionDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int TypeId { get; set; }
        public bool IsAvailableForAll { get; set; } = false;
        public List<QuestionOptionDto> Options { get; set; } = new List<QuestionOptionDto>();
    }

    public class QuestionOptionDto
    {
        public Guid Id { get; set; }
        public string Value { get; set; }
    }
}
