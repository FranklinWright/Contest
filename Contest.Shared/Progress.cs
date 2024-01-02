namespace Contest.Shared
{
    public class Progress
    {
        public int ProgressId { get; set; }
        public int UserId { get; set; }
        public int LessonId { get; set; }
        public int CompletedLessons { get; set; }
        public int QuizScore { get; set; }
    }
}
