using System;
using System.Collections.Generic;

namespace DATN_LiBrary.Models;

public partial class PhieuMuon
{
    public DateTime? Ngaytao { get; set; }

    public string Maphieumuon { get; set; } = null!;

    public string? Manguoidung { get; set; }

    public string? Manguoimuon { get; set; }

    public string? Trangthai { get; set; }

    public DateTime? Ngaymuon { get; set; }

    public DateTime? Ngaytra { get; set; }

    public string? Maphieuphat { get; set; }

    public virtual ICollection<Chitietphieumuon> Chitietphieumuons { get; } = new List<Chitietphieumuon>();

    public virtual NguoiDung? ManguoidungNavigation { get; set; }

    public virtual NguoiMuon? ManguoimuonNavigation { get; set; }

    public virtual Phieuphat? MaphieuphatNavigation { get; set; }
}
