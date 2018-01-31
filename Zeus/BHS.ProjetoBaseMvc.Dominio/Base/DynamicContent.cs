using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace Client.Zeus.Domain.Base
{
    public class DynamicContent
    {
        public DynamicContent(string idReferencia = "", string conteudo = "")
            : this(idReferencia, conteudo, enumDynamicContentType.CustomizedParcial)
        {

        }

        public DynamicContent(string idReferencia = "", string content = "", enumDynamicContentType tipoConteudo = enumDynamicContentType.CustomizedParcial, 
            enumIncludedReference inclusaoReferencia = enumIncludedReference.After, string property = null, string cssDiv = "")
        {
            IdReferencia = idReferencia;
            Content = content;
            ContentType = tipoConteudo;
            InclusaoReferencia = inclusaoReferencia;
            Property = property;
            CssDiv = cssDiv;
        }

        public string IdReferencia { get; set; }

        public string Content { get; set; }

        public enumDynamicContentType ContentType { get; set; }

        public enumIncludedReference InclusaoReferencia { get; set; }

        public string Property { get; set; }

        public string CssDiv { get; set; }

        public override string ToString()
        {
            return Content;
        }
    }
}