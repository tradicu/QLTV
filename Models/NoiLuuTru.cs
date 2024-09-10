using System;
using System.Collections.Generic;

namespace DATN_LiBrary.Models;

public partial class NoiLuuTru
{
    public string Mlt { get; set; } = null!;

    public string? Nlt { get; set; }

    public virtual ICollection<ChiTietSach> ChiTietSaches { get; } = new List<ChiTietSach>();
}
