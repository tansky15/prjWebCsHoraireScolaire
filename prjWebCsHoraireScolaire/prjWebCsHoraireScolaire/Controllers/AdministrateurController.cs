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
            using (prjWebCsHoraireScolaire.Models.db_a9c67b_horairescolaireEntities db = new prjWebCsHoraireScolaire.Models.db_a9c67b_horairescolaireEntities())
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
            using (prjWebCsHoraireScolaire.Models.db_a9c67b_horairescolaireEntities db = new Models.db_a9c67b_horairescolaireEntities())
            {
                var ListeCours = db.Cours.ToList();
                return View(ListeCours);
            }
        }

        [HttpPost]
        public ActionResult CreateCours(prjWebCsHoraireScolaire.Models.Entities.ModCours modCours)
        {
            using (prjWebCsHoraireScolaire.Models.db_a9c67b_horairescolaireEntities db = new Models.db_a9c67b_horairescolaireEntities())
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
        
        //Gestion des professeurs 
        [HttpGet]
        public ActionResult GestionProfs(prjWebCsHoraireScolaire.Models.Professeur modProf)
        {
            using(prjWebCsHoraireScolaire.Models.db_a9c67b_horairescolaireEntities db = new Models.db_a9c67b_horairescolaireEntities())
            {
                var liste_des_profs = db.Professeurs.ToList();
                return View(liste_des_profs);
            }
            
        }

        public ActionResult AjouterProfesseur(prjWebCsHoraireScolaire.Models.Entities.Prof modProf)
        {
            if(modProf.Nom != null)
            {
                using (prjWebCsHoraireScolaire.Models.db_a9c67b_horairescolaireEntities db = new Models.db_a9c67b_horairescolaireEntities())
                {
                    Models.Professeur newProf = new Models.Professeur
                    {
                        Nom = modProf.Nom,
                        Prenom = modProf.Prenom,
                        Sexe = modProf.Sexe,
                        Email = modProf.Email
                    };

                    db.Professeurs.Add(newProf);
                    db.SaveChanges();

                    

                    modProf.ErrorMessage = "Professeur enregistre avec success.";
                    return View(modProf);
                }
            }
            else
            {
                modProf.ErrorMessage = "";
                return View(modProf);
            }
          
        }
       
        public ActionResult ListeAssignation()
        {
            using (Models.db_a9c67b_horairescolaireEntities db = new Models.db_a9c67b_horairescolaireEntities())
            {
                var ListHoraire = db.Horaires.ToList();
                return View(ListHoraire);
            }
        }
        public ActionResult ListeDocuments()
        {
            using (Models.db_a9c67b_horairescolaireEntities db = new Models.db_a9c67b_horairescolaireEntities())
            {
                var ListeDocuments = db.Documents.ToList();
                return View(ListeDocuments);
            }
        }

        public ActionResult EffacerProfesseur()
        {
            using (prjWebCsHoraireScolaire.Models.db_a9c67b_horairescolaireEntities db = new Models.db_a9c67b_horairescolaireEntities())
            {
                if (Url.RequestContext.RouteData.Values["id"] != null)
                {
                    int ProfesseurID = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"]);

                    prjWebCsHoraireScolaire.Models.Professeur Professeur = db.Professeurs.First(x => x.Id == ProfesseurID);

                    db.Professeurs.Remove(Professeur);
                    db.SaveChanges();
                }

                var liste_des_profs = db.Professeurs.ToList();
                return View("GestionProfs",liste_des_profs);
            }
        }
        
        //Gestion d'Etudiant
        [HttpGet]
        public ActionResult GestionEtudiant()
        {
            using (prjWebCsHoraireScolaire.Models.db_a9c67b_horairescolaireEntities db = new Models.db_a9c67b_horairescolaireEntities())
            {
                var liste_des_etudiant = db.Etudiants.ToList();
                return View(liste_des_etudiant);
            }
        }

        public ActionResult EffacerEtudiant()
        {
            using (prjWebCsHoraireScolaire.Models.db_a9c67b_horairescolaireEntities db = new Models.db_a9c67b_horairescolaireEntities())
            {
                if (Url.RequestContext.RouteData.Values["id"] != null)
                {
                    try
                    {

                        int EtudiantID = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"]);

                        prjWebCsHoraireScolaire.Models.Etudiant Etudiant = db.Etudiants.First(x => x.EtudiantID == EtudiantID);

                        db.Etudiants.Remove(Etudiant);
                        db.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }

                var liste_des_etudiants = db.Etudiants.ToList();
                return View("GestionEtudiant", liste_des_etudiants);
            }
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return View("Index");
        }

        public ActionResult AjouterEtudiant(prjWebCsHoraireScolaire.Models.Etudiant modEtudiant)
        {
                using (prjWebCsHoraireScolaire.Models.db_a9c67b_horairescolaireEntities db = new Models.db_a9c67b_horairescolaireEntities())
                {

                        if (modEtudiant.Nom != null)
                        {
                            Models.Etudiant newEtudiant = new Models.Etudiant
                            {
                                Nom = modEtudiant.Nom,
                                Prenom = modEtudiant.Prenom,
                                Email = modEtudiant.Email
                            };

                            db.Etudiants.Add(newEtudiant);
                            db.SaveChanges();
                            var ListeEt = db.Etudiants.ToList();
                                    return View("GestionEtudiant", ListeEt);
                                }
                                else
                                {
                                    return View();
                                }
            }
        }

        public ActionResult DeleteSchedule()
        {
            int HoraireID = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"]);

            using (Models.db_a9c67b_horairescolaireEntities db = new Models.db_a9c67b_horairescolaireEntities())
            {
                Models.Horaire HoraireAEfacer = db.Horaires.Where(s => s.HoraireID == HoraireID).FirstOrDefault();
                db.Horaires.Remove(HoraireAEfacer);
                db.SaveChanges();

                var ListHoraire = db.Horaires.ToList();
                return View("ListeAssignation", ListHoraire);
            }
        }

        public string TrouverNomProfesseurAvecID(int ProfID)
        {
            return "Marc";
        }

        public ActionResult AcceptSchedule()
        {
            using (Models.db_a9c67b_horairescolaireEntities db = new Models.db_a9c67b_horairescolaireEntities())
            {
                int HoraireID = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"]);
                prjWebCsHoraireScolaire.Models.Horaire Schedule = new Models.Horaire();
                Schedule = db.Horaires.Where(u => u.HoraireID == HoraireID).First();
                Schedule.statut = "Accépté";
                db.SaveChanges();
                var ListHoraire = db.Horaires.ToList();
                return View("ListeAssignation", ListHoraire);
            }

        }
    }
}