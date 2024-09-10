using DATN_LiBrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DATN_LiBrary.Controllers
{
    public class QlNguoiDungController : Controller
    {
        private readonly DoanContext _context;

        public QlNguoiDungController(DoanContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var lstnguoidung = await _context.NguoiMuons
                                   .Include(nm => nm.ManguoidungNavigation)
                                   .Where(nm => nm.ManguoidungNavigation.Id == "RU03")
                                   .ToListAsync();
            return View(lstnguoidung);
            //var nguoiMuons = await _context.NguoiMuons.ToListAsync();
            //return View(nguoiMuons);
        }
        public async Task<IActionResult> Index1()
        {
            var lstnguoidung = await _context.NguoiDungs
                                   
                                   .Where(nm => nm.Id == "RU02")
                                   .ToListAsync();
            return View(lstnguoidung);
            //var nguoiMuons = await _context.NguoiMuons.ToListAsync();
            //return View(nguoiMuons);
        }
        public async Task<IActionResult> Details(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nguoiDung = await _context.NguoiDungs.FirstOrDefaultAsync(m => m.Manguoidung == id);
            if (nguoiDung == null)
            {
                return NotFound();
            }

            return View(nguoiDung);
        }
        public async Task<IActionResult> Details1(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nguoiDung = await _context.NguoiDungs.FirstOrDefaultAsync(m => m.Manguoidung == id);
            if (nguoiDung == null)
            {
                return NotFound();
            }

            return View(nguoiDung);
        }
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nguoiDung = await _context.NguoiDungs.FirstOrDefaultAsync(m => m.Manguoidung == id);
            if (nguoiDung == null)
            {
                return NotFound();
            }

            return View(nguoiDung);
        }

        // POST: NguoiDung/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string MaNguoiDung, [Bind("Manguoidung,Ten,Gioitinh,Email,Matkhau,Sodienthoai,Diachi,Ngaysinh")] NguoiDung nguoiDung)
        {
           
            try
            {
                if (MaNguoiDung != nguoiDung.Manguoidung)
                {
                    return NotFound();
                }
                _context.Update(nguoiDung);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NguoiDungExists(nguoiDung.Manguoidung))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return View(nguoiDung);
        }
        public async Task<IActionResult> Edit1(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nguoiDung = await _context.NguoiDungs.FirstOrDefaultAsync(m => m.Manguoidung == id);
            if (nguoiDung == null)
            {
                return NotFound();
            }

            return View(nguoiDung);
        }

        // POST: NguoiDung/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit1(string MaNguoiDung, [Bind("Manguoidung,Ten,Gioitinh,Email,Matkhau,Sodienthoai,Diachi,Ngaysinh")] NguoiDung nguoiDung)
        {

            try
            {
                if (MaNguoiDung != nguoiDung.Manguoidung)
                {
                    return NotFound();
                }
                _context.Update(nguoiDung);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NguoiDungExists(nguoiDung.Manguoidung))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return View(nguoiDung);
        }

        private bool NguoiDungExists(string id)
        {
            return _context.NguoiDungs.Any(e => e.Manguoidung == id);
        }
        public IActionResult Create()
        {
            var nguoiDungs = _context.NguoiDungs.ToList();
            var nguoiMuons = _context.NguoiMuons.ToList();
            var model = new Tuple<List<NguoiDung>, List<NguoiMuon>>(nguoiDungs, nguoiMuons);
            var latestND = _context.NguoiDungs.OrderByDescending(b => b.Manguoidung).FirstOrDefault();
            string newND;

            if (latestND != null)
            {
                // Tách phần số và tăng lên 1
                int latestNumber = int.Parse(latestND.Manguoidung.Substring(2));
                newND = "ND" + (latestNumber + 1).ToString().PadLeft(3, '0');
            }
            else
            {
                // Bắt đầu từ MS100 nếu không có mã sách nào
                newND = "MS100";
            }

            ViewBag.newND = newND;

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Manguoidung,Ten,Gioitinh,Email,Matkhau,Sodienthoai,Diachi,Ngaysinh")] NguoiDung nguoiDung, string manguoimuon)
        {
            var nguoiDungs = _context.NguoiDungs.ToList();
            var nguoiMuons = _context.NguoiMuons.ToList();

            
            if (nguoiDung.Manguoidung == null)
            {
                // Nếu là null, tạo mã người dùng tự động
                var latestNguoiDung = _context.NguoiDungs.OrderByDescending(nd => nd.Manguoidung).FirstOrDefault();
                int newId = (latestNguoiDung != null) ? int.Parse(latestNguoiDung.Manguoidung.Substring(2)) + 1 : 1;
                nguoiDung.Manguoidung = "ND" + newId.ToString().PadLeft(3, '0');
            }
            nguoiDung.Id = "RU03";
            nguoiDung.Trangthai = "ACTIVE";
         
            
            // Thêm người dùng mới vào cơ sở dữ liệu
            _context.Add(nguoiDung);
            await _context.SaveChangesAsync();

            var nguoiMuonToUpdate = nguoiMuons.FirstOrDefault(nm => nm.Manguoimuon == manguoimuon);
            if (nguoiMuonToUpdate != null)
            {
                nguoiMuonToUpdate.Manguoidung = nguoiDung.Manguoidung;
                _context.NguoiMuons.Update(nguoiMuonToUpdate);
                await _context.SaveChangesAsync();
            }
            await _context.SaveChangesAsync();
            //var existingNguoiMuon = _context.NguoiMuons.SingleOrDefault(nm => nm.Manguoimuon == manguoimuon);
            //if (existingNguoiMuon == null)
            //{
            //    var nguoiMuon = new NguoiMuon
            //    {
            //        Manguoimuon = manguoimuon,
            //        Manguoidung = nguoiDung.Manguoidung


            //    };

            //    // Thêm người mượn mới vào danh sách
            //    nguoiMuons.Add(nguoiMuon);

            //    // Cập nhật lại cơ sở dữ liệu
            //    _context.NguoiMuons.UpdateRange(nguoiMuons);
            //    await _context.SaveChangesAsync();
            //}

            var model = new Tuple<List<NguoiDung>, List<NguoiMuon>>(nguoiDungs, nguoiMuons);
            return View(model);
        }
        public IActionResult Create1()
        {
            var latestND = _context.NguoiDungs.OrderByDescending(b => b.Manguoidung).FirstOrDefault();
            string newND;

            if (latestND != null)
            {
                
                int latestNumber = int.Parse(latestND.Manguoidung.Substring(2));
                newND = "ND" + (latestNumber + 1).ToString().PadLeft(3, '0');
            }
            else
            {
                
                newND = "ND001";
            }

            ViewBag.newND = newND;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create1([Bind("Manguoidung,Ten,Gioitinh,Email,Matkhau,Sodienthoai,Diachi,Ngaysinh")] NguoiDung nguoiDung)
        {
            if (ModelState.IsValid)
            {
                
                if (string.IsNullOrEmpty(nguoiDung.Manguoidung))
                {
                    var latestNguoiDung = _context.NguoiDungs.OrderByDescending(nd => nd.Manguoidung).FirstOrDefault();
                    int newId = (latestNguoiDung != null) ? int.Parse(latestNguoiDung.Manguoidung.Substring(2)) + 1 : 1;
                    nguoiDung.Manguoidung = "ND" + newId.ToString().PadLeft(3, '0');
                }

                
                nguoiDung.Id = "RU02"; 
                nguoiDung.Trangthai = "ACTIVE"; 

                
                _context.NguoiDungs.Add(nguoiDung);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index1"); 
            }

            
            return View(nguoiDung);
        }
    }
}
