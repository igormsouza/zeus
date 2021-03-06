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

	public partial class UserManager : BaseManager<TB_USER>
	{
		public UserManager()
			: base()
		{
		}

		public UserManager(Context context)
			: base(context)
		{
		}

		public UserManager(Adapter adapter)
			: base(adapter)
		{
		}		  

		public UserRepository Repository
		{
			get
			{
				return (UserRepository)base.BaseRepository;
			}
		}
	}
}
