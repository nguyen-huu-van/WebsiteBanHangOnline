using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanHangOnline.Models;
using System.IO;

namespace WebsiteBanHangOnline.Controllers
{
    public class QuanLySanPhamController : Controller
    {
        QuanLyBanHangOnlineEntities2 db = new QuanLyBanHangOnlineEntities2();
        public ActionResult MenuProductPartial()
        {
            return View();
        }
        public ActionResult ThongTinSanPham()
        {
            if (Session["TaiKhoan"] != null)
            {
                return View(db.SanPhams.Where(m => m.DaXoa == false).OrderBy(m => m.MaLoaiSP).ToList());
            }
            else
            {
                return RedirectToAction("Http404", "Error");
            }
        }
        [HttpGet]
        public ActionResult ThemMoi()
        {
            if (Session["TaiKhoan"] != null)
            {
                ViewBag.MaLoaiSP = new SelectList(db.LoaiSanPhams.OrderBy(m => m.TenLoai), "MaLoaiSP", "TenLoai");
                ViewBag.MaPhong = new SelectList(db.Phongs.OrderBy(m => m.TenPhong), "MaPhong", "TenPhong");
                return View();
            }
            else
            {
                return RedirectToAction("Http404", "Error");
            }
        }
        [HttpPost]
        public ActionResult ThemMoi(SanPham sanPham, HttpPostedFileBase HinhAnh, HttpPostedFileBase HinhAnh1, HttpPostedFileBase HinhAnh2, HttpPostedFileBase HinhAnh3) // giao thức truyền dữ liệu hình ảnh 
        {
            // load DropDownList
            ViewBag.MaLoaiSP = new SelectList(db.LoaiSanPhams.OrderBy(m => m.TenLoai), "MaLoaiSP", "TenLoai");
            ViewBag.MaPhong = new SelectList(db.Phongs.OrderBy(m => m.TenPhong), "MaPhong", "TenPhong");
            if (HinhAnh == null || HinhAnh1 == null || HinhAnh2 == null || HinhAnh3 == null)
            {
                ViewBag.Images = "Cần chọn hình trước khi lưu";
                return View();
            }
            if (HinhAnh != null && HinhAnh.ContentLength > 0)
            {
                // Lấy tên hình ảnh 
                var fileName = Path.GetFileName(HinhAnh.FileName);
                // Lấy hình ảnh chuyển vào thư mục hình ảnh
                var path = Path.Combine(Server.MapPath("~/Content/assets/image/HinhAnh/"), fileName);

                // Nếu có rồi thì thông báo 
                if (System.IO.File.Exists(path))
                {
                    ViewBag.upload = "Hình đã tồn tại";
                    return View();
                }
                else
                {
                    // lấy hình ảnh đưa vào thư mục 
                    HinhAnh.SaveAs(path);
                    sanPham.HinhAnh = fileName;
                }
            }
            if (HinhAnh1 != null && HinhAnh1.ContentLength > 0)
            {
                // Lấy tên hình ảnh 
                var fileName = Path.GetFileName(HinhAnh1.FileName);
                // Lấy hình ảnh chuyển vào thư mục hình ảnh
                var path = Path.Combine(Server.MapPath("~/Content/assets/image/HinhAnh/"), fileName);
                // Nếu có rồi thì thông báo 
                if (System.IO.File.Exists(path))
                {
                    ViewBag.upload = "Hình đã tồn tại";
                    return View();
                }
                else
                {
                    // lấy hình ảnh đưa vào thư mục 
                    HinhAnh1.SaveAs(path);
                    sanPham.HinhAnh1 = fileName;
                }
            }
            if (HinhAnh2 != null && HinhAnh2.ContentLength > 0)
            {
                // Lấy tên hình ảnh 
                var fileName = Path.GetFileName(HinhAnh2.FileName);
                // Lấy hình ảnh chuyển vào thư mục hình ảnh
                var path = Path.Combine(Server.MapPath("~/Content/assets/image/HinhAnh/"), fileName);
                // Nếu có rồi thì thông báo 
                if (System.IO.File.Exists(path))
                {
                    ViewBag.upload = "Hình đã tồn tại";
                    return View();
                }
                else
                {
                    // lấy hình ảnh đưa vào thư mục 
                    HinhAnh2.SaveAs(path);
                    sanPham.HinhAnh2 = fileName;
                }
            }
            if (HinhAnh3 != null && HinhAnh3.ContentLength > 0)
            {
                // Lấy tên hình ảnh 
                var fileName = Path.GetFileName(HinhAnh3.FileName);
                // Lấy hình ảnh chuyển vào thư mục hình ảnh
                var path = Path.Combine(Server.MapPath("~/Content/assets/image/HinhAnh/"), fileName);
                // Nếu có rồi thì thông báo 
                if (System.IO.File.Exists(path))
                {
                    ViewBag.upload = "Hình đã tồn tại";
                    return View();
                }
                else
                {
                    // lấy hình ảnh đưa vào thư mục 
                    HinhAnh3.SaveAs(path);
                    sanPham.HinhAnh3 = fileName;
                }
            }
            sanPham.DaXoa = false;
            db.SanPhams.Add(sanPham);
            db.SaveChanges();
            return RedirectToAction("ThongTinSanPham");
        }
        [HttpGet]
        public ActionResult ChinhSua(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Http404", "Error");
            }
            SanPham sp = db.SanPhams.SingleOrDefault(m => m.MaSP == id);
            if (sp == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps.OrderBy(m => m.TenNCC), "MaNCC", "TenNCC", sp.MANCC);
            ViewBag.MaLoaiSP = new SelectList(db.LoaiSanPhams.OrderBy(m => m.TenLoai), "MaLoaiSP", "TenLoai", sp.MaLoaiSP);
            ViewBag.MaPhong = new SelectList(db.Phongs.OrderBy(m => m.TenPhong), "MaPhong", "TenPhong", sp.MaPhong);
            return View(sp);
        }
        [HttpPost]
        public ActionResult ChinhSua(SanPham model, HttpPostedFileBase HinhAnh, HttpPostedFileBase HinhAnh1, HttpPostedFileBase HinhAnh2, HttpPostedFileBase HinhAnh3)
        {
            if (ModelState.IsValid)
            {
                // Lấy sản phẩm hiện tại từ cơ sở dữ liệu
                var existingProduct = db.SanPhams.Find(model.MaSP);

                // Cập nhật các thuộc tính với các giá trị từ biểu mẫu
                existingProduct.TenSP = model.TenSP;
                existingProduct.DonGia = model.DonGia;
                existingProduct.NgayCapNhat = model.NgayCapNhat;
                existingProduct.MoTa = model.MoTa;
                existingProduct.SoLuongTon = model.SoLuongTon;
                existingProduct.LuotXem = model.LuotXem;
                existingProduct.LuotBinhLuan = model.LuotBinhLuan;
                existingProduct.LuotBinhChon = model.LuotBinhChon;
                existingProduct.SoLanMua = model.SoLanMua;
                existingProduct.Moi = model.Moi;
                existingProduct.MANCC = model.MANCC;
                existingProduct.MaPhong = model.MaPhong;
                existingProduct.MaLoaiSP = model.MaLoaiSP;
                //Chỉ cập nhật thuộc tính hình ảnh khi người dùng tải lên ảnh mới:
                if (HinhAnh == null)
                {
                    model.HinhAnh = existingProduct.HinhAnh;
                }
                if (HinhAnh1 == null)
                {
                    model.HinhAnh1 = existingProduct.HinhAnh1;
                }
                if (HinhAnh2 == null)
                {
                    model.HinhAnh2 = existingProduct.HinhAnh2;
                }
                if (HinhAnh3 == null)
                {
                    model.HinhAnh3 = existingProduct.HinhAnh3;
                }



                // Kiểm tra xem có hình ảnh mới được cung cấp hay không và cập nhật đường dẫn tương ứng
                if (HinhAnh != null && HinhAnh.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(HinhAnh.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/assets/image/HinhAnh/"), fileName);
                    HinhAnh.SaveAs(path);
                    existingProduct.HinhAnh = fileName;
                }

                if (HinhAnh1 != null && HinhAnh1.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(HinhAnh1.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/assets/image/HinhAnh/"), fileName);
                    HinhAnh1.SaveAs(path);
                    existingProduct.HinhAnh1 = fileName;
                }
                if (HinhAnh2 != null && HinhAnh2.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(HinhAnh2.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/assets/image/HinhAnh/"), fileName);
                    HinhAnh2.SaveAs(path);
                    existingProduct.HinhAnh2 = fileName;
                }
                if (HinhAnh3 != null && HinhAnh3.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(HinhAnh3.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/assets/image/HinhAnh/"), fileName);
                    HinhAnh3.SaveAs(path);
                    existingProduct.HinhAnh = fileName;
                }

                // Lưu các thay đổi vào cơ sở dữ liệu
                db.SaveChanges();

                return RedirectToAction("ThongTinSanPham");
            }
            return View(model);
        }
        [HttpGet]
        public ActionResult Remove(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            SanPham sp = db.SanPhams.SingleOrDefault(m => m.MaSP == id);
            if (sp == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps.OrderBy(m => m.TenNCC), "MaNCC", "TenNCC", sp.MANCC);
            ViewBag.MaLoaiSP = new SelectList(db.LoaiSanPhams.OrderBy(m => m.TenLoai), "MaLoaiSP", "TenLoai", sp.MaLoaiSP);
            ViewBag.MaPhong = new SelectList(db.Phongs.OrderBy(m => m.TenPhong), "MaPhong", "TenPhong", sp.MaPhong);
            return View(sp);
        }
        [HttpPost]
        public ActionResult Remove(int id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            SanPham sp = db.SanPhams.SingleOrDefault(m => m.MaSP == id);
            if (sp == null)
            {
                return HttpNotFound();
            }
            db.SanPhams.Remove(sp);
            db.SaveChanges();
            return RedirectToAction("ThongTinSanPham");
        }

    }
}
