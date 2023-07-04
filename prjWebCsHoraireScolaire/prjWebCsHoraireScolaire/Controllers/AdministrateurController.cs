using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjWebCsHoraireScolaire.Controllers
{
    public class AdministrateurController : Controller
    {
        // GET: Administrateur
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult AutherizeAdmin(prjWebCsHoraireScolaire.Models.Entities.Admin userModel)
        {
            using (prjWebCsHoraireScolaire.Models.HoraireScolaireEntities db = new prjWebCsHoraireScolaire.Models.HoraireScolaireEntities())
            {
                var userDetails = db.Administrateurs.Where(x => x.Email == userModel.Email && x.mdp == userModel.mdp).FirstOrDefault();
                //On verifie si l'utilisateur existe
                if (userDetails == null)
                {
                    userModel.LoginErrorMessage = "Mauvais Email et/ou mot de passe ";
                    return View("Index", userModel);
                }
                else
                {
                    Session["AdminId"] = userModel.Id;
                    return RedirectToAction("CreateCours");
                }
            }
        }

        public ActionResult CreateCours()
        {
            return View();

        }

        [HttpGet]
        public ActionResult ListeCours()
        {
            using (prjWebCsHoraireScolaire.Models.HoraireScolaireEntities db = new Models.HoraireScolaireEntities())
            {
                var ListeCours = db.Cours.ToList();
                return View(ListeCours);
            }
        }

        [HttpPost]
        public ActionResult CreateCours(prjWebCsHoraireScolaire.Models.Entities.ModCours modCours)
        {
            using (prjWebCsHoraireScolaire.Models.HoraireScolaireEntities db = new Models.HoraireScolaireEntities())
            {
                Models.Cour newCours = new Models.Cour
                {
                    Titre = modCours.Titre,
                    Description = modCours.Description
                };

                db.Cours.Add(newCours);
                db.SaveChanges();
                modCours.ErrorMessage = "Cours enregistre avec success.";
                return View(modCours);
            }

        }
    }
}