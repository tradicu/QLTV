using DATN_LiBrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DATN_LiBrary.ModelViews;
using X.PagedList;

namespace DATN_LiBrary.Controllers
{
    public class SachAdminController : Controller
    {
        private readonly DoanContext _context;

        public SachAdminController(DoanContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
          
                var sachList = _context.Saches
                    .Include(s => s.ChiTietSaches)
                    .ThenInclude(cts => cts.Chitietphieumuons)
                    .ThenInclude(ctpm => ctpm.MaphieumuonNavigation)
                    .ToList();

                var sachWithCounts = sachList.Select(s => new
                {
                    SachId = s.Masach,
                    TenSach = s.Nhande,
                    SoLuongChuaMuon = s.ChiTietSaches.Count(cts =>!cts.Chitietphieumuons.Any() ||!cts.Chitietphieumuons.Any(ctpm =>ctpm.MaphieumuonNavigation.Trangthai == "PHIEUMUON" ||ctpm.MaphieumuonNavigation.Trangthai == "PHIEUDAT")),
                    SoLuongDangMuon = s.ChiTietSaches.Count(cts => cts.Chitietphieumuons.Any(ctpm => ctpm.MaphieumuonNavigation.Trangthai == "PHIEUMUON")),
                    SoLuongDangDat = s.ChiTietSaches.Count(cts => cts.Chitietphieumuons.Any(ctpm => ctpm.MaphieumuonNavigation.Trangthai == "PHIEUDAT"))
                }).ToList();

                return View(sachWithCounts);
        }
        public IActionResult Index1()
        {

            var sachList = _context.Saches
                .Include(s => s.ChiTietSaches)
                .ThenInclude(cts => cts.Chitietphieumuons)
                .ThenInclude(ctpm => ctpm.MaphieumuonNavigation)
                .ToList();

            var sachWithCounts = sachList.Select(s => new
            {
                SachId = s.Masach,
                TenSach = s.Nhande,
                SoLuongChuaMuon = s.ChiTietSaches.Count(cts => !cts.Chitietphieumuons.Any() || !cts.Chitietphieumuons.Any(ctpm => ctpm.MaphieumuonNavigation.Trangthai == "PHIEUMUON" || ctpm.MaphieumuonNavigation.Trangthai == "PHIEUDAT")),
                SoLuongDangMuon = s.ChiTietSaches.Count(cts => cts.Chitietphieumuons.Any(ctpm => ctpm.MaphieumuonNavigation.Trangthai == "PHIEUMUON")),
                SoLuongDangDat = s.ChiTietSaches.Count(cts => cts.Chitietphieumuons.Any(ctpm => ctpm.MaphieumuonNavigation.Trangthai == "PHIEUDAT"))
            }).ToList();

            return View(sachWithCounts);
        }

        public async Task<IActionResult> Details(string? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

            var lstsach = await _context.Saches
                .Include(p => p.Mtgs)
                .Include(p => p.ChiTietSaches)
                .Include(ct => ct.MnxbNavigation)
                .Include(s => s.Matheloais)
                .FirstOrDefaultAsync(p => p.Masach == id);
            if (lstsach == null)
            {
                return NotFound();
            }
            return View(new List<Sach> { lstsach });
            }
        
