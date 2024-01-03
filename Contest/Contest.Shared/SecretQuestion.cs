using System.ComponentModel.DataAnnotations;

namespace Contest.Shared
{
    public class SecretQuestion
    {
        public int SecretQuestionId { get; set; }
        [Required]
        public string? Question { get; set; }
    }
}
