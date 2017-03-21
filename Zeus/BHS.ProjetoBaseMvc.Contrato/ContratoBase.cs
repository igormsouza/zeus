using BHS.ProjetoBaseMvc.Negocio;
using BHS.ProjetoBaseMvc.Negocio.Gerenciador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHS.ProjetoBaseMvc.Contrato
{
    public class ContratoBase
    {
        #region Gerenciadores

        // Implementa Gerenciadores

        private CidadeGerenciador cidadeGerenciador;
        public CidadeGerenciador CidadeGerenciador
        {
            get
            {
                if (cidadeGerenciador == null)
                    cidadeGerenciador = new CidadeGerenciador();
                return cidadeGerenciador;
            }
        }

        private UFGerenciador uFGerenciador;
        public UFGerenciador UFGerenciador
        {
            get
            {
                if (uFGerenciador == null)
                    uFGerenciador = new UFGerenciador();
                return uFGerenciador;
            }
        }

        #endregion
    }
}
