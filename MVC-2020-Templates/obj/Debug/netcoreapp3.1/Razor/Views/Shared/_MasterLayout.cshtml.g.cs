#pragma checksum "C:\Users\pmate\Documents\CRISUA\MVC-2020-Templates\Views\Shared\_MasterLayout.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "bae23b480fa6b366660292f9e08ea8e554b6bbea"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared__MasterLayout), @"mvc.1.0.view", @"/Views/Shared/_MasterLayout.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"bae23b480fa6b366660292f9e08ea8e554b6bbea", @"/Views/Shared/_MasterLayout.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c6dbcac5658e29f3e689f4716a6949d1930de775", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared__MasterLayout : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("rel", new global::Microsoft.AspNetCore.Html.HtmlString("stylesheet"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/css/site.min.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", "~/js/site.min.js", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.ScriptTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("<!DOCTYPE html>\r\n<html lang=\"en\">\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("head", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "bae23b480fa6b366660292f9e08ea8e554b6bbea4600", async() => {
                WriteLiteral("\r\n    <meta charset=\"utf-8\" />\r\n    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\" />\r\n    <title>");
#nullable restore
#line 6 "C:\Users\pmate\Documents\CRISUA\MVC-2020-Templates\Views\Shared\_MasterLayout.cshtml"
      Write(ViewData["Title"]);

