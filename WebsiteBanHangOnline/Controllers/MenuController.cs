using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanHangOnline.Models;
namespace WebsiteBanHangOnline.Controllers
{
    public class MenuController : Controller
    {
        QuanLyBanHangOnlineEntities2 db = new QuanLyBanHangOnlineEntities2();
        // GET: Menu
        public ActionResult Menuleft()
        {
            var sanpham = db.Phongs.ToList();
            return View(sanpham);
        }
    }
}