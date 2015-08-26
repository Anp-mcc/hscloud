using System.Web.Optimization;

namespace WebStone
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/css/hscloud.css"
               ));

            BundleTable.EnableOptimizations = false;
        }
    }
}