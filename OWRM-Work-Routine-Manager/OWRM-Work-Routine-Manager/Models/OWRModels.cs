namespace OWRM_Work_Routine_Manager.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class OWRModels : DbContext
    {
        public OWRModels()
            : base("name=OWRModelsH")
        {
        }

        public virtual DbSet<NOTA> NOTA { get; set; }
        public virtual DbSet<PONTO> PONTO { get; set; }
        public virtual DbSet<ROLE> ROLE { get; set; }
        public virtual DbSet<TAREFA> TAREFA { get; set; }
        public virtual DbSet<USUARIO> USUARIO { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NOTA>()
                .Property(e => e.DESCRICAO)
                .IsUnicode(false);

            modelBuilder.Entity<NOTA>()
                .Property(e => e.COR)
                .IsUnicode(false);


            modelBuilder.Entity<NOTA>()
                .Property(e => e.TITULO)
                .IsUnicode(false);

            modelBuilder.Entity<ROLE>()
                .Property(e => e.DESCRICAO)
                .IsUnicode(false);

            modelBuilder.Entity<TAREFA>()
                .Property(e => e.DESCRICAO)
                .IsUnicode(false);

            modelBuilder.Entity<TAREFA>()
                .Property(e => e.TITULO)
                .IsUnicode(false);

            modelBuilder.Entity<USUARIO>()
                .Property(e => e.NOME)
                .IsUnicode(false);

            modelBuilder.Entity<USUARIO>()
                .Property(e => e.LOGIN)
                .IsUnicode(false);

            modelBuilder.Entity<USUARIO>()
                .Property(e => e.SENHA)
                .IsUnicode(false);

            modelBuilder.Entity<USUARIO>()
                .Property(e => e.MATRICULA)
                .IsUnicode(false);
        }
    }
}
