using System.Web;
using System.Web.Optimization;

namespace mvc
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                       "~/Scripts/select2/css/select2.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/Script-custom-editor").Include(
                     "~/Scripts/script-custom-editor.js",
                      "~/Scripts/summernote.js"));

            bundles.Add(new ScriptBundle("~/bundles/custom").Include(
                    "~/Scripts/autocomplete/jquery.autocomplete.min.js",
                    "~/Scripts/select2/js/select2.full.js"
           
                    ));

            //bundles.Add(new ScriptBundle("~/bundles/refreshval").Include(
            //            "~/Scripts/jquery-1.12.4.min.js"));
        }
    }
}
