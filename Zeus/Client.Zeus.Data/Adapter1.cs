
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Reflection;
using System.Data.Entity;
using Client.Zeus.Data;
using Client.Zeus.Data.Repository;
using Client.Zeus.Domain.Base;

namespace Client.Zeus.Data
{
    public partial class Adapter
    {
		private Context context;

        public Adapter(Context existContext)
        {
            context = existContext;
        }

		// Implementa Repositorys
		#region [ Repositories ]

		private FunctionalityRepository functionalityrepository;
        public FunctionalityRepository FunctionalityRepository
        {
            get
            {
                if (this.functionalityrepository == null)
                {
                    this.functionalityrepository = new FunctionalityRepository(context);
                }
                return this.functionalityrepository;
            }
        }

		private MenuRepository menurepository;
        public MenuRepository MenuRepository
        {
            get
            {
                if (this.menurepository == null)
                {
                    this.menurepository = new MenuRepository(context);
                }
                return this.menurepository;
            }
        }

		private PerfilRepository perfilrepository;
        public PerfilRepository PerfilRepository
        {
            get
            {
                if (this.perfilrepository == null)
                {
                    this.perfilrepository = new PerfilRepository(context);
                }
                return this.perfilrepository;
            }
        }

		private UserRepository userrepository;
        public UserRepository UserRepository
        {
            get
            {
                if (this.userrepository == null)
                {
                    this.userrepository = new UserRepository(context);
                }
                return this.userrepository;
            }
        }
		#endregion			
	}
}


