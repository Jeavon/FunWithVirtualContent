using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunWithVirtualContent.Logic
{
    using System.Globalization;

    using Newtonsoft.Json;

    using Umbraco.Web;
    using Umbraco.Web.Routing;
    using Our.Umbraco.Vorto.Extensions;
    using Our.Umbraco.Vorto.Models;
    using Our.Umbraco.Vorto.Web.Controllers;

    using Umbraco.Core;

    public class FruitBoxUrlProvider : DefaultUrlProvider
    {
        public override string GetUrl(UmbracoContext umbracoContext, int id, Uri current, UrlProviderMode mode)
        {
            var node = umbracoContext.ContentCache.GetById(id);

            if (node != null && node.ContentType.Alias.Equals("Fruit"))
            {
                if (node.HasVortoValue("fruitName"))
                {
                    var v = new VortoApiController();
                    var lang = (List<Our.Umbraco.Vorto.Models.Language>)v.GetInstalledLanguages();

                    var primaryLang = string.Empty;

                    if (lang.Select(x => x.IsoCode).Contains("en-US"))
                    {
                        primaryLang = "en-US";
                    }
                    else
                    {
                        var firstOrDefault = lang.FirstOrDefault();
                        if (firstOrDefault != null)
                        {
                            primaryLang = firstOrDefault.IsoCode;
                        }
                    }

                    var vortoValue = node.GetVortoValue<string>("fruitName", primaryLang);
                    var culture = CultureInfo.GetCultureInfo(primaryLang);
                    var primaryBox = umbracoContext.ContentCache.GetAtRoot().DescendantsOrSelf("Box").FirstOrDefault(x => Equals(x.GetCulture(), culture));

                    if (primaryBox != null)
                    {
                        return string.Format("{0}{1}/", primaryBox.Url, vortoValue.ToUrlSegment());
                    }
                }
            }

            return base.GetUrl(umbracoContext, id, current, mode);
        }

        public override IEnumerable<string> GetOtherUrls(UmbracoContext umbracoContext, int id, Uri current)
        {
            var node = umbracoContext.ContentCache.GetById(id);

            if (node != null && node.ContentType.Alias.Equals("Fruit"))
            {
                if (node.HasVortoValue("fruitName"))
                {
                    var v = new VortoApiController();
                    var langs = (List<Our.Umbraco.Vorto.Models.Language>)v.GetInstalledLanguages();

                    var primaryLang = string.Empty;

                    var enUs = langs.FirstOrDefault(x => x.IsoCode == "en-US");

                    if (enUs != null)
                    {
                        langs.Remove(enUs);
                    }
                    else
                    {
                        var firstLang = langs.FirstOrDefault();
                        langs.Remove(firstLang);
                    }

                    var otherUrls = new List<string>();
                    var allBoxes = umbracoContext.ContentCache.GetAtRoot().DescendantsOrSelf("Box");

                    foreach (var lang in langs)
                    {
                        var vortoValue = node.GetVortoValue<string>("fruitName", lang.IsoCode);

                        var culture = CultureInfo.GetCultureInfo(lang.IsoCode);

                        var langBox = allBoxes.FirstOrDefault(x => Equals(x.GetCulture(), culture));

                        if (langBox != null)
                        {
                            otherUrls.Add(string.Format("{0}{1}/", langBox.Url, vortoValue.ToUrlSegment()));
                        }
                    }

                    return otherUrls;
                }
            }

            return base.GetOtherUrls(umbracoContext, id, current);
        }
    }
}
