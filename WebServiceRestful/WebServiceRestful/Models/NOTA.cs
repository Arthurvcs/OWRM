namespace OWRM_Work_Routine_Manager.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NOTA")]
    public partial class NOTA
    {
        [Key]
        public int ID_NOTA { get; set; }

        [StringLength(500)]
        public string DESCRICAO { get; set; }

        public int? ID_USUARIO { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DATA { get; set; }

        [StringLength(25)]
        public string COR { get; set; }

        public virtual USUARIO USUARIO { get; set; }

        [StringLength(100)]
        public string TITULO { get; set; }
    }
}
