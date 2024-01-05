using System.ComponentModel.DataAnnotations;

namespace Contest.Shared
{
    public class Quiz
    {
        public int QuizId { get; set; }
        public int TutorialId { get; set; }
        [Required]
        public string? Topic { get; set; }
        public int Order { get; set; }
        [Required]
        public string? Question { get; set; }
        [Required]
        public string? Answer { get; set; }
        [Required]
        public string? A { get; set; }
        [Required]
        public string? B { get; set; }
        [Required]
        public string? C { get; set; }
        [Required]
        public string? D { get; set; }
        
    }
}
