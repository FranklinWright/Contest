using System.ComponentModel.DataAnnotations;

namespace Contest.Shared
{
    public class Class
    {
        public int ClassId { get; set; }
        [Required]
        public string? ClassName { get; set; }
        [Required]
        public string? ClassCode { get; set; }
        [Required]
        public Guid UserId { get; set; }
    }
}
