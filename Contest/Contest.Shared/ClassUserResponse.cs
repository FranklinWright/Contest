namespace Contest.Shared
{
    public class ClassUserResponse
    {
        public int ClassUserId { get; set; }
        public int ClassId { get; set; }
        public Guid UserId { get; set; }

        public string ClassName { get; set; } = default!;

        public List<Student> Students { get; set; } = default!;
    }

    public class Student
    {
        public Guid UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
