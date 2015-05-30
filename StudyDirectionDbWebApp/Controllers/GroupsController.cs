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
    public class GroupsController : Controller
    {
        private StudyDirectionDbEntities1 db = new StudyDirectionDbEntities1();

        protected override void Initialize(RequestContext context)
        {
            base.Initialize(context);

            db.Database.Log = DatabaseLogger.Log;

            ViewBag.Controller = "Группы";
        }

        // GET: Groups
        public async Task<ActionResult> Index()
        {
            var groups = db.Groups.Include(g => g.StudyDirection).Include(g => g.StudyForm);
            return View(await groups.ToListAsync());
        }

        // GET: Groups/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = await db.Groups.FirstAsync(x => x.GroupId == id);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        // GET: Groups/Create
        public ActionResult Create()
        {
            ViewBag.DirectionId = new SelectList(db.StudyDirections, "DirectionId", "StudyDirectionName");
            ViewBag.Form = new SelectList(db.StudyForms, "Form", "Form");
            return View();
        }

        // POST: Groups/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "FacultyId,ChairId,SpecialityId,DirectionId,GroupId,Form")] Group group)
        {
            if (ModelState.IsValid)
            {
                db.Groups.Add(group);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.DirectionId = new SelectList(db.StudyDirections, "DirectionId", "StudyDirectionName", group.DirectionId);
            ViewBag.Form = new SelectList(db.StudyForms, "Form", "Form", group.Form);
            return View(group);
        }

        // GET: Groups/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = await db.Groups.FirstAsync(x => x.GroupId == id);
            if (group == null)
            {
                return HttpNotFound();
            }
            ViewBag.DirectionId = new SelectList(db.StudyDirections, "DirectionId", "StudyDirectionName", group.DirectionId);
            ViewBag.Form = new SelectList(db.StudyForms, "Form", "Form", group.Form);
            return View(group);
        }

        // POST: Groups/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "FacultyId,ChairId,SpecialityId,DirectionId,GroupId,Form")] Group group)
        {
            if (ModelState.IsValid)
            {
                db.Entry(group).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.DirectionId = new SelectList(db.StudyDirections, "DirectionId", "StudyDirectionName", group.DirectionId);
            ViewBag.Form = new SelectList(db.StudyForms, "Form", "Form", group.Form);
            return View(group);
        }

        // GET: Groups/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = await db.Groups.FirstAsync(x => x.GroupId == id);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        // POST: Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Group group = await db.Groups.FirstAsync(x => x.GroupId == id);
            db.Groups.Remove(group);
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
