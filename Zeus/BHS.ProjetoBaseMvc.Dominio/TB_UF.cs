//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BHS.ProjetoBaseMvc.Dominio
{
    using System;
    using System.Collections.Generic;
    
    							   
    using System.Collections.ObjectModel;
    using System.Runtime.Serialization;	 
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using BHS.ProjetoBaseMvc.Dominio.Base;
    using BHS.ProjetoBaseMvc.Util.Attributes;
    
    [MetadataType(typeof(TB_UFMetaData))]
    [DataContract]
    public partial class TB_UF : DominioBase
    {
        public TB_UF()
        {
            this.TB_CIDADE = new HashSet<TB_CIDADE>();
        }
    
        
        [StringLength(50, ErrorMessage="Este campo não pode ser maior que 50 caracteres")]
        [Required(ErrorMessage = "Campo obrigatório")]
    	[DataMember]
        public string NOME { get; set; }
        
        [StringLength(2, ErrorMessage="Este campo não pode ser maior que 2 caracteres")]
        [Required(ErrorMessage = "Campo obrigatório")]
    	[DataMember]
        public string SIGLA { get; set; }
    
    	[DataMember]
        public virtual ICollection<TB_CIDADE> TB_CIDADE { get; set; }
    }
    
    /// <summary>
    /// Classe opcional para configurar alguma validação não padrão.
    /// </summary>
    public partial class TB_UFMetaData 
    {
    }
}
