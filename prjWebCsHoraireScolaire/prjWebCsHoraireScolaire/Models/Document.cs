//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace prjWebCsHoraireScolaire.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Document
    {
        public int DOCUMENT_ID { get; set; }
        public string TITRE { get; set; }
        public string DATE_TIME_PUBLICATION { get; set; }
        public string CHEMIN { get; set; }
        public Nullable<int> UTILISATEUR { get; set; }
    }
}