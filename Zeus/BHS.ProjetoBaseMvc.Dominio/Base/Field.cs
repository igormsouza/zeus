﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHS.ProjetoBaseMvc.Dominio.Base
{
    public class Field
    {
        public Field(string nome)
            : this(nome, "", enumTipoCampo.Default)
        {

        }

        public Field(string nome, string linkControllerGrid, string linkActionGrid)
            : this(nome, "", enumTipoCampo.Default, linkControllerGrid: linkControllerGrid, linkActionGrid: linkActionGrid)
        {

        }

        public Field(string nome = "", string cssClass = "", enumTipoCampo tipo = enumTipoCampo.Default, 
            string valorFixo = "", string linkActionGrid = "", string linkControllerGrid = "", string largura = "6", bool ativo = true)
        {
            Nome = nome;
            CssClass = cssClass;
            Tipo = tipo;
            ValorFixo = valorFixo;
            LinkActionGrid = linkActionGrid;
            LinkControllerGrid = linkControllerGrid;
            Ativo = ativo;
            Largura = largura;
        }

        public string Nome { get; set; }

        public string LinkActionGrid { get; set; }

        public string LinkControllerGrid { get; set; }

        public string ValorFixo { get; set; }

        public string CssClass { get; set; }

        public enumTipoCampo Tipo { get; set; }

        public string CarregarControle { get; set; }

        public string Largura { get; set; }

        public bool Ativo { get; set; }

        public override string ToString()
        {
            return Nome;
        }
    }
}