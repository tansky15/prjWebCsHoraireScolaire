using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjWebCsHoraireScolaire.Controllers
{
    public class ProfesseurController : Controller
    {

        // GET: Professeur
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AutherizeProf(prjWebCsHoraireScolaire.Models.Entities.Prof userModel)
        {
            using (prjWebCsHoraireScolaire.Models.db_a9c67b_horairescolaireEntities db = new prjWebCsHoraireScolaire.Models.db_a9c67b_horairescolaireEntities())
            {

                var userDetails = db.Professeurs.Where(x => x.Email == userModel.Email && x.mdp == userModel.mdp).FirstOrDefault();
                //On verifie si l'utilisateur existe
                if (userDetails == null)
                {
                    userModel.ErrorMessage = "Mauvais Email et/ou mot de passe ";
                    return View("Index", userModel);
                }
                else
                {
                    Session["ProfId"] = userDetails.Id;
                    return View("dashboard");
                }
            }
        }
        
        public ActionResult Dashboard()
        {
            return View();
           
        }

        public ActionResult AddNotes()
        {
            return View();
        }
        public ActionResult MonHoraire()
        {
            int ProfId =  Convert.ToInt32(Session["ProfId"].ToString());

            using (Models.db_a9c67b_horairescolaireEntities db = new Models.db_a9c67b_horairescolaireEntities())
            {
                var ListHoraire = db.Horaires.Where(s => s.Professeur == ProfId).ToList();
                return View(ListHoraire);
            }
        }

        public ActionResult CreerHoraire()
        {
            return View();
        }
        public ActionResult EfacerHoraire()
        {
            int HoraireID = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"]);
            int ProfId = Convert.ToInt32(Session["ProfId"].ToString());

            using (Models.db_a9c67b_horairescolaireEntities db = new Models.db_a9c67b_horairescolaireEntities())
            {
                Models.Horaire HoraireAEfacer = db.Horaires.Where(s => s.HoraireID == HoraireID).FirstOrDefault();
                db.Horaires.Remove(HoraireAEfacer);
                db.SaveChanges();

                var ListHoraire = db.Horaires.Where(s => s.Professeur == ProfId).ToList();
                return View("MonHoraire", ListHoraire);
            }
        }
    }
}