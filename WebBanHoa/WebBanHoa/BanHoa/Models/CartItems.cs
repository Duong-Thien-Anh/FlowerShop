using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BanHoa.Models;

namespace BanHoa.Models
{
    public class CartItems
    {
        DBBANHOADataContext db = new DBBANHOADataContext();
        public string sMaHoa { get; set; }
        public string sTenHoa { get; set; }
        public string sHinhAnh { get; set; }
        public double dDonGia { get; set; }
        public int iSoLuong { get; set; }
        public double dThanhTien
        {
            get { return iSoLuong * dDonGia; }
        }
        public CartItems(string MaHoa, int SoLuong)
        {
            sMaHoa = MaHoa;
            SanPham hoa = db.SanPhams.Single(s => s.MaSP.Equals(sMaHoa));
            sTenHoa = hoa.TenSP;
            sHinhAnh = hoa.HinhAnh;
            dDonGia = double.Parse(hoa.Gia.ToString());
            iSoLuong = SoLuong;
        }

    }
}