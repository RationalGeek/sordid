using Sordid.Core.Interfaces;

namespace Sordid.Core.Model
{
    public class Aspect : BaseEntity, IIdKeyedEntity
    {
        public int Id { get; set; }
        public string MainLabel { get; set; }
        public string Title { get; set; }
        public string Notes { get; set; }
    }

    public class PhaseAspect : Aspect
    {
        public PhaseAspect()
        {
            MainLabel = "Phase Aspect";
        }

        public string PhaseName { get; set; }
        public string HeadingLabel { get; set; }    // Rising Conflict
        public string SubHeadingLabel { get; set; } // What Shaped You?
        public string DescriptiveBlurb { get; set; }
        public string Events { get; set; }
    }

    public class StoryAspect : PhaseAspect
    {
        public string StoryTitle { get; set; }
        public string StarringLabel { get; set; }
        public string Starring { get; set; }
    }
}
