using DATN_LiBrary.Models;
namespace DATN_LiBrary.Responsive
{
    public interface ITheLoaiResponsive
    {
        Theloai Add(Theloai theloai);

        Theloai Update(Theloai theloai);

        Theloai Delete(String matheloai);

        Theloai GetTheloai(String matheloai);

        IEnumerable<Theloai> GetAllTheloai();

    }
}
