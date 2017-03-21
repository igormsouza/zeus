using BHS.ProjetoBaseMvc.Contrato.Entidade;
using BHS.ProjetoBaseMvc.Contrato.Interface.Base;
using BHS.ProjetoBaseMvc.Dominio.Base;
using BHS.ProjetoBaseMvc.Negocio.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BHS.ProjetoBaseMvc.Contrato
{
    public class ContratoCrud<T> : ContratoBase, IContratoBase<T> where T : DominioBase, new()
    {
        private BaseGerenciador<T> retorno;
        protected BaseGerenciador<T> GerenciadorBase
        {
            get
            {
                if (retorno == null)
                {
                    var propriedades = typeof(ContratoBase).GetProperties();
                    foreach (PropertyInfo i in propriedades)
                    {
                        var propriedade = i.GetValue(this);
                        if (propriedade is BaseGerenciador<T>)
                        {
                            retorno = propriedade as BaseGerenciador<T>;
                            break;
                        }
                    }
                }

                return retorno;
            }
        }

        public IList<T> Listar()
        {
            IList<T> retorno = GerenciadorBase.Listar();
            return retorno;
        }

        public RetornoListaTotal<T> ListarPaginado(int paginaAtual, string ordenacao, string direcaoOrdenacao, int quantidadePorPagina)
        {
            RetornoListaTotal<T> retorno = new RetornoListaTotal<T>();
            int totalItens;
            retorno.Lista = GerenciadorBase.ListarPaginado(out totalItens, paginaAtual, ordenacao, direcaoOrdenacao, quantidadePorPagina);
            retorno.TotalItens = totalItens;
            return retorno;
        }

        public T BuscarPorId(int id)
        {
            T retorno = GerenciadorBase.BuscarPorId(id);
            return retorno;
        }

        public RetornoComMensagem Criar(T objetoNovo)
        {
            RetornoComMensagem retorno = new RetornoComMensagem();
            Dictionary<string, string> errosValidacao;
            retorno.Retorno = GerenciadorBase.Criar(objetoNovo, out errosValidacao);
            retorno.ErrosValidacao = errosValidacao;
            return retorno;
        }

        public RetornoComMensagem Editar(T objetoEditado)
        {
            RetornoComMensagem retorno = new RetornoComMensagem();
            Dictionary<string, string> errosValidacao;
            retorno.Retorno = GerenciadorBase.Editar(objetoEditado, out errosValidacao);
            retorno.ErrosValidacao = errosValidacao;
            return retorno;
        }

        public RetornoComMensagem Excluir(int id)
        {
            RetornoComMensagem retorno = new RetornoComMensagem();
            Dictionary<string, string> errosValidacao;
            retorno.Retorno = GerenciadorBase.Excluir(id, out errosValidacao);
            retorno.ErrosValidacao = errosValidacao;
            return retorno;
        }

        public RetornoComMensagem ExcluirVarios(IList<int> id)
        {
            RetornoComMensagem retorno = new RetornoComMensagem();
            Dictionary<string, string> errosValidacao;
            retorno.Retorno = GerenciadorBase.Excluir(id, out errosValidacao);
            retorno.ErrosValidacao = errosValidacao;
            return retorno;
        }
    }
}
