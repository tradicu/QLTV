using DATN_LiBrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DATN_LiBrary.Controllers
{
    public class QlNhaXuatBanController : Controller
    {
        private readonly DoanContext _context;

        public QlNhaXuatBanController(DoanContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.NhaXuatBans.ToListAsync());
        }
        public IActionResult Create()
        {
            ViewBag.NextMaNhaXuatBan = GenerateNextMaNhaXuatBan();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NhaXuatBan nhaXuatBan)
        {
            nhaXuatBan.Mnxb = GenerateNextMaNhaXuatBan();
            _context.Add(nhaXuatBan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhaXuatBan = await _context.NhaXuatBans.FirstOrDefaultAsync(m => m.Mnxb == id);
            if (nhaXuatBan == null)
            {
                return NotFound();
            }

            return View(nhaXuatBan);
        }

        // POST: QlTacGia/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var nhaXuatBan = await _context.NhaXuatBans.FindAsync(id);
            if (nhaXuatBan == null)
            {
                return NotFound();
            }

            _context.NhaXuatBans.Remove(nhaXuatBan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SachExists(string id)
        {
            return _context.Saches.Any(e => e.Masach == id);
        }
        private string GenerateNextMaNhaXuatBan()
        {
            
            var lastTacGia = _context.NhaXuatBans.OrderByDescending(t => t.Mnxb).FirstOrDefault();

           
            int nextNumber = 1;
            if (lastTacGia != null)
            {
               
                string lastNumberStr = lastTacGia.Mnxb.Substring(2); 
                if (int.TryParse(lastNumberStr, out int lastNumber))
                {
                    nextNumber = lastNumber + 1;
                }
            }

            // Tạo mã mới
            string nextMaTacGia = "NXB" + nextNumber.ToString("D2"); 

            return nextMaTacGia;
        }


    }
}
