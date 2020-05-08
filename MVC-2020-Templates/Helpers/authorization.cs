//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;

//namespace Ajuda.Helpers
//{
//    public class Authorization
//    {
//        // GET: authorization
//        public bool Success { get; private set; }

//        public Authorization()
//        {
//            var p = new Ajuda.Models.Person(Session.FullName, Session.ShortName, Session.IUPI, Session.Email);
//            Session.UserID = p.ID;
//            //Verifica se existe na BD
//            if (p.Authorized)
//            {
//                // grava eventuais alterações nos dados do utilizador
//                p.Save();

//                // carrega info para a sessão
//                if (p.Active)
//                {
//                    this.Success = p.Active;
//                }
//            }

//        }
//    }
//}
