using Sordid.Core.Model;
using System.Collections.Generic;

namespace Sordid.Web.Models
{
    public class ManageCharacterViewModel
    {
        public Character Character { get; set; }
        public List<PowerLevel> PowerLevels { get; set; }
    }
}