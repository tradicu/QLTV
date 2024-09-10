using System;
using System.Collections.Generic;

namespace DATN_LiBrary.Models;

public partial class QuyenNguoiMuon
{
    public string Maquyen { get; set; } = null!;

    public string? Tenquyen { get; set; }

    public virtual ICollection<NguoiMuon> NguoiMuons { get; } = new List<NguoiMuon>();

    public virtual ICollection<Sach> Masaches { get; } = new List<Sach>();
}
