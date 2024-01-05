using System.ComponentModel.DataAnnotations;

namespace Contest.Shared
{
    public class Lesson
    {
        public int LessonId { get; set; }
        public int TutorialId { get; set; }
        [Required]
        public string? Title { get; set; }
        public string? Body { get; set; }
        public int Order { get; set; }
    }
}
