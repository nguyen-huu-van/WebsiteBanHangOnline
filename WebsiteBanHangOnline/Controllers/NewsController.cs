using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanHangOnline.Models;

namespace WebsiteBanHangOnline.Controllers
{
    public class NewsController : Controller
    {
        QuanLyBanHangOnlineEntities2 db = new QuanLyBanHangOnlineEntities2();
        // GET: News
        public ActionResult MenuNews()
        {
            return View();
        }
        public ActionResult ListNews()
        {
            if (Session["TaiKhoan"] != null)
            {
                var listnew = db.TinTucs.ToList();
                return View(listnew);
            }
            else
            {
                return RedirectToAction("Http404", "Error");
            }
        }
        [HttpGet]
        public ActionResult CreateNews()
        {
            if (Session["TaiKhoan"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Http404", "Error");
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateNews(TinTuc model, HttpPostedFileBase Image)
        {
            if (Session["TaiKhoan"] != null)
            {
                try
                {
                    if (Image != null && Image.ContentLength > 0)
                    {
                        // Lấy tên file từ tệp hình ảnh
                        var fileName = Path.GetFileName(Image.FileName);

                        // Tạo thư mục đích nếu chưa tồn tại
                        var folderPath = Server.MapPath("~/Content/assets/image/News/");
                        if (!Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }

                        // Đường dẫn đầy đủ để lưu tệp
                        var filePath = Path.Combine(folderPath, fileName);

                        // Kiểm tra xem tệp đã tồn tại chưa
                        if (System.IO.File.Exists(filePath))
                        {
                            ViewBag.upload = "Hình đã tồn tại.";
                            return View(model);
                        }

                        // Lưu tệp vào thư mục
                        Image.SaveAs(filePath);

                        // Lưu đường dẫn vào cơ sở dữ liệu
                        model.HinhBia = fileName;
                    }

                    // Thêm tin tức vào database
                    db.TinTucs.Add(model);
                    db.SaveChanges();

                    // Chuyển hướng về danh sách tin tức
                    return RedirectToAction("ListNews");
                }
                catch (Exception ex)
                {
                    // Hiển thị lỗi trong trường hợp thất bại
                    ModelState.AddModelError("", "Lỗi khi lưu tin tức: " + ex.Message);
                    return View(model);
                }
            }
            else
            {
                return RedirectToAction("Http404", "Error");
            }
        }

        [HttpGet]
        public ActionResult UpdateNews(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            TinTuc news = db.TinTucs.SingleOrDefault(m => m.MaTin == id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UpdateNews(TinTuc model)
        {
            if (ModelState.IsValid)
            {
                var updateNews = db.TinTucs.Find(model.MaTin);
                updateNews.TieuDe = model.TieuDe;
                updateNews.NoiDung = model.NoiDung;
                updateNews.HinhBia = model.HinhBia;
                updateNews.NgayDang = model.NgayDang;
                db.SaveChanges();
                return RedirectToAction("ListNews");
            }
            return View(model);
        }
        [HttpGet]
        public ActionResult RemoveNews(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Http404", "Error");
            }
            TinTuc news = db.TinTucs.SingleOrDefault(m => m.MaTin == id);
            if (news == null)
            {
                return HttpNotFound();
            }
            db.TinTucs.Remove(news);
            db.SaveChanges();
            return RedirectToAction("ListNews");
        }
    }
}