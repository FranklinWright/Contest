namespace Contest.Shared
{
    public class Quiz
    {
        public int QuizId { get; set; }
        public int TutorialId { get; set; }
        public int Order { get; set; }
        public string? Question { get; set; }
        public string? Answer { get; set; }
        public string? A { get; set; }
        public string? B { get; set; }
        public string? C { get; set; }
        public string? D { get; set; }
    }
}
