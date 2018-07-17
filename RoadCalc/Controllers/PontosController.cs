using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using RoadCalc.Context;
using RoadCalc.Models.Entities;

namespace RoadCalc.Controllers
{
    public class PontosController : Controller
    {
        private EstradasContext db = new EstradasContext();

        // GET: Pontos
        public async Task<ActionResult> Index()
        {
            var pontosNotaveis = db.PontosNotaveis.Include(p => p.Projeto);
            return View(await pontosNotaveis.ToListAsync());
        }

        // GET: Pontos/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PontoNotavel pontoNotavel = await db.PontosNotaveis.FindAsync(id);
            if (pontoNotavel == null)
            {
                return HttpNotFound();
            }
            return View(pontoNotavel);
        }

        // GET: Pontos/Create
        public ActionResult Create()
        {
            ViewBag.ProjetoId = new SelectList(db.Projetos, "Id", "Nome");
            return View();
        }

        // POST: Pontos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Nome,CoordX,CoordY,Cota,ProjetoId,DateIncluded,DateAltered")] PontoNotavel pontoNotavel)
        {
            if (ModelState.IsValid)
            {
                db.PontosNotaveis.Add(pontoNotavel);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ProjetoId = new SelectList(db.Projetos, "Id", "Nome", pontoNotavel.ProjetoId);
            return View(pontoNotavel);
        }

        // GET: Pontos/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PontoNotavel pontoNotavel = await db.PontosNotaveis.FindAsync(id);
            if (pontoNotavel == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProjetoId = new SelectList(db.Projetos, "Id", "Nome", pontoNotavel.ProjetoId);
            return View(pontoNotavel);
        }

        // POST: Pontos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Nome,CoordX,CoordY,Cota,ProjetoId,DateIncluded,DateAltered")] PontoNotavel pontoNotavel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pontoNotavel).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ProjetoId = new SelectList(db.Projetos, "Id", "Nome", pontoNotavel.ProjetoId);
            return View(pontoNotavel);
        }

        // GET: Pontos/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PontoNotavel pontoNotavel = await db.PontosNotaveis.FindAsync(id);
            if (pontoNotavel == null)
            {
                return HttpNotFound();
            }
            return View(pontoNotavel);
        }

        // POST: Pontos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            PontoNotavel pontoNotavel = await db.PontosNotaveis.FindAsync(id);
            db.PontosNotaveis.Remove(pontoNotavel);
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
