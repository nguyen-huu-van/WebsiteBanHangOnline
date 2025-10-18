using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanHangOnline.Models;
using PagedList.Mvc;
using PagedList;

namespace WebsiteBanHangOnline.Controllers
{
    public class TimKiemController : Controller
    {
        QuanLyBanHangOnlineEntities2 db = new QuanLyBanHangOnlineEntities2();
        // GET: TimKiem
        public ActionResult KetQuaTimKiem(string tuKhoa, int? Page)
        {
            int pageSize = 2;
            int pageNumber = (Page ?? 1);
            var listSanPham = db.SanPhams.Where(m => m.TenSP.Contains(tuKhoa));
            ViewBag.TuKhoa = tuKhoa;
            return View(listSanPham.OrderBy(m => m.TenSP).ToPagedList(pageNumber, pageSize));
        }
        public ActionResult LayTuKhoaTimKiem(string tuKhoa)
        {
            // Láy từ khóa tìm kiếm sau đó gọi đến KetQuaTimKiem
            return RedirectToAction("KetQuaTimKiem", new { @tuKhoa = tuKhoa });
        }

        public ActionResult Menuleft()
        {
            var Producer = db.Phongs.ToList();
            return PartialView(Producer);
        }
    }
}