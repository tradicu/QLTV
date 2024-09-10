using System;
using System.Collections.Generic;

namespace DATN_LiBrary.Models;

public partial class NhaCungCap
{
    public string Mancc { get; set; } = null!;

    public string? Tenncc { get; set; }

    public virtual ICollection<Hoadonnhap> Hoadonnhaps { get; } = new List<Hoadonnhap>();
}
