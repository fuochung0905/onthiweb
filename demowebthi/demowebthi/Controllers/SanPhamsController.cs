using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using demowebthi.Models;


namespace demowebthi.Controllers
{
    public class SanPhamsController : Controller
    {
        private QLBanHangQuanAoEntities db = new QLBanHangQuanAoEntities();
      

        // GET: SanPhams
        public ActionResult Index()
        {
            var sanPham = db.SanPham.Include(s => s.PhanLoai).Include(s => s.PhanLoaiPhu);
            return View(sanPham.ToList());
        }

        // GET: SanPhams/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPham.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // GET: SanPhams/Create
        public ActionResult Create()
        {
            ViewBag.MaPhanLoai = new SelectList(db.PhanLoai, "MaPhanLoai", "PhanLoaiChinh");
            ViewBag.MaPhanLoaiPhu = new SelectList(db.PhanLoaiPhu, "MaPhanLoaiPhu", "TenPhanLoaiPhu");
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaSanPham,TenSanPham,MaPhanLoai,GiaNhap,DonGiaBanNhoNhat,DonGiaBanLonNhat,TrangThai,MoTaNgan,AnhDaiDien,NoiBat,MaPhanLoaiPhu")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                if (sanPham.ImageFile != null && sanPham.ImageFile.ContentLength > 0)
                {
                    var imageFileName = Guid.NewGuid().ToString() + Path.GetExtension(sanPham.ImageFile.FileName);
                    var imagePath = Path.Combine(Server.MapPath("~/images"), imageFileName);
                    sanPham.ImageFile.SaveAs(imagePath);

                    // Cập nhật đường dẫn ảnh trong cơ sở dữ liệu
                    sanPham.AnhDaiDien = imagePath;
                
                }
                var sanpham = new SanPham
                {
                    MaSanPham=sanPham.MaSanPham,
                    TenSanPham=sanPham.TenSanPham,
                    MaPhanLoai=sanPham.MaPhanLoai,
                    GiaNhap=sanPham.GiaNhap,
                    DonGiaBanNhoNhat=sanPham.DonGiaBanNhoNhat,
                    DonGiaBanLonNhat=sanPham.DonGiaBanLonNhat,
                    TrangThai=sanPham.TrangThai,
                    MoTaNgan=sanPham.MoTaNgan,
                    AnhDaiDien=sanPham.AnhDaiDien,
                    NoiBat=sanPham.NoiBat,
                    MaPhanLoaiPhu=sanPham.MaPhanLoaiPhu
                };
                db.SanPham.Add(sanpham);
                db.SaveChanges();
              
             
                return RedirectToAction("Index");
            }

            ViewBag.MaPhanLoai = new SelectList(db.PhanLoai, "MaPhanLoai", "PhanLoaiChinh", sanPham.MaPhanLoai);
            ViewBag.MaPhanLoaiPhu = new SelectList(db.PhanLoaiPhu, "MaPhanLoaiPhu", "TenPhanLoaiPhu", sanPham.MaPhanLoaiPhu);
            return View(sanPham);
        }

        // GET: SanPhams/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPham.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaPhanLoai = new SelectList(db.PhanLoai, "MaPhanLoai", "PhanLoaiChinh", sanPham.MaPhanLoai);
            ViewBag.MaPhanLoaiPhu = new SelectList(db.PhanLoaiPhu, "MaPhanLoaiPhu", "TenPhanLoaiPhu", sanPham.MaPhanLoaiPhu);
            return View(sanPham);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaSanPham,TenSanPham,MaPhanLoai,GiaNhap,DonGiaBanNhoNhat,DonGiaBanLonNhat,TrangThai,MoTaNgan,AnhDaiDien,NoiBat,MaPhanLoaiPhu")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sanPham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaPhanLoai = new SelectList(db.PhanLoai, "MaPhanLoai", "PhanLoaiChinh", sanPham.MaPhanLoai);
            ViewBag.MaPhanLoaiPhu = new SelectList(db.PhanLoaiPhu, "MaPhanLoaiPhu", "TenPhanLoaiPhu", sanPham.MaPhanLoaiPhu);
            return View(sanPham);
        }

        // GET: SanPhams/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPham.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // POST: SanPhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            SanPham sanPham = db.SanPham.Find(id);
            db.SanPham.Remove(sanPham);
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
        [HttpGet]
        public async Task<ActionResult>getproductByCategory(string phanLoai)
        {
            List<SanPham> products = null;
            if (phanLoai == null || phanLoai == "Tất cả sản phẩm")
            {
                products = await db.SanPham.ToListAsync();
            }
            else
            {
                products = await db.SanPham
                            .Where(sp => sp.PhanLoai.PhanLoaiChinh == phanLoai)
                            .ToListAsync();
            }

            var _sanPham = products
                .Select(sp => new SanPham
                {
                    MaSanPham = sp.MaSanPham,
                    TenSanPham = sp.TenSanPham,
                    DonGiaBanNhoNhat = sp.DonGiaBanNhoNhat,
                    TrangThai = sp.TrangThai,
                    MoTaNgan = sp.MoTaNgan,
                    AnhDaiDien = sp.AnhDaiDien,
                    NoiBat = sp.NoiBat,
                    MaPhanLoaiPhu = sp.MaPhanLoaiPhu,
                    MaPhanLoai = sp.MaPhanLoai,
                    GiaNhap = sp.GiaNhap
                }).ToList();
            return Json(new { sanPham = _sanPham }, JsonRequestBehavior.AllowGet);
        }
    }
}
