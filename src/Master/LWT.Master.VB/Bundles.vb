
Imports System.Web.Optimization

Public Class BundleConfig
    Public Shared Sub RegisterBundles(bundles As BundleCollection)
        bundles.Add(New ScriptBundle("~/bundles/scripts").Include("~/Scripts/jquery-{version}.js", "~/Scripts/jquery.mCustomScrollbar.concat.min.js", "~/Scripts/jquery.qrcode.min.js", "~/Scripts/URI.js", "~/Scripts/highlight.pack.js", "~/Scripts/clipboard.min.js", _
            "~/Scripts/bootstrap.min.js", "~/Scripts/common.js", "~/Scripts/search.js", "~/Scripts/pages.js"))

        bundles.Add(New ScriptBundle("~/bundles/scripts-main").Include("~/Scripts/jquery-{version}.js", "~/Scripts/jquery.mCustomScrollbar.concat.min.js", "~/Scripts/jquery.parallax.min.js", "~/Scripts/bootstrap.min.js", "~/Scripts/common.js", "~/Scripts/search.js", _
            "~/Scripts/landing.js"))


        bundles.Add(New StyleBundle("~/bundles/styles").Include("~/Content/demo-icons.css", "~/Content/jquery.mCustomScrollbar.min.css", "~/Content/font-awesome.min.css", "~/Content/highlightjs.default.css", "~/Content/common.css", "~/Content/themes.css", _
            "~/Content/pages.css", "~/Content/demos.css"))

        bundles.Add(New StyleBundle("~/bundles/styles-main").Include("~/Content/demo-icons.css", "~/Content/jquery.mCustomScrollbar.min.css", "~/Content/bootstrap.min.css", "~/Content/common.css", "~/Content/landing.css"))

        BundleTable.EnableOptimizations = True
    End Sub
End Class
