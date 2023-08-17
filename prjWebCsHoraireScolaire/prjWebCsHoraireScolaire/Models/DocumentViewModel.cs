using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace prjWebCsHoraireScolaire.Models
{
    public class DocumentViewModel
    {
        [Required]
        [Display(Name = "Titre du document")]
        public string TITRE { get; set; }
    }
}