using System.ComponentModel.DataAnnotations;

namespace Contest.Shared
{
    public class Class
    {
        public int ClassId { get; set; }
        [Required]
        [Display(Name = "Class Name")]
        public string? ClassName { get; set; }
        [Required]
        [MinLength(6)]
        [Display(Name = "Class Code")]
        public string? ClassCode { get; set; }
        [Required]
        public Guid UserId { get; set; }
    }
}