#line default
#line hidden
#nullable disable
                WriteLiteral(@" - UA CRIS</title>
    <link href=""//netdna.bootstrapcdn.com/bootstrap/3.0.0/css/bootstrap.min.css"" rel=""stylesheet""> <!-- bootstrap para icons-->
    <link rel=""shortcut icon"" href=""//static.ua.pt/favicon.ico"" />
    <link rel=""stylesheet"" href=""//static.ua.pt/css/semanticua/2.0b/dist/semantic.min.css"" />
    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "bae23b480fa6b366660292f9e08ea8e554b6bbea5549", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n\r\n\r\n    <script src=\"https://code.jquery.com/jquery-3.4.1.min.js\"\r\n            integrity=\"sha256-CSXorXvZcTkaix6Yvo6HppcZGetbYMGWSFlBw8HfCJo=\"\r\n            crossorigin=\"anonymous\"></script>\r\n");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "bae23b480fa6b366660292f9e08ea8e554b6bbea7638", async() => {
                WriteLiteral(@"

    <div class=""service area"">
        <div class=""limiter"">
            <div class=""common"">
                <a href=""https://www.ua.pt/pt/noticias"" target=""_blank"">Notícias</a> | <a href=""https://www.ua.pt/pt/agenda"" target=""_blank"">Agenda</a> |
                <a href=""https://www.facebook.com/universidadedeaveiro/"" target=""_blank""><i class=""facebook f icon""></i></a> |
                <a href=""https://www.instagram.com/universidadedeaveiro/?hl=pt"" target=""_blank""><i class=""instagram icon""></i></a>
            </div>
            <div class=""shortcuts"">
                <a href=""https://www.ua.pt/pt/comunidade"" target=""_blank"">Comunidade ua</a>
                <a href=""https://www.ua.pt/sbidm/biblioteca/externos"" target=""_blank"">Utilizador externo</a>
            </div>
            <div class=""session"">
");
#nullable restore
#line 31 "C:\Users\pmate\Documents\CRISUA\MVC-2020-Templates\Views\Shared\_MasterLayout.cshtml"
                   var session = new MVC_2020_Template.Helpers.Session(Context.Session);


                

#line default
#line hidden
#nullable disable
#nullable restore
#line 34 "C:\Users\pmate\Documents\CRISUA\MVC-2020-Templates\Views\Shared\_MasterLayout.cshtml"
                 if (@MVC_2020_Template.Helpers.Session.SessionActive)
                {

#line default
#line hidden
#nullable disable
                WriteLiteral("                <span style=\"color:white\">");
#nullable restore
#line 36 "C:\Users\pmate\Documents\CRISUA\MVC-2020-Templates\Views\Shared\_MasterLayout.cshtml"
                                     Write(MVC_2020_Template.Helpers.Session.FullName);

#line default
#line hidden
#nullable disable
                WriteLiteral("</span>\r\n");
                WriteLiteral("                    <i class=\"outline user icon\"></i>");
#nullable restore
#line 38 "C:\Users\pmate\Documents\CRISUA\MVC-2020-Templates\Views\Shared\_MasterLayout.cshtml"
                                                Write(Html.ActionLink("Sair", "Logout", "Secure", null));

#line default
#line hidden
#nullable disable
#nullable restore
#line 38 "C:\Users\pmate\Documents\CRISUA\MVC-2020-Templates\Views\Shared\_MasterLayout.cshtml"
                                                                                                       
                }
                else
                {
                

#line default
#line hidden
#nullable disable
                WriteLiteral("                    <i class=\"outline user icon\"></i>");
#nullable restore
#line 44 "C:\Users\pmate\Documents\CRISUA\MVC-2020-Templates\Views\Shared\_MasterLayout.cshtml"
                                                Write(Html.ActionLink("Entrar", "Index", "Secure", null));

#line default
#line hidden
#nullable disable
#nullable restore
#line 44 "C:\Users\pmate\Documents\CRISUA\MVC-2020-Templates\Views\Shared\_MasterLayout.cshtml"
                                                                                                        
                    //@Html.ActionLink("login", "Index", "Secure", null, new { @class = "image-button login", @title = "login", @alt = "login" })
                }

#line default
#line hidden
#nullable disable
                WriteLiteral("            </div>\r\n");
                WriteLiteral("        </div>\r\n    </div>\r\n\r\n    <div class=\"system area\">\r\n        <div class=\"limiter\">\r\n            <div class=\"logo\">\r\n                <a href=\"https://www.ua.pt/\">\r\n                    <img src=\"//static.ua.pt/images/ua/logo-black.svg\"");
                BeginWriteAttribute("alt", " alt=\"", 3685, "\"", 3691, 0);
                EndWriteAttribute();
                WriteLiteral(@" />
                </a>
            </div>
            <div class=""identity"">
                <h1>UA CRIS</h1>
            </div>
            <div class=""menu"">
                <a class=""js-menu-toggle"" href=""#""><i class=""bars icon""></i></a>
            </div>
            <nav class=""ua""></nav>
        </div>
    </div>

    <div class=""main area"">
        <div class=""limiter"">
            <div class=""menu"">
                <nav class=""ua"">
                    <ul data-arrow=""before"">
");
                WriteLiteral("                        <li>\r\n                            ");
#nullable restore
#line 97 "C:\Users\pmate\Documents\CRISUA\MVC-2020-Templates\Views\Shared\_MasterLayout.cshtml"
                       Write(Html.ActionLink("Instruções", "Readme", "Home", null));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                        </li>\r\n");
                WriteLiteral("\r\n                        <li>\r\n                            ");
#nullable restore
#line 103 "C:\Users\pmate\Documents\CRISUA\MVC-2020-Templates\Views\Shared\_MasterLayout.cshtml"
                       Write(Html.ActionLink("Perfil", "Perfil", "Home", null));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                        </li>\r\n                        <li>\r\n                            ");
#nullable restore
#line 106 "C:\Users\pmate\Documents\CRISUA\MVC-2020-Templates\Views\Shared\_MasterLayout.cshtml"
                       Write(Html.ActionLink("Publicações", "Publicacoes", "Home", null));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                        </li>\r\n                        <li>\r\n                            ");
#nullable restore
#line 109 "C:\Users\pmate\Documents\CRISUA\MVC-2020-Templates\Views\Shared\_MasterLayout.cshtml"
                       Write(Html.ActionLink("Sobre", "Sobre", "Home", null));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                        </li>\r\n                        <li>\r\n                            ");
#nullable restore
#line 112 "C:\Users\pmate\Documents\CRISUA\MVC-2020-Templates\Views\Shared\_MasterLayout.cshtml"
                       Write(Html.ActionLink("Detalhes", "Detail", "Home", null));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                        </li>\r\n                        <li>\r\n                            ");
#nullable restore
#line 115 "C:\Users\pmate\Documents\CRISUA\MVC-2020-Templates\Views\Shared\_MasterLayout.cshtml"
                       Write(Html.ActionLink("Exemplo", "Index", "Home", null));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                            <ul>\r\n                                <li>");
#nullable restore
#line 117 "C:\Users\pmate\Documents\CRISUA\MVC-2020-Templates\Views\Shared\_MasterLayout.cshtml"
                               Write(Html.ActionLink("Conteúdo", "Content", "Home", null));

#line default
#line hidden
#nullable disable
                WriteLiteral("</li>\r\n                                <li>");
#nullable restore
#line 118 "C:\Users\pmate\Documents\CRISUA\MVC-2020-Templates\Views\Shared\_MasterLayout.cshtml"
                               Write(Html.ActionLink("Formulário", "Forms", "Home", null));

#line default
#line hidden
#nullable disable
                WriteLiteral("</li>\r\n                                <li>");
#nullable restore
#line 119 "C:\Users\pmate\Documents\CRISUA\MVC-2020-Templates\Views\Shared\_MasterLayout.cshtml"
                               Write(Html.ActionLink("Erro", "Errors", "Home", null));

#line default
#line hidden
#nullable disable
                WriteLiteral("</li>\r\n                            </ul>\r\n                        </li>\r\n");
                WriteLiteral("                    </ul>\r\n                </nav>\r\n            </div>\r\n            ");
#nullable restore
#line 127 "C:\Users\pmate\Documents\CRISUA\MVC-2020-Templates\Views\Shared\_MasterLayout.cshtml"
       Write(RenderBody());

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n        </div>\r\n    </div>\r\n\r\n\r\n    <footer>\r\n");
                WriteLiteral(@"        <div class=""section"">
            <div class=""limiter"">
                <div class=""info"">
                    &copy; Universidade de Aveiro 2020
                </div>
            </div>
        </div>
    </footer>

    <script src=""//static.ua.pt/js/jquery/jquery-3.2.1.min.js""></script>
    <script src=""//static.ua.pt/css/semanticua/2.0b/dist/semantic.min.js""></script>
    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "bae23b480fa6b366660292f9e08ea8e554b6bbea16550", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ScriptTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.Src = (string)__tagHelperAttribute_2.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
#nullable restore
#line 151 "C:\Users\pmate\Documents\CRISUA\MVC-2020-Templates\Views\Shared\_MasterLayout.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.AppendVersion = true;

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-append-version", __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.AppendVersion, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n    ");
#nullable restore
#line 152 "C:\Users\pmate\Documents\CRISUA\MVC-2020-Templates\Views\Shared\_MasterLayout.cshtml"
Write(RenderSection("Scripts", required: false));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n</html>");
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
