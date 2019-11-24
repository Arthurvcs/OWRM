namespace OWRM_Work_Routine_Manager.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PONTO")]
    public partial class PONTO
    {
        [Key]
        public int ID_PONTO { get; set; }

        public int? ID_USUARIO { get; set; }

        [Column(TypeName = "date")]
        public DateTime? PONTO_ENTRADA { get; set; }

        [Column(TypeName = "date")]
        public DateTime? PONTO_ENTRADA_INTERVALO { get; set; }

        [Column(TypeName = "date")]
        public DateTime? PONTO_SAIDA_INTERVALO { get; set; }

        [Column(TypeName = "date")]
        public DateTime? PONTO_SAIDA { get; set; }

        public virtual USUARIO USUARIO { get; set; }
    }
}
