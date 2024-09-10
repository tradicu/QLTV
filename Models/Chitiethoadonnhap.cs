using System;
using System.Collections.Generic;

namespace DATN_LiBrary.Models;

public partial class Chitiethoadonnhap
{
    public string Machitiethoadonnhap { get; set; } = null!;

    public string? Macthd { get; set; }

    public string? Id { get; set; }

    public virtual Hoadonnhap? IdNavigation { get; set; }

    public virtual ChiTietSach? MacthdNavigation { get; set; }
}
