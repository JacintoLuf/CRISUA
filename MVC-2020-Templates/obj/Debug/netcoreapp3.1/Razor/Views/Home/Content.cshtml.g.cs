#pragma checksum "C:\Users\samue\OneDrive\Documentos\CRISUA\MVC-2020-Templates\Views\Home\Content.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "f242553e872fad2e6c97098fbcf9ac748cb61ffb"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Content), @"mvc.1.0.view", @"/Views/Home/Content.cshtml")]
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
#line 1 "C:\Users\samue\OneDrive\Documentos\CRISUA\MVC-2020-Templates\Views\_ViewImports.cshtml"
using MVC_2020_Template;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\samue\OneDrive\Documentos\CRISUA\MVC-2020-Templates\Views\_ViewImports.cshtml"
using MVC_2020_Template.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f242553e872fad2e6c97098fbcf9ac748cb61ffb", @"/Views/Home/Content.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c6dbcac5658e29f3e689f4716a6949d1930de775", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Content : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "C:\Users\samue\OneDrive\Documentos\CRISUA\MVC-2020-Templates\Views\Home\Content.cshtml"
  
    Layout = "_3ColsLayout";
    ViewData["Title"] = "Conteúdo";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h2>");
#nullable restore
#line 6 "C:\Users\samue\OneDrive\Documentos\CRISUA\MVC-2020-Templates\Views\Home\Content.cshtml"
Write(ViewData["Title"]);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</h2>

<div class=""code example"" data-text=""icon input"">
    <div class=""ui search"">
        <div class=""ui icon fluid input"">
            <input placeholder=""Digite o que pretende pesquisar...""
                   type=""text"" />
            <i class=""search icon""></i>
        </div>
        <div class=""results""></div>
    </div>
</div>

<div class=""code example"" data-text=""h1, h2 e h3"">
    <h1>
        Candidatura ao Programa Doutoral em Informação e Comunicação em
        Plataformas Digitais da Universidade de Aveiro
    </h1>
    <h2>
        Candidatura ao Programa Doutoral em Informação e Comunicação em
        Plataformas Digitais da Universidade de Aveiro
    </h2>
    <h3>
        Candidatura ao Programa Doutoral em Informação e Comunicação em
        Plataformas Digitais da Universidade de Aveiro
    </h3>
</div>

