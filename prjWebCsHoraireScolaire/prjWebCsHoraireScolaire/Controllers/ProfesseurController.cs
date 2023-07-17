using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjWebCsHoraireScolaire.Controllers
{
    public class ProfesseurController : Controller
    {


        //public static prjWebCsHoraireScolaire.Models.Horaire horaireModel;
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
                    //string SerializeUser = JsonConvert.SerializeObject(userDetails);
                    //Session["UserJson"] = SerializeUser;

                    //using (db_iisprotect db1 = new db_iisprotect())
                    //{
                    //    var ListInstitutions = db1.institutions.ToList();
                    //    return View("dashboard", ListInstitutions);
                    //}
                    Session["ProfId"] = userModel.Id;
                    return View("dashboard");
                }
            }
        }

        
        public ActionResult Dashboard()
        {
            using (Models.db_a9c67b_horairescolaireEntities db = new Models.db_a9c67b_horairescolaireEntities())
            {
                int ProId = Convert.ToInt32(Session["ProfId"]);
                var monhoraire = db.Horaires.ToList();
                return View(monhoraire);
            }
           
        }

        public ActionResult AddNotes()
        {
            return View();
        }
    }
}