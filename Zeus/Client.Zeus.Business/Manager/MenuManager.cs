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
	using Client.Zeus.Business.Base;

	public partial class MenuManager : BaseManager<TB_MENU>
	{
		public MenuManager()
			: base()
		{
		}

		public MenuManager(Context context)
			: base(context)
		{
		}

		public MenuManager(Adapter adapter)
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
