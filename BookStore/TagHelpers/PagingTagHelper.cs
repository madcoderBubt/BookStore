using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace BookStore.TagHelpers
{
    [HtmlTargetElement("paging")]
    public class PagingTagHelper : TagHelper
    {
        public int TotalPage { get; set; }
        public int CurrentPage { get; set; }
        public string Link { get; set; }
        [HtmlAttributeName("filter-Category")]
        public string Category { get; set; }
        [HtmlAttributeName("filter-Price")]
        public string Price { get; set; }

        private HtmlEncoder _encoder;
        public PagingTagHelper(HtmlEncoder htmlEncoder)
        {
            _encoder = htmlEncoder;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "CA2000:Dispose objects before losing scope", Justification = "<Pending>")]
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            //base.Process(context, output);
            if (output == null)
            {
                output = new TagHelperOutput("paging",null,null);
            }

            string _category = (Category != "All Books") ? Category : "";

            output.TagName = "ul";
            var items = new StringBuilder();
            if (CurrentPage != 1)
            {
                items.AppendLine($"<li><a href='{Link}?pn={CurrentPage-1}&c={_category}&fb={Price}'><i class='zmdi zmdi-chevron-left'></i></a></li>");
            }

            for (int i = 1; i <= TotalPage; i++)
            {
                var li = new TagBuilder("li");
                var a = new TagBuilder("a");
                a.MergeAttribute("href", $"{Link}?pn={i}&c={_category}&fb={Price}");
                a.InnerHtml.Append(i.ToString(CultureInfo.InvariantCulture));                

                if (i == CurrentPage)
                {
                    li.AddCssClass("active");
                }
                li.InnerHtml.AppendHtml(a);

                var writer = new System.IO.StringWriter();
                li.WriteTo(writer, _encoder);
                var s = writer.ToString();
                items.AppendLine(s);
            }

            if (CurrentPage != TotalPage)
            {
                items.AppendLine($"<li><a href='{Link}?pn={CurrentPage + 1}&c={_category}&fb={Price}'><i class='zmdi zmdi-chevron-right'></i></a></li>");
            }

            output.Content.SetHtmlContent(items.ToString());
            output.PostContent.SetHtmlContent("</ul>");
            output.Attributes.Clear();
            output.Attributes.Add("class", "wn__pagination");
        }
    }
}
