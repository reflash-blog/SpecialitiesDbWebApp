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
    public class StudyFormsController : Controller
    {
        private StudyDirectionDbEntities1 db = new StudyDirectionDbEntities1();

        protected override void Initialize(RequestContext context)
        {
            base.Initialize(context);

            db.Database.Log = DatabaseLogger.Log;

            ViewBag.Controller = "Формы подготовки";
        }

        // GET: StudyForms
        public async Task<ActionResult> Index()
        {
            return View(await db.StudyForms.ToListAsync());
        }

        // GET: StudyForms/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudyForm studyForm = await db.StudyForms.FindAsync(id);
            if (studyForm == null)
            {
                return HttpNotFound();
            }
            return View(studyForm);
        }

        // GET: StudyForms/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StudyForms/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Form")] StudyForm studyForm)
        {
            if (ModelState.IsValid)
            {
                db.StudyForms.Add(studyForm);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(studyForm);
        }

        // GET: StudyForms/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudyForm studyForm = await db.StudyForms.FindAsync(id);
            if (studyForm == null)
            {
                return HttpNotFound();
            }
            return View(studyForm);
        }

        // POST: StudyForms/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Form")] StudyForm studyForm)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studyForm).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(studyForm);
        }

        // GET: StudyForms/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudyForm studyForm = await db.StudyForms.FindAsync(id);
            if (studyForm == null)
            {
                return HttpNotFound();
            }
            return View(studyForm);
        }

        // POST: StudyForms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            StudyForm studyForm = await db.StudyForms.FindAsync(id);
            db.StudyForms.Remove(studyForm);
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
