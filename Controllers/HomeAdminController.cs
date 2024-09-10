using Microsoft.AspNetCore.Mvc;
using DATN_LiBrary.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace DATN_LiBrary.Controllers
{
    public class HomeAdminController : Controller
    {

        #region Properties
        private readonly DoanContext _context;

        #endregion

        #region Constructor
        public HomeAdminController(DoanContext context)
        {
            _context = context;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Action index admin

        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
           
            
            List<PhieuMuon> lstPM = _context.PhieuMuons.Where(p => p.Trangthai == "PHIEUDAT").Include(x => x.ManguoidungNavigation).Include(x => x.ManguoimuonNavigation).ToList();

            //controler đây index đâyy
            return View(lstPM);
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
               .Include(p => p.Chitietphieumuons)
                   .ThenInclude(ct => ct.MacthdNavigation)
                       .ThenInclude(s => s.MasachNavigation)
               .ToListAsync();

            return View(lstphieumuon);


            #endregion
        }
        [HttpPost]
        public async Task<IActionResult> Edit(string id, List<string> bookIds)
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
                return Json(new { success = false, error = "Không tìm thấy phiếu mượn" });
            }

            try
            {
                
                if (bookIds != null && bookIds.Any())
                {
                    var chitietphieumuonsToRemove = phieuMuon.Chitietphieumuons
                                                             .Where(ct => !bookIds.Contains(ct.MacthdNavigation.Macthd))
                                                             .ToList();

                    _context.Chitietphieumuons.RemoveRange(chitietphieumuonsToRemove);
                }
                else
                {
                    
                    _context.Chitietphieumuons.RemoveRange(phieuMuon.Chitietphieumuons);
                }
                
               
                phieuMuon.Trangthai = "DADUYET"; phieuMuon.Manguoidung = userId;

                _context.Update(phieuMuon);
                await _context.SaveChangesAsync();

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }



        [HttpPost]
        public async Task<IActionResult> EditKhongDuyet(string id)
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

            
            phieuMuon.Trangthai = "KHONGDUYET";
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
        private bool PhieuMuonExists(string id)
        {
            return _context.PhieuMuons.Any(e => e.Maphieumuon == id);
        }



    }
}
