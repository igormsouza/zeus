//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Client.Zeus.Business.Manager
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Linq.Expressions;
	using System.Text;
	using System.Threading.Tasks;
	using Client.Zeus.Data;
	using Client.Zeus.Data.Repository;
	using Client.Zeus.Domain;	 
	using Client.Zeus.Domain.PesquisaDTO;
	using Client.Zeus.Business.Base;

	public partial class MenuGerenciador : BaseGerenciador<TB_MENU>
	{
		public MenuManger()
			: base()
		{
		}

		public MenuManger(Contexto contexto)
			: base(context)
		{
		}

		public MenuManger(Adapter adapter)
			: base(adapter)
		{
		}		  

		public MenuRepository Repository
		{
			get
			{
				return (MenuRepository)base.BaseRepository;
			}
		}
	}
}
