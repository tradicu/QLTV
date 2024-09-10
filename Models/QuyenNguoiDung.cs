using System;
using System.Collections.Generic;

namespace DATN_LiBrary.Models;

public partial class QuyenNguoiDung
{
    public string Id { get; set; } = null!;

    public string? Tenquyennguoidung { get; set; }

    public virtual ICollection<NguoiDung> NguoiDungs { get; } = new List<NguoiDung>();
}
