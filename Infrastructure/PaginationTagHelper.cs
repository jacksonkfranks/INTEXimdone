using INTEXimdone.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INTEXimdone.Infrastructure
{
    [HtmlTargetElement("div", Attributes = "page-num")]
    public class PaginationTagHelper : TagHelper
    {
        private IUrlHelperFactory uhf;

        public PaginationTagHelper(IUrlHelperFactory temp)
        {
            uhf = temp;
        }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext vc { get; set; }

        public PageInfo PageNum { get; set; }
        public string PageAction { get; set; }
        public bool PageClassesEnabled { get; set; } = false;
        public string PageClass { get; set; }
        public string PageClassNormal { get; set; }
        public string PageClassSelected { get; set; }

        public override void Process(TagHelperContext thc, TagHelperOutput tho)
        {
            IUrlHelper uh = uhf.GetUrlHelper(vc);
            TagBuilder final = new TagBuilder("div");

            for (int i = 0; i < PageNum.TotalPages; i++)
            {
                if (i == 0)
                {
                    TagBuilder tb = new TagBuilder("a");

                    tb.Attributes["href"] = uh.Action(PageAction, new { PageNum = i + 1 });
                    if (PageClassesEnabled)
                    {
                        tb.AddCssClass(PageClass);
                        tb.AddCssClass(i + 1 == PageNum.CurrentPage ? PageClassSelected : PageClassNormal);
                    }
                    tb.InnerHtml.Append((i + 1).ToString());

                    final.InnerHtml.AppendHtml(tb);
                }
                else if (i + 1 == PageNum.CurrentPage || i + 1 == PageNum.CurrentPage - 1 || i + 1 == PageNum.CurrentPage + 1)
                {
                    TagBuilder tb = new TagBuilder("a");

                    tb.Attributes["href"] = uh.Action(PageAction, new { PageNum = i + 1 });
                    if (PageClassesEnabled)
                    {
                        tb.AddCssClass(PageClass);
                        tb.AddCssClass(i + 1 == PageNum.CurrentPage ? PageClassSelected : PageClassNormal);
                    }
                    tb.InnerHtml.Append((i + 1).ToString());

                    final.InnerHtml.AppendHtml(tb);
                }
                else if (i == PageNum.TotalPages - 1)
                {
                    TagBuilder tb = new TagBuilder("a");

                    tb.Attributes["href"] = uh.Action(PageAction, new { PageNum = i + 1 });
                    if (PageClassesEnabled)
                    {
                        tb.AddCssClass(PageClass);
                        tb.AddCssClass(i + 1 == PageNum.CurrentPage ? PageClassSelected : PageClassNormal);
                    }
                    tb.InnerHtml.Append((i + 1).ToString());

                    final.InnerHtml.AppendHtml(tb);
                }

            }

            tho.Content.AppendHtml(final.InnerHtml);
        }
    }
}
