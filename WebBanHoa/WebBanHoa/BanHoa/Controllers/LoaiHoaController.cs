using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BanHoa.Models;

namespace BanHoa.Controllers
{
    public class LoaiHoaController : Controller
    {
        DBBANHOADataContext db = new DBBANHOADataContext();
        //
        // GET: /LoaiHoa/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LoaiHoaPartial()
        {
            var lstLoai = db.LoaiSanPhams.OrderBy(t => t.TenLoaiSP).ToList();
            return View(lstLoai);
        }
        public ActionResult LoaiHoaTheoMa(string MaLH)
        {
            
            var list = db.SanPhams.Where(t => t.MaLoaiSP == MaLH && t.SoLuongTonKho >0).ToList();
            if (list.Count == 0)
            {
                ViewBag.Loai = "Không tồn tại hoa theo loài hoa này!";
            }
            return View(list);
        }
	}
}