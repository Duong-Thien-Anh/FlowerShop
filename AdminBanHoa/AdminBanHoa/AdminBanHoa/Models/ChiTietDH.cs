using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminBanHoa.Models
{
    public class ChiTietDH
    {
        public string MaDH { get; set; }

        public string MaSP { get; set; }
        public string TenSP { get; set; }

        public string HinhAnh { get; set; }
        public int? SL { get; set; }
        public double ThanhTien { get; set; }
    }
}