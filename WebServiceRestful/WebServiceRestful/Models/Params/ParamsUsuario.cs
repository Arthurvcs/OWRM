using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebServiceRestful.Models
{
    public class ParamsUsuario
    {
        public int idUsuario { get; set; }
        public string nome { get; set; }
        public string matricula { get; set; }
        public string login { get; set; }
        public string senha { get; set; }

    }
}