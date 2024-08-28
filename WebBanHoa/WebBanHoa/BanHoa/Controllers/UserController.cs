using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BanHoa.Models;

namespace BanHoa.Controllers
{
    public class UserController : Controller
    {
        DBBANHOADataContext db = new DBBANHOADataContext();
        // GET: User
        public ActionResult Index()
        {
            if (Session["TenKH"] != null)
                return View();
            else
                return RedirectToAction("LogInAndRegister");
        }
        public ActionResult LogInAndRegister()
        {
            if (Session["TenKH"] != null)
                return RedirectToAction("Index");
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(KhachHang KH)
        {
            if(ModelState.IsValid)
            {
                KhachHang model = db.KhachHangs.Where(t => t.MaKH.Equals(KH.MaKH)).FirstOrDefault();

                int count  = (from u in db.KhachHangs select u).Count() + 1;
                string maKH;

                if (count < 10)
                    maKH = "KH00" + count.ToString();
                else
                    maKH = "KH0" + count.ToString();

                var check = db.KhachHangs.FirstOrDefault(s => s.Email == KH.Email);

                if(check == null)
                {
                    KH.MaKH = maKH;
                    KH.TenKH = Request.Form["TenKH"];
                    KH.DiaChi = Request.Form["DiaChi"];
                    KH.Email = Request.Form["Email"];
                    KH.MatKhau = Request.Form["MatKhau"];

                    db.KhachHangs.InsertOnSubmit(KH);

                    db.SubmitChanges();

                    Session["Success"] = "Đăng kí thành công";

                    return RedirectToAction("LogInAndRegister");
                    
                }
                else
                {
                    Session["LogInError"] = "Email đã được đăng ký!";
                    return RedirectToAction("LogInAndRegister");
                }
            }
            return View();
        }
        
        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogIn(KhachHang KH)
        {
            if (ModelState.IsValid)
            {
                var user = db.KhachHangs.Where(t => t.Email.Equals(Request.Form["Email"]) && t.MatKhau.Equals(Request.Form["MatKhau"]));
                if(user.Count() > 0)
                {
                    Session["TenKH"] = user.FirstOrDefault().TenKH;
                    Session["Email"] = user.FirstOrDefault().Email;
                    Session["DiaChi"] = user.FirstOrDefault().DiaChi;
                    Session["MaKH"] = user.FirstOrDefault().MaKH;
                    
                    return RedirectToAction("Index");
                }
                else 
                {
                    Session["LogInError"] = "Đăng nhập thất bại";
                    return RedirectToAction("LogInAndRegister");
                }
            }
            return View();
        }
        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("LogInAndRegister");
        }

        public ActionResult Edit(string id ="")
        {
            var query = from u in db.KhachHangs
                        where u.MaKH.Equals(id)
                        select u;

            var KH = query.AsEnumerable().Select(u =>
                         new KhachHang
                         {
                             MaKH = u.MaKH,
                             //MaKH, TenKH, Email, DiaChi, MatKhau
                             TenKH = u.TenKH,
                             Email = u.Email,
                             DiaChi = u.DiaChi,
                             MatKhau = u.MatKhau
                         }).FirstOrDefault();
            return View(KH);
        }
        [HttpPost]
        public ActionResult Edit(KhachHang KH)
        {
            if (ModelState.IsValid)
            {
                KhachHang model = db.KhachHangs.Where(t => t.MaKH.Equals(KH.MaKH)).FirstOrDefault();


                model.TenKH = KH.TenKH;
                Session["TenKH"] = KH.TenKH;

                model.DiaChi = KH.DiaChi;
                Session["DiaChi"] = KH.DiaChi;

                model.Email = KH.Email;
                Session["Email"] = KH.Email;

                model.MatKhau = KH.MatKhau;

                
                
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return View(KH);
        }

    }
}