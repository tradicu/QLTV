
using DATN_LiBrary.Models;
using DATN_LiBrary.Responsive;
using Microsoft.AspNetCore.Mvc;
namespace DATN_LiBrary.ViewComponents
{
    public class TheLoaiMenuViewComponent:ViewComponent
    {
        private readonly ITheLoaiResponsive _theloai;
        public TheLoaiMenuViewComponent(ITheLoaiResponsive theLoaiResponsive)
        {
            _theloai = theLoaiResponsive;
        }
        public IViewComponentResult Invoke()
        {
            var theloai = _theloai.GetAllTheloai().OrderBy(x=>x.Matheloai);
            return View(theloai);
        }
    }
}
