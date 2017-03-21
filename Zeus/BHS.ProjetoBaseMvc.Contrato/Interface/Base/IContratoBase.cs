using BHS.ProjetoBaseMvc.Contrato.Entidade;
using BHS.ProjetoBaseMvc.Dominio.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace BHS.ProjetoBaseMvc.Contrato.Interface.Base
{
    [ServiceContract]
    public interface IContratoBase<T> where T : DominioBase, new()
    {
        [OperationContract]
        IList<T> Listar();

        [OperationContract]
        RetornoListaTotal<T> ListarPaginado(int paginaAtual, string ordenacao, string direcaoOrdenacao, int quantidadePorPagina);

        [OperationContract]
        T BuscarPorId(int id);

        [OperationContract]
        RetornoComMensagem Criar(T objetoNovo);

        [OperationContract]
        RetornoComMensagem Editar(T objetoEditado);

        [OperationContract]
        RetornoComMensagem Excluir(int id);

        [OperationContract]
        RetornoComMensagem ExcluirVarios(IList<int> id);
    }
}
