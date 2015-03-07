namespace HelperExtentions
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using System.Web.Mvc.Ajax;
    using System.Web.Routing;

    public static class HtmlExtentions
    {
        public static MvcHtmlString RawActionLink(this AjaxHelper ajaxHelper, string linkText, string actionName, string controllerName, object routeValues, AjaxOptions ajaxOptions, object htmlAttributes)
        {
            // cannot use linkText directly because it's sanitized. Use repId to create link and replace it afterward
            // var link = ajaxHelper.ActionLink(linkText, actionName, controllerName, routeValues, ajaxOptions, htmlAttributes);
            
            var repID = Guid.NewGuid().ToString(); 
            var link = ajaxHelper.ActionLink(repID, actionName, controllerName, routeValues, ajaxOptions, htmlAttributes);
            return MvcHtmlString.Create(link.ToString().Replace(repID, linkText));
        }

        public static MvcHtmlString Submit(this HtmlHelper helper, object htmlAttributes)
        {
            var input = new TagBuilder("input");
            var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes) as IDictionary<string, object>;
            input.MergeAttributes(attributes);
            input.Attributes.Add("type", "submit");
           
            return new MvcHtmlString(input.ToString(TagRenderMode.Normal));
        }
    }
}
