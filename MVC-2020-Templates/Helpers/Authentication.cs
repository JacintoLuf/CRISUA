using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;
using System;
using System.Configuration;
using System.Diagnostics.Contracts;
using System.Web;

namespace MVC_2020_Template.Helpers
{
    public class Authentication
    {
        public bool Success { get; private set; }
        public static HttpContext Ses { get; set; }
        public static IHostingEnvironment Local { get; set; }
        public static IConfiguration Config { get; set; }
        public Authentication(HttpContext session, IHostingEnvironment local, IConfiguration config)
        {
            Ses = session;
            Local = local;
            Config = config;
             Shibboleth.ses = session;
             Shibboleth.serverVars = session.Features.Get<IServerVariablesFeature>();
            var sessao = new Session(Ses.Session);
            Session.session = Ses.Session;
            //if (secao.Impersonated)
            //    return;

            // se não houver iupi em sessão
            if (sessao.IUPI == Guid.Empty) //if (secao.IUPI == Guid.Empty)
            {
                //new Helpers.Log(Log.ActionType.Debug, "sessão sem iupi");
                var c = session;

                Contract.Assert(c != null);

                //Console.WriteLine("uu shibb: " + Shibboleth.Email);
                //Console.WriteLine("web config: " + ConfigurationManager.AppSettings["Auth: Shib:Email"]);
                //Console.WriteLine("uu shibb: " + Shibboleth.shib_uu);
                //new Helpers.Log(Log.ActionType.Debug, "uu shibb: " + Shibboleth.Email);
                //new Helpers.Log(Log.ActionType.Debug, "web config: " + ConfigurationManager.AppSettings["Auth:Shib:Email"]);
                //new Helpers.Log(Log.ActionType.Debug, "uu shibb: " + Shibboleth.shib_uu);

                // verifica se existem Headers para a autenticação				
                if (!String.IsNullOrWhiteSpace(Shibboleth.Email) /*|| local.IsDevelopment()*/)
                {
                    //new Helpers.Log(Log.ActionType.Debug, "cabeçalhos de shibboleth encontrados");


                    // existem cabeçalhos, processa o login
                    Guid iupi = new Guid();
                    string fullName = "";
                    string shortName = "";
                    string email ;
                    /***********************************************/
                    /* bloco para acesso local, simula credenciais */

                    // existem cabeçalhos, processa o login
                    iupi = Shibboleth.IUPI;
                    fullName = Shibboleth.NomeCompleto;
                    // shortName = Shibboleth.NomeCurto;
                    email = Shibboleth.Email;
                    /***********************************************/

                    sessao.FullName = fullName;
                    sessao.ShortName = shortName;
                    sessao.IUPI = iupi;
                    sessao.Email = email;
                    Session.SessionActive = true;

                    this.Success = true;
                }
                else
                {
                    this.Success = false;
                }
                //else
                //{
                //	// não existem cabeçalhos, redireciona para área segura (caso ainda não tenha ido lá)
                //	if (requestCredentials && !Session.AuthTried)
                //		c.Response.Redirect(String.Format("{0}Default.aspx?referer={1}", ConfigurationManager.AppSettings["Auth:SecurePath"], HttpUtility.UrlEncode(c.Request.Url.PathAndQuery)));					
                //	else
                //		new Helpers.Log(Helpers.Log.ActionType.LoginFailed, "sem dados de Shibboleth");
                //}
            }
            else
            {
                this.Success = true;
            }
        }


        public static class Shibboleth
        {
            public static HttpContext ses { get; set; }
            public static IServerVariablesFeature serverVars;
            //public  Shibboleth(HttpContext ses)
            //{
            //    Ses = ses;
            //    serverVars = ses.Features.Get<IServerVariablesFeature>();
            //    //iupiSession = serverVars?["IUPI"].ToString();
            //}
            public static Guid IUPI
            {
                get
                {
                    Guid output;
                    Guid.TryParse(serverVars?[shib_iupi].ToString(), out output);
                    return output;
                }
            }
            
            public static string NomeCompleto { get { return serverVars?[shib_nomecompleto].ToString(); } }

            public static string NomeCurto
            {
                get
                {
                    if (!String.IsNullOrEmpty(shib_nomeamigavel))
                        return serverVars?["shib_nomeamigavel"].ToString();
                    else
                        return serverVars?["shib_nome"].ToString() + " " + serverVars?["shib_apelido"].ToString();
                }
            }

            public static string Email { get { return serverVars?[shib_uu]?.ToString(); } }

            private static string shib_iupi = "iupi";
            private static string shib_nomecompleto = "fullname";
            private static string shib_nomeamigavel = ConfigurationManager.AppSettings["Auth:Shib:NomeAmigavel"];
            private static string shib_uu = "mail";
            private static string shib_nome = ConfigurationManager.AppSettings["Auth:Shib:Nome"];
            private static string shib_apelido = ConfigurationManager.AppSettings["Auth:Shib:Apelido"];
            
        }
    }
}