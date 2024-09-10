using DATN_LiBrary.Models;
using DATN_LiBrary.ModelViews;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Net;
using X.PagedList;
namespace DATN_LiBrary.Controllers
{
    public class HomeController : Controller
    {
        DoanContext db = new DoanContext();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult tracuu(int? page)
        {
            var lstsanpham = db.Saches.Include(x => x.Mtgs).Include(x => x.MnxbNavigation).Include(x => x.Matheloais).AsNoTracking().OrderBy(x => x.Nhande);
            int pageSize = 3;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;

            PagedList<Sach> lst = new PagedList<Sach>(lstsanpham, pageNumber, pageSize);

            var sachList = db.Saches
                .Include(s => s.ChiTietSaches)
                .ThenInclude(cts => cts.Chitietphieumuons)
                .ThenInclude(ctpm => ctpm.MaphieumuonNavigation)
                .ToList();

            var sachWithCounts = sachList.Select(s => new SachWithCounts
            {
                SachId = s.Masach,
                TenSach = s.Nhande,
                SoLuongChuaMuon = s.ChiTietSaches.Count(cts => !cts.Chitietphieumuons.Any() || !cts.Chitietphieumuons.Any(ctpm => ctpm.MaphieumuonNavigation.Trangthai == "PHIEUMUON" || ctpm.MaphieumuonNavigation.Trangthai == "PHIEUDAT"|| ctpm.MaphieumuonNavigation.Trangthai == "DADUYET")),
                SoLuongDangMuon = s.ChiTietSaches.Count(cts => cts.Chitietphieumuons.Any(ctpm => ctpm.MaphieumuonNavigation.Trangthai == "PHIEUMUON")),
                SoLuongDangDat = s.ChiTietSaches.Count(cts => cts.Chitietphieumuons.Any(ctpm => ctpm.MaphieumuonNavigation.Trangthai == "PHIEUDAT"))
            }).ToList();

            return View(new Tuple<PagedList<Sach>, List<SachWithCounts>>(lst, sachWithCounts));
        }

        

