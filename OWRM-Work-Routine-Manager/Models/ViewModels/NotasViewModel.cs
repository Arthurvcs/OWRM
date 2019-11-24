using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OWRM_Work_Routine_Manager.Models
{
    public class NotasViewModel
    {
        public int ID_NOTA { get; set; }
        public string DESCRICAO { get; set; }
        public DateTime? DATA { get; set; }
        public string COR { get; set; }
        public string TITULO { get; set; }

    }
}