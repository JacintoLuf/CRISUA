#pragma checksum "C:\Users\Asus\Desktop\CRISUA\MVC-2020-Templates\Views\Shared\_3ColsLayout.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9d738135b6dde18cbd64e24c9e6d8bac9ca6901c"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared__3ColsLayout), @"mvc.1.0.view", @"/Views/Shared/_3ColsLayout.cshtml")]
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
#line 1 "C:\Users\Asus\Desktop\CRISUA\MVC-2020-Templates\Views\_ViewImports.cshtml"
using MVC_2020_Template;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Asus\Desktop\CRISUA\MVC-2020-Templates\Views\_ViewImports.cshtml"
using MVC_2020_Template.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9d738135b6dde18cbd64e24c9e6d8bac9ca6901c", @"/Views/Shared/_3ColsLayout.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c6dbcac5658e29f3e689f4716a6949d1930de775", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared__3ColsLayout : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "C:\Users\Asus\Desktop\CRISUA\MVC-2020-Templates\Views\Shared\_3ColsLayout.cshtml"
  
    Layout = "_MasterLayout";

#line default
#line hidden
#nullable disable
            WriteLiteral("<div class=\"content\">\r\n    ");
#nullable restore
#line 5 "C:\Users\Asus\Desktop\CRISUA\MVC-2020-Templates\Views\Shared\_3ColsLayout.cshtml"
Write(RenderBody());

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n</div>\r\n<div class=\"side\">\r\n");
#nullable restore
#line 8 "C:\Users\Asus\Desktop\CRISUA\MVC-2020-Templates\Views\Shared\_3ColsLayout.cshtml"
     if (IsSectionDefined("RightColumn"))
    {
        

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "C:\Users\Asus\Desktop\CRISUA\MVC-2020-Templates\Views\Shared\_3ColsLayout.cshtml"
   Write(RenderSection("RightColumn"));

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "C:\Users\Asus\Desktop\CRISUA\MVC-2020-Templates\Views\Shared\_3ColsLayout.cshtml"
                                     
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
