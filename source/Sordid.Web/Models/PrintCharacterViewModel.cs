﻿using Sordid.Core.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public string BuildBubbles(int n)
        {
            var sb = new StringBuilder();
            for (int i = 1; i <= 8; i++)
            {
                sb.Append("<span class=\"bubble");
                if (i > n)
                {
                    sb.Append(" disabled");
                }
                sb.Append("\"></span>");
            }
            return sb.ToString();
        }

        public string FormatCost(int n)
        {
            if (n >= 0)
            {
                return string.Format("+{0}", n);
            }
            else
            {
                return n.ToString();
            }
        }

        public IList<Consequence> GetConsequencesOrdered()
        {
            return Character.Consequences
                .OrderBy(c => (c.Type == "Extreme" && !c.UserCreated) ? 1 : 0) // Extreme goes to the very bottom
                .ThenBy(c => (c.UserCreated) ? 1 : 0)                          // User created goes after system created
                .ThenByDescending(c => c.StressAmount)                         // Then sort by stress amount
                .ToList();
        }
    }
}