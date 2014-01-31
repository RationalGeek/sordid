using System.Web.Optimization;

namespace Sordid.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            // TODO: Convert to using CDNs for jquery, etc.  Theoretically this happens automagically?

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/lib/jquery/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery.ui").Include(
                        //"~/Scripts/jquery.ui/jquery.ui.*",
                        "~/Scripts/lib/jquery.ui/jquery.ui.core.js",
                        "~/Scripts/lib/jquery.ui/jquery.ui.widget.js",
                        "~/Scripts/lib/jquery.ui/jquery.ui.mouse.js",
                        "~/Scripts/lib/jquery.ui/jquery.ui.draggable.js",
                        "~/Scripts/lib/jquery.ui/jquery.ui.droppable.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/lib/jquery/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/knockout").Include(
                        "~/Scripts/lib/knockout/knockout-{version}.js",
                        "~/Scripts/lib/knockout/knockout.mapping-latest.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/json2").Include(
                        "~/Scripts/lib/json2/json2.js"
                        ));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/lib/modernizr/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/lib/bootstrap/bootstrap.js",
                      "~/Scripts/lib/bootstrap/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/require").Include(
                      "~/Scripts/lib/require/require.js"));

            bundles.Add(new ScriptBundle("~/bundles/site").Include(
                      "~/Scripts/site/requireStart.js",
                      "~/Scripts/site/util.js",
                      "~/Scripts/site/alerts.js",
                      "~/Scripts/site/errorHandling.js",
                      "~/Scripts/site/koDirty.js"
                      ));

            // TODO: Tried to upgrade to Bootstrap 3.1 but it caused style issues with active list group items in powers stock add
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/lib/bootstrap/bootstrap.css",
                      "~/Content/site/site.css"));

            bundles.Add(new StyleBundle("~/Content/css/character").Include(
                      "~/Content/site/character/characterManage.css",
                      "~/Content/site/character/characterManageBasics.css",
                      "~/Content/site/character/characterManageAspects.css",
                      "~/Content/site/character/characterManageSkills.css",
                      "~/Content/site/character/characterManagePowers.css"));
        }
    }
}
