namespace FunWithVirtualContent.Logic
{
    using System.Web.Mvc;
    using System.Web.Routing;

    using Umbraco.Core;
    using Umbraco.Web;

    public class UmbracoEvents : ApplicationEventHandler
    {
        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            RouteTable.Routes.MapUmbracoRoute(
            "test",
            "Box/{sku}",
            new
            {
                controller = "MyProduct",
                action = "Product",
                sku = UrlParameter.Optional
            },
            new ProductsRouteHandler(1055));
        }
    }
}
