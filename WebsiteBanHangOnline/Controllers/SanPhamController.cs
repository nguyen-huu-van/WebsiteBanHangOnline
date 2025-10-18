using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanHangOnline.Models;
using PagedList;
using System.Web.UI;

namespace WebsiteBanHangOnline.Controllers
{
    public class SanPhamController : Controller
    {
        QuanLyBanHangOnlineEntities2 db = new QuanLyBanHangOnlineEntities2();
        // GET: SanPham
        public ActionResult Sanphamstyle1Partial()
        {
            return PartialView();
        }
        public ActionResult Sanphamstyle2Partial()
        {
            return PartialView();
        }
        public ActionResult ChiTietSanPham(int maLoai, int maphong, int? id, string tensp)
        {
            var sanpham = db.SanPhams.SingleOrDefault(s => s.MaLoaiSP == maLoai && s.MaPhong == maphong && s.MaSP == id);
            if (sanpham == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sanpham);
        }
        //public ActionResult ChiTietSanPham()
        //{
        //    return View();
        //}
        //public ActionResult SanPhamLienQuan()
        //{
        //    return PartialView();
        //}

        public ActionResult SanPhamLienQuan(int maLoai, int maSP)
        {
            // Lấy sản phẩm đang xem
            var sanphamDangXem = db.SanPhams.SingleOrDefault(s => s.MaLoaiSP == maLoai && s.MaSP == maSP);
            if (sanphamDangXem == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            // Lấy sản phẩm liên quan
            var sanphamLienQuan = db.SanPhams
            .Where(s => s.MaLoaiSP == sanphamDangXem.MaLoaiSP && s.MaSP != sanphamDangXem.MaSP && s.DaXoa == false)
            .OrderByDescending(s => s.NgayCapNhat).ToList();

            return PartialView(sanphamLienQuan);
        }
        public ActionResult SidebarSanPhamPartial()
        {
            var sanPham = db.SanPhams;
            return View(sanPham);
        }
        public ActionResult SanPhamTheoPhong(int? maLoai, int? maphong, int? Page)
        {
            var sanpham = db.SanPhams.Where(m => m.MaPhong == maphong && m.MaLoaiSP == maLoai && m.DaXoa == false).ToList();
            int pageSize = 9;
            int pageNumbber = (Page ?? 1);

            ViewBag.MaLoai = maLoai;
            ViewBag.MaPhong = maphong;
            return View(sanpham.OrderBy(m => m.DonGia).ToPagedList(pageNumbber, pageSize));
        }
        public ActionResult SanPhamTheoLoai(int? maLoai, int? page)
        {
            var sanpham = db.SanPhams.Where(s => s.MaLoaiSP == maLoai && s.DaXoa == false).ToList();

            // Thực hiện phân trang theo loại 
            int pageSize = 12;// Số sản phẩm có trên trang 
            int pageNumbber = (page ?? 1); // số trang hiện tại

            ViewBag.MaLoai = maLoai;
            return View(sanpham.OrderBy(m => m.DonGia).ToPagedList(pageNumbber, pageSize));
        }
        public ActionResult DonGia(int? maLoai, int? maNSX)
        {
            ViewBag.MaLoai = maLoai;
            ViewBag.MaNSX = maNSX;
            return View();
        }
        public ActionResult SanPhamTheoPhongDonGia(int? maLoai, int? maphong, int? Page, string priceRange)
        {
            int pageSize = 9;
            int pageNumber = (Page ?? 1);

            // Lấy danh sách sản phẩm theo phạm vi giá
            IQueryable<SanPham> sanPhams = db.SanPhams.Where(m => m.MaLoaiSP == maLoai && m.MaPhong == maphong && m.DaXoa == false);

            // Xử lý tùy theo phạm vi giá
            switch (priceRange)
            {
                case "1":
                    sanPhams = sanPhams.Where(m => m.DonGia >= 1000000 && m.DonGia <= 2000000);
                    break;
                case "2":
                    sanPhams = sanPhams.Where(m => m.DonGia >= 2000000 && m.DonGia <= 3000000);
                    break;
                case "3":
                    sanPhams = sanPhams.Where(m => m.DonGia >= 3000000 && m.DonGia <= 4000000); // Sửa lại giá đúng
                    break;
                case "4":
                    sanPhams = sanPhams.Where(m => m.DonGia >= 4000000 && m.DonGia <= 5000000);
                    break;
                case "5":
                    sanPhams = sanPhams.Where(m => m.DonGia >= 5000000 && m.DonGia <= 6000000);
                    break;
                case "6":
                    sanPhams = sanPhams.Where(m => m.DonGia >= 6000000 && m.DonGia <= 7000000);
                    break;
                case "Tren10":
                    sanPhams = sanPhams.Where(m => m.DonGia >= 10000000);
                    break;
                default:
                    // Không lọc theo giá nếu không có priceRange
                    break;
            }

            // Sắp xếp theo giá tăng dần và phân trang
            var result = sanPhams.OrderBy(m => m.DonGia).ToPagedList(pageNumber, pageSize);

            return View(result);
        }

    }
}