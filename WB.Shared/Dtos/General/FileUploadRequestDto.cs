using Microsoft.AspNetCore.Http;

namespace WB.Shared.Dtos.General
{    public class FileUploadRequestDto
    {
        public string TargetTable { get; set; }
        public int? SelectedForeignKeyId { get; set; }
        public IFormFile File { get; set; }
        public string CreatedBy { get; set; }
    }
}
