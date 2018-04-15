using System.Web;
using System.Web.Optimization;

namespace Freelancer
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                      "~/Scripts/angular.js",
                      "~/Scripts/angular-sanitize.js",
                      "~/Scripts/angular-route.js",
                      "~/assets/scripts/controller/MyApp.js",
                      "~/assets/scripts/controller/UploadCtrl.js"
                      ));
            bundles.Add(new ScriptBundle("~/bundles/assets/helper").Include(
                      "~/assets/scripts/helper/TweenMax.min.js",
                      "~/assets/scripts/helper/hammer.min.js",
                      "~/assets/scripts/helper/helper.js",
                      "~/assets/scripts/helper/pixi.min.js",
                      "~/assets/scripts/helper/gpixi.js",
                      "~/assets/scripts/helper/pixi-transform-tool.min.js",
                      "~/assets/scripts/helper/packery.js",
                      "~/assets/scripts/custom.js"
                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
        }
    }
}
