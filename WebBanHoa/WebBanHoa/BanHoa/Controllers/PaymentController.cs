using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BanHoa.Models;

namespace BanHoa.Controllers
{
    public class PaymentController : Controller
    {
        DBBANHOADataContext db = new DBBANHOADataContext();
        //
        // GET: /Payment/
        public ActionResult Index()
        {
            if (Session["PaymentSuccess"] == null)
                return RedirectToAction("LogInAndRegister", "User");
            if(Session["TenKH"] != null)
                return View();
            return RedirectToAction("LogInAndRegister", "User");
        }

        [HttpPost]
        public ActionResult Index(string temp)
        {
            var lstCart = (List<CartItems>)Session["GioHang"];

            DonHang dh = new DonHang();

            int count = (from u in db.DonHangs select u).Count() + 1;

            

            dh.MaDH = "DH" + count.ToString();

            string MaKH = Session["MaKH"].ToString();
            dh.MaKH = MaKH;

            dh.NgayDatHoa = DateTime.Now;

            KhachHang kh = db.KhachHangs.FirstOrDefault(s => s.MaKH == MaKH);
            if (Convert.ToString(Request.Form["txtDiaChi"]) == null)
                dh.DiaChiGiaoHoa = kh.DiaChi;
            else
                dh.DiaChiGiaoHoa = Request.Form["txtDiaChi"].ToString();
            
            dh.NgayGiaoHoa = null;
            dh.TinhTrangDH = "Đang chờ xử lí";
            dh.PhuongThucTT = "Tiền Mặt - Thanh toán khi nhận hàng";

            db.DonHangs.InsertOnSubmit(dh);
            db.SubmitChanges();

            string MaDH = dh.MaDH;

            List<CTDonHang> lstCTDH = new List<CTDonHang>(); 

            foreach(var item in lstCart)
            {
                CTDonHang ct = new CTDonHang();
                ct.MaDH = MaDH;
                ct.MaSP = item.sMaHoa;
                ct.SL = item.iSoLuong;
                ct.ThanhTien = item.dThanhTien;

                lstCTDH.Add(ct);

                SanPham sp = db.SanPhams.FirstOrDefault(t => t.MaSP == item.sMaHoa);
                sp.SoLuongTonKho -= item.iSoLuong;
            }


            db.CTDonHangs.InsertAllOnSubmit(lstCTDH);
            db.SubmitChanges();

            Session.Remove("GioHang");

            Session["PaymentSuccess"] = "1";

            return View();
        }

        public ActionResult DanhSachDonHang()
        {
            if(Session["TenKH"] == null)
                return RedirectToAction("LogInAndRegister", "User");
            var lstDH = db.DonHangs.Where(t => t.MaKH == Session["MaKH"].ToString()).ToList();


            return View(lstDH);
        }

        public ActionResult DanhSachCTDonHang(string id)
        {

            if (id == null)
                return RedirectToAction("LogInAndRegister", "User");
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

        public ActionResult Details(string id)
        {
            return this.RedirectToAction("Details", "Product", new { id = id});
        }

        public ActionResult HuyDonHang(string id)
        {
            if (id == null)
                return RedirectToAction("LogInAndRegister", "User");
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
            Session["MaDH"] = id;
            return View(chiTiet);

        }
        [HttpPost]
        public ActionResult HuyDonHang(string id,string temp)
        {
            DonHang dh = db.DonHangs.FirstOrDefault(t => t.MaDH == id);

            List<CTDonHang> ct = db.CTDonHangs.Where(t => t.MaDH == id).ToList();

            foreach(CTDonHang item in ct)
            {
                SanPham sp = db.SanPhams.FirstOrDefault(t => t.MaSP == item.MaSP);
                sp.SoLuongTonKho += item.SL == null ? 0 : item.SL.Value;
            }

            

            dh.TinhTrangDH = "Đã Huỷ";
            db.SubmitChanges();
            return RedirectToAction("DanhSachDonHang");
        }
	}
}