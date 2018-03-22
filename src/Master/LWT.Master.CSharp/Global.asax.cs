using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Optimization;

namespace LWT.Master.CSharp
{

    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery.mCustomScrollbar.concat.min.js",
                "~/Scripts/jquery.qrcode.min.js",
                "~/Scripts/URI.js",
                "~/Scripts/highlight.pack.js",
                "~/Scripts/clipboard.min.js",
                "~/Scripts/bootstrap.min.js",
                "~/Scripts/common.js",
                "~/Scripts/search.js",
                "~/Scripts/pages.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/scripts-main").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery.mCustomScrollbar.concat.min.js",
                "~/Scripts/jquery.parallax.min.js",
                "~/Scripts/bootstrap.min.js",
                "~/Scripts/common.js",
                "~/Scripts/search.js",
                "~/Scripts/landing.js"
            ));


            bundles.Add(new StyleBundle("~/bundles/styles").Include(
                "~/Content/demo-icons.css",
                "~/Content/jquery.mCustomScrollbar.min.css",
                "~/Content/font-awesome.min.css",
                "~/Content/highlightjs.default.css",
                "~/Content/common.css",
                "~/Content/themes.css",
                "~/Content/pages.css",
                "~/Content/demos.css"
            ));

            bundles.Add(new StyleBundle("~/bundles/styles-main").Include(
                "~/Content/demo-icons.css",
                "~/Content/jquery.mCustomScrollbar.min.css",
                "~/Content/bootstrap.min.css",
                "~/Content/common.css",
                "~/Content/landing.css"
            ));

            BundleTable.EnableOptimizations = true;
        }
    }

    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            BundleConfig.RegisterBundles(System.Web.Optimization.BundleTable.Bundles);
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}