using DATN_LiBrary.Models;
using DATN_LiBrary.ModelViews;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DATN_LiBrary.Controllers
{
    public class ThongKeController : Controller
    {
        private readonly DoanContext _context;

        public ThongKeController(DoanContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            int tongSoDauSach = await _context.Saches.CountAsync();
            var topNguoiMuon = await _context.PhieuMuons
                                             .Where(pm => pm.Trangthai == "HOANTHANH")
                                             .GroupBy(pm => pm.Manguoimuon)
                                             .Select(group => new
                                             {
                                                 Manguoimuon = group.Key,
                                                 SoLanMuon = group.Count()
                                             })
                                             .OrderByDescending(x => x.SoLanMuon)
                                             .Take(3)
                                             .ToListAsync();
            DateTime fromDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime toDate = fromDate.AddMonths(1).AddDays(-1);
            var phieuMuons = await _context.PhieuMuons
                                   .Where(pm => pm.Trangthai == "HOANTHANH" && pm.Ngaymuon >= fromDate && pm.Ngaymuon <= toDate)
                                   .ToListAsync();

            // Lấy tổng số sách đã mượn trong tháng này
            int tongSoSachMuon = phieuMuons.SelectMany(pm => pm.Chitietphieumuons).Count();
            var phieuPhats = await _context.Phieuphats
                                   .Where(pp => pp.Ngayphat >= fromDate && pp.Ngayphat <= toDate)
                                   .ToListAsync();
            int tongSoPhieuPhat = phieuPhats.Count;
            decimal tongTienPhat = phieuPhats.Sum(pp => pp.Sotienphat ?? 0);

            // Lấy thông tin chi tiết của từng người mượn
            var nguoiMuons = await _context.NguoiMuons.ToListAsync();
            var phieuPhats1 = await _context.Phieuphats
                                   .Where(pp => pp.Ngayphat >= fromDate && pp.Ngayphat <= toDate)
                                   .GroupBy(pp => pp.Ngayphat)
                                   .Select(group => new
                                   {
                                       Ngay = group.Key,
                                       TongTienPhat = group.Sum(pp => pp.Sotienphat)
                                   })
                                   .ToListAsync();
            // Truyền danh sách top 3 người mượn vào view
            ViewBag.TongSoDauSach = tongSoDauSach;
            ViewBag.TopNguoiMuon = topNguoiMuon;
            ViewBag.TongSoSachMuon = tongSoSachMuon;
            ViewBag.TongSoPhieuPhat = tongSoPhieuPhat;
            ViewBag.TongTienPhat = tongTienPhat;
            ViewBag.FromDate = fromDate;
            ViewBag.ToDate = toDate;
            ViewBag.TienPhatTheoNgay = phieuPhats1;

            List<string> ngaymuon = new List<string>(); // Ngày mượn
            List<int> booksBorrowedPerDay = new List<int>(); // Số lượng sách mượn trong ngày
            List<int> booksReturnedPerDay = new List<int>(); // Số lượng sách trả trong ngày

            foreach (var phieuMuon in phieuMuons)
            {
                if (phieuMuon.Ngaymuon != null && phieuMuon.Ngaymuon >= fromDate && phieuMuon.Ngaymuon <= toDate)
                {
                    ngaymuon.Add(phieuMuon.Ngaymuon.HasValue ? phieuMuon.Ngaymuon.Value.ToShortDateString() : ""); // Format ngày thành chuỗi "dd/MM/yyyy"
                    booksBorrowedPerDay.Add(phieuMuon.Chitietphieumuons.Count()); // Số lượng sách được mượn trong ngày

                    // Tính số lượng sách đã trả trong ngày
                    int booksReturnedCount = 0;
                    foreach (var chitiet in phieuMuon.Chitietphieumuons)
                    {
                        if (chitiet.MaphieumuonNavigation.Ngaytra != null && chitiet.MaphieumuonNavigation.Ngaytra >= fromDate && chitiet.MaphieumuonNavigation.Ngaytra <= toDate)
                        {
                            booksReturnedCount++;
                        }
                    }
                    booksReturnedPerDay.Add(booksReturnedCount);
                }
            }


            // Truyền dữ liệu cho biểu đồ vào ViewBag
            ViewBag.ChartNgaymuon = Newtonsoft.Json.JsonConvert.SerializeObject(ngaymuon);
            ViewBag.ChartBooksBorrowed = Newtonsoft.Json.JsonConvert.SerializeObject(booksBorrowedPerDay);
            ViewBag.ChartBooksReturned = Newtonsoft.Json.JsonConvert.SerializeObject(booksReturnedPerDay);

            return View(nguoiMuons);

        }


    }
}
