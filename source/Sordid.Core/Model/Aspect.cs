using Sordid.Core.Interfaces;

namespace Sordid.Core.Model
{
    public class Aspect : BaseEntity, IIdKeyedEntity
    {
        public int Id { get; set; }
        public int Order { get; set; }

        public string PhaseName { get; set; }
        public string HeadingLabel { get; set; }     // Rising Conflict
        public string SubHeadingLabel { get; set; }  // What Shaped You?
        public string DescriptiveBlurb { get; set; } // Who were the prominnent people in your life at this point?  Etc...

        public string EventsLabel { get; set; }      // Events
        public string StoryTitleLabel { get; set; }  // Story Title
        public string StarringLabel { get; set; }    // Whose story was this? Who else was in it?
    }
}
