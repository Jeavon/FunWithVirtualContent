using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunWithVirtualContent.Logic
{
    using System.Globalization;
    using System.Web.Mvc;

    using Umbraco.Core.Models;
    using Umbraco.Web.Models;
    using Umbraco.Web.Mvc;

    public class FruitsController : RenderMvcController
    {
        public ActionResult Fruit(RenderModel model, string slug)
        {
            if (string.IsNullOrEmpty(slug))
            {
                return null;
            }
            else
            {
                foreach (var node in model.Content.Children)
                {
                    if (node.UrlName == slug)
                    {
                        // match
                        return this.View("~/Views/Fruit.cshtml", this.CreateRenderModel(node));                        
                    }
                }

                return this.HttpNotFound();

                //return RenderProduct(model, sku);
            }
        }

        private RenderModel CreateRenderModel(IPublishedContent content)
        {
            var model = new RenderModel(content, CultureInfo.CurrentUICulture);

            //add an umbraco data token so the umbraco view engine executes
            this.RouteData.DataTokens["umbraco"] = model;

            return model;
        }
    }


}
