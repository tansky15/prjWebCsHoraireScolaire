using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace prjWebCsHoraireScolaire.Models.Entities
{
    public class Admin:Administrateur
    {
        public string LoginErrorMessage { get; set; }
    }
}