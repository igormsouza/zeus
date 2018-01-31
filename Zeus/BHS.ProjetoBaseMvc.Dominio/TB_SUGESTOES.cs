//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Client.Zeus.Domain
{
    using System;
    using System.Collections.Generic;
    
    							   
    using System.Collections.ObjectModel;
    using System.Runtime.Serialization;	 
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using Client.Zeus.Domain.Base;
    using Client.Zeus.Util.Attributes;
    
    [MetadataType(typeof(TB_SUGESTOESMetaData))]
    [DataContract]
    public partial class TB_SUGESTOES : BaseDomain
    {
        [Required(ErrorMessage = "Campo obrigatório")]
    	[DataMember]
        public int NOTA { get; set; }
        
        [StringLength(1000, ErrorMessage="Este campo não pode ser maior que 1000 caracteres")]
    	[DataMember]
        public string OBSERVACAO { get; set; }
        
        [StringLength(100, ErrorMessage="Este campo não pode ser maior que 100 caracteres")]
        [Required(ErrorMessage = "Campo obrigatório")]
    	[DataMember]
        public string USUARIO { get; set; }
    }
    
    /// <summary>
    /// Classe opcional para configurar alguma validação não padrão.
    /// </summary>
    public partial class TB_SUGESTOESMetaData 
    {
    }
}
