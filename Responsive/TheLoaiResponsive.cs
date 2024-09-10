using DATN_LiBrary.Models;

namespace DATN_LiBrary.Responsive
{
    public class TheLoaiResponsive : ITheLoaiResponsive
    {
        private readonly DoanContext _context;
        public TheLoaiResponsive(DoanContext context)
        {
            _context = context;
        }
        public Theloai Add(Theloai theloai)
        {
            _context.Theloais.Add(theloai);
            _context.SaveChanges();
            return theloai;
        }

        public Theloai Delete(string matheloai)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Theloai> GetAllTheloai()
        {
            return _context.Theloais;
        }

        public Theloai GetTheloai(string matheloai)
        {
            return _context.Theloais.Find(matheloai);
        }

        public Theloai Update(Theloai theloai)
        {
            _context.Theloais.Update(theloai);
            _context.SaveChanges();
            return theloai;
        }
    }
}
