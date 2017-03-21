using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;

namespace BHS.ProjetoBaseMvc.Util
{
    public class ConsultaCache
    {
        private static MemoryCache cache = MemoryCache.Default;

        public static bool? GetPermissaoAcesso(int idUsuario, string itemMenu)
        {
            string chave = string.Format("permisao_{0}_{1}", idUsuario, itemMenu);

            bool? retorno = (bool?)cache.Get(chave);
            return retorno;
        }

        public static int? GetPermissaoAcessoEditar(int idUsuario, string itemMenu)
        {
            string chave = string.Format("permisaoEditar_{0}_{1}", idUsuario, itemMenu);

            int? retorno = (int?)cache.Get(chave);
            return retorno;
        }

        public static void SetPermissaoAcesso(int idUsuario, string itemMenu, bool valor)
        {
            string chave = string.Format("permisao_{0}_{1}", idUsuario, itemMenu);

            CacheItemPolicy politica = new CacheItemPolicy();
            politica.AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(30);
            cache.Add(chave, valor, politica);
        }

        public static void SetPermissaoAcessoEditar(int idUsuario, string itemMenu, int valor)
        {
            string chave = string.Format("permisaoEditar_{0}_{1}", idUsuario, itemMenu);

            CacheItemPolicy politica = new CacheItemPolicy();
            politica.AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(30);
            cache.Add(chave, valor, politica);
        }
    }
}
