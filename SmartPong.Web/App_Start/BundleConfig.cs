using System.Web.Optimization;
using BundleTransformer.Core.Builders;
using BundleTransformer.Core.Orderers;
using BundleTransformer.Core.Resolvers;
using BundleTransformer.Core.Transformers;

namespace SmartPong
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            //bundles.Add(new StyleBundle("/content/smartadmin").IncludeDirectory("/css", "*.min.css"));
            bundles.IgnoreList.Clear();
            bundles.UseCdn = true;
            //var nullBuilder = new NullBuilder();
            //var styleTransformer = new StyleTransformer();
            //var scriptTransformer = new ScriptTransformer();
            //var nullOrderer = new NullOrderer();
            //BundleResolver.Current = new CustomBundleResolver();

            bundles.Add(new ScriptBundle("~/js/smartadmin").Include(
                "~/js/app.config.js",
                "~/js/plugin/jquery-touch/jquery.ui.touch-punch.min.js",
                "~/js/bootstrap/bootstrap.min.js",
                "~/js/notification/SmartNotification.min.js",
                "~/js/smartwidgets/jarvis.widget.min.js",
                "~/js/plugin/jquery-validate/jquery.validate.min.js",
                "~/js/plugin/masked-input/jquery.maskedinput.min.js",
                "~/js/plugin/select2/select2.min.js",
                "~/js/plugin/bootstrap-slider/bootstrap-slider.min.js",
                "~/js/plugin/bootstrap-progressbar/bootstrap-progressbar.min.js",
                "~/js/plugin/msie-fix/jquery.mb.browser.min.js",
                "~/js/plugin/fastclick/fastclick.min.js",
                "~/js/app.min.js"));
            //var smartadminScriptBundle = new Bundle("~/js/smartadmin");
            //smartadminScriptBundle.Include(
            //    "~/js/app.config.js",
            //    "~/js/plugin/jquery-touch/jquery.ui.touch-punch.min.js",
            //    "~/js/bootstrap/bootstrap.min.js",
            //    "~/js/notification/SmartNotification.min.js",
            //    "~/js/smartwidgets/jarvis.widget.min.js",
            //    "~/js/plugin/jquery-validate/jquery.validate.min.js",
            //    "~/js/plugin/masked-input/jquery.maskedinput.min.js",
            //    "~/js/plugin/select2/select2.min.js",
            //    "~/js/plugin/bootstrap-slider/bootstrap-slider.min.js",
            //    "~/js/plugin/bootstrap-progressbar/bootstrap-progressbar.min.js",
            //    "~/js/plugin/msie-fix/jquery.mb.browser.min.js",
            //    "~/js/plugin/fastclick/fastclick.min.js",
            //    "~/js/app.min.js");
            //smartadminScriptBundle.Builder = nullBuilder;
            //smartadminScriptBundle.Transforms.Add(styleTransformer);
            //smartadminScriptBundle.Orderer = nullOrderer;
            //bundles.Add(smartadminScriptBundle);

            //bundles.Add(new ScriptBundle("/js/full-calendar").Include(
            //    "/js/plugin/moment/moment.min.js",
            //    "/js/plugin/fullcalendar/jquery.fullcalendar.min.js"
            //    ));

            //bundles.Add(new ScriptBundle("/js/charts").Include(
            //    "/js/plugin/easy-pie-chart/jquery.easy-pie-chart.min.js",
            //    "/js/plugin/sparkline/jquery.sparkline.min.js",
            //    "/js/plugin/morris/morris.min.js",
            //    "/js/plugin/morris/raphael.min.js",
            //    "/js/plugin/flot/jquery.flot.cust.min.js",
            //    "/js/plugin/flot/jquery.flot.resize.min.js",
            //    "/js/plugin/flot/jquery.flot.time.min.js",
            //    "/js/plugin/flot/jquery.flot.fillbetween.min.js",
            //    "/js/plugin/flot/jquery.flot.orderBar.min.js",
            //    "/js/plugin/flot/jquery.flot.pie.min.js",
            //    "/js/plugin/flot/jquery.flot.tooltip.min.js",
            //    "/js/plugin/dygraphs/dygraph-combined.min.js",
            //    "/js/plugin/chartjs/chart.min.js"
            //    ));

            //bundles.Add(new ScriptBundle("~/js/datatables").Include(
            //    "/js/plugin/datatables/jquery.dataTables.min.js",
            //    "/js/plugin/datatables/dataTables.colVis.min.js",
            //    "/js/plugin/datatables/dataTables.tableTools.min.js",
            //    "/js/plugin/datatables/dataTables.bootstrap.min.js",
            //    "/js/plugin/datatable-responsive/datatables.responsive.min.js"
            //    ));

            //bundles.Add(new ScriptBundle("/js/jq-grid").Include(
            //    "/js/plugin/jqgrid/jquery.jqGrid.min.js",
            //    "/js/plugin/jqgrid/grid.locale-en.min.js"
            //    ));

            //bundles.Add(new ScriptBundle("/js/forms").Include(
            //    "/js/plugin/jquery-form/jquery-form.min.js"
            //    ));

            //bundles.Add(new ScriptBundle("/js/vector-map").Include(
            //    "/js/plugin/vectormap/jquery-jvectormap-1.2.2.min.js",
            //    "/js/plugin/vectormap/jquery-jvectormap-world-mill-en.js"
            //    ));
            bundles.Add(new StyleBundle("~/Content/kendo/2018.1.117/fonts/glyph").Include(
                "~/Content/kendo/2018.1.117/fonts/glyphs/WebComponentsIcons.ttf?gedxeo"));
               // "~/Content/kendo/2018.1.117/Bootstrap/loading-image.gif"));

            bundles.Add(new StyleBundle("~/css/bootstrap").Include(
                "~/Content/css/bootstrap.min.css"));

            //var bootstrapStylesBundle = new Bundle("~/css/bootstrap");
            //bootstrapStylesBundle.Include(
            //    "~/Content/css/bootstrap.min.css");
            //bootstrapStylesBundle.Builder = nullBuilder;
            //bootstrapStylesBundle.Transforms.Add(styleTransformer);
            //bootstrapStylesBundle.Orderer = nullOrderer;
            //bundles.Add(bootstrapStylesBundle);

            bundles.Add(new ScriptBundle("~/Script/js/skin").Include(
                "~/Script/js/skins.min.js"));

            //bundles.Add(new StyleBundle("~/img/load").Include(
              //  "~/Content/kendo/2018.1.117/Bootstrap/loading-image.gif"));


            //var skinBundle = new Bundle("~/bundles/skin");
            //skinBundle.Include("~/Script/js/skins.min.js");
            //skinBundle.Builder = nullBuilder;
            //skinBundle.Transforms.Add(scriptTransformer);
            //skinBundle.Orderer = nullOrderer;
            //bundles.Add(skinBundle);

            bundles.Add(new StyleBundle("~/css/kendo").Include(
                       "~/Content/kendo/2018.1.117/kendo.common.min.css",
                     "~/Content/kendo/2018.1.117/kendo.default.min.css",
                     "~/Content/kendo/2018.1.117/kendo.common-bootstrap.min.css",
                     "~/Content/kendo/2018.1.117/kendo.bootstrap.min.css"
                ));
            //var kendoBundle = new Bundle("~/css/kendo");
            //kendoBundle.Include(
            //    "~/Content/kendo/2018.1.117/kendo.common.min.css",
            //    "~/Content/kendo/2018.1.117/kendo.default.min.css",
            //    "~/Content/kendo/2018.1.117/kendo.common-bootstrap.min.css",
            //    "~/Content/kendo/2018.1.117/kendo.bootstrap.min.css");
            //kendoBundle.Builder = nullBuilder;
            //kendoBundle.Transforms.Add(styleTransformer);
            //kendoBundle.Orderer = nullOrderer;
            //bundles.Add(kendoBundle);

            bundles.Add(new ScriptBundle("~/bundles/kendo").Include(
                "~/Script/kendo/2018.1.117/kendo.all.min.js",
                // "~/Script/kendo/kendo.timezones.min.js", // uncomment if using the Scheduler
                "~/Script/kendo/2018.1.117/kendo.aspnetmvc.min.js"
            ));
            //var kendoScriptBundle = new Bundle("~/bundles/kendo");
            //kendoScriptBundle.Include(
            //    "~/Script/kendo/2018.1.117/kendo.all.min.js",
            //    // "~/Script/kendo/kendo.timezones.min.js", // uncomment if using the Scheduler
            //    "~/Script/kendo/2018.1.117/kendo.aspnetmvc.min.js");
            //kendoScriptBundle.Builder = nullBuilder;
            //kendoScriptBundle.Transforms.Add(scriptTransformer);
            //kendoScriptBundle.Orderer = nullOrderer;
            //bundles.Add(kendoScriptBundle);

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Script/js/jquery.min.js",
                "~/Script/js/slimscroll/jquery.slimscroll.min.js"
                ));
            //var jQueryBundle = new Bundle("~/bundles/jquery");
            //jQueryBundle.Include("~/Script/js/jquery.min.js",
            //    "~/Script/js/slimscroll/jquery.slimscroll.min.js");
            //jQueryBundle.Builder = nullBuilder;
            //jQueryBundle.Transforms.Add(scriptTransformer);
            //jQueryBundle.Orderer = nullOrderer;
            //bundles.Add(jQueryBundle);

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Script/js/jqueryval/jquery.validate"
            ));
            //var jQueryValBundle = new Bundle("~/bundles/jqueryval");
            //jQueryValBundle.Include("~/Script/js/jqueryval/jquery.validate*");
            //jQueryValBundle.Builder = nullBuilder;
            //jQueryValBundle.Transforms.Add(scriptTransformer);
            //jQueryValBundle.Orderer = nullOrderer;
            //bundles.Add(jQueryValBundle);

            //var jQueryBundle = new ScriptBundle("~/bundles/jquery");
            //jQueryBundle.Include("~/Script/js/jquery.min.js",
            //    "~/Script/js/slimscroll/jquery.slimscroll.min.js");

            // var kendoBundle = new Bundle("~/css/kendo");
            // kendoBundle.Include(
            //     "~/Content/kendo/2018.1.117/kendo.common.min.css",
            //     "~/Content/kendo/2018.1.117/kendo.default.min.css",
            //     "~/Content/kendo/2018.1.117/kendo.common-bootstrap.min.css",
            //     "~/Content/kendo/2018.1.117/kendo.bootstrap.min.css");
            // kendoBundle.Builder = nullBuilder;
            //// kendoBundle.Transforms.Add(styleTransformer);
            // //kendoBundle.Orderer = nullOrderer;
            // bundles.Add(kendoBundle);

            // var kendoScriptBundle = new Bundle("~/bundles/kendo");
            // kendoScriptBundle.Include(
            //     "~/Script/kendo/2018.1.117/kendo.all.min.js",
            //     // "~/Script/kendo/kendo.timezones.min.js", // uncomment if using the Scheduler
            //     "~/Script/kendo/2018.1.117/kendo.aspnetmvc.min.js");
            // //kendoScriptBundle.Builder = nullBuilder;
            // //kendoScriptBundle.Transforms.Add(scriptTransformer);
            // //kendoScriptBundle.Orderer = nullOrderer;
            // bundles.Add(kendoScriptBundle);

            BundleTable.EnableOptimizations = true;
        }
    }
}