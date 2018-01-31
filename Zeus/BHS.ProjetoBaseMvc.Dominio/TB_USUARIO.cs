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
    
    [MetadataType(typeof(TB_USUARIOMetaData))]
    [DataContract]
    public partial class TB_USUARIO : BaseDomain
    {
        public TB_USUARIO()
        {
            this.TB_PERFIL = new HashSet<TB_PERFIL>();
        }
    
        
        [StringLength(50, ErrorMessage="Este campo não pode ser maior que 50 caracteres")]
        [Required(ErrorMessage = "Campo obrigatório")]
    	[DataMember]
        public string NOME { get; set; }
      
    	[RegularExpression(@"([0-9]{2}[\.]?[0-9]{3}[\.]?[0-9]{3}[\/]?[0-9]{4}[-]?[0-9]{2})|([0-9]{3}[\.]?[0-9]{3}[\.]?[0-9]{3}[-]?[0-9]{2})", ErrorMessage = "CPF inválido")]
        
        [StringLength(14, ErrorMessage="Este campo não pode ser maior que 14 caracteres")]
    	[DataMember]
        public string CPF { get; set; }
    	[DataMember]
        public Nullable<int> ID_CIDADE { get; set; }
        
        [StringLength(256, ErrorMessage="Este campo não pode ser maior que 256 caracteres")]
        [Required(ErrorMessage = "Campo obrigatório")]
    	[DataMember]
        public string SENHA { get; set; }
        
        [EmailAddress(ErrorMessage = "E-mail inválido")]
        
        [StringLength(128, ErrorMessage="Este campo não pode ser maior que 128 caracteres")]
        [Required(ErrorMessage = "Campo obrigatório")]
    	[DataMember]
        public string EMAIL { get; set; }
        
        [StringLength(20, ErrorMessage="Este campo não pode ser maior que 20 caracteres")]
        [Required(ErrorMessage = "Campo obrigatório")]
    	[DataMember]
        public string LOGIN { get; set; }
    
    	[DataMember]
        public virtual TB_CIDADE TB_CIDADE { get; set; }
    	[DataMember]
        public virtual ICollection<TB_PERFIL> TB_PERFIL { get; set; }
    }
    
    /// <summary>
    /// Classe opcional para configurar alguma validação não padrão.
    /// </summary>
    public partial class TB_USUARIOMetaData 
    {
    }
}
