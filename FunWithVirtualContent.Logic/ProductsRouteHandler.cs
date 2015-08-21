using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunWithVirtualContent.Logic
{
    using Umbraco.Web.Mvc;

    public class ProductsRouteHandler : UmbracoVirtualNodeByIdRouteHandler
    {
        public ProductsRouteHandler(int realNodeId)
            : base(realNodeId)
        {
        }
    }
}
