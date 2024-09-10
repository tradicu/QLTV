using DATN_LiBrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DATN_LiBrary.Controllers
{
    public class QlTacGiaController : Controller
    {
        private readonly DoanContext _context;

        public QlTacGiaController(DoanContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.TacGia.ToListAsync());
        }
        public IActionResult Create()
        {
            ViewBag.NextMaTacGia = GenerateNextMaTacGia();
            return View();
        }
    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TacGium tacGia)
        {
                tacGia.Mtg = GenerateNextMaTacGia();
                _context.Add(tacGia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

        }
        [HttpGet]
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tacGia = await _context.TacGia.FirstOrDefaultAsync(m => m.Mtg == id);
            if (tacGia == null)
            {
                return NotFound();
            }

            return View(tacGia);
        }

        // POST: QlTacGia/Delete/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var tacGia = await _context.TacGia.FindAsync(id);
            if (tacGia == null)
            {
                return NotFound();
            }

            _context.TacGia.Remove(tacGia);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SachExists(string id)
        {
            return _context.Saches.Any(e => e.Masach == id);
        }
        private string GenerateNextMaTacGia()
        {
            var lastTacGia = _context.TacGia.OrderByDescending(t => t.Mtg).FirstOrDefault();

            
            int nextNumber = 1;
            if (lastTacGia != null)
            {
                
                string lastNumberStr = lastTacGia.Mtg.Substring(2); 
                if (int.TryParse(lastNumberStr, out int lastNumber))
                {
                    nextNumber = lastNumber + 1;
                }
            }

           
            string nextMaTacGia = "TG" + nextNumber.ToString("D2"); 

            return nextMaTacGia;
        }


    }
}
