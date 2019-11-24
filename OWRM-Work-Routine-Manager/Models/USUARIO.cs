namespace OWRM_Work_Routine_Manager.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("USUARIO")]
    public partial class USUARIO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public USUARIO()
        {
            NOTA = new HashSet<NOTA>();
            PONTO = new HashSet<PONTO>();
            TAREFA = new HashSet<TAREFA>();
        }

        [Key]
        public int ID_USUARIO { get; set; }

        [StringLength(500)]
        public string NOME { get; set; }

        [StringLength(100)]
        public string LOGIN { get; set; }

        [StringLength(100)]
        public string SENHA { get; set; }

        [StringLength(50)]
        public string MATRICULA { get; set; }

        public int? ID_ROLE { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NOTA> NOTA { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PONTO> PONTO { get; set; }

        public virtual ROLE ROLE { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TAREFA> TAREFA { get; set; }
    }
}
