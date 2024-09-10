using System;
using System.Collections.Generic;

namespace DATN_LiBrary.Models;

public partial class NguoiDung
{
    public string Manguoidung { get; set; } = null!;

    public string? Anhdaidien { get; set; }

    public string? Matkhau { get; set; }

    public string? Email { get; set; }

    public int? Gioitinh { get; set; }

    public string? Ten { get; set; }

    public string? Sodienthoai { get; set; }

    public string? Diachi { get; set; }

    public DateTime? Ngaysinh { get; set; }

    public string? Id { get; set; }

    public string? Trangthai { get; set; }

    public string? Chucvu { get; set; }

    public virtual ICollection<Hoadonnhap> Hoadonnhaps { get; } = new List<Hoadonnhap>();

    public virtual QuyenNguoiDung? IdNavigation { get; set; }

    public virtual ICollection<NguoiMuon> NguoiMuons { get; } = new List<NguoiMuon>();

    public virtual ICollection<PhieuMuon> PhieuMuons { get; } = new List<PhieuMuon>();
}
