using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using StudyDirectionDbWebApp.Models;
using StudyDirectionDbWebApp.Utility;

namespace StudyDirectionDbWebApp.Controllers
{
    public class SpecialitiesController : Controller
    {
        private StudyDirectionDbEntities1 db = new StudyDirectionDbEntities1();

        protected override void Initialize(RequestContext context)
        {
            base.Initialize(context);

            db.Database.Log = DatabaseLogger.Log;

            ViewBag.Controller = "Специальности";
        }

        // GET: Specialities
        public async Task<ActionResult> Index()
        {
            var specialities = db.Specialities.Include(s => s.Chair);
            return View(await specialities.ToListAsync());
        }

        // GET: Specialities/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Speciality speciality = await db.Specialities.FirstAsync(x => x.SpecialityId == id);
            if (speciality == null)
            {
                return HttpNotFound();
            }
            return View(speciality);
        }

        // GET: Specialities/Create
        public ActionResult Create()
        {
            ViewBag.ChairId = new SelectList(db.Chairs, "ChairId", "ChairName");
            return View();
        }

        // POST: Specialities/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "FacultyId,ChairId,SpecialityId,SpecialityName")] Speciality speciality)
        {
            if (ModelState.IsValid)
            {
                db.Specialities.Add(speciality);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ChairId = new SelectList(db.Chairs, "ChairId", "ChairName", speciality.FacultyId);
            return View(speciality);
        }

        // GET: Specialities/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Speciality speciality = await db.Specialities.FirstAsync(x => x.SpecialityId == id);
            if (speciality == null)
            {
                return HttpNotFound();
            }
            return View(speciality);
        }

        // POST: Specialities/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "FacultyId,ChairId,SpecialityId,SpecialityName")] Speciality speciality)
        {
            if (ModelState.IsValid)
            {
                db.Entry(speciality).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(speciality);
        }

        // GET: Specialities/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Speciality speciality = await db.Specialities.FirstAsync(x => x.SpecialityId == id);
            if (speciality == null)
            {
                return HttpNotFound();
            }
            return View(speciality);
        }

        // POST: Specialities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Speciality speciality = await db.Specialities.FirstAsync(x => x.SpecialityId == id);
            db.Specialities.Remove(speciality);
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
