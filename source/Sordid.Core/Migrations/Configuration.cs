namespace Sordid.Core.Migrations
{
    using Sordid.Core.Model;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Sordid.Core.SordidDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            // TODO: Disable AutomaticMigrationDataLossAllowed before production
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "Sordid.Core.SordidDbContext";
        }

        // TODO: Seed didn't seem to run the first time running app after deleting DB?

        protected override void Seed(Sordid.Core.SordidDbContext context)
        {
            // TODO: All of this AddOrUpdate is causing DateAdded tracking column to be reset on every run

            context.Skills.AddOrUpdate(s => s.Name,
                new Skill { Type = SkillType.Standard, Name = "Alertness", Trappings = "Avoiding Surprise, Combat Initiative, Passive Awareness" },
                new Skill { Type = SkillType.Standard, Name = "Athletics", Trappings = "Climbing, Dodging, Falling, Jumping, Sprinting, Other Physical Actions" },
                new Skill { Type = SkillType.Standard, Name = "Burglary", Trappings = "Casing, Infiltration, Lockpicking" },
                new Skill { Type = SkillType.Standard, Name = "Contacts", Trappings = "Gathering Information, Getting the Tip-Off, Knowing People, Rumors" },
                new Skill { Type = SkillType.Standard, Name = "Conviction", Trappings = "Acts of Faith, Mental Fortitude" },
                new Skill { Type = SkillType.Standard, Name = "Craftsmanship", Trappings = "Breaking, Building, Fixing" },
                new Skill { Type = SkillType.Standard, Name = "Deceit", Trappings = "Cat and Mouse, Disguise, Distraction and Misdirection, False Face Forward, Falsehood and Deception" },
                new Skill { Type = SkillType.Standard, Name = "Discipline", Trappings = "Concentration, Emotional Control, Mental Defense"},
                new Skill { Type = SkillType.Standard, Name = "Driving", Trappings = "Chases, One Hand on the Wheel, Other Vehicles, Street Knowledge and Navigation"},
                new Skill { Type = SkillType.Standard, Name = "Empathy", Trappings = "Reading People, A Shoulder to Cry On, Social Defense, Social Initiative"},
                new Skill { Type = SkillType.Standard, Name = "Endurance", Trappings = "Long-Term Action, Physical Fortitude"},
                new Skill { Type = SkillType.Standard, Name = "Fists", Trappings = "Brawling, Close-Combat Defense"},
                new Skill { Type = SkillType.Standard, Name = "Guns", Trappings = "Aiming, Gun Knowledge, Gunplay, Other Projectile Weapons"},
                new Skill { Type = SkillType.Standard, Name = "Intimidation", Trappings = "The Brush-Off, Interrogation, Provocation, Social Attacks, Threats"},
                new Skill { Type = SkillType.Standard, Name = "Investigation", Trappings = "Eavesdropping, Examination, Surveillance"},
                new Skill { Type = SkillType.Standard, Name = "Lore", Trappings = "Arcane Research, Common Ritual, Mystic Perception"},
                new Skill { Type = SkillType.Standard, Name = "Might", Trappings = "Breaking Things, Exerting Force, Lifting Things, Wrestling"},
                new Skill { Type = SkillType.Standard, Name = "Performance", Trappings = "Art Appreciation, Composition, Creative Communication, Playing to an Audience"},
                new Skill { Type = SkillType.Standard, Name = "Presence", Trappings = "Charisma, Command, Reputation, Social Fortitude"},
                new Skill { Type = SkillType.Standard, Name = "Rapport", Trappings = "Chit-Chat, Closing Down, First Impressions, Opening Up, Social Defense"},
                new Skill { Type = SkillType.Standard, Name = "Resources", Trappings = "Buying Things, Equipment, Lifestyle, Money Talks, Workspaces"},
                new Skill { Type = SkillType.Standard, Name = "Scholarship", Trappings = "Answers, Computer Use, Declaring Minor Details, Exposition and Knowledge Dumping, Languages, Medical Attention, Research and Lab Work"},
                new Skill { Type = SkillType.Standard, Name = "Stealth", Trappings = "Ambush, Hiding, Shadowing, Skulking"},
                new Skill { Type = SkillType.Standard, Name = "Survival", Trappings = "Animal Handling, Camouflage, Riding, Scavenging, Tracking"},
                new Skill { Type = SkillType.Standard, Name = "Weapons", Trappings = "Melee Combat, Melee Defense, Distance Weaponry, Weapon Knowledge"}
                );

            context.Aspects.AddOrUpdate(a => a.Order,
                new Aspect { HeadingLabel = "High Concept", Order = 1 },
                new Aspect { HeadingLabel = "Trouble", Order = 2 },
                new Aspect
                    {
                        HeadingLabel = "Background",
                        SubHeadingLabel = "Where did you come from?",
                        DescriptiveBlurb = "What nation, region, culture are you from? What were your family circumstances like? What's your relationship with your family? How were you educated? What were your friends like? Did you get into trouble much? If you're supernatural, how early did you learn this? Were there problems?",
                        EventsLabel = "Events",
                        Order = 3,
                    },
                new Aspect
                    {
                        HeadingLabel = "Rising Conflict",
                        SubHeadingLabel = "What shaped you?",
                        DescriptiveBlurb = "Who were the prominent people in your life at this point? Do you have enemies? Close and fast friends? How did your high concept and trouble aspects shape you and events around you? What were the most significant choices you made? What lessons did you learn in this time?",
                        EventsLabel = "Events",
                        Order = 4,
                    },
                new Aspect
                    {
                        HeadingLabel = "The Story",
                        SubHeadingLabel = "What was your first adventure?",
                        StoryTitleLabel = "Story Title",
                        StarringLabel = "Guest starring...",
                        EventsLabel = "Events",
                        Order = 5,
                    },
                new Aspect
                    {
                        HeadingLabel = "Guest Star",
                        SubHeadingLabel = "Whose path have you crossed?",
                        StoryTitleLabel = "Story Title",
                        StarringLabel = "Whose story was this? Who else was in it?",
                        EventsLabel = "Events",
                        Order = 6,
                    },
                new Aspect
                    {
                        HeadingLabel = "Guest Star",
                        SubHeadingLabel = "Whose else's path have you crossed?",
                        StoryTitleLabel = "Story Title",
                        StarringLabel = "Whose story was this? Who else was in it?",
                        EventsLabel = "Events",
                        Order = 7,
                    }
                );

            context.Powers.AddOrUpdate(p => new { p.Type, p.Name },
                new Power { Type = PowerType.Stock, Name = "Evocation", Cost = -3 },
                new Power { Type = PowerType.Stock, Name = "Thaumaturgy", Cost = -3 },
                new Power { Type = PowerType.Stock, Name = "The Sight", Cost = -1 },
                new Power { Type = PowerType.Stock, Name = "Soulgaze", Cost = -1 },
                new Power { Type = PowerType.Stock, Name = "Wizard's Constitution", Cost = 0 }
                );

            context.PowerLevels.AddOrUpdate(p => p.Name,
                new PowerLevel { Name = "Feet in the Water", BaseRefresh = 6, SkillPoints = 20, MaxSkillRank = 4 },
                new PowerLevel { Name = "Up to Your Waist",  BaseRefresh = 7, SkillPoints = 25, MaxSkillRank = 4 },
                new PowerLevel { Name = "Chest-Deep",        BaseRefresh = 8, SkillPoints = 30, MaxSkillRank = 5 },
                new PowerLevel { Name = "Submerged",         BaseRefresh = 8, SkillPoints = 35, MaxSkillRank = 5 }
                );

            context.Templates.AddOrUpdate(t => t.Name,
                new Template { Name = "Wizard" },
                new Template { Name = "True Mortal" },
                new Template { Name = "Wild Talent" },
                new Template { Name = "Shapechanger" }
                );
        }
    }
}
