using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FinalExam.Models;

namespace FinalExam.Controllers
{
    public class NhanVienController : Controller
    {
        private ModelDB db = new ModelDB();

        // GET: NhanVien
        public ActionResult Index()
        {
            var nhanVien = db.NhanVien.Include(n => n.Phong);
            return View(nhanVien.ToList());
        }
        [ChildActionOnly]
        public PartialViewResult CategoryMenu()
        {
            var li = db.Phong.ToList();
            return PartialView(li);
        }

        [Route("TimTheoPhong/{Maphong}")]
        public ActionResult TimTheoPhong(int Maphong)
        {
            var li = db.NhanVien.Where(s => s.Maphong == Maphong).ToList();
            return View(li);
        }
        // GET: NhanVien/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhanVien nhanVien = db.NhanVien.Find(id);
            if (nhanVien == null)
            {
                return HttpNotFound();
            }
            return View(nhanVien);
        }

        // GET: NhanVien/Create
        public ActionResult Create()
        {
            if(Session["username"] == null)
            {
                TempData["msg"] = "<script>alert('Change succesfully');</script>"; 
                return RedirectToAction("Index", "Nhanvien");
            }
            else
            {
                ViewBag.Maphong = new SelectList(db.Phong, "Maphong", "Tenphong");
                return View();
            }
            
        }

        // POST: NhanVien/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Manv,Hoten,Tuoi,Diachi,Luong,Maphong,Anh")] NhanVien nhanVien)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.NhanVien.Add(nhanVien);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.Maphong = new SelectList(db.Phong, "Maphong", "Tenphong", nhanVien.Maphong);
        //    return View(nhanVien);
        //}
        public ActionResult Create(NhanVien nv)
        {
            try
            {
                db.NhanVien.Add(nv);
                db.SaveChanges();
                return Json(new { result = true, JsonRequestBehavior.AllowGet });
            }
            catch (Exception ex)
            {
                return Json(new { result = false, error = ex.Message });
            }
        }
        // GET: NhanVien/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhanVien nhanVien = db.NhanVien.Find(id);
            if (nhanVien == null)
            {
                return HttpNotFound();
            }
            ViewBag.Maphong = new SelectList(db.Phong, "Maphong", "Tenphong", nhanVien.Maphong);
            return View(nhanVien);
        }

        // POST: NhanVien/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Manv,Hoten,Tuoi,Diachi,Luong,Maphong,Anh")] NhanVien nhanVien)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nhanVien).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Maphong = new SelectList(db.Phong, "Maphong", "Tenphong", nhanVien.Maphong);
            return View(nhanVien);
        }

        // GET: NhanVien/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhanVien nhanVien = db.NhanVien.Find(id);
            if (nhanVien == null)
            {
                return HttpNotFound();
            }
            return View(nhanVien);
        }

        // POST: NhanVien/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NhanVien nhanVien = db.NhanVien.Find(id);
            db.NhanVien.Remove(nhanVien);
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
