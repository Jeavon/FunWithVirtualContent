namespace FunWithVirtualContent.Logic
{
    using System;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;

    using Umbraco.Core;
    using Umbraco.Core.Services;
    using Umbraco.Web;
    using Umbraco.Web.Models;
    using Umbraco.Web.Routing;

    public class UmbracoEvents : ApplicationEventHandler
    {
        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {

            ContentService.Published += ContentService_Published;

            // Map routes for all Box urls in each site
            if (UmbracoContext.Current != null)
            {
                var allBoxes = UmbracoContext.Current.ContentCache.GetAtRoot().DescendantsOrSelf("Box");
                if (allBoxes.Any())
                {
                    foreach (var box in allBoxes)
                    {
                        var langIso = box.GetCulture().ThreeLetterISOLanguageName;
                        RouteTable.Routes.MapUmbracoRoute(
                        langIso + "MarketToBox",
                        box.UrlName + "/{slug}",
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
        }

        public void ContentService_Published(Umbraco.Core.Publishing.IPublishingStrategy sender, Umbraco.Core.Events.PublishEventArgs<Umbraco.Core.Models.IContent> e)
        {
            foreach (var node in e.PublishedEntities)
            {
                if (node.ContentType.Alias.Equals("Box"))
                {
                    // Ugh, restart the application to make the route table update, there must be a better way and this won't work in LB environment
                    var webConfigPath = HttpContext.Current.Request.PhysicalApplicationPath + "\\\\Web.config"; 
                    System.IO.File.SetLastWriteTimeUtc(webConfigPath, DateTime.UtcNow); 
                }
            }
        }

        protected override void ApplicationStarting(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            // add custom url provider
            UrlProviderResolver.Current.RemoveType<DefaultUrlProvider>();
            UrlProviderResolver.Current.AddType<FruitBoxUrlProvider>();
        }
    }
}
