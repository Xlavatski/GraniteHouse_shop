#pragma checksum "C:\Users\dglavas\source\repos\GraniteHouse\GraniteHouse\Areas\Admin\Views\ProductStructure\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "524c58f05ecfba0929b38791453b25bf6f644689"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Admin_Views_ProductStructure_Index), @"mvc.1.0.view", @"/Areas/Admin/Views/ProductStructure/Index.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\dglavas\source\repos\GraniteHouse\GraniteHouse\Areas\Admin\Views\_ViewImports.cshtml"
using GraniteHouse;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\dglavas\source\repos\GraniteHouse\GraniteHouse\Areas\Admin\Views\_ViewImports.cshtml"
using GraniteHouse.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"524c58f05ecfba0929b38791453b25bf6f644689", @"/Areas/Admin/Views/ProductStructure/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b3ff79c5991d2311e881eeb249d5c5895f6ee895", @"/Areas/Admin/Views/_ViewImports.cshtml")]
    public class Areas_Admin_Views_ProductStructure_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<GraniteHouse.Models.Queries.GroupByModelQuery>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\dglavas\source\repos\GraniteHouse\GraniteHouse\Areas\Admin\Views\ProductStructure\Index.cshtml"
  
    ViewData["Title"] = "Index";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"<br />
<br />
<br />
<div class=""row"">
    <div class=""col-6"">
        <h2 class=""text-info"">Product Structure List</h2>
    </div>
</div>

<br />
<div>
    <table class=""table table-striped border"">
        <tr class=""table-info"">
            <th>
                ");
#nullable restore
#line 20 "C:\Users\dglavas\source\repos\GraniteHouse\GraniteHouse\Areas\Admin\Views\ProductStructure\Index.cshtml"
           Write(Html.DisplayNameFor(m => m.ProductTypes.Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 23 "C:\Users\dglavas\source\repos\GraniteHouse\GraniteHouse\Areas\Admin\Views\ProductStructure\Index.cshtml"
           Write(Html.DisplayNameFor(m => m.Count));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n        </tr>\r\n\r\n");
#nullable restore
#line 27 "C:\Users\dglavas\source\repos\GraniteHouse\GraniteHouse\Areas\Admin\Views\ProductStructure\Index.cshtml"
         foreach (var item in Model)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <tr>\r\n                <td>\r\n                    ");
#nullable restore
#line 31 "C:\Users\dglavas\source\repos\GraniteHouse\GraniteHouse\Areas\Admin\Views\ProductStructure\Index.cshtml"
               Write(Html.DisplayFor(m => item.ProductTypes.Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 34 "C:\Users\dglavas\source\repos\GraniteHouse\GraniteHouse\Areas\Admin\Views\ProductStructure\Index.cshtml"
               Write(Html.DisplayFor(m => item.Count));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n            </tr>\r\n");
#nullable restore
#line 37 "C:\Users\dglavas\source\repos\GraniteHouse\GraniteHouse\Areas\Admin\Views\ProductStructure\Index.cshtml"

        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </table>\r\n</div>\r\n\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<GraniteHouse.Models.Queries.GroupByModelQuery>> Html { get; private set; }
    }
}
#pragma warning restore 1591
