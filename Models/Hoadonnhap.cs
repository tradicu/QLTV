using System;
using System.Collections.Generic;

namespace DATN_LiBrary.Models;

public partial class Hoadonnhap
{
    public string Id { get; set; } = null!;

    public DateTime? Ngaynhap { get; set; }

    public decimal? Tongtien { get; set; }

    public string? Lydo { get; set; }

    public string? Mancc { get; set; }

    public string? Manguoidung { get; set; }

    public virtual ICollection<Chitiethoadonnhap> Chitiethoadonnhaps { get; } = new List<Chitiethoadonnhap>();

    public virtual NhaCungCap? ManccNavigation { get; set; }

    public virtual NguoiDung? ManguoidungNavigation { get; set; }
}
