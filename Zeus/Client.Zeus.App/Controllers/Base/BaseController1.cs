
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Client.Zeus.Domain;
using Client.Zeus.Business;
using Client.Zeus.Util;
using System.Configuration;
using Client.Zeus.Domain.Base;
using System.Linq.Expressions;
using Client.Zeus.Business.Base;
using System.Reflection;
using Client.Zeus.Business.Manager;

namespace Client.Zeus.App.Controllers
{
    public partial class BaseController : Controller
    {
		#region [ Managers ]

		private FunctionalityManager functionalitymanager;
        public FunctionalityManager FunctionalityManager
        {
            get
            {
                if (this.functionalitymanager == null)
                {
                    this.functionalitymanager = new FunctionalityManager();
                }
                return this.functionalitymanager;
            }
        }

		private MenuManager menumanager;
        public MenuManager MenuManager
        {
            get
            {
                if (this.menumanager == null)
                {
                    this.menumanager = new MenuManager();
                }
                return this.menumanager;
            }
        }

		private PerfilManager perfilmanager;
        public PerfilManager PerfilManager
        {
            get
            {
                if (this.perfilmanager == null)
                {
                    this.perfilmanager = new PerfilManager();
                }
                return this.perfilmanager;
            }
        }

		private UserManager usermanager;
        public UserManager UserManager
        {
            get
            {
                if (this.usermanager == null)
                {
                    this.usermanager = new UserManager();
                }
                return this.usermanager;
            }
        }
		#endregion			
	}
}


