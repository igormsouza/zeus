//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BHS.ProjetoBaseMvc.Negocio.Gerenciador
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Linq.Expressions;
	using System.Text;
	using System.Threading.Tasks;
	using BHS.ProjetoBaseMvc.Dados;
	using BHS.ProjetoBaseMvc.Dados.Repositorio;
	using BHS.ProjetoBaseMvc.Dominio;	 
	using BHS.ProjetoBaseMvc.Dominio.PesquisaDTO;
	using BHS.ProjetoBaseMvc.Negocio.Base;

	public partial class MenuGerenciador : BaseGerenciador<TB_MENU>
	{
		public MenuGerenciador()
			: base()
		{
		}

		public MenuGerenciador(Contexto contexto)
			: base(contexto)
		{
		}

		public MenuGerenciador(Adaptador adaptador)
			: base(adaptador)
		{
		}		  

		public MenuRepositorio Repositorio
		{
			get
			{
				return (MenuRepositorio)base.RepositorioBase;
			}
		}
	}
}