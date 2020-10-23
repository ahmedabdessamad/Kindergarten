using CloudKids.Data;
using CloudKids.Data.Infrastructure;
using CloudKids.Domain.Entities;
using CloudKids.Service;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CloudKids.Web.Controllers
{
    public class ReputationController : Controller
    {



        CloudKidsContext db = new CloudKidsContext();
        IDBFactory dbf;
        IUnitOfWork uow;
        IService<Reputation> serviceRep;

        public ReputationController()
        {
            dbf = new DBFactory();
            uow = new UnitOfWork(dbf);
            serviceRep = new Service<Reputation>(uow);
        }
        // GET: Reputation
        public ActionResult Index(string search)
        {
            if (search == null)
                return View(serviceRep.GetAll());
            else
                return View(serviceRep.GetAll().Where(x => x.Type.ToString().Contains(search) || search == null));
        }

        // GET: Reputation/Details/5
        public ActionResult Details(int id)
        {
            return View(serviceRep.GetById(id));
        }

        // GET: Reputation/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Reputation/Create
        [HttpPost]
        public ActionResult Create(Reputation p)
        {
            try
            {

                serviceRep.Add(p);
                serviceRep.Commit();
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Reputation/Edit/5
        public ActionResult Edit(int id)
        {
            var rep = serviceRep.GetById(id);
            return View(rep);
        }

        // POST: Reputation/Edit/5
        [HttpPost]
        public ActionResult Edit(Reputation R)  
        {
            if (ModelState.IsValid)

            {
               var Rep =  db.Reputations.Single(c => c.Id == R.Id);
                Rep.Description = R.Description;
                Rep.Prix = R.Prix;
                Rep.Type = R.Type;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
              
                // TODO: Add update logic here

                return RedirectToAction("Index");
            
              return View();
            
        }

        // GET: Reputation/Delete/5
        public ActionResult Delete(int id)
        {
            return View(serviceRep.GetById(id));
        }

        // POST: Reputation/Delete/5
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