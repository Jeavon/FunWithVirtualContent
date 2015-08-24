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
            "MarketToBox",
            "Box/{slug}",
            new
            {
                controller = "Fruits",
                action = "Fruit",
                slug = UrlParameter.Optional
            },
            new FruitsRouteHandler(1055));
        }
    }
}
