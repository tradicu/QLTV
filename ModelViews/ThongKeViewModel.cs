
using DATN_LiBrary.Models;
using DATN_LiBrary.ModelViews;

public class ThongKeViewModel
{
    public List<NguoiMuon> NguoiMuons { get; set; }
    public List<object> TopNguoiMuon { get; set; }
    public int TongSoSachMuon { get; set; }
    public int TongSoPhieuPhat { get; set; }
    public decimal TongTienPhat { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
    public List<PhieuPhatViewModel> PhieuPhats { get; set; }

}

