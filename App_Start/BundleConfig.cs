using System.Web;
using System.Web.Optimization;

namespace BookLibarySystem
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            // Bundle for JS
            bundles.Add(new Bundle("~/bundles/jquery").Include(
                "~/Scripts/vendor/jquery.min.js"));
            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                "~/Scripts/vendor/bootstrap.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                 "~/Scripts/vendor/modernizr-3.5.0.min.js",
                 "~/Scripts/popper.min.js",
                 "~/Scripts/plugins.js",
                 "~/Scripts/active.js"
             ));

            // Bundle for CSS
            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/css/bootstrap.min.css",
                "~/Content/css/plugins.css",
                "~/Content/css/style.css"
                ));
            bundles.Add(new Bundle("~/Content/custom-css").Include(
                "~/Content/css/custom.css"));
        }
    }
}
