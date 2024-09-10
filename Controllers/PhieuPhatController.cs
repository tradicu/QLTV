using DATN_LiBrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DATN_LiBrary.Controllers
{
    public class PhieuPhatController : Controller
    {
        private readonly DoanContext _context;

        public PhieuPhatController(DoanContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string id)
        {
            var phieuPhatList = await _context.PhieuMuons
                .Include(x=>x.MaphieuphatNavigation)
                .Where(y=>y.Maphieumuon==id)
                .ToListAsync();
            return View(phieuPhatList);
        }

        // GET: PhieuPhat/Create
        public IActionResult Create(string maPhieuMuon)
        {
            ViewBag.mpp = GenerateNextMaPhieuPhat();
            ViewBag.MaPhieuMuon = maPhieuMuon; 
            return View();
        }

        // POST: PhieuPhat/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Maphieuphat,Lydo,Sotienphat,Ngayphat")] Phieuphat phieuPhat, string maPhieuMuon)
        {
            if (ModelState.IsValid)
            {
                
                string maPhieuPhat = GenerateNextMaPhieuPhat();
                phieuPhat.Maphieuphat = maPhieuPhat;
                
                
                _context.Phieuphats.Add(phieuPhat);
                await _context.SaveChangesAsync();

                
                var phieuMuon = await _context.PhieuMuons.FindAsync(maPhieuMuon);
                if (phieuMuon != null)
                {
                    phieuMuon.Maphieuphat = maPhieuPhat;
                    _context.Update(phieuMuon);
                    await _context.SaveChangesAsync();
                }
                else
                {
                   
                    return NotFound();
                }

                return RedirectToAction("Index","PhieuMuonAdmin");
            }

            return View(phieuPhat);
        }

        private string GenerateNextMaPhieuPhat()
        {
          
            var lastPhieuPhat = _context.Phieuphats.OrderByDescending(t => t.Maphieuphat).FirstOrDefault();

            
            int nextNumber = 1;
            if (lastPhieuPhat != null)
            {
              
                string lastNumberStr = lastPhieuPhat.Maphieuphat.Substring(2); 
                if (int.TryParse(lastNumberStr, out int lastNumber))
                {
                    nextNumber = lastNumber + 1;
                }
            }

            
            string nextMaPhieuPhat = "PP" + nextNumber.ToString("D3"); 

            return nextMaPhieuPhat;
        }
    }
}
