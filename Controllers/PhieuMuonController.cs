using DATN_LiBrary.Models;
using DATN_LiBrary.ModelViews;
using Microsoft.AspNetCore.Mvc;
using DATN_Library.Extension;
using Microsoft.EntityFrameworkCore;

namespace DATN_LiBrary.Controllers
{
    public class PhieuMuonController : Controller
    {
        private readonly DoanContext _context;

        public PhieuMuonController(DoanContext context)
        {
            _context = context;
        }

       

        public async Task<IActionResult> Index()
        {
            var Manguoidung = HttpContext.Session.GetString("Manguoidung");
            var customerUser = await _context.NguoiMuons.FindAsync(Manguoidung);
            return View(customerUser);
        }


        [HttpPost]
        public IActionResult Index(NguoiMuon customer,DateTime ngaymuon,DateTime ngaytra)
        {
            
                var cart = HttpContext.Session.Get<List<CartItem>>("GioHang");

                if (ModelState.IsValid)
                {
                //Khởi tạo đơn hàng
                 
                    PhieuMuon phieudat = new PhieuMuon();
                    phieudat.Maphieumuon = GenerateNextMaPhieuDat();
                    phieudat.Manguoimuon = customer.Manguoimuon;
                    phieudat.Ngaytao = DateTime.Now.Date;
                    phieudat.Trangthai = "PHIEUDAT";
                    phieudat.Ngaymuon= ngaymuon;
                    phieudat.Ngaytra = ngaytra;
            
                    _context.Add(phieudat);
                    _context.SaveChanges();

                    foreach (var item in cart)
                    {
                        
                        Chitietphieumuon chiTietPhieuMuon = new Chitietphieumuon();
                        chiTietPhieuMuon.Idctpm= "CTPM" + Guid.NewGuid().ToString().Substring(0, 8);
                        chiTietPhieuMuon.Maphieumuon = phieudat.Maphieumuon;
                        chiTietPhieuMuon.Macthd = _context.ChiTietSaches
                                          .Where(x => x.Masach == item.masach.Masach)
                                          .Select(x => x.Macthd)
                                          .Except(_context.Chitietphieumuons
                                                         .Where(y => y.MaphieumuonNavigation.Trangthai == "PHIEUDAT")
                                                         .Select(y => y.Macthd))
                                          .FirstOrDefault();

                    _context.Add(chiTietPhieuMuon);

                    //var masach = _context.Saches.AsNoTracking().SingleOrDefault(x => x.Masach == item.masach.Masach);
                    //masach.Soluong = masach.Soluong - item.soLuong;
                    //_context.Update(masach);
                }
                    _context.SaveChanges();
                    HttpContext.Session.Remove("GioHang");
                    return RedirectToAction("Index", "Home", new { area = "" });
                }
                return View();
        }
        //[HttpGet]
        //public async Task<IActionResult> LichSu()
        //{
        //    var manguoidung = HttpContext.Session.GetString("Manguoidung");
        //    var customerUser = await _context.NguoiDungs.Where(n => n.Manguoidung == manguoidung).FirstOrDefaultAsync();
        //    return View(customerUser);

        //}

  
        public async Task<IActionResult> LichSu()
        {
            var manguoimuon = HttpContext.Session.GetString("Manguoimuon");
            var phieuMuon = await _context.PhieuMuons
                                           .Where(don => don.Manguoimuon== manguoimuon)
                                           .ToListAsync();
            return View(phieuMuon);
        }

        private string GenerateNextMaPhieuDat()
        {
            
            var lastPhieuMuon = _context.PhieuMuons.OrderByDescending(t => t.Maphieumuon).FirstOrDefault();

            
            if (lastPhieuMuon == null)
            {
                return "PM001";
            }

            
            int nextNumber = int.Parse(lastPhieuMuon.Maphieumuon.Substring(2)) + 1;

           
            string nextMaPhieuMuon = "PM" + nextNumber.ToString("D3");

            return nextMaPhieuMuon;
        }
        public async Task<IActionResult> ChiTiet(string id)
        {
            
            var chiTietPhieuMuon = await _context.Chitietphieumuons
                                                 .Include(ct => ct.MaphieumuonNavigation)
                                                 .ThenInclude(pm => pm.ManguoidungNavigation)
                                                 .Include(ct => ct.MacthdNavigation)
                                                 .ThenInclude(cthd => cthd.MasachNavigation)
                                                 .Where(ct => ct.Maphieumuon == id)
                                                 .ToListAsync();

            return View(chiTietPhieuMuon);
        }
  
    }

}
