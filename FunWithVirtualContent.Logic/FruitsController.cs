using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunWithVirtualContent.Logic
{
    using System.Globalization;
    using System.Web.Mvc;

    using Our.Umbraco.Vorto.Extensions;
    using Our.Umbraco.Vorto.Web.Controllers;

    using Umbraco.Core;
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
                    var potentialUrls = new List<string>();

                    var v = new VortoApiController();
                    var langs = (List<Our.Umbraco.Vorto.Models.Language>)v.GetInstalledLanguages();

                    foreach (var lang in langs)
                    {
                        var vortoValue = node.GetVortoValue<string>("fruitName", lang.IsoCode);
                        if (!string.IsNullOrEmpty(vortoValue))
                        {
                            potentialUrls.Add(vortoValue.ToUrlSegment());
                        }
                    }

                    if (potentialUrls.Contains(slug))
                    {
                        // match
                        return this.View("~/Views/Fruit.cshtml", this.CreateRenderModel(node));                        
                    }
                }
                return this.HttpNotFound();
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
