using System;
using System.Collections.Generic;

namespace DATN_LiBrary.Models;

public partial class Phieuphat
{
    public string Maphieuphat { get; set; } = null!;

    public string? Lydo { get; set; }

    public decimal? Sotienphat { get; set; }

    public DateTime? Ngayphat { get; set; }

    public virtual ICollection<PhieuMuon> PhieuMuons { get; } = new List<PhieuMuon>();
}
