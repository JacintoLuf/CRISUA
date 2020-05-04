using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_2020_Template.Helpers
{
	public class Session
	{
		public ISession session { get; set; }
		public Session(ISession ses)
		{
			session = ses;
		}
		public string FullName
		{
			get
			{
				string nome = "";
				try
				{
					nome = session.GetString("s_UserFullName");
				}
				catch { }
				return nome;
			}
			set
			{
				session.SetString("s_UserFullName", value);
			}
		}

		public string ShortName
		{
			get
			{
				string nome = "";
				try
				{
					nome = session.GetString("s_UserShortName");
				}
				catch { }
				return nome;
			}
			set
			{
				session.SetString("s_UserShortName", value);
			}
		}

		public int UserID
		{
			get
			{
				int id = 0;
				try
				{
					if (session.GetString("s_UserID") != null)
						Int32.TryParse(session.GetString("s_UserID"), out id);
				}
				catch { }
				return id;
			}
			set
			{
				session.SetString("s_UserID", value.ToString());
			}
		}

		public Guid IUPI
		{
			get
			{
				Guid iupi = new Guid();
				try
				{
					iupi = Guid.Parse(session.GetString("s_IUPI"));
				}
				catch { }
				return iupi;
			}
			set
			{
				try
				{
					session.SetString("s_IUPI", value.ToString());
				}
				catch { }
				
			}
		}

		public string Email
		{
			get
			{
				string s = "";
				try
				{
					s = session.GetString("s_UserEmail");
				}
				catch { }
				return s;
			}
			set
			{
				session.SetString("s_UserEmail", value);
			}
		}

		public bool IsAdmin
		{
			get
			{
				bool admin = false;
				try
				{
					if (session.GetString("s_UserIsAdmin") != null)
						admin = Boolean.Parse(session.GetString("s_UserIsAdmin"));
				}
				catch { }
				return admin;
			}
			set
			{
				session.SetString("s_UserIsAdmin", value.ToString());
			}
		}

		public string Language
		{
			get
			{
				string s = "";
				try
				{
					if (session.GetString("s_Language") != null)
						s = session.GetString("s_Language");
				}
				catch { }
				return s;
			}
			set
			{
				session.SetString("s_UserIsAdmin", value);
			}
		}

		public string ContentLanguage
		{
			get
			{
				string s = "pt";
				try
				{
					if (session.GetString("s_ContentLanguage") != null)
						s = session.GetString("s_ContentLanguage");
				}
				catch { }
				return s;
			}
			set
			{
				session.SetString("s_ContentLanguage", value);
			}
		}

		public bool Impersonated
		{
			get
			{
				bool imp = false;
				try
				{
					if (session.GetString("s_Impersonated") != null)
						imp = Boolean.Parse(session.GetString("s_Impersonated"));
				}
				catch { }
				return imp;
			}
			set
			{
				session.SetString("s_Impersonated", value.ToString());
			}
		}

		public bool AuthTried
		{
			get
			{
				bool temp = false;
				try
				{
					if (session.GetString("s_SecureAccessed") != null)
						temp = Boolean.Parse(session.GetString("s_SecureAccessed"));
				}
				catch { }
				return temp;
			}
			set
			{
				session.SetString("s_SecureAccessed", value.ToString());
			}
		}

		public string Roles
		{
			get
			{
				string s = "";
				try
				{
					if (session.GetString("s_UserRoles") != null)
						s = session.GetString("s_UserRoles");
				}
				catch { }
				return s;
			}
			set
			{
				session.SetString("s_UserRoles", value.ToString());
			}
		}

		public string ReturnAddress
		{
			get
			{
				string s = "";
				try
				{
					if (session.GetString("s_ReturnTo") != null)
						s = session.GetString("s_ReturnTo");
				}
				catch { }
				return s;
			}
			set
			{
				session.SetString("s_ReturnTo", value.ToString());
			}
		}

		public int Unit
		{
			get
			{
				int id = 0;
				try
				{
					if (session.GetString("s_Unit") != null)
						Int32.TryParse(session.GetString("s_Unit"), out id);
				}
				catch { }
				return id;
			}
			set
			{
				session.SetString("s_ReturnTo", value.ToString());
			}
		}
		public bool UserActive
		{
			get
			{
				bool temp = false;
				try
				{
					if (session.GetString("s_UserActive") != null)
						temp = Boolean.Parse(session.GetString("s_UserActive"));
				}
				catch { }
				return temp;
			}
			set
			{
				session.SetString("s_ReturnTo", value.ToString());
			}
		}

		public bool SessionActive
		{
			get
			{
				bool temp = false;
				try
				{
					if (session.GetString("s_SessionActive") != null)
						temp = Boolean.Parse(session.GetString("s_SessionActive"));
				}
				catch { }
				return temp;
			}
			set
			{
				session.SetString("s_SessionActive", value.ToString());
			}
		}


		protected const string s_UserRoles = "a_user-roles";
		protected const string s_UserFullName = "a_full-name";
		protected const string s_UserShortName = "a_user-short-name";
		protected const string s_UserID = "a_user-id";
		protected const string s_IUPI = "a_iupi";
		protected const string s_UserEmail = "a_user-email";
		protected const string s_UserIsAdmin = "a_user-is-admin";
		protected const string s_Language = "a_language";
		protected const string s_ContentLanguage = "a_content-language";
		protected const string s_Impersonated = "a_impersonated";
		protected const string s_SecureAccessed = "a_secure-accessed";
		protected const string s_ReturnTo = "a_return-to";
		protected const string s_Unit = "a_unit";
		protected const string s_UserActive = "a_active-user";
		protected const string s_SessionActive = "a_active-session";

		//public class History
		//{
		//	public   void Push(string url)
		//	{
		//		var items = VisitedPages.Split(new char['|'], StringSplitOptions.RemoveEmptyEntries).ToList();
		//		items.Add(url);
		//		VisitedPages = String.Join("|", items);
		//	}

		//	public   string Pop()
		//	{
		//		string last = "#";
		//		var items = VisitedPages.Split(new char['|'], StringSplitOptions.RemoveEmptyEntries).ToList();
		//		int num = items.Count();
		//		if (num > 0)
		//		{
		//			last = items[num - 1];
		//			items.RemoveAt(num - 1);
		//			VisitedPages = String.Join("|", items);
		//		}
		//		return last;
		//	}

			//private   string VisitedPages
			//{
			//	get
			//	{
			//		string s = "";
			//		try
			//		{
			//			if (HttpContext.Current.Session[s_History] != null)
			//				s = HttpContext.Current.Session[s_History].ToString();
			//		}
			//		catch { }
			//		return s;
			//	}
			//	set
			//	{
			//		HttpContext.Current.Session[s_History] = value;
			//	}
			//}

			//private const string s_History = "red_history";
		//}
	}

}