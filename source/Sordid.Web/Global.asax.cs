using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Sordid.Core;
using Sordid.Core.Model;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Sordid.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Database.SetInitializer<SordidDbContext>(new SordidDatabaseInitializer<SordidDbContext>());
            ModelBinders.Binders.Add(typeof(DateTime), new CustomDateTimeModelBinder());
        }

        // TODO: Below DB init crap seems a bit hacky. Fix it.
        private bool _dbInitialized = false;
        protected void Application_PostAuthenticateRequest(object sender, System.EventArgs e)
        {
            if (!_dbInitialized)
            {
                _dbInitialized = true;

                // Force the DB to initialize in startup as opposed to whenever we randomly do the first query
                var ctx = new SordidDbContext();
                var count = ctx.Users.Count();
            }
        }
    }

    public class SordidDatabaseInitializer<T> : DropCreateDatabaseIfModelChanges<T>
        where T : SordidDbContext
    {
        protected override void Seed(T context)
        {
            base.Seed(context);

            // TODO: Delete test user stuff before prod.  Makes dev round tripping easier.

            var logger = new Ninject.Extensions.Logging.Log4net.Infrastructure.Log4NetLogger(this.GetType());
            logger.Info("Database re-initializing.  Creating test user and logging in.");

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var user = new ApplicationUser { UserName = "RationalGeek" };
            var result = userManager.Create(user, "password");
            context.SaveChanges();

            var authMgr = HttpContext.Current.GetOwinContext().Authentication;
            authMgr.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
            authMgr.SignIn(new AuthenticationProperties() { IsPersistent = true }, identity);
            System.Threading.Thread.CurrentPrincipal = authMgr.User;

            SeedSkills(context);
            SeedAspects(context);
        }

        private void SeedSkills(T context)
        {
            // TODO: This needs to be moved somewhere better, probably EF migrations script

            context.Skills.Add(new Skill { Type = SkillType.Standard, Name = "Alertness", Trappings = "Avoiding Surprise, Combat Initiative, Passive Awareness" });
            context.Skills.Add(new Skill { Type = SkillType.Standard, Name = "Athletics", Trappings = "Climbing, Dodging, Falling, Jumping, Sprinting, Other Physical Actions" });
            context.Skills.Add(new Skill { Type = SkillType.Standard, Name = "Burglary", Trappings = "Casing, Infiltration, Lockpicking" });
            context.Skills.Add(new Skill { Type = SkillType.Standard, Name = "Contacts", Trappings = "Gathering Information, Getting the Tip-Off, Knowing People, Rumors" });
            context.Skills.Add(new Skill { Type = SkillType.Standard, Name = "Conviction", Trappings = "Acts of Faith, Mental Fortitude" });
            context.Skills.Add(new Skill { Type = SkillType.Standard, Name = "Craftsmanship", Trappings = "Breaking, Building, Fixing" });
            context.Skills.Add(new Skill { Type = SkillType.Standard, Name = "Deceit", Trappings = "Cat and Mouse, Disguise, Distraction and Misdirection, False Face Forward, Falsehood and Deception" });
            context.Skills.Add(new Skill { Type = SkillType.Standard, Name = "Discipline", Trappings = "Concentration, Emotional Control, Mental Defense"});
            context.Skills.Add(new Skill { Type = SkillType.Standard, Name = "Driving", Trappings = "Chases, One Hand on the Wheel, Other Vehicles, Street Knowledge and Navigation"});
            context.Skills.Add(new Skill { Type = SkillType.Standard, Name = "Empathy", Trappings = "Reading People, A Shoulder to Cry On, Social Defense, Social Initiative"});
            context.Skills.Add(new Skill { Type = SkillType.Standard, Name = "Endurance", Trappings = "Long-Term Action, Physical Fortitude"});
            context.Skills.Add(new Skill { Type = SkillType.Standard, Name = "Fists", Trappings = "Brawling, Close-Combat Defense"});
            context.Skills.Add(new Skill { Type = SkillType.Standard, Name = "Guns", Trappings = "Aiming, Gun Knowledge, Gunplay, Other Projectile Weapons"});
            context.Skills.Add(new Skill { Type = SkillType.Standard, Name = "Intimidation", Trappings = "The Brush-Off, Interrogation, Provocation, Social Attacks, Threats"});
            context.Skills.Add(new Skill { Type = SkillType.Standard, Name = "Investigation", Trappings = "Eavesdropping, Examination, Surveillance"});
            context.Skills.Add(new Skill { Type = SkillType.Standard, Name = "Lore", Trappings = "Arcane Research, Common Ritual, Mystic Perception"});
            context.Skills.Add(new Skill { Type = SkillType.Standard, Name = "Might", Trappings = "Breaking Things, Exerting Force, Lifting Things, Wrestling"});
            context.Skills.Add(new Skill { Type = SkillType.Standard, Name = "Performance", Trappings = "Art Appreciation, Composition, Creative Communication, Playing to an Audience"});
            context.Skills.Add(new Skill { Type = SkillType.Standard, Name = "Presence", Trappings = "Charisma, Command, Reputation, Social Fortitude"});
            context.Skills.Add(new Skill { Type = SkillType.Standard, Name = "Rapport", Trappings = "Chit-Chat, Closing Down, First Impressions, Opening Up, Social Defense"});
            context.Skills.Add(new Skill { Type = SkillType.Standard, Name = "Resources", Trappings = "Buying Things, Equipment, Lifestyle, Money Talks, Workspaces"});
            context.Skills.Add(new Skill { Type = SkillType.Standard, Name = "Scholarship", Trappings = "Answers, Computer Use, Declaring Minor Details, Exposition and Knowledge Dumping, Languages, Medical Attention, Research and Lab Work"});
            context.Skills.Add(new Skill { Type = SkillType.Standard, Name = "Stealth", Trappings = "Ambush, Hiding, Shadowing, Skulking"});
            context.Skills.Add(new Skill { Type = SkillType.Standard, Name = "Survival", Trappings = "Animal Handling, Camouflage, Riding, Scavenging, Tracking"});
            context.Skills.Add(new Skill { Type = SkillType.Standard, Name = "Weapons", Trappings = "Melee Combat, Melee Defense, Distance Weaponry, Weapon Knowledge"});
        }

        private void SeedAspects(T context)
        {
            context.Aspects.Add(new Aspect { HeadingLabel = "High Concept", Order =  1 });
            context.Aspects.Add(new Aspect { HeadingLabel = "Trouble", Order = 2 });
            context.Aspects.Add(new Aspect
            {
                PhaseName = "Phase One",
                HeadingLabel = "Background",
                SubHeadingLabel = "Where did you come from?",
                DescriptiveBlurb = "What nation, region, culture are you from? What were your family circumstances like? What's your relationship with your family? How were you educated? What were your friends like? Did you get into trouble much? If you're supernatural, how early did you learn this? Were there problems?",
                EventsLabel = "Events",
                Order = 3,
            });
            context.Aspects.Add(new Aspect
            {
                PhaseName = "Phase Two",
                HeadingLabel = "Rising Conflict",
                SubHeadingLabel = "What shaped you?",
                DescriptiveBlurb = "Who were the prominent people in your life at this point? Do you have enemies? Close and fast friends? How did your high concept and trouble aspects shape you and events around you? What were the most significant choices you made? What lessons did you learn in this time?",
                EventsLabel = "Events",
                Order = 4,
            });
            context.Aspects.Add(new Aspect
            {
                PhaseName = "Phase Three",
                HeadingLabel = "The Story",
                SubHeadingLabel = "What was your first adventure?",
                StoryTitleLabel = "Story Title",
                StarringLabel = "Guest starring...",
                EventsLabel = "Events",
                Order = 5,
            });
            context.Aspects.Add(new Aspect
            {
                PhaseName = "Phase Four",
                HeadingLabel = "Guest Star",
                SubHeadingLabel = "Whose path have you crossed?",
                StoryTitleLabel = "Story Title",
                StarringLabel = "Whose story was this? Who else was in it?",
                EventsLabel = "Events",
                Order = 6,
            });
            context.Aspects.Add(new Aspect
            {
                PhaseName = "Phase Four",
                HeadingLabel = "Guest Star",
                SubHeadingLabel = "Whose else's path have you crossed?",
                StoryTitleLabel = "Story Title",
                StarringLabel = "Whose story was this? Who else was in it?",
                EventsLabel = "Events",
                Order = 7,
            });
        }
    }
}
