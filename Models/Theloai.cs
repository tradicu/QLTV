using System;
using System.Collections.Generic;

namespace DATN_LiBrary.Models;

public partial class Theloai
{
    public string Matheloai { get; set; } = null!;

    public string? Tentheloai { get; set; }

    public virtual ICollection<Sach> Masaches { get; } = new List<Sach>();
}
