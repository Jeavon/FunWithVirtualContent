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

    public class MyProductController : RenderMvcController
    {
        public ActionResult Product(RenderModel model, string sku)
        {
            if (string.IsNullOrEmpty(sku))
            {
                return null;
            }
            else
            {
                foreach (var node in model.Content.Children)
                {
                    if (node.UrlName == sku)
                    {
                        return this.View("~/Views/Fruit.cshtml", this.CreateRenderModel(node));
                        //match
                    }
                }

                return null;
                //return RenderProduct(model, sku);
            }
        }

        private RenderModel CreateRenderModel(IPublishedContent content)
        {
            var model = new RenderModel(content, CultureInfo.CurrentUICulture);

            //add an umbraco data token so the umbraco view engine executes
            RouteData.DataTokens["umbraco"] = model;

            return model;
        }
    }


}
