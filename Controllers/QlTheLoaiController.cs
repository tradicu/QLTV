using DATN_LiBrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DATN_LiBrary.Controllers
{
    public class QlTheLoaiController : Controller
    {
        private readonly DoanContext _context;

        public QlTheLoaiController(DoanContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Theloais.ToListAsync());
        }
        public IActionResult Create()
        {
            ViewBag.NextMaTheLoai = GenerateNextMaTheLoai();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Theloai theLoai)
        {
            theLoai.Matheloai = GenerateNextMaTheLoai();
            _context.Add(theLoai);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var theLoai = await _context.Theloais.FirstOrDefaultAsync(m => m.Matheloai == id);
            if (theLoai == null)
            {
                return NotFound();
            }

            return View(theLoai);
        }

        // POST: QlTacGia/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var theLoai = await _context.Theloais.FindAsync(id);
            if (theLoai == null)
            {
                return NotFound();
            }

            _context.Theloais.Remove(theLoai);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SachExists(string id)
        {
            return _context.Saches.Any(e => e.Masach == id);
        }
        private string GenerateNextMaTheLoai()
        {
           
            var lastTacGia = _context.Theloais.OrderByDescending(t => t.Matheloai).FirstOrDefault();

           
            int nextNumber = 1;
            if (lastTacGia != null)
            {
               
                string lastNumberStr = lastTacGia.Matheloai.Substring(2); 
                if (int.TryParse(lastNumberStr, out int lastNumber))
                {
                    nextNumber = lastNumber + 1;
                }
            }

            
            string nextMaTacGia = "TL" + nextNumber.ToString("D2");

            return nextMaTacGia;
        }


    }
}
