namespace OWRM_Work_Routine_Manager.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TAREFA")]
    public partial class TAREFA
    {
        [Key]
        public int ID_TAREFA { get; set; }

        [StringLength(500)]
        public string TITULO { get; set; }

        [StringLength(500)]
        public string DESCRICAO { get; set; }

        public int? ID_USUARIO { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DATA_INICIO { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DATA_FIM { get; set; }

        public virtual USUARIO USUARIO { get; set; }
    }
}
