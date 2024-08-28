using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BanHoa.Models;

namespace BanHoa.Controllers
{
    public class CartController : Controller
    {
        DBBANHOADataContext db = new DBBANHOADataContext();
        //
        // GET: /Cart/
        public ActionResult CartPartial()
        {
            ViewBag.TongSoLuong = TongSoLuong();
            return View();
        }
        public ActionResult EmptyCart()
        {
            return View();
        }
        public ActionResult Index()
        {
            if (Session["TenKH"] == null)
                return RedirectToAction("LogInAndRegister", "User");
            else if (Session["GioHang"] == null)
                return RedirectToAction("EmptyCart");
            else
            {
                List<CartItems> GioHang = Session["GioHang"] as List<CartItems>;
                List<CartItems> lstGioHang = LayGioHang();
                ViewBag.TongSoLuong = TongSoLuong();
                ViewBag.TongThanhTien = TongThanhTien();
                return View(lstGioHang);
            }
        }

        public List<CartItems> LayGioHang()
        {
            List<CartItems> lstGioHang = Session["GioHang"] as List<CartItems>;
            if (lstGioHang == null)
            {
                
                lstGioHang = new List<CartItems>();
                Session["GioHang"] = lstGioHang;
            }
            return lstGioHang;
        }

        [HttpPost]
        public ActionResult ThemGioHang(string mh, string strURL)
        {
            if (Session["TenKH"] == null)
                return RedirectToAction("LogInAndRegister", "User");
            List<CartItems> lstGioHang = LayGioHang();
            CartItems SanPham = lstGioHang.Find(sp => sp.sMaHoa == mh);
            if (SanPham == null)
            {
                int SL = int.Parse(Request.Form["SoLuong"]);
                SanPham = new CartItems(mh,SL);
                lstGioHang.Add(SanPham);
               
                return Redirect(strURL);
            }
            else
            {
                SanPham.iSoLuong++;
                return Redirect(strURL);
            }
        }

        private int TongSoLuong()
        {
            int tsl = 0;
            List<CartItems> lstGioHang = Session["GioHang"] as List<CartItems>;
            if (lstGioHang != null)
            {
                tsl = lstGioHang.Sum(sp => sp.iSoLuong);
            }
            return tsl;
        }

        private double TongThanhTien()
        {
            double ttt = 0;
            List<CartItems> lstGioHang = Session["GioHang"] as List<CartItems>;
            if (lstGioHang != null)
                ttt += lstGioHang.Sum(sp => sp.dThanhTien);
            return ttt;
        }

        public ActionResult GioHangPartial()
        {
            ViewBag.TongSoLuong = TongSoLuong();
            return View();
        }

        public ActionResult XoaGioHang(string MaSP)
        {
            List<CartItems> lstGH = LayGioHang();
            CartItems sp = lstGH.Single(s => s.sMaHoa == MaSP);

            if (sp != null)
            {
                lstGH.RemoveAll(s => s.sMaHoa == MaSP);
                if(lstGH.Count == 0)
                {
                    Session.Remove("GioHang");
                    return RedirectToAction("EmptyCart", "Cart");
                }
                return RedirectToAction("Index", "Cart");
            }
            return RedirectToAction("Index", "Cart");
        }

        public ActionResult GioHangRong()
        {
            return View();
        }

        public ActionResult XoaGioHang_All()
        {
            List<CartItems> lstGH = LayGioHang();
            Session.Remove("GioHang");
            lstGH.Clear();
            return RedirectToAction("EmptyCart", "Cart");
        }

        [HttpPost]
        public ActionResult CapNhatGioHang(string MaSP)
        {
            List<CartItems> lstGH = LayGioHang();
            CartItems sp = lstGH.Single(s => s.sMaHoa == MaSP);
            if (sp != null)
            {
                sp.iSoLuong = int.Parse(Request.Form["txtSoLuong"]);
            }
            return RedirectToAction("Index", "Cart");
        }

	}
}