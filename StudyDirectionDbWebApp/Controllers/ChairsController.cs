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
    public class ChairsController : Controller
    {
        private StudyDirectionDbEntities1 db = new StudyDirectionDbEntities1();

        protected override void Initialize(RequestContext context)
        {
            base.Initialize(context);

            db.Database.Log = DatabaseLogger.Log;

            ViewBag.Controller = "Кафедры";
        }

        // GET: Chairs
        public async Task<ActionResult> Index()
        {
            var chairs = db.Chairs.Include(c => c.Faculty);
            return View(await chairs.ToListAsync());
        }

        // GET: Chairs/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chair chair = await db.Chairs.FirstAsync(x=>x.ChairId == id);
            if (chair == null)
            {
                return HttpNotFound();
            }
            return View(chair);
        }

        // GET: Chairs/Create
        public ActionResult Create()
        {
            ViewBag.FacultyId = new SelectList(db.Faculties, "FacultyId", "Name");
            return View();
        }

        // POST: Chairs/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "FacultyId,ChairId,ChairName")] Chair chair)
        {
            if (ModelState.IsValid)
            {
                db.Chairs.Add(chair);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.FacultyId = new SelectList(db.Faculties, "FacultyId", "Name", chair.FacultyId);
            return View(chair);
        }

        // GET: Chairs/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chair chair = await db.Chairs.FirstAsync(x => x.ChairId == id);
            if (chair == null)
            {
                return HttpNotFound();
            }
            ViewBag.FacultyId = new SelectList(db.Faculties, "FacultyId", "Name", chair.FacultyId);
            return View(chair);
        }

        // POST: Chairs/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "FacultyId,ChairId,ChairName")] Chair chair)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chair).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.FacultyId = new SelectList(db.Faculties, "FacultyId", "Name", chair.FacultyId);
            return View(chair);
        }

        // GET: Chairs/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chair chair = await db.Chairs.FirstAsync(x => x.ChairId == id);
            if (chair == null)
            {
                return HttpNotFound();
            }
            return View(chair);
        }

        // POST: Chairs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Chair chair = await db.Chairs.FirstAsync(x => x.ChairId == id);
            db.Chairs.Remove(chair);
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
