﻿using Sordid.Core.Interfaces;
using System.Web.Script.Serialization;

namespace Sordid.Core.Model
{
    public class CharacterAspect : BaseEntity, IIdKeyedEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Events { get; set; }
        public string Starring { get; set; }
        public string StoryTitle { get; set; }

        public int CharacterId { get; set; }
        [ScriptIgnore] // TODO: Ew, fix this JSON ignore attribute somehow to avoid circular references...
        public Character Character { get; set; }

        public int AspectId { get; set; }
        public Aspect Aspect { get; set; }
    }
}
