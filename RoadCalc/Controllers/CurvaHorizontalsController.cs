using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using RoadCalc.Context;
using RoadCalc.Models.Entities;

namespace RoadCalc.Controllers
{
    public class CurvaHorizontalsController : Controller
    {
        private EstradasContext db = new EstradasContext();

        // GET: CurvaHorizontals
        public async Task<ActionResult> Index()
        {
            return View(await db.Curvas.ToListAsync());
        }

        // GET: CurvaHorizontals/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CurvaHorizontal curvaHorizontal = await db.Curvas.FindAsync(id);
            if (curvaHorizontal == null)
            {
                return HttpNotFound();
            }
            return View(curvaHorizontal);
        }

        // GET: CurvaHorizontals/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CurvaHorizontals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Nome,Raio,AnguloCentral,Desenvolvimento,Corda,GrauDeCurva,Deflexao,TangenteExterior,Transicao,SuperElevacao,VelDiretriz,LcMinAbsoluto,LcMinFluenciaOtica,CTaxaMaximaAdmissivel,LcMinConforto,LcMaxAnguloCentral,LcMaxTempoPercurso,Lc,ACEspiral,AcComTransicao,DateIncluded,DateAltered")] CurvaHorizontal curvaHorizontal)
        {
            if (ModelState.IsValid)
            {
                db.Curvas.Add(curvaHorizontal);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(curvaHorizontal);
        }

        // GET: CurvaHorizontals/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CurvaHorizontal curvaHorizontal = await db.Curvas.FindAsync(id);
            if (curvaHorizontal == null)
            {
                return HttpNotFound();
            }
            return View(curvaHorizontal);
        }

        // POST: CurvaHorizontals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Nome,Raio,AnguloCentral,Desenvolvimento,Corda,GrauDeCurva,Deflexao,TangenteExterior,Transicao,SuperElevacao,VelDiretriz,LcMinAbsoluto,LcMinFluenciaOtica,CTaxaMaximaAdmissivel,LcMinConforto,LcMaxAnguloCentral,LcMaxTempoPercurso,Lc,ACEspiral,AcComTransicao,DateIncluded,DateAltered")] CurvaHorizontal curvaHorizontal)
        {
            if (ModelState.IsValid)
            {
                db.Configuration.LazyLoadingEnabled = true;
                var curva = db.Curvas.Find(curvaHorizontal.Id);
                curva.Lc = curvaHorizontal.Lc;
                curva?.AtualizaCurva();
                db.Entry(curva).State = EntityState.Modified;
                await db.SaveChangesAsync();
                db.Configuration.LazyLoadingEnabled = false;
                return RedirectToAction("Index");
            }
            return View(curvaHorizontal);
        }

        // GET: CurvaHorizontals/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CurvaHorizontal curvaHorizontal = await db.Curvas.FindAsync(id);
            if (curvaHorizontal == null)
            {
                return HttpNotFound();
            }
            return View(curvaHorizontal);
        }

        // POST: CurvaHorizontals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CurvaHorizontal curvaHorizontal = await db.Curvas.FindAsync(id);
            db.Curvas.Remove(curvaHorizontal);
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
