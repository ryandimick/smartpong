using System.Web.Optimization;

namespace SmartPong
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("/content/smartadmin").IncludeDirectory("/css", "*.min.css"));

            bundles.Add(new ScriptBundle("/js/smartadmin").Include(
                "/js/app.config.js",
                "/js/plugin/jquery-touch/jquery.ui.touch-punch.min.js",
                "/js/bootstrap/bootstrap.min.js",
                "/js/notification/SmartNotification.min.js",
                "/js/smartwidgets/jarvis.widget.min.js",
                "/js/plugin/jquery-validate/jquery.validate.min.js",
                "/js/plugin/masked-input/jquery.maskedinput.min.js",
                "/js/plugin/select2/select2.min.js",
                "/js/plugin/bootstrap-slider/bootstrap-slider.min.js",
                "/js/plugin/bootstrap-progressbar/bootstrap-progressbar.min.js",
                "/js/plugin/msie-fix/jquery.mb.browser.min.js",
                "/js/plugin/fastclick/fastclick.min.js",
                "/js/app.min.js"));

            bundles.Add(new ScriptBundle("/js/full-calendar").Include(
                "/js/plugin/moment/moment.min.js",
                "/js/plugin/fullcalendar/jquery.fullcalendar.min.js"
                ));

            bundles.Add(new ScriptBundle("/js/charts").Include(
                "/js/plugin/easy-pie-chart/jquery.easy-pie-chart.min.js",
                "/js/plugin/sparkline/jquery.sparkline.min.js",
                "/js/plugin/morris/morris.min.js",
                "/js/plugin/morris/raphael.min.js",
                "/js/plugin/flot/jquery.flot.cust.min.js",
                "/js/plugin/flot/jquery.flot.resize.min.js",
                "/js/plugin/flot/jquery.flot.time.min.js",
                "/js/plugin/flot/jquery.flot.fillbetween.min.js",
                "/js/plugin/flot/jquery.flot.orderBar.min.js",
                "/js/plugin/flot/jquery.flot.pie.min.js",
                "/js/plugin/flot/jquery.flot.tooltip.min.js",
                "/js/plugin/dygraphs/dygraph-combined.min.js",
                "/js/plugin/chartjs/chart.min.js"
                ));

            bundles.Add(new ScriptBundle("~/js/datatables").Include(
                "/js/plugin/datatables/jquery.dataTables.min.js",
                "/js/plugin/datatables/dataTables.colVis.min.js",
                "/js/plugin/datatables/dataTables.tableTools.min.js",
                "/js/plugin/datatables/dataTables.bootstrap.min.js",
                "/js/plugin/datatable-responsive/datatables.responsive.min.js"
                ));

            bundles.Add(new ScriptBundle("/js/jq-grid").Include(
                "/js/plugin/jqgrid/jquery.jqGrid.min.js",
                "/js/plugin/jqgrid/grid.locale-en.min.js"
                ));

            bundles.Add(new ScriptBundle("/js/forms").Include(
                "/js/plugin/jquery-form/jquery-form.min.js"
                ));

            bundles.Add(new ScriptBundle("/js/vector-map").Include(
                "/js/plugin/vectormap/jquery-jvectormap-1.2.2.min.js",
                "/js/plugin/vectormap/jquery-jvectormap-world-mill-en.js"
                ));

            BundleTable.EnableOptimizations = true;
        }
    }
}