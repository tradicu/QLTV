using System;
using System.Collections.Generic;

namespace DATN_LiBrary.Models;

public partial class NguoiMuon
{
    public string Manguoimuon { get; set; } = null!;

    public string? Ten { get; set; }

    public string? Khoas { get; set; }

    public string? Khoa { get; set; }

    public string? Maquyen { get; set; }

    public string? Manguoidung { get; set; }

    public virtual NguoiDung? ManguoidungNavigation { get; set; }

    public virtual QuyenNguoiMuon? MaquyenNavigation { get; set; }

    public virtual ICollection<PhieuMuon> PhieuMuons { get; } = new List<PhieuMuon>();
}
