using System.Web;
using System.Web.Optimization;

namespace LinkedIn_Test
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
            "~/Scripts/modernizr-*",
            "~/Scripts/jquery-{version}.js",
            "~/Scripts/jquery.validate.js",
            "~/Scripts/jquery.validate.unobtrusive.js",
            "~/Scripts/jquery.unobtrusive-ajax.js",
            "~/Scripts/bootstrap.js",
            "~/Scripts/CustomScripts/mainLayoutScript.js",
            "~/Scripts/CustomScripts/Messages.js",
            "~/Scripts/CustomScripts/ProfileScripts.js",
            "~/Scripts/CustomScripts/HomeScript.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/jquery-confirm.css",
                      "~/Content/CustomStyles/mainLayoutStyle.css",
                      "~/Content/CustomStyles/NavbarStyle.css",
                      "~/Content/CustomStyles/MessagePopupStyle.css",
                      "~/Content/CustomStyles/MessagePageStyle.css",
                      "~/Content/ProfilePageStyle.css",
                      "~/Content/CustomStyles/Home_page.css"));

        }
    }
}
