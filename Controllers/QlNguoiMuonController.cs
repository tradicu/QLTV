using DATN_LiBrary.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace DATN_LiBrary.Controllers
{
    public class QlNguoiMuonController : Controller
    {
        private readonly DoanContext _context;

        public QlNguoiMuonController(DoanContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            
            var nguoiMuons = _context.NguoiMuons?.ToList() ?? new List<NguoiMuon>();

            // Giả sử giảng viên có thuộc tính đặc biệt để phân biệt với sinh viên
            var giangViens = nguoiMuons.Where(nd => nd.Maquyen == "QNM01").ToList();
            var sinhViens = nguoiMuons.Where(nd => nd.Maquyen == "QNM02").ToList();
            ViewBag.GiangViens = giangViens;
            ViewBag.SinhViens = sinhViens;

            return View();
        }

        public IActionResult Create()
        {

            //if (role == "teacher")
            //{
            //    var model = new NguoiMuon { Manguoimuon = "" }; // Hoặc gán cho Manguoimuon một giá trị cố định
            //    return View(model);
            //}
            //else if (role == "student")
            //{
            //    var nguoimuon = new NguoiMuon();
            //    return View(nguoimuon);
            //}
            
            return View();
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NguoiMuon nguoiMuon, string role)
        {
            if (role == "teacher")
            {
                nguoiMuon.Maquyen = "QNM01";
                 // Tạo mã người mượn tự động
                _context.Add(nguoiMuon);
            }
            else if (role == "student")
            {
                nguoiMuon.Maquyen = "QNM02";
                                            
                _context.Add(nguoiMuon);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));


        }
        
    }
}
