using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BanHoa.Models;
using System.IO;

namespace BanHoa.Controllers
{
    public class ProductController : Controller
    {
        DBBANHOADataContext db = new DBBANHOADataContext();
        //
        // GET: /Product/
        public ActionResult Products()
        {
            var listProduct = db.SanPhams.Where(t => t.SoLuongTonKho > 0).ToList();
            return View(listProduct);
        }
        public ActionResult Details(string id = "")
        {
            if (String.IsNullOrEmpty(id))
                return RedirectToAction("Products");
            var query = from u in db.SanPhams
                        where u.MaSP.Equals(id)
                        select u;
                        

            var SP = query.AsEnumerable().Select(u =>
                         new SanPham
                         {
                             MaSP = u.MaSP,
                             //MaSP, MaLoaiSP, TenNCC, TenSP, MauSP, Gia, MoTa, HinhAnh, SoLuongTonKho, TinhTrang
                             MaLoaiSP = u.MaLoaiSP,
                             TenNCC = u.TenNCC,
                             TenSP = u.TenSP,
                             MauSP = u.MauSP,
                             Gia = u.Gia,
                             MoTa = u.MoTa,
                             HinhAnh = u.HinhAnh,
                             SoLuongTonKho = u.SoLuongTonKho,
                             TinhTrang = u.TinhTrang
                         }).FirstOrDefault();
            return View(SP);
        }
	}
}