﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BHS.ProjetoBaseMvc.Dominio.Base
{
    public class DynamicContent
    {
        public DynamicContent(string idReferencia = "", string conteudo = "")
            : this(idReferencia, conteudo, enumTipoConteudoDinamico.CustomizedParcial)
        {

        }

        public DynamicContent(string idReferencia = "", string conteudo = "", enumTipoConteudoDinamico tipoConteudo = enumTipoConteudoDinamico.CustomizedParcial, 
            enumInclusaoReferencia inclusaoReferencia = enumInclusaoReferencia.After, string propriedade = null, string cssDiv = "")
        {
            IdReferencia = idReferencia;
            Conteudo = conteudo;
            TipoConteudo = tipoConteudo;
            InclusaoReferencia = inclusaoReferencia;
            Propriedade = propriedade;
            CssDiv = cssDiv;
        }

        public string IdReferencia { get; set; }

        public string Conteudo { get; set; }

        public enumTipoConteudoDinamico TipoConteudo { get; set; }

        public enumInclusaoReferencia InclusaoReferencia { get; set; }

        public string Propriedade { get; set; }

        public string CssDiv { get; set; }

        public override string ToString()
        {
            return Conteudo;
        }
    }
}