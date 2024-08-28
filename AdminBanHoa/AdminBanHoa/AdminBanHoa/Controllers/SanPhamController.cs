using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdminBanHoa.Models;

namespace AdminBanHoa.Controllers
{
    public class SanPhamController : Controller
    {
        DBBANHOADataContext db = new DBBANHOADataContext();
        //
        // GET: /SanPham/
        public ActionResult SanPhamPartial()
        {
            return View();
        }
        public ActionResult Index()
        {
            return View(db.SanPhams.ToList());
        }
        public ActionResult Details(string id)
        {
            if (String.IsNullOrEmpty(id))
                return RedirectToAction("Index");
            return View(db.SanPhams.FirstOrDefault(t => t.MaSP == id));
        }
        public ActionResult Edit(string id)
        {
            if (String.IsNullOrEmpty(id))
                return RedirectToAction("Index");
            var items = db.LoaiSanPhams.ToList();
            if( items != null)
            {
                ViewBag.data = items;
            }
            return View(db.SanPhams.FirstOrDefault(t => t.MaSP == id));
        }
        private string SaveImage(HttpPostedFileBase file)
        {
            // Tạo đường dẫn lưu trữ hình ảnh

            string imagePath = file.FileName;

            // Trả về đường dẫn hình ảnh
            return imagePath;
        }
        [HttpPost]
        public ActionResult Edit(SanPham sp, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                SanPham model = db.SanPhams.Where(t => t.MaSP.Equals(sp.MaSP)).FirstOrDefault();

                // Lấy thông tin hình ảnh cũ
                string oldImagePath = model.HinhAnh;

                // Kiểm tra xem người dùng có tải lên hình ảnh mới hay không
                if (Request.Files.Count > 0)
                {
                    // Lưu hình ảnh mới vào thư mục lưu trữ hình ảnh
                    string newImagePath = SaveImage(Request.Files["HinhAnh"]);

                   

                    // Cập nhật thông tin hình ảnh mới vào cơ sở dữ liệu
                    model.HinhAnh = newImagePath;

                    if (String.IsNullOrEmpty(newImagePath))
                        model.HinhAnh = oldImagePath;
                }

                string MaLoaiSP = Request.Form["TenLoaiSP"].ToString();

                model.TenNCC = sp.TenNCC;
                model.TenSP = sp.TenSP;
                model.MauSP = sp.MauSP;
                model.Gia = sp.Gia;
                model.MoTa = sp.MoTa;

                model.MaLoaiSP = MaLoaiSP;

                //model.HinhAnh = sp.HinhAnh;

                model.SoLuongTonKho = sp.SoLuongTonKho;

                if (sp.SoLuongTonKho > 0)
                    model.TinhTrang = "Còn hàng";
                else
                    model.TinhTrang = "Hết hàng";
                //db.SanPhams.InsertOnSubmit(sp);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return View(sp);
        }

        public ActionResult Create()
        {
            var data = db.LoaiSanPhams.ToList();
            List<SelectListItem> lstLoaiHoa = new List<SelectListItem>();
            foreach (var item in data)
            {
                lstLoaiHoa.Add(new SelectListItem { Text = item.TenLoaiSP, Value = item.MaLoaiSP.ToString() });
            }
            ViewBag.MaLoaiSP = lstLoaiHoa;
            return View();
            //var data = db.LoaiSanPhams.ToList();
            //ViewBag.MaLoaiSP = new SelectList(data, dataValueField: "MaLoaiSP", dataTextField: "TenLoaiSP");
            //return View();
        }

        [HttpPost]
        public ActionResult Create(SanPham sp)
        {

            if(ModelState.IsValid)
            {
                

                var MaSP = Request.Form["MaSP"].ToString();
                var MaLoaiSP = Request.Form["MaLoaiSP"].ToString();
                var TenNCC = Request.Form["TenNCC"].ToString();
                var TenSP = Request.Form["TenSP"].ToString();
                var MauSP = Request.Form["MauSP"].ToString();
                var Gia = Request.Form["Gia"];
                var MoTa = Request.Form["MoTa"].ToString();
                
                var SoLuongTonKho = Request.Form["SoLuongTonKho"];

                string newImagePath = SaveImage(Request.Files["HinhAnh"]);

                var check = db.SanPhams.FirstOrDefault(t => t.MaSP == MaSP);
                if (check != null)
                {
                    Session["AddError"] = "Thêm thất bại";
                    return RedirectToAction("Index");
                }
                else
                {
                    sp.MaSP = MaSP;
                    sp.TenSP = TenSP;
                    sp.MaLoaiSP = MaLoaiSP;
                    sp.MauSP = MauSP;
                    sp.Gia = double.Parse(Gia);
                    sp.MoTa = MoTa;
                    sp.HinhAnh = newImagePath;
                    sp.SoLuongTonKho = int.Parse(SoLuongTonKho);
                    if (sp.SoLuongTonKho > 0)
                        sp.TinhTrang = "Còn Hàng";

                    db.SanPhams.InsertOnSubmit(sp);
                    db.SubmitChanges();
                    return RedirectToAction("Index");
                }

            }
            return View();
        }
	}
}