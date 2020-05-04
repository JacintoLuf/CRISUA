using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
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
        public Authentication(HttpContext session, IHostingEnvironment local)
        {
            Ses = session;
            Local = local;
            var secao = new Session(Ses.Session);
            //if (secao.Impersonated)
            //    return;

            // se não houver iupi em sessão
            if (secao.IUPI == Guid.Empty)
            {
                //new Helpers.Log(Log.ActionType.Debug, "sessão sem iupi");
                var c = session;

                Contract.Assert(c != null);

                Console.WriteLine("uu shibb: " + Shibboleth.Email);
                Console.WriteLine("web config: " + ConfigurationManager.AppSettings["Auth: Shib:Email"]);
                Console.WriteLine("uu shibb: " + Shibboleth.shib_uu);
                //new Helpers.Log(Log.ActionType.Debug, "uu shibb: " + Shibboleth.Email);
                //new Helpers.Log(Log.ActionType.Debug, "web config: " + ConfigurationManager.AppSettings["Auth:Shib:Email"]);
                //new Helpers.Log(Log.ActionType.Debug, "uu shibb: " + Shibboleth.shib_uu);

                // verifica se existem Headers para a autenticação				
                if (!String.IsNullOrWhiteSpace(Shibboleth.Email) || local.IsDevelopment())
                {
                    //new Helpers.Log(Log.ActionType.Debug, "cabeçalhos de shibboleth encontrados");

                    // existem cabeçalhos, processa o login
                    Guid iupi = Shibboleth.IUPI;
                    string fullName = Shibboleth.NomeCompleto;
                    string shortName = Shibboleth.NomeCurto;
                    string email = Shibboleth.Email;

                    /***********************************************/
                    /* bloco para acesso local, simula credenciais */
                    if (local.IsDevelopment())
                    {
                        switch (Environment.MachineName.ToLower())
                        {
                            case "stic-nb0015":
                                fullName = "Filipe António Rodrigues Barreto Trancho";
                                shortName = "Filipe Trancho";
                                email = "ftrancho@ua.pt";
                                iupi = new Guid("9be52ad0-e982-407d-b4bf-f9c724e96bba");
                                break;
                            case "stic-nb0054":
                                fullName = "Rui Gonçalo Marques Pereira";
                                shortName = "Rui Pereira";
                                email = "ruigp@ua.pt";
                                iupi = new Guid("e8f6d698-33d9-4f91-a5ea-4fb6e844b950");
                                break;
                            default:
                                fullName = "Bruno André Mansilha Andrade";
                                shortName = "Bruno Andrade";
                                email = "brunoaandrade@ua.pt";
                                iupi = new Guid("81291d36-e0fe-482e-9017-ffbce04c8583");
                                break;
                        }
                    }
                    /***********************************************/

                    secao.FullName = fullName;
                    secao.ShortName = shortName;
                    secao.IUPI = iupi;
                    secao.Email = email;
                    secao.SessionActive = true;

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


        public class Shibboleth
        {
            public static HttpContext ses { get; set; }
            public static IServerVariablesFeature serverVars;
            public Shibboleth(HttpContext ses)
            {
                Ses = ses;
                serverVars = ses.Features.Get<IServerVariablesFeature>();
                //iupiSession = serverVars?["IUPI"].ToString();
            }
            public static Guid IUPI
            {
                get
                {
                    Guid output;
                    Guid.TryParse(serverVars?["shib_iupi"].ToString(), out output);
                    return output;
                }
            }

            public static string NomeCompleto { get { return serverVars?["shib_nomecompleto"].ToString(); } }

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

            public static string Email { get { return serverVars?["shib_uu"].ToString(); } }

            private static string shib_iupi = ConfigurationManager.AppSettings["Auth:Shib:IUPI"];
            private static string shib_nomecompleto = ConfigurationManager.AppSettings["Auth:Shib:NomeCompleto"];
            private static string shib_nomeamigavel = ConfigurationManager.AppSettings["Auth:Shib:NomeAmigavel"];
            public static string shib_uu = ConfigurationManager.AppSettings["Auth:Shib:Email"];
            private static string shib_nome = ConfigurationManager.AppSettings["Auth:Shib:Nome"];
            private static string shib_apelido = ConfigurationManager.AppSettings["Auth:Shib:Apelido"];
            
        }
    }
}