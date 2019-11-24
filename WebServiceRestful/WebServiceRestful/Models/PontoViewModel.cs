using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OWRM_Work_Routine_Manager.Models
{
    public class PontoViewModel
    {
        public DateTime? PONTO_ENTRADA { get; set; }
        public DateTime? PONTO_ENTRADA_INTERVALO { get; set; }
        public DateTime? PONTO_SAIDA_INTERVALO { get; set; }
        public DateTime? PONTO_SAIDA { get; set; }

    }
}