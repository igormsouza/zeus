using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BHS.ProjetoBaseMvc.App.Models.Base
{
    public class DominioCrudAutomatico
    {
        //ViewBag.Titulo = "Assinaturas do PagSeguro";
        //ViewBag.CamposPesquisa = new[] { "Id_Cliente", "Sucesso", "Data_Inicio_Transacao" };
        //ViewBag.CamposGrid = new[] { "TB_ASSINATURA.TB_CLIENTE.ID", "TB_ASSINATURA.TB_CLIENTE.NOME", "TRANSACAO", "SUCESSO", "STATUS_PAGSEGURO", "DATA_CRIACAO" };
        //ViewBag.Detalhe = new[] { "Assinatura_Pagseguro_Cobranca", "Assinatura_Pagseguro_Notificacao" };
        //ViewBag.DetalheTitulo = new[] { "Cobranças", "Histórico" };
        //ViewBag.Acoes = new[] { "Atualizar" };
        //ViewBag.ExibirNovo = true;

        //IncluirTabelas = "TB_ASSINATURA.TB_CLIENTE";

        public string Titulo { get; set; }

        private IList<string> camposPesquisa;
        public IList<string> CamposPesquisa
        {
            get
            {
                if (camposPesquisa == null)
                {
                    camposPesquisa = new List<string>();
                }
                return camposPesquisa;
            }
            set
            {
                camposPesquisa = value;
            }
        }

        private IList<string> camposGrid;
        public IList<string> CamposGrid
        {
            get
            {
                if (camposGrid == null)
                {
                    camposGrid = new List<string>();
                }
                return camposGrid;
            }
            set
            {
                camposGrid = value;
            }
        }

        public bool ExibirNovo { get; set; }

        public bool ExibirEditar { get; set; }

        public string IncluirTabelas { get; set; }

        private IList<string> acoes;
        public IList<string> Acoes
        {
            get
            {
                if (acoes == null)
                {
                    acoes = new List<string>();
                }
                return acoes;
            }
            set
            {
                acoes = value;
            }
        }

        private IList<string> detalhe;
        public IList<string> Detalhe
        {
            get
            {
                if (detalhe == null)
                {
                    detalhe = new List<string>();
                }
                return detalhe;
            }
            set
            {
                detalhe = value;
            }
        }

        private IList<string> detalheTitulo;
        public IList<string> DetalheTitulo
        {
            get
            {
                if (detalheTitulo == null)
                {
                    detalheTitulo = new List<string>();
                }
                return detalheTitulo;
            }
            set
            {
                detalheTitulo = value;
            }
        }
    }
}