        public IActionResult ChiTiet(string id)
        {
          
            var sach = _context.Saches
                         .Include(s => s.ChiTietSaches)
                         
                         .ThenInclude(cts => cts.Chitietphieumuons)
                         .ThenInclude(ctpm => ctpm.MaphieumuonNavigation)
                         .Include(s => s.ChiTietSaches)
                         .ThenInclude(lt => lt.MltNavigation)
                         
                         .FirstOrDefault(s => s.Masach == id);

            
            if (sach == null)
            {
                return NotFound();
            }
            
           
            var chiTietSachWithStatus = sach.ChiTietSaches.Select(cts =>
            {
               
                var trangThai = cts.Chitietphieumuons.Any(ctpm => ctpm.MaphieumuonNavigation.Trangthai == "PHIEUMUON") ? "PHIEUMUON" :
                                cts.Chitietphieumuons.Any(ctpm => ctpm.MaphieumuonNavigation.Trangthai == "PHIEUDAT") ? "PHIEUDAT" :
                                "NULL";

                
                return new Tuple<ChiTietSach, string>(cts, trangThai);
            }).ToList();

            
            var viewModel = new Tuple<Sach, List<Tuple<ChiTietSach, string>>>(sach, chiTietSachWithStatus);

           
            return View(viewModel);
        }
        public IActionResult ChiTiet1(string id)
        {

            var sach = _context.Saches
                         .Include(s => s.ChiTietSaches)

                         .ThenInclude(cts => cts.Chitietphieumuons)
                         .ThenInclude(ctpm => ctpm.MaphieumuonNavigation)
                         .Include(s => s.ChiTietSaches)
                         .ThenInclude(lt => lt.MltNavigation)

                         .FirstOrDefault(s => s.Masach == id);


            if (sach == null)
            {
                return NotFound();
            }

           
            var chiTietSachWithStatus = sach.ChiTietSaches.Select(cts =>
            {

                var trangThai = cts.Chitietphieumuons.Any(ctpm => ctpm.MaphieumuonNavigation.Trangthai == "PHIEUMUON") ? "PHIEUMUON" :
                                cts.Chitietphieumuons.Any(ctpm => ctpm.MaphieumuonNavigation.Trangthai == "PHIEUDAT") ? "PHIEUDAT" :
                                "NULL";


                return new Tuple<ChiTietSach, string>(cts, trangThai);
            }).ToList();

            
            var viewModel = new Tuple<Sach, List<Tuple<ChiTietSach, string>>>(sach, chiTietSachWithStatus);

           
            return View(viewModel);
        }
        // GET: SachAdmin/EditChiTiet/5
        public async Task<IActionResult> EditChiTiet(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chiTietSach = await _context.ChiTietSaches
                .Include(cts => cts.MasachNavigation)
                .Include(cts => cts.MltNavigation)
                .FirstOrDefaultAsync(m => m.Macthd == id);

            if (chiTietSach == null)
            {
                return NotFound();
            }

            ViewBag.SachList = await _context.Saches.ToListAsync();
            ViewBag.NoiLuuTruList = await _context.NoiLuuTrus.ToListAsync();

            return View(chiTietSach);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditChiTiet(string Macthd, [Bind("Macthd,Masach,Mlt")] ChiTietSach chiTietSach)
        {
            if (Macthd != chiTietSach.Macthd)
            {
                return NotFound();
            }

         
                try
                {
                    _context.Update(chiTietSach);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChiTietSachExists(chiTietSach.Macthd))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
               
            ViewBag.SachList = await _context.Saches.ToListAsync();
            ViewBag.NoiLuuTruList = await _context.NoiLuuTrus.ToListAsync();
            return View(chiTietSach);
        }
        
        private bool ChiTietSachExists(string id)
        {
            return _context.ChiTietSaches.Any(e => e.Macthd == id);
        }
        



        public async Task<IActionResult> Edit(string? id)
                {
            if (id == null)
            {
                return NotFound();
            }

            var sachToUpdate = await _context.Saches
                .Include(p => p.Mtgs)
                .Include(p => p.ChiTietSaches)
                .Include(ct => ct.MnxbNavigation)
                .Include(s => s.Matheloais)
                .FirstOrDefaultAsync(p => p.Masach == id);
            if (sachToUpdate == null)
            {
                return NotFound();
            }
            return View(sachToUpdate);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(string maSach, [Bind("Masach,Nhande,Namxuatban,Sotrang,Soluong,Anh")] Sach sach, string Nhaxuatban, string[] Tacgia, string[] Theloai, IFormFile hinhanhFile, string MoTa)
        {
            if (maSach != sach.Masach)
            {
                return NotFound();
            }

            var sachToUpdate = await _context.Saches
                .Include(p => p.Mtgs)
                .Include(p => p.ChiTietSaches)
                .Include(ct => ct.MnxbNavigation)
                .Include(s => s.Matheloais)
                .FirstOrDefaultAsync(p => p.Masach == maSach);

            if (sachToUpdate == null)
            {
                return NotFound();
            }

            try
            {
                sachToUpdate.Nhande = sach.Nhande;
                sachToUpdate.Namxuatban = sach.Namxuatban;
                sachToUpdate.Sotrang = sach.Sotrang;
                sachToUpdate.Soluong = sach.Soluong;
                if (sachToUpdate.MnxbNavigation?.Tnxb != Nhaxuatban)
                {
                    sachToUpdate.MnxbNavigation = await _context.NhaXuatBans.FirstOrDefaultAsync(nxb => nxb.Tnxb == Nhaxuatban);
                }
                // Cập nhật tác giả
                var currentTacgia = sachToUpdate.Mtgs.Select(tg => tg.Ttg).ToList();
                var newTacgia = Tacgia.Except(currentTacgia).ToList();
                var removedTacgia = currentTacgia.Except(Tacgia).ToList();

                foreach (var tacgia in newTacgia)
                {
                    var existingTacgia = await _context.TacGia.FirstOrDefaultAsync(tg => tg.Ttg == tacgia);
                    if (existingTacgia != null)
                    {
                        sachToUpdate.Mtgs.Add(existingTacgia);
                    }
                }

                foreach (var tacgia in removedTacgia)
                {
                    var existingTacgia = sachToUpdate.Mtgs.FirstOrDefault(tg => tg.Ttg == tacgia);
                    if (existingTacgia != null)
                    {
                        sachToUpdate.Mtgs.Remove(existingTacgia);
                    }
                }

                // Cập nhật thể loại
                var currentTheloai = sachToUpdate.Matheloais.Select(tl => tl.Tentheloai).ToList();
                var newTheloai = Theloai.Except(currentTheloai).ToList();
                var removedTheloai = currentTheloai.Except(Theloai).ToList();

                foreach (var theloai in newTheloai)
                {
                    var existingTheloai = await _context.Theloais.FirstOrDefaultAsync(tl => tl.Tentheloai == theloai);
                    if (existingTheloai != null)
                    {
                        sachToUpdate.Matheloais.Add(existingTheloai);
                    }
                }

                foreach (var theloai in removedTheloai)
                {
                    var existingTheloai = sachToUpdate.Matheloais.FirstOrDefault(tl => tl.Tentheloai == theloai);
                    if (existingTheloai != null)
                    {
                        sachToUpdate.Matheloais.Remove(existingTheloai);
                    }
                }


             
                if (hinhanhFile != null && hinhanhFile.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await hinhanhFile.CopyToAsync(memoryStream);
                        sachToUpdate.Anh = Convert.ToBase64String(memoryStream.ToArray());
                        
                    }
                }
                sachToUpdate.Mota = MoTa;


                _context.Update(sachToUpdate);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SachExists(sachToUpdate.Masach))
                {
                    return NotFound();
                }
                else
                {
                    return View(sach);
                }
            }
            return RedirectToAction("Index1" , "SachAdmin");
            
            
        }

