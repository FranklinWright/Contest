namespace Contest.Shared
{
    public class Quiz
    {
        public int QuizId { get; set; }
        public int TutorialId { get; set; }
        public int Order { get; set; }
        public required string Question { get; set; }
        public required string Answer { get; set; }
        public required string A { get; set; }
        public required string B { get; set; }
        public required string C { get; set; }
        public required string D { get; set; }
    }
}
