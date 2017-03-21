using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BHS.ProjetoBaseMvc.Negocio;
using BHS.ProjetoBaseMvc.Dominio;
using System.Collections.Generic;
using BHS.ProjetoBaseMvc.Negocio.Gerenciador;

namespace BHS.ProjetoBaseMvc.TesteUnitario
{
    [TestClass]
    public class UFTeste
    {
        UFGerenciador uFGerenciador;
        UFGerenciador Gerenciador
        {
            get
            {
                if (uFGerenciador == null)
                    uFGerenciador = new UFGerenciador();
                return uFGerenciador;
            }
        }

        [TestMethod]
        public void CriarItem()
        {
            var item = new TB_UF();
            item.NOME = "Minas Gerais";
            item.SIGLA = "MG";

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
