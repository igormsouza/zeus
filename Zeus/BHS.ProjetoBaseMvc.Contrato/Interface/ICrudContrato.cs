using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using BHS.ProjetoBaseMvc.Contrato.Interface.Base;
using BHS.ProjetoBaseMvc.Dominio;
using BHS.ProjetoBaseMvc.Contrato.Entidade;

namespace BHS.ProjetoBaseMvc.Contrato.Interface
{
    [ServiceContract]
    public interface ICrudContrato
    {
        #region TB_CIDADE

        [OperationContract]
        IList<TB_CIDADE> ListarCidade();

        [OperationContract]
        RetornoListaTotal<TB_CIDADE> ListarPaginadoCidade(int paginaAtual, string ordenacao, string direcaoOrdenacao, int quantidadePorPagina);

        [OperationContract]
        TB_CIDADE BuscarPorIdCidade(int id);

        [OperationContract]
        RetornoComMensagem CriarCidade(TB_CIDADE objetoNovo);

        [OperationContract]
        RetornoComMensagem EditarCidade(TB_CIDADE objetoEditado);

        [OperationContract]
        RetornoComMensagem ExcluirCidade(int id);

        [OperationContract]
        RetornoComMensagem ExcluirVariosCidade(IList<int> id);

        #endregion

        #region TB_UF

        [OperationContract]
        IList<TB_UF> ListarUF();

        [OperationContract]
        RetornoListaTotal<TB_UF> ListarPaginadoUF(int paginaAtual, string ordenacao, string direcaoOrdenacao, int quantidadePorPagina);

        [OperationContract]
        TB_UF BuscarPorIdUF(int id);

        [OperationContract]
        RetornoComMensagem CriarUF(TB_UF objetoNovo);

        [OperationContract]
        RetornoComMensagem EditarUF(TB_UF objetoEditado);

        [OperationContract]
        RetornoComMensagem ExcluirUF(int id);

        [OperationContract]
        RetornoComMensagem ExcluirVariosUF(IList<int> id);

        [OperationContract]
        RetornoComMensagem TestarServico(IList<int> id);

        #endregion
    }
}
