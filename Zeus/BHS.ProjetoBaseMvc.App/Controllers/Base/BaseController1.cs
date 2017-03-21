
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using BHS.ProjetoBaseMvc.Dominio;
using BHS.ProjetoBaseMvc.Negocio;
using BHS.ProjetoBaseMvc.Util;
using System.Configuration;
using BHS.ProjetoBaseMvc.Dominio.Base;
using System.Linq.Expressions;
using BHS.ProjetoBaseMvc.Negocio.Base;
using System.Reflection;
using BHS.ProjetoBaseMvc.Negocio.Gerenciador;

namespace BHS.ProjetoBaseMvc.App.Controllers
{
    public partial class BaseController : Controller
    {
		#region [ Gerenciador ]

		private CidadeGerenciador cidadegerenciador;
        public CidadeGerenciador CidadeGerenciador
        {
            get
            {
                if (this.cidadegerenciador == null)
                {
                    this.cidadegerenciador = new CidadeGerenciador();
                }
                return this.cidadegerenciador;
            }
        }

		private UFGerenciador ufgerenciador;
        public UFGerenciador UFGerenciador
        {
            get
            {
                if (this.ufgerenciador == null)
                {
                    this.ufgerenciador = new UFGerenciador();
                }
                return this.ufgerenciador;
            }
        }

		private UsuarioGerenciador usuariogerenciador;
        public UsuarioGerenciador UsuarioGerenciador
        {
            get
            {
                if (this.usuariogerenciador == null)
                {
                    this.usuariogerenciador = new UsuarioGerenciador();
                }
                return this.usuariogerenciador;
            }
        }
		#endregion			
	}
}


