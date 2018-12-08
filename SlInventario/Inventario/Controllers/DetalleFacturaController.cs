using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Inventario.Models;

namespace Inventario.Controllers
{
    public class DetalleFacturaController : Controller
    {
        private dbInventarioEntities db = new dbInventarioEntities();

        // GET: DetalleFactura
        public ActionResult Index()
        {
            var detalleFactura = db.DetalleFactura.Include(d => d.Factura).Include(d => d.Producto);
            return View(detalleFactura.ToList());
        }

        // GET: DetalleFactura/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetalleFactura detalleFactura = db.DetalleFactura.Find(id);
            if (detalleFactura == null)
            {
                return HttpNotFound();
            }
            return View(detalleFactura);
        }

        // GET: DetalleFactura/Create
        public ActionResult Create()
        {
            ViewBag.IdFactura = new SelectList(db.Factura, "IdFactura", "NombreTienda");
            ViewBag.IdProducto = new SelectList(db.Producto, "IdProducto", "Nombre");
            return View();
        }

        // POST: DetalleFactura/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdDetalle,IdFactura,IdProducto,Cantidad,Precio")] DetalleFactura detalleFactura)
        {
            if (ModelState.IsValid)
            {
                db.DetalleFactura.Add(detalleFactura);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdFactura = new SelectList(db.Factura, "IdFactura", "NombreTienda", detalleFactura.IdFactura);
            ViewBag.IdProducto = new SelectList(db.Producto, "IdProducto", "Nombre", detalleFactura.IdProducto);
            return View(detalleFactura);
        }

        // GET: DetalleFactura/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetalleFactura detalleFactura = db.DetalleFactura.Find(id);
            if (detalleFactura == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdFactura = new SelectList(db.Factura, "IdFactura", "NombreTienda", detalleFactura.IdFactura);
            ViewBag.IdProducto = new SelectList(db.Producto, "IdProducto", "Nombre", detalleFactura.IdProducto);
            return View(detalleFactura);
        }

        // POST: DetalleFactura/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdDetalle,IdFactura,IdProducto,Cantidad,Precio")] DetalleFactura detalleFactura)
        {
            if (ModelState.IsValid)
            {
                db.Entry(detalleFactura).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdFactura = new SelectList(db.Factura, "IdFactura", "NombreTienda", detalleFactura.IdFactura);
            ViewBag.IdProducto = new SelectList(db.Producto, "IdProducto", "Nombre", detalleFactura.IdProducto);
            return View(detalleFactura);
        }

        // GET: DetalleFactura/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetalleFactura detalleFactura = db.DetalleFactura.Find(id);
            if (detalleFactura == null)
            {
                return HttpNotFound();
            }
            return View(detalleFactura);
        }

        // POST: DetalleFactura/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DetalleFactura detalleFactura = db.DetalleFactura.Find(id);
            db.DetalleFactura.Remove(detalleFactura);
            db.SaveChanges();
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
