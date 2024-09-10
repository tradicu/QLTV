using DATN_LiBrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace DATN_LiBrary.Controllers
{
    public class PhieuMuonAdminController : Controller
    {
        private readonly DoanContext _context;

        public PhieuMuonAdminController(DoanContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {

            var phieuMuons = _context.PhieuMuons?.Include(x => x.ManguoidungNavigation).Include(x => x.ManguoimuonNavigation).ToList() ?? new List<PhieuMuon>();

            // Giả sử giảng viên có thuộc tính đặc biệt để phân biệt với sinh viên
            var phieumuons = phieuMuons.Where(nd => nd.Trangthai == "PHIEUMUON").ToList();
            var hoanThanhs = phieuMuons.Where(nd => nd.Trangthai == "HOANTHANH").ToList();
            var daDuyets = phieuMuons.Where(nd => nd.Trangthai == "DADUYET").ToList();
            var khongDuyets = phieuMuons.Where(nd => nd.Trangthai == "KHONGDUYET").ToList();
            ViewBag.phieumuons = phieumuons;
            ViewBag.hoanThanhs = hoanThanhs;
            ViewBag.daDuyets = daDuyets;
            ViewBag.khongDuyets = khongDuyets;
            return View();
        }
        public async Task<IActionResult> Details(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var lstphieumuon = await _context.PhieuMuons
               .Where(p => p.Maphieumuon == id)
               .Include(p => p.ManguoimuonNavigation)
               .Include(p => p.ManguoidungNavigation)
               .Include(p => p.Chitietphieumuons)
                   .ThenInclude(ct => ct.MacthdNavigation)
                       .ThenInclude(s => s.MasachNavigation)
               .ToListAsync();

            return View(lstphieumuon);



        }
        // GET: PhieuMuon/Create
        public IActionResult Create()
        {
            // Lấy danh sách người dùng, người mượn và sách từ cơ sở dữ liệu
            var nguoiDungs = _context.NguoiDungs.ToList();
            var nguoiMuons = _context.NguoiMuons.ToList();
            var chitietphieumuon = _context.Chitietphieumuons.Include(ct=>ct.MaphieumuonNavigation).ToList();
            // var chitietsach = _context.ChiTietSaches.ToList();
            var chitietsachList = (from cts in _context.ChiTietSaches
                                   join s in _context.Saches on cts.Masach equals s.Masach
                       
                                   select new
                                   {
                                       cts.Macthd,
                                       s.Nhande
                                   }).ToList<dynamic>();
            var sachs = _context.Saches.ToList();
            

            // Tạo một đối tượng chứa tất cả thông tin cần thiết và truyền nó vào view
            var model = new Tuple<List<NguoiDung>, List<NguoiMuon>, List<Chitietphieumuon>, List<dynamic>, List<Sach>>(nguoiDungs, nguoiMuons, chitietphieumuon, chitietsachList, sachs);

            return View(model);
        }
        // Method sinh khóa cho Maphieumuon
        

        // Method sinh khóa cho Idctpm
        private string GenerateIdctpm()
        {
            return "CTPM" + Guid.NewGuid().ToString().Substring(0, 8);
        }

        //// POST: PhieuMuon/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string manguoidung, string manguoimuon, List<string> machitietsach, DateTime ngaytao, DateTime ngaymuon, DateTime ngaytra, /*string tensach, string tennguoidung, string khoa, string khoas, string tennguoimuon,*/ string trangthai)
        {
            if (ModelState.IsValid)
            {

                string maphieumuon = GenerateMaphieumuon();
                string idctpm = GenerateIdctpm();
                var phieuMuon = new PhieuMuon
                {
                    Maphieumuon = maphieumuon,
                    Manguoidung = manguoidung,
                    Manguoimuon = manguoimuon,
                    Trangthai = "PHIEUMUON",
                    Ngaytao = ngaytao,
                    Ngaymuon = ngaymuon,
                    Ngaytra = ngaytra
                };

                _context.PhieuMuons.Add(phieuMuon);
                await _context.SaveChangesAsync();

                foreach (var macthd in machitietsach)
                {
                    var chiTietPhieuMuon = new Chitietphieumuon
                    {
                        Maphieumuon = phieuMuon.Maphieumuon,
                        Macthd = macthd,
                        Idctpm = GenerateIdctpm() // Mỗi chi tiết cần một Idctpm duy nhất
                    };

                    _context.Chitietphieumuons.Add(chiTietPhieuMuon);
                }

                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "PhieuMuonAdmin");
            }

            // Lấy danh sách người dùng, người mượn và sách từ cơ sở dữ liệu
            var nguoiDungs = _context.NguoiDungs.ToList();
            var nguoiMuons = _context.NguoiMuons.ToList();
            var chitietphieumuon = _context.Chitietphieumuons.Include(ct => ct.MaphieumuonNavigation).ToList();
            // var chitietsach = _context.ChiTietSaches.ToList();
            var chitietsachList = (from cts in _context.ChiTietSaches
                                   join s in _context.Saches on cts.Masach equals s.Masach
                                   select new
                                   {
                                       cts.Macthd,
                                       s.Nhande
                                   }).ToList<dynamic>();
            var sachs = _context.Saches.ToList();


            // Tạo một đối tượng chứa tất cả thông tin cần thiết và truyền nó vào view
            var model = new Tuple<List<NguoiDung>, List<NguoiMuon>, List<Chitietphieumuon>, List<dynamic>, List<Sach>>(nguoiDungs, nguoiMuons, chitietphieumuon, chitietsachList, sachs);

            return View(model);
        }
        private string GenerateMaphieumuon()
        {
            var lastPhieuMuon = _context.PhieuMuons.OrderByDescending(p => p.Maphieumuon).FirstOrDefault();
            int counter = 1;
            if (lastPhieuMuon != null)
            {
                if (int.TryParse(lastPhieuMuon.Maphieumuon.Substring(2), out int lastNumber))
                {
                    counter = lastNumber + 1;
                }
                else
                {
               
                }
            }
            return "PM" + counter.ToString("D3");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(string id)
        {
            string userId = HttpContext.Session.GetString("Manguoidung");

            var phieuMuon = await _context.PhieuMuons
                .Include(p => p.ManguoimuonNavigation)
                .Include(p => p.Chitietphieumuons)
                    .ThenInclude(ct => ct.MacthdNavigation)
                        .ThenInclude(s => s.MasachNavigation)
                .FirstOrDefaultAsync(p => p.Maphieumuon == id);

            if (phieuMuon == null)
            {
                return Json(new { success = false });
            }

            // Cập nhật trạng thái của phiếu mượn
            phieuMuon.Trangthai = "HOANTHANH"; // Thay đổi trạng thái theo yêu cầu của bạn
            phieuMuon.Manguoidung = userId;
            phieuMuon.Ngaytra = DateTime.Now;
            bool isLate = DateTime.Now > phieuMuon.Ngaytra;

            try
            {
                _context.Update(phieuMuon);
                await _context.SaveChangesAsync();
                return Json(new { success = true, isLate = isLate }); 
            }
            catch (Exception ex)
            {
                _context.Update(phieuMuon);
                await _context.SaveChangesAsync();
                return Json(new { success = false, error = ex.Message });
            }

        }
        public async Task<IActionResult> CancelApproved(string id)
        {
            string userId = HttpContext.Session.GetString("Manguoidung");

            var phieuMuon = await _context.PhieuMuons
                .Include(p => p.ManguoimuonNavigation)
                .Include(p => p.Chitietphieumuons)
                    .ThenInclude(ct => ct.MacthdNavigation)
                        .ThenInclude(s => s.MasachNavigation)
                .FirstOrDefaultAsync(p => p.Maphieumuon == id);

            if (phieuMuon == null)
            {
                return Json(new { success = false });
            }

            // Cập nhật trạng thái của phiếu mượn
            phieuMuon.Trangthai = "DAHUY";
            phieuMuon.Manguoidung = userId;


            try
            {
                _context.Update(phieuMuon);
                await _context.SaveChangesAsync();
                return Json(new { success = true }); 
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }
            public async Task<IActionResult> SubmitPhieuMuon(string id)
            {
                string userId = HttpContext.Session.GetString("Manguoidung");

                var phieuMuon = await _context.PhieuMuons
                    .Include(p => p.ManguoimuonNavigation)
                    .Include(p => p.Chitietphieumuons)
                        .ThenInclude(ct => ct.MacthdNavigation)
                            .ThenInclude(s => s.MasachNavigation)
                    .FirstOrDefaultAsync(p => p.Maphieumuon == id);

                if (phieuMuon == null)
                {
                    return Json(new { success = false });
                }

              
                phieuMuon.Trangthai = "PHIEUMUON";
                phieuMuon.Manguoidung = userId;


                try
                {
                    _context.Update(phieuMuon);
                    await _context.SaveChangesAsync();
                    return Json(new { success = true }); 
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, error = ex.Message });
                }

            }
        }
    

}
