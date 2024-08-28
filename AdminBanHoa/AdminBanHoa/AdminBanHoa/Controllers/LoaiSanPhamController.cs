using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdminBanHoa.Models;
namespace AdminBanHoa.Controllers
{
    public class LoaiSanPhamController : Controller
    {
        DBBANHOADataContext db = new DBBANHOADataContext();
        //
        // GET: /LoaiSanPham/
        public ActionResult LoaiSanPhamPartial()
        {
            return View();
        }

        public ActionResult Index()
        {
            return View(db.LoaiSanPhams.ToList());
        }


        [HttpPost]
        public JsonResult Add(LoaiSanPham f)
        {   
            int kq = 1;
            try
            {
                db.LoaiSanPhams.InsertOnSubmit(f);
                db.SubmitChanges();
            }
            catch
            {
                kq = 0;
            }
            return Json(kq, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Edit(string id)
        {
            if (String.IsNullOrEmpty(id))
                return RedirectToAction("Index");
            return View(db.LoaiSanPhams.FirstOrDefault(t => t.MaLoaiSP == id));
        }

        [HttpPost]
        public ActionResult Edit(LoaiSanPham l)
        {
            LoaiSanPham model = db.LoaiSanPhams.FirstOrDefault(t => t.MaLoaiSP == l.MaLoaiSP);

            model.TenLoaiSP = l.TenLoaiSP;

            db.SubmitChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Delete(string id)
        {
            if (String.IsNullOrEmpty(id))
                return RedirectToAction("Index");
            return View(db.LoaiSanPhams.FirstOrDefault(t => t.MaLoaiSP == id));
        }

        [HttpPost]
        public ActionResult Delete(string id, string temp)
        {
            var check = db.SanPhams.Where(t => t.MaLoaiSP == id).ToList();
            if (check.Count > 0)
            {
                Session["DeleteError"] = "Xoá Thất Bại";
                return RedirectToAction("Index");
                
            }
            else
            {
                LoaiSanPham l = db.LoaiSanPhams.FirstOrDefault(t => t.MaLoaiSP == id);
                db.LoaiSanPhams.DeleteOnSubmit(l);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
        }
	}
}