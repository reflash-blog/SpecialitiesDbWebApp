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
    public class StudyDirectionsController : Controller
    {
        private StudyDirectionDbEntities1 db = new StudyDirectionDbEntities1();

        protected override void Initialize(RequestContext context)
        {
            base.Initialize(context);

            db.Database.Log = DatabaseLogger.Log;

            ViewBag.Controller = "Направления подготовки";
        }

        // GET: StudyDirections
        public async Task<ActionResult> Index()
        {
            var studyDirections = db.StudyDirections.Include(s => s.Speciality);
            return View(await studyDirections.ToListAsync());
        }

        // GET: StudyDirections/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudyDirection studyDirection = await db.StudyDirections.FirstAsync(x => x.DirectionId == id);
            if (studyDirection == null)
            {
                return HttpNotFound();
            }
            return View(studyDirection);
        }

        // GET: StudyDirections/Create
        public ActionResult Create()
        {
            ViewBag.SpecialityId = new SelectList(db.Specialities, "SpecialityId", "SpecialityName");
            return View();
        }

        // POST: StudyDirections/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "FacultyId,ChairId,SpecialityId,DirectionId,StudyDirectionName")] StudyDirection studyDirection)
        {
            if (ModelState.IsValid)
            {
                db.StudyDirections.Add(studyDirection);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.SpecialityId = new SelectList(db.Specialities, "SpecialityId", "SpecialityName", studyDirection.FacultyId);
            return View(studyDirection);
        }

        // GET: StudyDirections/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudyDirection studyDirection = await db.StudyDirections.FirstAsync(x => x.DirectionId == id);
            if (studyDirection == null)
            {
                return HttpNotFound();
            }
            ViewBag.SpecialityId = new SelectList(db.Specialities, "SpecialityId", "SpecialityName", studyDirection.FacultyId);
            return View(studyDirection);
        }

        // POST: StudyDirections/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "FacultyId,ChairId,SpecialityId,DirectionId,StudyDirectionName")] StudyDirection studyDirection)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studyDirection).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.SpecialityId = new SelectList(db.Specialities, "SpecialityId", "SpecialityName", studyDirection.FacultyId);
            return View(studyDirection);
        }

        // GET: StudyDirections/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudyDirection studyDirection = await db.StudyDirections.FirstAsync(x => x.DirectionId == id);
            if (studyDirection == null)
            {
                return HttpNotFound();
            }
            return View(studyDirection);
        }

        // POST: StudyDirections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            StudyDirection studyDirection = await db.StudyDirections.FirstAsync(x => x.DirectionId == id);
            db.StudyDirections.Remove(studyDirection);
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
