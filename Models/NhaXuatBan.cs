using System;
using System.Collections.Generic;

namespace DATN_LiBrary.Models;

public partial class NhaXuatBan
{
    public string Mnxb { get; set; } = null!;

    public string? Tnxb { get; set; }

    public virtual ICollection<Sach> Saches { get; } = new List<Sach>();
}
