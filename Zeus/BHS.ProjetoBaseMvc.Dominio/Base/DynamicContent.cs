using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace Client.Zeus.Domain.Base
{
    public class DynamicContent
    {
        public DynamicContent(string referenceId = "", string content = "")
            : this(referenceId, content, enumDynamicContentType.CustomizedParcial)
        {

        }

        public DynamicContent(string referenceId = "", string content = "", enumDynamicContentType contentType = enumDynamicContentType.CustomizedParcial, 
            enumIncludedReference includedReference = enumIncludedReference.After, string property = null, string cssDiv = "")
        {
            ReferenceId = referenceId;
            Content = content;
            ContentType = contentType;
            IncludedReference = includedReference;
            Property = property;
            CssDiv = cssDiv;
        }

        public string ReferenceId { get; set; }

        public string Content { get; set; }

        public enumDynamicContentType ContentType { get; set; }

        public enumIncludedReference IncludedReference { get; set; }

        public string Property { get; set; }

        public string CssDiv { get; set; }

        public override string ToString()
        {
            return Content;
        }
    }
}