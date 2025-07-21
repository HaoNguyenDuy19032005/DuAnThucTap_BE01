namespace Nhom2ThucTap.DTO
{
    public class TestStudentAnswerDto
    {
        public int SubmissionId { get; set; }
        public int? QuestionId { get; set; }
        public string? SelectedOption { get; set; }
        public string? AnswerContent { get; set; }
        public IFormFile? Attachment { get; set; }
    }

}
