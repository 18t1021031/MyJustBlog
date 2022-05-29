using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace Blogs.CustomeHelper
{
    public static class CustomeHelper
    {
        public static IHtmlString TagLink(this HtmlHelper html, string expression)
        {
            TagBuilder tb = new TagBuilder("a");
            tb.AddCssClass("btn m-2 fst-italic text-white bg-black");
            tb.InnerHtml = expression;
            return new MvcHtmlString(tb.ToString());
        }
        public static IHtmlString CategoryLink(this HtmlHelper html, string expression)
        {
            TagBuilder tb = new TagBuilder("a");
            tb.AddCssClass("m-2");
            tb.InnerHtml = expression;
            return new MvcHtmlString(tb.ToString());
        }
        public static IHtmlString PostOn(this HtmlHelper html, DateTime date, int? rate, int? view)
        {
            string dayFormat;
            TimeSpan sub;
            TagBuilder tb = new TagBuilder("p");
            tb.AddCssClass("m-2 fst-italic text-black-50  post-meta");
            dayFormat = date.ToString("dd/MM/yyyy hh:mm:ss tt ", new CultureInfo("en-US"));
            sub = DateTime.Now - date;
            if (Math.Floor(sub.TotalDays) == 1)
                dayFormat = "Yesterday at " + date.ToString("hh/mm tt ", new CultureInfo("en-US")) + " ";

            if (Math.Floor(sub.TotalDays) == 0)
            {
                dayFormat = sub.Hours + " hours " + sub.Minutes + " minutes " + "ago ";

            }
                
            tb.InnerHtml = "Posted " + dayFormat + " with rate " + rate + " by " + view + " view(s).";
            return new MvcHtmlString(tb.ToString());
        }
    }
}