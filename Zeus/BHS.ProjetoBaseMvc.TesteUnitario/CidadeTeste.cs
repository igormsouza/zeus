using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BHS.ProjetoBaseMvc.Negocio;
using BHS.ProjetoBaseMvc.Dominio;
using System.Collections.Generic;
using BHS.ProjetoBaseMvc.Negocio.Gerenciador;

namespace BHS.ProjetoBaseMvc.TesteUnitario
{
    [TestClass]
    public class CidadeTeste
    {
        CidadeGerenciador cidadeGerenciador;
        CidadeGerenciador Gerenciador
        {
            get
            {
                if (cidadeGerenciador == null)
                    cidadeGerenciador = new CidadeGerenciador();
                return cidadeGerenciador;
            }
        }

        [TestMethod]
        public void CriarItem()
        {
            var item = new TB_CIDADE();
            item.NOME = "Belo Horizonte";
            item.ID_UF = 1; 

            Dictionary<string, string> msg;
            bool retorno = Gerenciador.Criar(item, out msg);

            Assert.AreEqual(true, retorno);
        }


        [TestMethod]
        public void ListarItens()
        {
            var itens = Gerenciador.Listar();
            bool retorno = itens.Count > 0;

            Assert.AreEqual(true, retorno);
        }
    }
}
