using Client.Zeus.Business.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Zeus.Contract
{
    public class BaseContract
    {
        #region Mangers

        public UserManager userManager;
        public UserManager UserManager
        {
            get
            {
                if (userManager == null)
                    userManager = new UserManager();
                return userManager;
            }
            set { userManager = value; }
        }

        #endregion
    }
}
