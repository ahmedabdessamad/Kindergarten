using CloudKids.Data;
using CloudKids.Data.Infrastructure;
using CloudKids.Domain.Entities;
using CloudKids.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CloudKids.Web.Controllers
{
    public class ReclamationController : Controller
    {





        CloudKidsContext db = new CloudKidsContext();
        IDBFactory dbf;
        IUnitOfWork uow;
        IService<Reclamation> serviceRep;

        public ReclamationController()
        {
            dbf = new DBFactory();
            uow = new UnitOfWork(dbf);
            serviceRep = new Service<Reclamation>(uow);
        }




        // GET: Reclamation
        public ActionResult Index(string search)
        {
            if (search == null)
                return View(serviceRep.GetAll());
            else
                return View(serviceRep.GetAll().Where(x => x.Motif.Contains(search) || search == null));
        }

        // GET: Reclamation/Details/5
        public ActionResult Details(int id)
        {
            return View(serviceRep.GetById(id));
        }

        // GET: Reclamation/Create
        public ActionResult Create()
        {
          
           
            return View();
        }

        // POST: Reclamation/Create
        [HttpPost]
        public ActionResult Create(Reclamation R)
        {
            try
            {
                serviceRep.Add(R);
                serviceRep.Commit();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Reclamation/Edit/5
        public ActionResult Edit(int id)
        {
            var rep = serviceRep.GetById(id);
            return View(rep);
        }

        // POST: Reclamation/Edit/5
        [HttpPost]
        public ActionResult Edit(Reclamation reclamation)
        {
            try
            {

                var Rec = db.Reclamations.Single(c => c.Id == reclamation.Id);

                Rec.Motif = reclamation.Motif;  
                    Rec.ProfilJardinId = reclamation.ProfilJardinId;
                db.SaveChanges();
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Reclamation/Delete/5
        public ActionResult Delete(int id)
        {
            return View(serviceRep.GetById(id));
        }

        // POST: Reclamation/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {

                serviceRep.Delete(serviceRep.GetById(id));
                serviceRep.Commit();

                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
