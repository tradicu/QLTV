using System;
using System.Collections.Generic;

namespace DATN_LiBrary.Models;

public partial class ChiTietSach
{
    public string? Trangthai { get; set; }

    public string? Tinhtrang { get; set; }

    public string Macthd { get; set; } = null!;

    public string? Masach { get; set; }

    public string? Mlt { get; set; }

    public virtual ICollection<Chitiethoadonnhap> Chitiethoadonnhaps { get; } = new List<Chitiethoadonnhap>();

    public virtual ICollection<Chitietphieumuon> Chitietphieumuons { get; } = new List<Chitietphieumuon>();

    public virtual Sach? MasachNavigation { get; set; }

    public virtual NoiLuuTru? MltNavigation { get; set; }
}
