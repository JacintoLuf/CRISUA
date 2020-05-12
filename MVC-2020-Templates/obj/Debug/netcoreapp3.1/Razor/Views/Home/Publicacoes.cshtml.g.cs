#pragma checksum "C:\Users\pmate\Documents\CRISUA\MVC-2020-Templates\Views\Home\Publicacoes.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9118f97ece9e5d06a15ae08417b211f98f2c56d8"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Publicacoes), @"mvc.1.0.view", @"/Views/Home/Publicacoes.cshtml")]
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
#line 1 "C:\Users\pmate\Documents\CRISUA\MVC-2020-Templates\Views\_ViewImports.cshtml"
using MVC_2020_Template;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\pmate\Documents\CRISUA\MVC-2020-Templates\Views\_ViewImports.cshtml"
using MVC_2020_Template.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9118f97ece9e5d06a15ae08417b211f98f2c56d8", @"/Views/Home/Publicacoes.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c6dbcac5658e29f3e689f4716a6949d1930de775", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Publicacoes : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "C:\Users\pmate\Documents\CRISUA\MVC-2020-Templates\Views\Home\Publicacoes.cshtml"
  
    //Layout = "_3ColsLayout";
    ViewData["Title"] = "Publicações";
    //List<MVC_2020_Business.Models.Product> publicacoes = ViewBag.Publicacoes;
    List<MVC_2020_Business.Models.Product> publicacoes = ViewBag.PublicacoesRIA;
    List<MVC_2020_Business.Models.Work> works = ViewBag.PublicacoesPTCris;
    var worksMeu = ViewBag.PublicacoesOrcid;

    var listTitle = @MVC_2020_Business.Services.DatabaseServices.select(@MVC_2020_Template.Controllers.HomeController.DB(), "Publication", "Synced", "0");

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n<h2>");
#nullable restore
#line 13 "C:\Users\pmate\Documents\CRISUA\MVC-2020-Templates\Views\Home\Publicacoes.cshtml"
Write(ViewData["Title"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h2>\r\n<div class=\"container\">\r\n    <div class=\"ui grid\">\r\n        <div class=\"five wide column\">ORCID</div>\r\n        <div class=\"one wide column\"></div>\r\n        <div class=\"five wide column\">RIA</div>\r\n");
            WriteLiteral("        <div class=\"one wide column\"><button class=\"ui primary button\" onclick=\"saveAll()\">save");
            WriteLiteral("</button></div>\r\n    </div>\r\n    <div class=\"ui grid\">\r\n");
#nullable restore
#line 23 "C:\Users\pmate\Documents\CRISUA\MVC-2020-Templates\Views\Home\Publicacoes.cshtml"
           var count = 0;
            var seta1 = "seta1_" + count;
            var seta2 = "seta2_" + count;
            var info = "info_" + count;
        

#line default
#line hidden
#nullable disable
#nullable restore
#line 28 "C:\Users\pmate\Documents\CRISUA\MVC-2020-Templates\Views\Home\Publicacoes.cshtml"
         foreach (var work in works)
        {
            if (listTitle.Contains(work.title.title))
            {
                count++;
                seta1 = "seta1_" + count;
                seta2 = "seta2_" + count;
                info = "info_" + count;

#line default
#line hidden
#nullable disable
            WriteLiteral("                <div class=\"five wide column\" data-tooltip=\"");
#nullable restore
#line 36 "C:\Users\pmate\Documents\CRISUA\MVC-2020-Templates\Views\Home\Publicacoes.cshtml"
                                                       Write(string.Join(", ", work.contributors.ToString(work.publicationDate.year.value, work.source.sourceName.content)));

#line default
#line hidden
#nullable disable
            WriteLiteral("\" data-position=\"top left\">\r\n                    ");
#nullable restore
#line 37 "C:\Users\pmate\Documents\CRISUA\MVC-2020-Templates\Views\Home\Publicacoes.cshtml"
               Write(work.title.title);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </div>\r\n                <div class=\"two wide column\">\r\n                    <i");
            BeginWriteAttribute("id", " id=\"", 1875, "\"", 1886, 1);
#nullable restore
#line 40 "C:\Users\pmate\Documents\CRISUA\MVC-2020-Templates\Views\Home\Publicacoes.cshtml"
WriteAttributeValue("", 1880, seta1, 1880, 6, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"arrow alternate circle right icon\"");
            BeginWriteAttribute("onclick", " onclick=\"", 1929, "\"", 2009, 5);
            WriteAttributeValue("", 1939, "OrcidToRia(", 1939, 11, true);
#nullable restore
#line 40 "C:\Users\pmate\Documents\CRISUA\MVC-2020-Templates\Views\Home\Publicacoes.cshtml"
WriteAttributeValue("", 1950, Newtonsoft.Json.JsonConvert.SerializeObject(work), 1950, 50, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 2000, ",", 2000, 1, true);
#nullable restore
#line 40 "C:\Users\pmate\Documents\CRISUA\MVC-2020-Templates\Views\Home\Publicacoes.cshtml"
WriteAttributeValue(" ", 2001, count, 2002, 6, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 2008, ")", 2008, 1, true);
            EndWriteAttribute();
            WriteLiteral("></i>\r\n                    <i");
            BeginWriteAttribute("id", " id=\"", 2039, "\"", 2050, 1);
#nullable restore
#line 41 "C:\Users\pmate\Documents\CRISUA\MVC-2020-Templates\Views\Home\Publicacoes.cshtml"
WriteAttributeValue("", 2044, seta2, 2044, 6, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"arrow alternate circle left icon\" style=\"display:none\"");
            BeginWriteAttribute("onclick", " onclick=\"", 2113, "\"", 2199, 5);
            WriteAttributeValue("", 2123, "CancelOrcidToRia(", 2123, 17, true);
#nullable restore
#line 41 "C:\Users\pmate\Documents\CRISUA\MVC-2020-Templates\Views\Home\Publicacoes.cshtml"
WriteAttributeValue("", 2140, Newtonsoft.Json.JsonConvert.SerializeObject(work), 2140, 50, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 2190, ",", 2190, 1, true);
#nullable restore
#line 41 "C:\Users\pmate\Documents\CRISUA\MVC-2020-Templates\Views\Home\Publicacoes.cshtml"
WriteAttributeValue(" ", 2191, count, 2192, 6, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 2198, ")", 2198, 1, true);
            EndWriteAttribute();
            WriteLiteral("></i>\r\n                </div>\r\n                <div");
            BeginWriteAttribute("id", " id=\"", 2251, "\"", 2262, 1);
#nullable restore
#line 43 "C:\Users\pmate\Documents\CRISUA\MVC-2020-Templates\Views\Home\Publicacoes.cshtml"
WriteAttributeValue("", 2256, count, 2256, 6, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"five wide column\"></div>\r\n");
#nullable restore
#line 44 "C:\Users\pmate\Documents\CRISUA\MVC-2020-Templates\Views\Home\Publicacoes.cshtml"
            }
        }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"    </div>
