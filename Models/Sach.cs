using System;
using System.Collections.Generic;

namespace DATN_LiBrary.Models;

public partial class Sach
{
    public string Masach { get; set; } = null!;

    public string? Nhande { get; set; }

    public string? Mota { get; set; }

    public string? Anh { get; set; }

    public int? Namxuatban { get; set; }

    public int? Sotrang { get; set; }

    public string? Kichthuoc { get; set; }

    public int? Soluong { get; set; }

    public decimal? Giatien { get; set; }

    public string? Mnxb { get; set; }

    public string? Noidung { get; set; }

    public byte[]? PdfData { get; set; }

    public virtual ICollection<ChiTietSach> ChiTietSaches { get; } = new List<ChiTietSach>();

    public virtual NhaXuatBan? MnxbNavigation { get; set; }

    public virtual ICollection<QuyenNguoiMuon> Maquyens { get; } = new List<QuyenNguoiMuon>();

    public virtual ICollection<Theloai> Matheloais { get; } = new List<Theloai>();

    public virtual ICollection<TacGium> Mtgs { get; } = new List<TacGium>();
}
