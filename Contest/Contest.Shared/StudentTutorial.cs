using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contest.Shared
{
    public class StudentTutorial
    {
        public int StudentTutorialId { get; set; }
        public Guid UserId { get; set; }
        public int TutorialId { get; set; }
        public int CompletedLessons { get; set; }
        public int? QuizScore { get; set; }
    }
}
