﻿using System;
using System.Text;
using System.Web.Mvc;
using CQS.QueryResult;

namespace WebStone.HtmlHelpers
{
    public static class PagingHelpers
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html, PagingInfo pagingInfo, Func<int, string> pageUrl)
        {
            var result = new StringBuilder();
            for (var i = 1; i <= pagingInfo.TotalPages; i++)
            {
                var tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml = i.ToString();

                if (i == pagingInfo.CurrentPage)
                    tag.AddCssClass("selected");

                result.Append(tag);
            }

            return MvcHtmlString.Create(result.ToString());
        }
    }
}