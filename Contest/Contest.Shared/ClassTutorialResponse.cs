using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contest.Shared
{
    public class ClassTutorialResponse
    {
        public int ClassTutorialId { get; set; }
        public int ClassId { get; set; }
        public int TutorialId { get; set; }
        public int LessonCount { get; set; }

        public Class? Class { get; set; }
        public Tutorial? Tutorial { get; set; }
    }
}
