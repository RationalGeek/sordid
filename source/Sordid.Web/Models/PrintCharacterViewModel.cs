using Sordid.Core.Model;

namespace Sordid.Web.Models
{
    public class PrintCharacterViewModel
    {
        public Character Character { get; set; }

        public string TranslateAspectTitle(string aspectTitle)
        {
            var ret = aspectTitle;
            if (ret != "High Concept" && ret != "Trouble")
            {
                ret = "Other Aspects";
            }
            return ret;
        }

        // TODO: This should be reused in the manage UI where we also translate this - should it be in DB?
        public string TranslateSkillRank(int rank)
        {
            switch(rank)
            {
                case 0:
                    return "Mediocre";
                case 1:
                    return "Average";
                case 2:
                    return "Fair";
                case 3:
                    return "Good";
                case 4:
                    return "Great";
                case 5:
                    return "Superb";
            }

            return "WTF?";
        }
    }
}