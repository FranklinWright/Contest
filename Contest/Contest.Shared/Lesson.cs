namespace Contest.Shared
{
    public class Lesson
    {
        public int LessonId { get; set; }
        public int TutorialId { get; set; }
        public required string Title { get; set; }
        public required string Body { get; set; }
        public int Order { get; set; }
    }
}
