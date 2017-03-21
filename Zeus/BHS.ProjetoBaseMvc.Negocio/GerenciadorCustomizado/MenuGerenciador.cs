using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Linq.Expressions;
using BHS.ProjetoBaseMvc.Dominio;
using BHS.ProjetoBaseMvc.Dados;
using BHS.ProjetoBaseMvc.Negocio.Base;
using BHS.ProjetoBaseMvc.Dados.Repositorio;

namespace BHS.ProjetoBaseMvc.Negocio.Gerenciador
{
    public partial class MenuGerenciador : BaseGerenciador<TB_MENU>
    {
        public IList<TB_MENU> ConsultarMenuPorPerfil(int idPerfil)
        {
            var query = (from a in base.Query
                         from b in a.TB_PERFIL
                         where b.ID == idPerfil
                         select a);

            return query.ToList();
        }
    }
}
