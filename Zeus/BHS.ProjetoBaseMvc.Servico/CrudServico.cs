using BHS.ProjetoBaseMvc.Negocio;
using BHS.ProjetoBaseMvc.Contrato;
using BHS.ProjetoBaseMvc.Contrato.Entidade;
using BHS.ProjetoBaseMvc.Contrato.Interface;
using BHS.ProjetoBaseMvc.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BHS.ProjetoBaseMvc.Negocio.Gerenciador;

namespace BHS.ProjetoBaseMvc.Servico
{
    public class CrudServico : ICrudContrato
    {
        #region Gerenciadores

        public CidadeGerenciador cidadeGerenciador;
        public CidadeGerenciador CidadeGerenciador
        {
            get
            {
                if (cidadeGerenciador == null)
                    cidadeGerenciador = new CidadeGerenciador();
                return cidadeGerenciador;
            }
            set { cidadeGerenciador = value; }
        }

        public UFGerenciador uFGerenciador;
        public UFGerenciador UFGerenciador
        {
            get
            {
                if (uFGerenciador == null)
                    uFGerenciador = new UFGerenciador();
                return uFGerenciador;
            }
            set { uFGerenciador = value; }
        }

        #endregion

        #region TB_CIDADE

        public IList<TB_CIDADE> ListarCidade()
        {
            var retorno = CidadeGerenciador.Listar();
            return retorno;
        }

        public RetornoListaTotal<TB_CIDADE> ListarPaginadoCidade(int paginaAtual, string ordenacao, string direcaoOrdenacao, int quantidadePorPagina)
        {
            var retorno = new RetornoListaTotal<TB_CIDADE>();
            int totalItens;
            retorno.Lista = CidadeGerenciador.ListarPaginado(out totalItens, paginaAtual, ordenacao, direcaoOrdenacao, quantidadePorPagina);
            retorno.TotalItens = totalItens;
            return retorno;
        }

        public TB_CIDADE BuscarPorIdCidade(int id)
        {
            var retorno = CidadeGerenciador.BuscarPorId(id);
            return retorno;
        }

        public RetornoComMensagem CriarCidade(TB_CIDADE objetoNovo)
        {
            RetornoComMensagem retorno = new RetornoComMensagem();
            Dictionary<string, string> errosValidacao;
            retorno.Retorno = CidadeGerenciador.Criar(objetoNovo, out errosValidacao);
            retorno.ErrosValidacao = errosValidacao;
            return retorno;
        }

        public RetornoComMensagem EditarCidade(TB_CIDADE objetoEditado)
        {
            RetornoComMensagem retorno = new RetornoComMensagem();
            Dictionary<string, string> errosValidacao;
            retorno.Retorno = CidadeGerenciador.Editar(objetoEditado, out errosValidacao);
            retorno.ErrosValidacao = errosValidacao;
            return retorno;
        }

        public RetornoComMensagem ExcluirCidade(int id)
        {
            RetornoComMensagem retorno = new RetornoComMensagem();
            Dictionary<string, string> errosValidacao;
            retorno.Retorno = CidadeGerenciador.Excluir(id, out errosValidacao);
            retorno.ErrosValidacao = errosValidacao;
            return retorno;
        }

        public RetornoComMensagem ExcluirVariosCidade(IList<int> id)
        {
            RetornoComMensagem retorno = new RetornoComMensagem();
            Dictionary<string, string> errosValidacao;
            retorno.Retorno = cidadeGerenciador.Excluir(id, out errosValidacao);
            retorno.ErrosValidacao = errosValidacao;
            return retorno;
        }

        #endregion

        #region TB_UF

        public IList<TB_UF> ListarUF()
        {
            var retorno = UFGerenciador.Listar();
            return retorno;
        }

        public RetornoListaTotal<TB_UF> ListarPaginadoUF(int paginaAtual, string ordenacao, string direcaoOrdenacao, int quantidadePorPagina)
        {
            var retorno = new RetornoListaTotal<TB_UF>();
            int totalItens;
            retorno.Lista = UFGerenciador.ListarPaginado(out totalItens, paginaAtual, ordenacao, direcaoOrdenacao, quantidadePorPagina);
            retorno.TotalItens = totalItens;
            return retorno;
        }

        public TB_UF BuscarPorIdUF(int id)
        {
            var retorno = UFGerenciador.BuscarPorId(id);
            return retorno;
        }

        public RetornoComMensagem CriarUF(TB_UF objetoNovo)
        {
            RetornoComMensagem retorno = new RetornoComMensagem();
            Dictionary<string, string> errosValidacao;
            retorno.Retorno = UFGerenciador.Criar(objetoNovo, out errosValidacao);
            retorno.ErrosValidacao = errosValidacao;
            return retorno;
        }

        public RetornoComMensagem EditarUF(TB_UF objetoEditado)
        {
            RetornoComMensagem retorno = new RetornoComMensagem();
            Dictionary<string, string> errosValidacao;
            retorno.Retorno = UFGerenciador.Editar(objetoEditado, out errosValidacao);
            retorno.ErrosValidacao = errosValidacao;
            return retorno;
        }

        public RetornoComMensagem ExcluirUF(int id)
        {
            RetornoComMensagem retorno = new RetornoComMensagem();
            Dictionary<string, string> errosValidacao;
            retorno.Retorno = UFGerenciador.Excluir(id, out errosValidacao);
            retorno.ErrosValidacao = errosValidacao;
            return retorno;
        }

        public RetornoComMensagem ExcluirVariosUF(IList<int> id)
        {
            RetornoComMensagem retorno = new RetornoComMensagem();
            Dictionary<string, string> errosValidacao;
            retorno.Retorno = UFGerenciador.Excluir(id, out errosValidacao);
            retorno.ErrosValidacao = errosValidacao;
            return retorno;
        }

        public RetornoComMensagem TestarServico(IList<int> id)
        {
            RetornoComMensagem retorno = new RetornoComMensagem();
            Dictionary<string, string> errosValidacao;
            retorno.Retorno = UFGerenciador.Excluir(id, out errosValidacao);
            retorno.ErrosValidacao = errosValidacao;
            return retorno;
        }

        #endregion
    }
}