        public IActionResult chitietsach(string masach)
        {
            var sp = db.Saches.Include(x => x.Mtgs).Include(x => x.MnxbNavigation).Include(x => x.Matheloais).Include(x => x.Maquyens).AsNoTracking().OrderBy(x => x.Nhande).SingleOrDefault(x => x.Masach==masach);
            var anhsp = db.Saches.Include(x => x.Anh).Where(x => x.Masach == masach);
            ViewBag.anhsp = anhsp;
            return View(sp);
        }
        public IActionResult SanPhamTheoTheLoai(int? page, string matheloai)
        {
            int pageSize = 3;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            List<Sach> lstSach = db.Saches.Include(x => x.Matheloais).Where(sach => sach.Matheloais.Any
            (theloai => theloai.Matheloai.Equals(matheloai))).ToList();
            PagedList<Sach> lst = new PagedList<Sach>(lstSach, pageNumber, pageSize);
            ViewBag.matheloai = matheloai;
            return View(lst);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Login(string email, string matkhau)
        {
           
            var user = db.NguoiDungs.SingleOrDefault(x => x.Email.Trim().ToLower() == email.Trim().ToLower() && x.Matkhau == matkhau);

            if (user != null)
            {
               
                HttpContext.Session.SetString("Manguoidung", user.Manguoidung.ToString());
                HttpContext.Session.SetString("Ten", user.Ten.ToString());
                HttpContext.Session.SetString("Email", user.Email.Trim().ToLower());

                
                var checkAcount = db.NguoiDungs.SingleOrDefault(x => x.Email.Trim().ToLower() == email.Trim().ToLower());

                if (checkAcount != null)
                {
                    HttpContext.Session.SetString("Manguoidung", checkAcount.Manguoidung.ToString());
                    HttpContext.Session.SetString("Id", checkAcount.Id.ToString());
                    var roleID = HttpContext.Session.GetString("Id");
                    var checkRole = db.QuyenNguoiDungs.SingleOrDefault(x => x.Id == roleID);

                    if (checkRole != null)
                    {
                       
                        if (checkRole.Id == "RU02")
                        {
                            try
                            {
                                _logger.LogInformation("Redirecting to Admin area.");
                                
                                return RedirectToAction("Index", "HomeAdmin", new { area = "Admin" });
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("Loi" + e);
                            }


                        }
                        if (checkRole.Id == "RU01")
                        {
                            try
                            {
                                _logger.LogInformation("Redirecting to Admin area.");
                               
                                return RedirectToAction("Index", "ThongKe", new { area = "AdminAdmin" });
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("Loi" + e);
                            }


                        }
                        else
                        {
                            var nguoimuon = db.NguoiMuons.SingleOrDefault(x => x.Manguoidung == user.Manguoidung);
                            if (nguoimuon != null)
                            {
                                HttpContext.Session.SetString("Khoa", nguoimuon.Khoa.ToString());
                                HttpContext.Session.SetString("Khoas", nguoimuon.Khoas.ToString());
                                HttpContext.Session.SetString("Manguoimuon", nguoimuon.Manguoimuon.ToString());
                            }
                            _logger.LogInformation("Redirecting to Home.");
                            
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }
            }

           
             ModelState.AddModelError("", "Đăng Nhập Thất Bại! Kiểm Lại Thông Tin Đăng Nhập");
            
            return RedirectToAction();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();//remove session
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ChangePassword(string matkhau)
        {
            
            var currentUser = db.NguoiDungs.SingleOrDefault(x => x.Manguoidung == HttpContext.Session.GetString("Manguoidung"));

            if (currentUser == null)
            {
              
                return RedirectToAction("Error");
            }

            try
            {
               
                currentUser.Matkhau = matkhau;

               
                db.Update(currentUser);
                db.SaveChanges();

                
                TempData["SuccessMessage"] = "Mật khẩu đã được thay đổi thành công.";
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Đã xảy ra lỗi khi thay đổi mật khẩu. Vui lòng thử lại sau.");
                
                _logger.LogError($"Lỗi khi thay đổi mật khẩu: {ex.Message}");
                return View();
            }
        }

        public IActionResult ChangePassword1()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ChangePassword1(string matkhau)
        {
            
            var currentUser = db.NguoiDungs.SingleOrDefault(x => x.Manguoidung == HttpContext.Session.GetString("Manguoidung"));

            if (currentUser == null)
            {
                
                return RedirectToAction("Error");
            }

            try
            {
               
                currentUser.Matkhau = matkhau;

              
                db.Update(currentUser);
                db.SaveChanges();

                
                TempData["SuccessMessage"] = "Mật khẩu đã được thay đổi thành công.";
                return RedirectToAction("Index", "ThongKe");
            }
            catch (Exception ex)
            {
                
                ModelState.AddModelError("", "Đã xảy ra lỗi khi thay đổi mật khẩu. Vui lòng thử lại sau.");
               
                _logger.LogError($"Lỗi khi thay đổi mật khẩu: {ex.Message}");
                return View();
            }
        }
        public async Task<IActionResult> GiangVien()
        {
            var users = await db.NguoiDungs
                        .Where(u => u.Id == "RU02" || u.Id == "RU01")
                        .OrderBy(u => u.Id == "RU01" ? 0 : 1)
                        .ToListAsync();
            return View(users);
        }
        public IActionResult QuyDinh()
        {
            return View();
        }
        public IActionResult QuyTrinh()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Search(string searchCategory, string searchInput)
        {
            if (string.IsNullOrEmpty(searchInput))
            {
                return View(new List<Sach>()); // Trả về một danh sách rỗng nếu không có đầu vào tìm kiếm
            }

            IQueryable<Sach> query = db.Saches;

            switch (searchCategory)
            {
                case "title":
                    query = query.Where(s => s.Nhande.Contains(searchInput));
                    break;
                
            }

            var results = await query.ToListAsync();

            return View(results);
        }
    

}
}