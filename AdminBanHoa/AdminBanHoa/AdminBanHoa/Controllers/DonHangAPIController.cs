using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AdminBanHoa.Models;

namespace AdminBanHoa.Controllers
{
    public class DonHangAPIController : ApiController
    {
        DBBANHOADataContext db = new DBBANHOADataContext();

        [HttpPut]
        public bool Update(string id, DateTime? NgayGiaoHoa, string TinhTrangDH)
        {
            try
            {
                DonHang dh = db.DonHangs.FirstOrDefault(t => t.MaDH == id);
                if (dh == null)
                    return false;

                if(NgayGiaoHoa != null && NgayGiaoHoa > DateTime.Now && NgayGiaoHoa > dh.NgayDatHoa)
                {
                    dh.NgayGiaoHoa = NgayGiaoHoa;
                    dh.TinhTrangDH = "Thành Công";
                }

                if (dh.TinhTrangDH == "Thành Công")
                {
                    db.SubmitChanges();
                    return true;
                }
                    dh.TinhTrangDH = TinhTrangDH;



                db.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