</div>


<script>
    var objs = [];

    function OrcidToRia(obj, idDiv) {
        console.log(obj);
        var div = document.getElementById("""" + idDiv);
        if (typeof (div) != 'undefined' && div != null) {
            var seta1 = document.getElementById(""seta1_"" + idDiv);
            seta1.style.display = ""none"";
            var seta2 = document.getElementById(""seta2_"" + idDiv);
            seta2.style.display = ""block"";
            div.innerHTML = obj.title.title;
            objs.push(obj);
        }
    }

    function CancelOrcidToRia(obj, idDiv) {
        console.log(obj);
        var div = document.getElementById("""" + idDiv);
        if (typeof (div) != 'undefined' && div != null) {
            var seta1 = document.getElementById(""seta1_"" + idDiv);
            seta1.style.display = ""block"";
            var seta2 = document.getElementById(""seta2_"" + idDiv);
            seta2.style.display = ""none"";
            div.innerHTML = null;
            objs.pop(obj");
            WriteLiteral(@");
        }
    }

    function saveAll() {
        $.post(""SavePub"", { works: JSON.stringify(objs) }, function (data) {
            //alert(""Go to Details"");
            window.location.href = ""Detail"";
            //$(""#main_area"").html(data);
        });
        //window.open('");
#nullable restore
#line 85 "C:\Users\pmate\Documents\CRISUA\MVC-2020-Templates\Views\Home\Publicacoes.cshtml"
                  Write(Url.Action("Detail", "Home"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\', \"_blank\");\r\n        //window.open(\'");
#nullable restore
#line 86 "C:\Users\pmate\Documents\CRISUA\MVC-2020-Templates\Views\Home\Publicacoes.cshtml"
                  Write(Url.Action("Detail", "Home"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"');
        //var workss = JSON.stringify({ 'works': objs });

        //$.ajax({
        //    contentType: 'application/json; charset=utf-8',
        //    dataType: 'json',
        //    type: 'POST',
        //    url: '/Home/SavePub',
        //    data: workss,
        //    success: function (data) {
        //        alert(""Data: "" + data);
        //    },
        //    failure: function (data) {
        //        alert(""Data: "" + data);
        //    }
        //});
    }


</script>

");
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
