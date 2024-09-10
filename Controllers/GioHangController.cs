using DATN_LiBrary.ModelViews;
using Microsoft.AspNetCore.Mvc;
using DATN_Library.Extension;
using DATN_LiBrary.Models;
using Newtonsoft.Json;
using DATN_LiBrary.Controllers;

namespace DATN_Library.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class GioHangController : Controller
    {
        private readonly DoanContext _context;

        public GioHangController(DoanContext context)
        {
            _context = context;
        }
        
        DoanContext db = new DoanContext();

        public List<CartItem> GioHang
        {
            get
            {
                var gh = HttpContext.Session.Get<List<CartItem>>("GioHang");
                if (gh == default(List<CartItem>))
                {
                    gh = new List<CartItem>();
                }
                return gh;
            }
        }
        

        public IActionResult Index()
        {
            var listGio = GioHang;
            return View(listGio);

        }

        [HttpPost]
        [Route("giohang/add-cart")]
        public IActionResult AddToCart(string ms, int soLuong)
        {
            try
            {
                List<CartItem> gioHang = GioHang;

                // Kiểm tra nếu sản phẩm đã có trong giỏ hàng
                CartItem existingItem = gioHang.SingleOrDefault(p => p.masach.Masach == ms);
                if (existingItem != null)
                {
                    return Json(new { success = false, message = "Sản phẩm đã có trong giỏ hàng" });
                }

                // Tìm sách
                Sach sp = db.Saches.SingleOrDefault(p => p.Masach == ms);
                if (sp == null)
                {
                    return Json(new { success = false, message = "Sản phẩm không tồn tại" });
                }

                // Tìm ChiTietSach
                ChiTietSach chiTietSach = db.ChiTietSaches.FirstOrDefault(cts => cts.Masach == ms && !db.Chitietphieumuons.Any(ctpm => ctpm.Macthd == cts.Macthd && (ctpm.MaphieumuonNavigation.Trangthai == "PHIEUMUON" || ctpm.MaphieumuonNavigation.Trangthai == "PHIEUDAT" || ctpm.MaphieumuonNavigation.Trangthai == "DADUYET")));
                if (chiTietSach == null)
                {
                    // Tìm ChiTietSach có ngày trả gần nhất
                    var nearestReturnChiTietSach = db.ChiTietSaches
                                                    .Where(cts => cts.Masach == ms)
                                                    .Join(db.Chitietphieumuons,
                                                          cts => cts.Macthd,
                                                          ctpm => ctpm.Macthd,
                                                          (cts, ctpm) => new { cts, ctpm })
                                                    .Where(x => x.ctpm.MaphieumuonNavigation.Trangthai == "PHIEUMUON" || x.ctpm.MaphieumuonNavigation.Trangthai == "PHIEUDAT" || x.ctpm.MaphieumuonNavigation.Trangthai == "DADUYET")
                                                    .OrderBy(x => x.ctpm.MaphieumuonNavigation.Ngaytra)
                                                    .Select(x => new { x.cts, x.ctpm.MaphieumuonNavigation.Ngaytra })
                                                    .FirstOrDefault();
                    var nearestReturnChiTietSach1 = db.ChiTietSaches
                                                    .Where(cts => cts.Masach == ms)
                                                    .Join(db.Chitietphieumuons,
                                                          cts => cts.Macthd,
                                                          ctpm => ctpm.Macthd,
                                                          (cts, ctpm) => new { cts, ctpm })
                                                    .Where(x => (x.ctpm.MaphieumuonNavigation.Trangthai == "PHIEUMUON"|| x.ctpm.MaphieumuonNavigation.Trangthai == "PHIEUDAT") &&
                                                    !db.Chitietphieumuons
                                                    .Where(ctpm => ctpm.Macthd == x.cts.Macthd)
                                                    .Any(ctpm => ctpm.MaphieumuonNavigation.Trangthai == "PHIEUDAT"|| ctpm.MaphieumuonNavigation.Trangthai == "DADUYET"))

                                                    .OrderBy(x => x.ctpm.MaphieumuonNavigation.Ngaytra)
                                                    .Select(x => new { x.cts, x.ctpm.MaphieumuonNavigation.Ngaytra })
                                                    .FirstOrDefault();
                    

                    if (nearestReturnChiTietSach == null )
                    {
                        return Json(new { success = false, message = "Sách này không có sẵn" });
                    }
                    ViewBag.NgayTraGanNhat = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
                    if (nearestReturnChiTietSach != null)
                    {
                        // Hiển thị thông báo để người dùng chọn mượn hoặc không
                        return Json(new
                        {
                            success = false,
                            message = $"Sách này đã hết. Quyển sách được trả gần nhất vào ngày {nearestReturnChiTietSach1.Ngaytra:dd/MM/yyyy}. Bạn có muốn đặt không?",
                            macthd = nearestReturnChiTietSach.cts.Macthd
                        });
                    }
                    

                }
                chiTietSach.MasachNavigation = null;
                // Thêm sản phẩm vào giỏ hàng
                CartItem newItem = new CartItem
                {
                    soLuong = soLuong,
                    masach = sp,
                    macthd = chiTietSach
                };
                gioHang.Add(newItem);

                HttpContext.Session.Set("GioHang", gioHang);

                return Json(new { success = true, message = "Thêm thành công" });
            }
            catch (Exception ex)
            {
                // Ghi log lỗi
                // Ví dụ: _logger.LogError(ex, "Error adding item to cart");
                return StatusCode(500, new { success = false, message = "Đã có lỗi xảy ra: " + ex.Message });
            }
        }


        [HttpPost]
        [Route("giohang/update-cart")]
        public IActionResult UpdateCart(string ms, string macthdValue, int soLuong)
        {
            List<CartItem> gioHang = GioHang;

            CartItem item = gioHang.SingleOrDefault(p => p.masach.Masach == ms && p.macthd.Macthd == macthdValue);
            if (item != null)
            {
                // Nếu sản phẩm đã có trong giỏ hàng
                item.soLuong += soLuong;
            }
            else
            {
                // Nếu sản phẩm chưa có trong giỏ hàng
                Sach sp = db.Saches.SingleOrDefault(p => p.Masach == ms);
                ChiTietSach ctsach = db.ChiTietSaches.SingleOrDefault(p => p.Macthd == macthdValue);
                if (gioHang.Any(p => p.masach.Masach == ms && p.macthd.Macthd == macthdValue))
                {
                    // Nếu sản phẩm đã có trong giỏ hàng, báo lỗi
                    return Json(new { success = false, message = "Sản phẩm này đã có trong giỏ hàng." });
                }
                else
                {
                    // Thêm sản phẩm mới vào giỏ hàng
                    item = new CartItem
                    {
                        soLuong = soLuong,
                        masach = sp,
                        macthd = ctsach
                    };
                    gioHang.Add(item);
                }
            }

            HttpContext.Session.Set<List<CartItem>>("GioHang", gioHang);

            return Json(new { success = true });
        }


        [HttpPost]
        [Route("giohang/remove")]
        public ActionResult Remove(string ms)
        {
            try
            {
                List<CartItem> gioHang = GioHang;
                CartItem item = gioHang.SingleOrDefault(p => p.masach.Masach == ms);
                if (item != null)
                {
                    gioHang.Remove(item);
                }
                HttpContext.Session.Set<List<CartItem>>("GioHang", gioHang);
                return Json(new { success = true });
            }
            catch
            {
                return Json(new { success = false });
            }

        }

        public ActionResult CleanCart()
        {
            HttpContext.Session.Remove("GioHang");
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("cart/count")]
        public IActionResult GetCartCount()
        {
            int count = GioHang.Sum(item => item.soLuong);
            return Json(count);
        }

    }
}

