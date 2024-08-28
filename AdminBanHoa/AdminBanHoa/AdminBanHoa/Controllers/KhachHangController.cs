using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdminBanHoa.Models;
namespace AdminBanHoa.Controllers
{
    public class KhachHangController : Controller
    {
        DBBANHOADataContext db = new DBBANHOADataContext();
        //
        // GET: /KhachHang/
        public ActionResult KhachHangPartial()
        {
            return View();
        }
        public ActionResult Index()
        {
            return View(db.KhachHangs.ToList());
        }
	}
}