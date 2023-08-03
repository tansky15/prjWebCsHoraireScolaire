using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
                    ViewBag.ProfID = userDetails.Id;
                    Session["Nom"] = userDetails.Nom + userDetails.Prenom;
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
            int ProfId = Convert.ToInt32(Session["ProfId"].ToString());

            using (Models.db_a9c67b_horairescolaireEntities db = new Models.db_a9c67b_horairescolaireEntities())
            {
                var ListHoraire = db.Horaires.Where(s => s.Professeur == ProfId).ToList();
                return View(ListHoraire);
            }
        }

        [HttpGet]
        public ActionResult CreerHoraire()
        {
            return View();
        }


        public ActionResult CreerHoraire(prjWebCsHoraireScolaire.Models.Horaire modHoraire)
        {

            if (ModelState.IsValid)
            {
                // Appel de la procédure stockée PROC_CREATE_SCHEDULE pour insérer le nouvel horaire
                using (Models.db_a9c67b_horairescolaireEntities db = new Models.db_a9c67b_horairescolaireEntities())
                {
                    DateTime Date = Convert.ToDateTime(modHoraire.Date);
                    int HeureDebut = Convert.ToInt32(modHoraire.HeureDebut);
                    int HeureFin = Convert.ToInt32(modHoraire.HeureFin);
                    int Classe = Convert.ToInt32(modHoraire.Classe);
                    int Professeur = Convert.ToInt32(Session["ProfId"]);



                    var erreurMessageParam = new SqlParameter("ErreurMessage", SqlDbType.NVarChar, -1);
                    erreurMessageParam.Direction = ParameterDirection.Output;

                    var ConflitCountMessageParam = new SqlParameter("ConflitCountMessage", SqlDbType.NVarChar, -1);
                    ConflitCountMessageParam.Direction = ParameterDirection.Output;


                    var result = db.Database.ExecuteSqlCommand("EXEC [dbo].[PROC_CREATE_SCHEDULE] @ParmDatetime, @ParmHeureDebut, @ParmHeureFin, @ParmClasse, @ParmProfesseur,@ErreurMessage OUT,@ConflitCountMessage OUT",
                                                                new SqlParameter("ParmDatetime", Date),
                                                                new SqlParameter("ParmHeureDebut", HeureDebut),
                                                                new SqlParameter("ParmHeureFin", HeureFin),
                                                                new SqlParameter("ParmClasse", Classe),
                                                                new SqlParameter("ParmProfesseur", Professeur),
                                                                erreurMessageParam,
                                                                ConflitCountMessageParam);
                    string erreurMessage = erreurMessageParam.Value.ToString();
                    ViewBag.ErreurMessage = erreurMessage;

                    string ConflitCountMessage = ConflitCountMessageParam.Value.ToString();
                    bool IsConflitExist = Convert.ToInt32(ConflitCountMessage) > 0;

                    if (Convert.ToInt32(ConflitCountMessage) == 0)
                    {
                            var ListHoraire = db.Horaires.Where(s => s.Professeur == Professeur).ToList();
                            return View("MonHoraire", ListHoraire);
                    }
                    else
                    {
                        return View();
                    }
                }

               
               
            }
          
            ViewBag.ErrorMessage = "Erreur inconnu";
            return View();
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return View("Index");
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