using System.ComponentModel.DataAnnotations;

namespace Contest.Shared
{
    public class Tutorial
    {
        public int TutorialId { get; set; }
        [Required]
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Tags { get; set; }
    }
}
