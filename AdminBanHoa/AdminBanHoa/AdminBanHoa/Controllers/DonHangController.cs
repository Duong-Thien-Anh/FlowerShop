using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdminBanHoa.Models;

namespace AdminBanHoa.Controllers
{
    public class DonHangController : Controller
    {
        DBBANHOADataContext db = new DBBANHOADataContext();
        DonHangAPIController APIDonHang = new DonHangAPIController();
        //
        // GET: /DonHang/
        public ActionResult DonHangPartial()
        {
            return View();
        }
        public ActionResult Index()
        {
            return View(db.DonHangs.ToList());
        }
        public ActionResult Edit(string id)
        {
            if (String.IsNullOrEmpty(id))
                return RedirectToAction("Index");
            return View(db.DonHangs.FirstOrDefault(t =>t.MaDH == id));
        }

        [HttpPost]
        public ActionResult Edit(DonHang dh)
        {
            //DonHang model = db.DonHangs.FirstOrDefault(t => t.MaDH == dh.MaDH);

            //if (dh.NgayGiaoHoa < DateTime.Now || dh.NgayGiaoHoa < dh.NgayDatHoa)
            //    return View(model);
            //model.NgayGiaoHoa = dh.NgayGiaoHoa;
            //if (String.IsNullOrEmpty(dh.TinhTrangDH))
            //    model.TinhTrangDH = "Đang Chờ Xử Lí";
            //else
            //    model.TinhTrangDH = dh.TinhTrangDH;

            //db.SubmitChanges();

            //return RedirectToAction("Index");

            bool check = APIDonHang.Update(dh.MaDH, dh.NgayGiaoHoa, dh.TinhTrangDH);
            if (check == true)
                return RedirectToAction("Index");
            else
                return View();
        }

        public ActionResult Details(string id)
        {
            if (String.IsNullOrEmpty(id))
                return RedirectToAction("Index");
            var JoinTable = db.CTDonHangs
                                .Join(db.SanPhams, ct => ct.MaSP, sp => sp.MaSP, (ct, sp) => new { ct, sp })
                                .Select(m => new ChiTietDH
                                {
                                    MaDH = m.ct.MaDH,
                                    MaSP = m.ct.MaSP,
                                    TenSP = m.sp.TenSP,
                                    HinhAnh = m.sp.HinhAnh,
                                    SL = m.ct.SL,
                                    ThanhTien = m.ct.ThanhTien
                                });
            var chiTiet = JoinTable.Where(j => j.MaDH == id);
            return View(chiTiet);
        }
        public ActionResult DetailsSP(string id)
        {
            return this.RedirectToAction("Details", "SanPham", new { id = id });
        }
	}
}