        private bool SachExists(string id)
        {
            return _context.Saches.Any(e => e.Masach == id);
        }


        public IActionResult Create()
        {
            var lstsach = _context.Saches
                    .Include(p => p.Mtgs)
                    .Include(p => p.ChiTietSaches)
                    .Include(ct => ct.MnxbNavigation)
                    .Include(s => s.Matheloais)
                    .ToArray();
            var latestBook = _context.Saches.OrderByDescending(b => b.Masach).FirstOrDefault();
            string newMasach;

            if (latestBook != null)
            {
               
                int latestNumber = int.Parse(latestBook.Masach.Substring(2));
                newMasach = "S" + (latestNumber + 1).ToString().PadLeft(3, '0');
            }
            else
            {
                
                newMasach = "MS100";
            }

            ViewBag.NewMasach = newMasach;

            return View(lstsach);
        }
  

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Masach,Nhande,Namxuatban,Sotrang,Soluong,Anh")] Sach sach, string Nhaxuatban, string[] Tacgia, string[] Theloai, IFormFile hinhanhFile, IFormFile pdfFile, string MoTa)
        {
           
            
            var nxb = await _context.NhaXuatBans.FirstOrDefaultAsync(n => n.Tnxb == Nhaxuatban);
                if (nxb == null)
                {
                   
                    nxb = new NhaXuatBan { Tnxb = Nhaxuatban };
                    _context.NhaXuatBans.Add(nxb);
                    await _context.SaveChangesAsync(); 
                }
                sach.MnxbNavigation = nxb;

            

            var newTacgia = new List<TacGium>();
            var tacgiaString = string.Join(";", Tacgia);

            if (!string.IsNullOrEmpty(tacgiaString))
            {
                
                var tacgiaList = tacgiaString.Trim().Split(';').Select(name => name.Trim()).ToList();
               
                foreach (var tacgia in tacgiaList)
                {
                    if (!string.IsNullOrEmpty(tacgia))
                    {
                        var existingTacgia = await _context.TacGia.FirstOrDefaultAsync(tg => tg.Ttg == tacgia);
                        if (existingTacgia != null)
                        {
                            sach.Mtgs.Add(existingTacgia);
                        }
                        else
                        {
                            
                            var newTg = new TacGium { Ttg = tacgia };
                            _context.TacGia.Add(newTg);
                            await _context.SaveChangesAsync();
                            sach.Mtgs.Add(newTg);
                        }
                    }
                }
            }
            sach.Mota = MoTa;

            
            var newTheloais = new List<Theloai>();
            var theloaiString = string.Join(";", Theloai);


            if (!string.IsNullOrEmpty(theloaiString))
            {
                var theloaiList = theloaiString.Trim().Split(';').Select(t => t.Trim()).ToList();
                foreach (var theloai in theloaiList)
                {
                    var existingTheloai = await _context.Theloais.FirstOrDefaultAsync(tl => tl.Tentheloai == theloai);
                    if (existingTheloai != null)
                    {
                        
                        newTheloais.Add(existingTheloai);
                    }
                    else
                    {
                        
                        var newTheloai = new Theloai { Tentheloai = theloai };
                        _context.Theloais.Add(newTheloai);
                        
                        newTheloais.Add(newTheloai);
                    }
                }
            }

            
            foreach (var theloai in newTheloais)
            {
                //var sachTheLoai = new Sach_TheLoai
                //{
                //    Matheloai = theloai.Matheloai,
                //    Masaches = sach.Masach
                //};
                sach.Matheloais.Add(_context.Theloais.Find(theloai.Matheloai));
            }

            
            await _context.SaveChangesAsync();



            var lastChiTietSach = await _context.ChiTietSaches
                .OrderByDescending(cts => cts.Macthd)
                .FirstOrDefaultAsync();

            int lastNumber = 0;
            if (lastChiTietSach != null)
            {
                string lastMact = lastChiTietSach.Macthd;
                if (lastMact.StartsWith("CT") && int.TryParse(lastMact.Substring(2), out lastNumber))
                {
                    
                    lastNumber++;
                }
            }

            for (int i = 1; i <= sach.Soluong; i++)
            {
                var chiTietSach = new ChiTietSach
                {
                    Masach = sach.Masach,
                    Macthd = "CT" + lastNumber++.ToString("D3")
                };
                _context.ChiTietSaches.Add(chiTietSach);
            }
            
                
                
                if (hinhanhFile != null && hinhanhFile.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await hinhanhFile.CopyToAsync(memoryStream);
                        sach.Anh = Convert.ToBase64String(memoryStream.ToArray());
                    }
                }

                _context.Saches.Add(sach);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index1", "SachAdmin");

        }
        
    }
}
