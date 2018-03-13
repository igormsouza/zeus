using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;

namespace Client.Zeus.Util
{
    public class CacheSearch
    {
        private static MemoryCache cache = MemoryCache.Default;

        public static bool? GetPermissionToAcess(int idUser, string itemMenu)
        {
            string key = string.Format("permission_{0}_{1}", idUser, itemMenu);

            bool? result = (bool?)cache.Get(key);
            return result;
        }

        public static int? GetPermissionToEdit(int idUser, string itemMenu)
        {
            string key = string.Format("edit_permission_{0}_{1}", idUser, itemMenu);

            int? result = (int?)cache.Get(key);
            return result;
        }

        public static void SetPermissionToAcess(int idUser, string itemMenu, bool value)
        {
            string key = string.Format("permission_{0}_{1}", idUser, itemMenu);

            CacheItemPolicy policy = new CacheItemPolicy();
            policy.AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(30);
            cache.Add(key, value, policy);
        }

        public static void SetPermissaoAcessoEditar(int idUser, string itemMenu, int value)
        {
            string key = string.Format("edit_permission_{0}_{1}", idUser, itemMenu);

            CacheItemPolicy policy = new CacheItemPolicy();
            policy.AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(30);
            cache.Add(key, value, policy);
        }
    }
}
