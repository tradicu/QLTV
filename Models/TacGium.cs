using System;
using System.Collections.Generic;

namespace DATN_LiBrary.Models;

public partial class TacGium
{
    public string Mtg { get; set; } = null!;

    public string? Ttg { get; set; }

    public virtual ICollection<Sach> Masaches { get; } = new List<Sach>();
}
