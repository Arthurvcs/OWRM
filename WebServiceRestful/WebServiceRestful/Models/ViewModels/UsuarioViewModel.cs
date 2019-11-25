using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebServiceRestful.Models
{
    public class UsuarioViewModel
    {
        public int ID_USUARIO { get; set; }
        public string NOME { get; set; }
        public string MATRICULA { get; set; }
        public string LOGIN { get; set; }
        public string ROLE { get; set; }

    }
}