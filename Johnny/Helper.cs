using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;
using Johnny.Logic;
using Johnny.Models;
using System.Web.WebPages;

namespace Johnny
{
    public static class Helper
    {
        public static int ReadParam(string paramName)
        {
            int number;
            string raw = HttpContext.Current.Request.Params[paramName];
            if (null != raw && int.TryParse(raw, out number))
            {
                return number;
            }
            return -1;
        }

        public static bool IsAjax()
        {
            return true; // !Manager.Current.IsStatic;
        }

        public static string GetController()
        {
            return HttpContext.Current.Request.RequestContext.RouteData.Values["Controller"] as string;
        }

        public static string GetAction()
        {
            return HttpContext.Current.Request.RequestContext.RouteData.Values["Action"] as string;
        }

        public static MvcHtmlString PermanentLink(this Model model)
        {
            return IsAjax()
                       ? MvcHtmlString.Create(string.Format("<a href='{0}/{1}/{2}'>Посилання</a>",
                            GetAction(), GetController(), model.Id))
                       : MvcHtmlString.Empty;
        }

        public static MvcHtmlString InitView(this WebPageBase page)
        {
            if (IsAjax())
            {
                page.Layout = null;
                return MvcHtmlString.Create(
                    string.Format("<script>$(function(){{Controller='{0}';Action='{1}';Form=$('form');}});</script>",
                    GetController(), GetAction()));
            }
            page.Layout = "~/Views/Shared/_Layout.cshtml";
            return MvcHtmlString.Empty;
        }

        public static MvcHtmlString UniAction(this HtmlHelper html,
            string text, string controller, string action, int Id)
        {
            if (IsAjax())
            {
                return MvcHtmlString.Create(string.Format(
                    "<div class='cmd' onclick=\"go('{0}', '{1}', {2})\">{3}</div>",
                    controller, action, Id, text));
            }
            return html.ActionLink(text, action, controller, new { id = Id }, new { @class = "cmd" });
        }

        public static MvcHtmlString UniAction(this HtmlHelper html,
           string text, string controller, string action)
        {
            if (IsAjax())
            {
                return MvcHtmlString.Create(string.Format(
                    "<div class='cmd' onclick=\"go('{0}', '{1}')\">{2}</div>",
                    controller, action, text));
            }
            return html.ActionLink(text, action, controller);
        }

        public static MvcHtmlString Submit(this HtmlHelper html,
            string text = "Зберегти", string action = null, string controller = null)
        {
            if (IsAjax())
            {
                return MvcHtmlString.Create(string.Format(
                    "<div onclick='submitForm({0},{1})'>{2}</div>",
                    action.GetOrNull(), controller.GetOrNull(), text));
            }
            return MvcHtmlString.Create("<input type='submit' value='Зберегти' name='Save' />");
        }

        public static string GetOrNull(this string value)
        {
            return null != value ? string.Format("\"{0}\"", value) : "null";
        }

        public static void RemoveInvisible<T>(this ICollection<T> collection)
            where T : Model
        {
            if (Manager.Current.User.IsAdmin)
            {
                return;
            }
            foreach (var model in collection)
            {
                if (!model.IsVisible)
                {
                    collection.Remove(model);
                }
            }
        }

        public static MvcHtmlString NameOf<T, TProperty>(this HtmlHelper<T> html,
            Expression<Func<T, TProperty>> propertyExp)
        {
            return MvcHtmlString.Create(ModelMetadata
                .FromLambdaExpression(propertyExp, html.ViewData).GetDisplayName());
        }

        public static MvcHtmlString UniAction(this HtmlHelper html,
            string text, string action, int id)
        {
            return UniAction(html, text, GetController(), action, id);
        }

        public static MvcHtmlString UniAction(this HtmlHelper html,
           string text, string action)
        {
            return UniAction(html, text, GetController(), action);
        }

        public static MvcHtmlString UniController(this HtmlHelper html,
           string text, string controller)
        {
            return UniAction(html, text, controller, "Index");
        }
    }
}                                                  