<div class=""code example"" data-text=""breadcrumb"">
    <div class=""ui small breadcrumb"">
        <a class=""section"">Início</a>
        <div class=""divider"">/<");
            WriteLiteral(@"/div>
        <div class=""active section"">Inscrições</div>
    </div>
</div>

<div class=""code example"" data-text=""icon message"">
    <div class=""ui icon message"">
        <i class=""chart area icon""></i>
        <div class=""content"">
            <div class=""header"">
                UA acolhe 410 estudantes estrangeiros de 38 países
            </div>
            <p>
                As nacionalidades mais representadas são a Polónia, a Itália,
                Republica Checa, Alemanha, sendo os restantes provenientes de
                Cazaquistão, Montenegro, Brasil, Índia, Bélgica, China,
                Eslováquia, Finlândia, França, Grécia, Holanda, Letónia,
                Lituânia, Roménia, Rússia e Turquia, Sérvia, entre outros.
            </p>
        </div>
    </div>
</div>

<div class=""code example"" data-text=""statistic"">
    <div class=""ui statistics"">
        <div class=""statistic"">
            <div class=""value"">
                22
            </div>
            <div");
            WriteLiteral(@" class=""label"">
                Favoritos
            </div>
        </div>
        <div class=""statistic"">
            <div class=""value"">
                31,200
            </div>
            <div class=""label"">
                Visualizações
            </div>
        </div>
        <div class=""statistic"">
            <div class=""value"">
                22
            </div>
            <div class=""label"">
                Membros
            </div>
        </div>
    </div>
</div>

<div class=""code example"" data-text=""evenly divided statistic"">
    <div class=""ui three statistics"">
        <div class=""statistic"">
            <div class=""value"">
                22
            </div>
            <div class=""label"">
                alunos
            </div>
        </div>
        <div class=""statistic"">
            <div class=""value"">
                <i class=""history icon""></i>
            </div>
            <div class=""label"">
                Histórico
            </div");
            WriteLiteral(@">
        </div>
        <div class=""ui statistic"">
            <div class=""value"">
                <img src=""resources/square-people.jpg""
                     class=""ui circular inline image"" />
            </div>
            <div class=""label"">
                Docentes
            </div>
        </div>
    </div>
</div>

<div class=""code example"" data-text=""tabs"">
    <div class=""js-remove ui icon message"">
        <i class=""info icon""></i>
        <div class=""content"">
            <div class=""header"">
                Nota relativa à utilização de Tabs:
            </div>
            <p>
                Para que funcione corretamente, este componente precisa de
                jQuery e deve ser inicializado da seguinte forma:
            </p>
            <code>
                <pre>$('.js-tab .item').tab();</pre>
            </code>
            <p>
                Se necessário podem ser usados seletores mais específicos
                (ex.: '#tabs-info')
            </p>
  ");
            WriteLiteral(@"      </div>
    </div>

    <div class=""js-tab ui top attached tabular menu"">
        <a class=""item active"" data-tab=""one"">Primeira secção</a>
        <a class=""item"" data-tab=""two"">Segunda secção</a>
        <a class=""item"" data-tab=""three"">Terceira secção</a>
    </div>
    <div class=""ui bottom attached tab segment active"" data-tab=""one"">
        O Lorem Ipsum é um texto modelo da indústria tipográfica e de
        impressão. O Lorem Ipsum tem vindo a ser o texto padrão usado por
        estas indústrias desde o ano de 1500, quando uma misturou os
        caracteres de um texto para criar um espécime de livro.
    </div>
    <div class=""ui bottom attached tab segment"" data-tab=""two"">
        Foi popularizada nos anos 60 com a disponibilização das folhas de
        Letraset, que continham passagens com Lorem Ipsum, e mais
        recentemente com os programas de publicação como o Aldus PageMaker
        que incluem versões do Lorem Ipsum.
    </div>
    <div class=""ui bottom attached t");
            WriteLiteral(@"ab segment"" data-tab=""three"">
        É um facto estabelecido de que um leitor é distraído pelo conteúdo
        legível de uma página quando analisa a sua mancha gráfica.
    </div>
</div>

<div class=""code example"" data-text=""basic horizontal segment"">
    <div class=""ui horizontal segments"">
        <div class=""ui segment"">
            <p>
                O Lorem Ipsum é um texto modelo da indústria tipográfica e de
                impressão.
            </p>
        </div>
        <div class=""ui segment"">
            <p>
                O Lorem Ipsum é um texto modelo da indústria tipográfica e de
                impressão. O Lorem Ipsum é um texto modelo da indústria
                tipográfica e de impressão.
            </p>
        </div>
        <div class=""ui segment"">
            <p>
                O Lorem Ipsum é um texto modelo da indústria tipográfica e de
                impressão.
            </p>
        </div>
    </div>
</div>

<div class=""code example"" data-t");
            WriteLiteral(@"ext=""raised segment"">
    <div class=""ui raised segment"">
        <p>
            O Lorem Ipsum é um texto modelo da indústria tipográfica e de
            impressão. O Lorem Ipsum tem vindo a ser o texto padrão usado
            por estas indústrias desde o ano de 1500, quando uma misturou os
            caracteres de um texto para criar um espécime de livro.
        </p>
    </div>
</div>

<div class=""code example"" data-text=""padded segment"">
    <div class=""ui padded segment"">
        <p>
            O Lorem Ipsum é um texto modelo da indústria tipográfica e de
            impressão. O Lorem Ipsum tem vindo a ser o texto padrão usado
            por estas indústrias desde o ano de 1500, quando uma misturou os
            caracteres de um texto para criar um espécime de livro. Este
            texto não só sobreviveu 5 séculos, mas também o salto para a
            tipografia electrónica, mantendo-se essencialmente inalterada.
            Foi popularizada nos anos 60 com a disponibilizaç");
            WriteLiteral(@"ão das folhas
            de Letraset, que continham passagens com Lorem Ipsum, e mais
            recentemente com os programas de publicação como o Aldus
            PageMaker que incluem versões do Lorem Ipsum.
        </p>
    </div>
</div>

<div class=""code example"" data-text=""simple segment"">
    <div class=""ui segment"">
        <p>
            O Lorem Ipsum é um texto modelo da indústria tipográfica e de
            impressão. O Lorem Ipsum tem vindo a ser o texto padrão usado
            por estas indústrias desde o ano de 1500, quando uma misturou os
            caracteres de um texto para criar um espécime de livro.
        </p>
    </div>
</div>");
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
