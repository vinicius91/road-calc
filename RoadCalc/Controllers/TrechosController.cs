using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using RoadCalc.Context;
using RoadCalc.Models.Entities;

namespace RoadCalc.Controllers
{
    public class TrechosController : Controller
    {
        private EstradasContext db = new EstradasContext();

        // GET: Trechos
        public async Task<ActionResult> Index()
        {
            var trechos = db.Trechos.Include(t => t.Projeto);
            return View(await trechos.ToListAsync());
        }

        // GET: Trechos/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trecho trecho = await db.Trechos.FindAsync(id);
            if (trecho == null)
            {
                return HttpNotFound();
            }
            return View(trecho);
        }

        // GET: Trechos/Create
        public ActionResult Create()
        {
            ViewBag.ProjetoId = new SelectList(db.Projetos, "Id", "Nome");
            return View();
        }

        // POST: Trechos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Nome,Distancia,Inclinacao,Azimute,ProjetoId,DateIncluded,DateAltered")] Trecho trecho)
        {
            if (ModelState.IsValid)
            {
                db.Trechos.Add(trecho);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ProjetoId = new SelectList(db.Projetos, "Id", "Nome", trecho.ProjetoId);
            return View(trecho);
        }

        // GET: Trechos/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trecho trecho = await db.Trechos.FindAsync(id);
            if (trecho == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProjetoId = new SelectList(db.Projetos, "Id", "Nome", trecho.ProjetoId);
            return View(trecho);
        }

        // POST: Trechos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Nome,Distancia,Inclinacao,Azimute,ProjetoId,DateIncluded,DateAltered")] Trecho trecho)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trecho).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ProjetoId = new SelectList(db.Projetos, "Id", "Nome", trecho.ProjetoId);
            return View(trecho);
        }

        // GET: Trechos/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trecho trecho = await db.Trechos.FindAsync(id);
            if (trecho == null)
            {
                return HttpNotFound();
            }
            return View(trecho);
        }

        // POST: Trechos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Trecho trecho = await db.Trechos.FindAsync(id);
            db.Trechos.Remove(trecho);
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
