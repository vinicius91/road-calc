using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using RoadCalc.Context;
using RoadCalc.Models.Entities;

namespace RoadCalc.Controllers
{
    public class CoordenadasController : Controller
    {
        private EstradasContext db = new EstradasContext();

        // GET: Coordenadas
        public async Task<ActionResult> Index()
        {
            return View(await db.Coordenadas.ToListAsync());
        }

        // GET: Coordenadas/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Coordenada coordenada = await db.Coordenadas.FindAsync(id);
            if (coordenada == null)
            {
                return HttpNotFound();
            }
            return View(coordenada);
        }

        // GET: Coordenadas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Coordenadas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Lat,Lng,X,Y,Z,ZoneLetter,ZoneNumber,DateIncluded,DateAltered")] Coordenada coordenada)
        {
            if (ModelState.IsValid)
            {
                db.Coordenadas.Add(coordenada);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(coordenada);
        }

        // GET: Coordenadas/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Coordenada coordenada = await db.Coordenadas.FindAsync(id);
            if (coordenada == null)
            {
                return HttpNotFound();
            }
            return View(coordenada);
        }

        // POST: Coordenadas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Lat,Lng,X,Y,Z,ZoneLetter,ZoneNumber,DateIncluded,DateAltered")] Coordenada coordenada)
        {
            if (ModelState.IsValid)
            {
                db.Entry(coordenada).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(coordenada);
        }

        // GET: Coordenadas/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Coordenada coordenada = await db.Coordenadas.FindAsync(id);
            if (coordenada == null)
            {
                return HttpNotFound();
            }
            return View(coordenada);
        }

        // POST: Coordenadas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Coordenada coordenada = await db.Coordenadas.FindAsync(id);
            db.Coordenadas.Remove(coordenada);
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
