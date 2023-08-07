﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace prjWebCsHoraireScolaire.Models.Entities
{
    public class ModelViewHoraireProfesseur
    {
        public int HoraireID { get; set; }
        public DateTime Date { get; set; }
        public int HeureDebut { get; set; }
        public int HeureFin { get; set; }
        public int Professeur { get; set; }
        public string ProfesseurNom { get; set; }
        public string Classe { get; set; }
        public string Statut { get; set; }
    }
}