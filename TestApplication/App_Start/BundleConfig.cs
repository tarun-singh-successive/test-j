using System.Web;
using System.Web.Optimization;

namespace TestApplication
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/custom.js",
                        "~/Assets/js/project.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*",
                        "~/Assets/js/project.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/sitecss").Include(
                     "~/assets/images/favicon.ico",
                     "~/assets/css/bootstrap.min.css",
                     "~/plugins/select2/css/select2.min.css",
                     "~/plugins/datatables/dataTables.bootstrap4.min.css",
                     "~/plugins/datatables/buttons.bootstrap4.min.css",
                     "~/plugins/sweet-alert2/sweetalert2.min.css",
                     "~/plugins/chartist/css/chartist.min.css",
                     
                     "~/assets/css/metismenu.min.css",
                     "~/assets/css/icons.css",
                     "~/assets/css/style.css"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/style.css",
                      "~/Content/metismenu.min.css",
                      "~/Content/icons.css",
                      "~/Content/Akash.css",
                       "~/Content/Site.css,",
                       "~/assets/css/bootstrap-multiselect.css"));

            bundles.Add(new ScriptBundle("~/bundles/datetime").Include(
                     "~/Scripts/moment*",
                     "~/Scripts/bootstrap-datetimepicker*"));
        }
    }
}
