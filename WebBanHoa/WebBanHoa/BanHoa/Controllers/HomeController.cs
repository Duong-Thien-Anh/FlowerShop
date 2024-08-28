using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BanHoa.Controllers;
using BanHoa.Models;

namespace BanHoa.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AboutUs()
        {
            return View();
        }
        public ActionResult Contacts()
        {
            return View();
        }
        //public RedirectToRouteResult SuaSoLuong(int SanPhamID, int soluongmoi)
        //{
        //    // tìm carditem muon sua
        //    List<Cartitem> giohang = Session["giohang"] as List<Cartitem>;
        //    Cartitem itemSua = giohang.FirstOrDefault(m => m.MaSP == MaSP);
        //    if (itemSua != null)
        //    {
        //        itemSua.SoLuong = soluongmoi;
        //    }
        //    return RedirectToAction("Index");

        //}
        //public RedirectToRouteResult XoaKhoiGio(int SanPhamID)
        //{
        //    List<Cartitem> giohang = Session["giohang"] as List<Cartitem>;
        //    Cartitem itemXoa = giohang.FirstOrDefault(m => m.MaSP == MaSP);
        //    if (itemXoa != null)
        //    {
        //        giohang.Remove(itemXoa);
        //    }
        //    return RedirectToAction("Index");
        //}
    }
}