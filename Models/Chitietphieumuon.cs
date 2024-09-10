using System;
using System.Collections.Generic;

namespace DATN_LiBrary.Models;

public partial class Chitietphieumuon
{
    public int? Thoigianmuon { get; set; }

    public string? Maphieumuon { get; set; }

    public string? Macthd { get; set; }

    public string? Maphieuphat { get; set; }

    public string Idctpm { get; set; } = null!;

    public virtual ChiTietSach? MacthdNavigation { get; set; }

    public virtual PhieuMuon? MaphieumuonNavigation { get; set; }
}
