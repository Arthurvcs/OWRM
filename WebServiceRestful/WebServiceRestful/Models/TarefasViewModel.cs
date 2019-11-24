using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OWRM_Work_Routine_Manager.Models
{
    public class TarefasViewModel
    {
        public int ID_TAREFA { get; set; }
        public string DESCRICAO { get; set; }
        public string TITULO { get; set; }
        public DateTime? DATA_INICIO { get; set; }
        public DateTime? DATA_FIM { get; set; }
    }
}