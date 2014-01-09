﻿using System.Web.Optimization;

namespace Sordid.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            // TODO: Convert to using CDNs for jquery, etc.

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery.ui").Include(
                        //"~/Scripts/jquery.ui/jquery.ui.*",
                        "~/Scripts/jquery.ui/jquery.ui.core.js",
                        "~/Scripts/jquery.ui/jquery.ui.widget.js",
                        "~/Scripts/jquery.ui/jquery.ui.mouse.js",
                        "~/Scripts/jquery.ui/jquery.ui.draggable.js",
                        "~/Scripts/jquery.ui/jquery.ui.droppable.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/knockout").Include(
                        "~/Scripts/knockout-{version}.js",
                        "~/Scripts/knockout.mapping-latest.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/json").Include(
                        "~/Scripts/json2.js"
                        ));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/site").Include(
                      "~/Scripts/Site/alerts.js",
                      "~/Scripts/Site/errorHandling.js"
                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
        }
    }
}
