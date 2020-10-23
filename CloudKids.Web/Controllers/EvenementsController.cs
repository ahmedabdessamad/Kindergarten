using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using CloudKids.Data;
using CloudKids.Domain.Entities;

namespace CloudKids.Web.Controllers
{
    public class EvenementsController : Controller
    {
        private CloudKidsContext db = new CloudKidsContext();

        // GET: Evenements
        public async Task<ActionResult> Index()
        {
            return View(await db.Evenements.ToListAsync());
        }

        public JsonResult GetEvent()
        {
                var events = db.Evenements.ToList();
                return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        // GET: Evenements/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evenement evenement = await db.Evenements.FindAsync(id);
            if (evenement == null)
            {
                return HttpNotFound();
            }
            return View(evenement);
        }

        // GET: Evenements/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Evenements/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Nom,Host,Description,Start,End,Lieu,Tel,EmailContact,file")] Evenement evenement)
        {
            if (ModelState.IsValid)
            {
                string filename = Path.GetFileNameWithoutExtension(evenement.file.FileName);
                string extension = Path.GetExtension(evenement.file.FileName);
                filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                evenement.image= "/UploadedEvenement/" + filename;
                filename = Path.Combine(Server.MapPath("/UploadedEvenement/"), filename);
                evenement.file.SaveAs(filename);
                evenement.Start = DateTime.UtcNow;
                evenement.End = DateTime.UtcNow;
                db.Evenements.Add(evenement);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(evenement);
        }

        // GET: Evenements/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evenement evenement = await db.Evenements.FindAsync(id);
            if (evenement == null)
            {
                return HttpNotFound();
            }
            return View(evenement);
        }

        // POST: Evenements/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Host,Description,Start,End,Lieu,Tel,EmailContact")] Evenement evenement)
        {
            if (ModelState.IsValid)
            {
                db.Entry(evenement).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(evenement);
        }

        // GET: Evenements/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evenement evenement = await db.Evenements.FindAsync(id);
            if (evenement == null)
            {
                return HttpNotFound();
            }
            return View(evenement);
        }

        // POST: Evenements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Evenement evenement = await db.Evenements.FindAsync(id);
            db.Evenements.Remove(evenement);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
