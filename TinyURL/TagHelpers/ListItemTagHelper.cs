using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using TinyURL.Models;

namespace TinyURL.TagHelpers
{
    public class ListItemTagHelper : TagHelper
    {
        private readonly IUrlHelperFactory urlHelperFactory;

        public Link Link { get; set; }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext Context { get; set; }

        public ListItemTagHelper(IUrlHelperFactory urlHelperFactory)
        {
            this.urlHelperFactory = urlHelperFactory;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(Context);
            output.TagName = "a";
            output.Attributes.Add("href", Link.ShortURL);
            output.Content.Append($"{Context.HttpContext.Request.Scheme}/{Context.HttpContext.Request.Host}/{ Link.ShortURL}");
        }
    }
}
