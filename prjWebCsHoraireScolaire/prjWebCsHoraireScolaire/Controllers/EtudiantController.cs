using prjWebCsHoraireScolaire.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjWebCsHoraireScolaire.Controllers
{
    public class EtudiantController : Controller
    {
        // GET: Etudiant
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Dashboard()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AutherizeProf(prjWebCsHoraireScolaire.Models.Etudiant userModel)
        {
            using (prjWebCsHoraireScolaire.Models.db_a9c67b_horairescolaireEntities db = new prjWebCsHoraireScolaire.Models.db_a9c67b_horairescolaireEntities())
            {

                var userDetails = db.Etudiants.Where(x => x.Email == userModel.Email && x.Mdp == userModel.Mdp).FirstOrDefault();
                //On verifie si l'utilisateur existe
                if (userDetails == null)
                {
                    //userModel.ErrorMessage = "Mauvais Email et/ou mot de passe ";
                    return View("Index", userModel);
                }
                else
                {
                    Session["EtudiantID"] = userDetails.EtudiantID;
                    ViewBag.EtudiantID = userDetails.EtudiantID;
                    Session["Nom"] = userDetails.Nom + userDetails.Prenom;
                    return View("dashboard");
                }
            }
        }


        public ActionResult DepotDocument()
        {
            return View();
        }


    // Action pour traiter le formulaire d'ajout de document
    [HttpPost]
    public ActionResult DepotDocument(prjWebCsHoraireScolaire.Models.DocumentViewModel model, HttpPostedFileBase file)
    {
        if (ModelState.IsValid && file != null && file.ContentLength > 0)
        {
            // Enregistrement du fichier dans un dossier sur le serveur
            string cheminPhysique = Path.Combine(Server.MapPath("~/Documents"), Path.GetFileName(file.FileName));
            string NomDocument = Path.GetFileName(file.FileName);

            file.SaveAs(cheminPhysique);

            // Insertion des informations du document dans la base de données
            using (prjWebCsHoraireScolaire.Models.db_a9c67b_horairescolaireEntities  contexte = new prjWebCsHoraireScolaire.Models.db_a9c67b_horairescolaireEntities()) // Assurez-vous d'utiliser le bon contexte
            {
                    Document document = new Document
                    {
                        UTILISATEUR = Convert.ToInt16(Session["EtudiantID"].ToString()),
                    TITRE = model.TITRE,
                    DATE_TIME_PUBLICATION = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    CHEMIN = NomDocument
                    };

                contexte.Documents.Add(document);
                contexte.SaveChanges();
            }

            ViewBag.Message = "Le document a été ajouté avec succès.";
        }

        return View(model);
    }
}
}