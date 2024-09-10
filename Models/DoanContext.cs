using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DATN_LiBrary.Models;

public partial class DoanContext : DbContext
{
    public DoanContext()
    {
    }

    public DoanContext(DbContextOptions<DoanContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ChiTietSach> ChiTietSaches { get; set; }

    public virtual DbSet<Chitiethoadonnhap> Chitiethoadonnhaps { get; set; }

    public virtual DbSet<Chitietphieumuon> Chitietphieumuons { get; set; }

    public virtual DbSet<Hoadonnhap> Hoadonnhaps { get; set; }

    public virtual DbSet<NguoiDung> NguoiDungs { get; set; }

    public virtual DbSet<NguoiMuon> NguoiMuons { get; set; }

    public virtual DbSet<NhaCungCap> NhaCungCaps { get; set; }

    public virtual DbSet<NhaXuatBan> NhaXuatBans { get; set; }

    public virtual DbSet<NoiLuuTru> NoiLuuTrus { get; set; }

    public virtual DbSet<PhieuMuon> PhieuMuons { get; set; }

    public virtual DbSet<Phieuphat> Phieuphats { get; set; }

    public virtual DbSet<QuyenNguoiDung> QuyenNguoiDungs { get; set; }

    public virtual DbSet<QuyenNguoiMuon> QuyenNguoiMuons { get; set; }

    public virtual DbSet<Sach> Saches { get; set; }

    public virtual DbSet<TacGium> TacGia { get; set; }

    public virtual DbSet<Theloai> Theloais { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-S006F5J\\DIUKING;Initial Catalog=doan;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ChiTietSach>(entity =>
        {
            entity.HasKey(e => e.Macthd).HasName("PK__ChiTietS__50C3A3F8C36F3685");

            entity.ToTable("ChiTietSach");

            entity.Property(e => e.Macthd)
                .HasMaxLength(255)
                .HasColumnName("macthd");
            entity.Property(e => e.Masach)
                .HasMaxLength(255)
                .HasColumnName("masach");
            entity.Property(e => e.Mlt)
                .HasMaxLength(255)
                .HasColumnName("mlt");
            entity.Property(e => e.Tinhtrang)
                .HasColumnType("text")
                .HasColumnName("tinhtrang");
            entity.Property(e => e.Trangthai)
                .HasMaxLength(50)
                .HasColumnName("trangthai");

            entity.HasOne(d => d.MasachNavigation).WithMany(p => p.ChiTietSaches)
                .HasForeignKey(d => d.Masach)
                .HasConstraintName("FK__ChiTietSa__masac__4CA06362");

            entity.HasOne(d => d.MltNavigation).WithMany(p => p.ChiTietSaches)
                .HasForeignKey(d => d.Mlt)
                .HasConstraintName("FK__ChiTietSach__mlt__4D94879B");
        });

        modelBuilder.Entity<Chitiethoadonnhap>(entity =>
        {
            entity.HasKey(e => e.Machitiethoadonnhap).HasName("PK__Chitieth__2CF6B54491632D4C");

            entity.ToTable("Chitiethoadonnhap");

            entity.Property(e => e.Machitiethoadonnhap)
                .HasMaxLength(255)
                .HasColumnName("machitiethoadonnhap");
            entity.Property(e => e.Id)
                .HasMaxLength(255)
                .HasColumnName("id");
            entity.Property(e => e.Macthd)
                .HasMaxLength(255)
                .HasColumnName("macthd");

            entity.HasOne(d => d.IdNavigation).WithMany(p => p.Chitiethoadonnhaps)
                .HasForeignKey(d => d.Id)
                .HasConstraintName("FK__Chitiethoado__id__5FB337D6");

            entity.HasOne(d => d.MacthdNavigation).WithMany(p => p.Chitiethoadonnhaps)
                .HasForeignKey(d => d.Macthd)
                .HasConstraintName("FK__Chitietho__macth__5EBF139D");
        });

        modelBuilder.Entity<Chitietphieumuon>(entity =>
        {
            entity.HasKey(e => e.Idctpm).HasName("PK__chitietp__4EF6F286709B47A8");

            entity.ToTable("chitietphieumuon");

            entity.Property(e => e.Idctpm)
                .HasMaxLength(255)
                .HasColumnName("idctpm");
            entity.Property(e => e.Macthd)
                .HasMaxLength(255)
                .HasColumnName("macthd");
            entity.Property(e => e.Maphieumuon)
                .HasMaxLength(255)
                .HasColumnName("maphieumuon");
            entity.Property(e => e.Maphieuphat)
                .HasMaxLength(255)
                .HasColumnName("maphieuphat");
            entity.Property(e => e.Thoigianmuon).HasColumnName("thoigianmuon");

            entity.HasOne(d => d.MacthdNavigation).WithMany(p => p.Chitietphieumuons)
                .HasForeignKey(d => d.Macthd)
                .HasConstraintName("FK__chitietph__macth__571DF1D5");

            entity.HasOne(d => d.MaphieumuonNavigation).WithMany(p => p.Chitietphieumuons)
                .HasForeignKey(d => d.Maphieumuon)
                .HasConstraintName("FK__chitietph__maphi__5629CD9C");
        });

        modelBuilder.Entity<Hoadonnhap>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__hoadonnh__3213E83F927FD8EC");

            entity.ToTable("hoadonnhap");

            entity.Property(e => e.Id)
                .HasMaxLength(255)
                .HasColumnName("id");
            entity.Property(e => e.Lydo)
                .HasColumnType("text")
                .HasColumnName("lydo");
            entity.Property(e => e.Mancc)
                .HasMaxLength(255)
                .HasColumnName("mancc");
            entity.Property(e => e.Manguoidung)
                .HasMaxLength(255)
                .HasColumnName("manguoidung");
            entity.Property(e => e.Ngaynhap)
                .HasColumnType("date")
                .HasColumnName("ngaynhap");
            entity.Property(e => e.Tongtien)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("tongtien");

            entity.HasOne(d => d.ManccNavigation).WithMany(p => p.Hoadonnhaps)
                .HasForeignKey(d => d.Mancc)
                .HasConstraintName("FK__hoadonnha__mancc__5AEE82B9");

            entity.HasOne(d => d.ManguoidungNavigation).WithMany(p => p.Hoadonnhaps)
                .HasForeignKey(d => d.Manguoidung)
                .HasConstraintName("FK__hoadonnha__mangu__5BE2A6F2");
        });

        modelBuilder.Entity<NguoiDung>(entity =>
        {
            entity.HasKey(e => e.Manguoidung).HasName("PK__NguoiDun__2D5730E6A60B8AD0");

            entity.ToTable("NguoiDung");

            entity.Property(e => e.Manguoidung)
                .HasMaxLength(255)
                .HasColumnName("manguoidung");
            entity.Property(e => e.Anhdaidien)
                .HasMaxLength(255)
                .HasColumnName("anhdaidien");
            entity.Property(e => e.Chucvu)
                .HasMaxLength(250)
                .HasColumnName("chucvu");
            entity.Property(e => e.Diachi)
                .HasMaxLength(50)
                .HasColumnName("diachi");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Gioitinh).HasColumnName("gioitinh");
            entity.Property(e => e.Id)
                .HasMaxLength(255)
                .HasColumnName("id");
            entity.Property(e => e.Matkhau)
                .HasMaxLength(100)
                .HasColumnName("matkhau");
            entity.Property(e => e.Ngaysinh)
                .HasColumnType("date")
                .HasColumnName("ngaysinh");
            entity.Property(e => e.Sodienthoai)
                .HasMaxLength(255)
                .HasColumnName("sodienthoai");
            entity.Property(e => e.Ten)
                .HasMaxLength(255)
                .HasColumnName("ten");
            entity.Property(e => e.Trangthai)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("trangthai");

            entity.HasOne(d => d.IdNavigation).WithMany(p => p.NguoiDungs)
                .HasForeignKey(d => d.Id)
                .HasConstraintName("FK__NguoiDung__id__46E78A0C");
        });

        modelBuilder.Entity<NguoiMuon>(entity =>
        {
            entity.HasKey(e => e.Manguoimuon).HasName("PK__NguoiMuo__8824BC503BEB585A");

            entity.ToTable("NguoiMuon");

            entity.Property(e => e.Manguoimuon)
                .HasMaxLength(255)
                .HasColumnName("manguoimuon");
            entity.Property(e => e.Khoa)
                .HasMaxLength(255)
                .HasColumnName("khoa");
            entity.Property(e => e.Khoas).HasMaxLength(50);
            entity.Property(e => e.Manguoidung)
                .HasMaxLength(255)
                .HasColumnName("manguoidung");
            entity.Property(e => e.Maquyen)
                .HasMaxLength(50)
                .HasColumnName("maquyen");
            entity.Property(e => e.Ten)
                .HasMaxLength(255)
                .HasColumnName("ten");

            entity.HasOne(d => d.ManguoidungNavigation).WithMany(p => p.NguoiMuons)
                .HasForeignKey(d => d.Manguoidung)
                .HasConstraintName("FK_NguoiDung_NguoiMuon");

            entity.HasOne(d => d.MaquyenNavigation).WithMany(p => p.NguoiMuons)
                .HasForeignKey(d => d.Maquyen)
                .HasConstraintName("FK__NguoiMuon__maquy__5070F446");
        });

        modelBuilder.Entity<NhaCungCap>(entity =>
        {
            entity.HasKey(e => e.Mancc).HasName("PK__NhaCungC__0A7AC4355CE09EAC");

            entity.ToTable("NhaCungCap");

            entity.Property(e => e.Mancc)
                .HasMaxLength(255)
                .HasColumnName("mancc");
            entity.Property(e => e.Tenncc)
                .HasMaxLength(255)
                .HasColumnName("tenncc");
        });

        modelBuilder.Entity<NhaXuatBan>(entity =>
        {
            entity.HasKey(e => e.Mnxb).HasName("PK__NhaXuatB__7770C07A6800E402");

            entity.ToTable("NhaXuatBan");

            entity.Property(e => e.Mnxb)
                .HasMaxLength(255)
                .HasColumnName("mnxb");
            entity.Property(e => e.Tnxb)
                .HasMaxLength(255)
                .HasColumnName("tnxb");
        });

        modelBuilder.Entity<NoiLuuTru>(entity =>
        {
            entity.HasKey(e => e.Mlt).HasName("PK__NoiLuuTr__DF50D65E05B7D26D");

            entity.ToTable("NoiLuuTru");

            entity.Property(e => e.Mlt)
                .HasMaxLength(255)
                .HasColumnName("mlt");
            entity.Property(e => e.Nlt)
                .HasMaxLength(255)
                .HasColumnName("nlt");
        });

        modelBuilder.Entity<PhieuMuon>(entity =>
        {
            entity.HasKey(e => e.Maphieumuon).HasName("PK__PhieuMuo__352EDDCD437D8F5A");

            entity.ToTable("PhieuMuon");

            entity.Property(e => e.Maphieumuon)
                .HasMaxLength(255)
                .HasColumnName("maphieumuon");
            entity.Property(e => e.Manguoidung)
                .HasMaxLength(255)
                .HasColumnName("manguoidung");
            entity.Property(e => e.Manguoimuon)
                .HasMaxLength(255)
                .HasColumnName("manguoimuon");
            entity.Property(e => e.Maphieuphat)
                .HasMaxLength(255)
                .HasColumnName("maphieuphat");
            entity.Property(e => e.Ngaymuon)
                .HasColumnType("date")
                .HasColumnName("ngaymuon");
            entity.Property(e => e.Ngaytao)
                .HasColumnType("date")
                .HasColumnName("ngaytao");
            entity.Property(e => e.Ngaytra)
                .HasColumnType("date")
                .HasColumnName("ngaytra");
            entity.Property(e => e.Trangthai)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("trangthai");

            entity.HasOne(d => d.ManguoidungNavigation).WithMany(p => p.PhieuMuons)
                .HasForeignKey(d => d.Manguoidung)
                .HasConstraintName("FK__PhieuMuon__mangu__534D60F1");

            entity.HasOne(d => d.ManguoimuonNavigation).WithMany(p => p.PhieuMuons)
                .HasForeignKey(d => d.Manguoimuon)
                .HasConstraintName("FK__PhieuMuon__mangu__5441852A");

            entity.HasOne(d => d.MaphieuphatNavigation).WithMany(p => p.PhieuMuons)
                .HasForeignKey(d => d.Maphieuphat)
                .HasConstraintName("FK_PhieuMuon_PhieuPhat");
        });

        modelBuilder.Entity<Phieuphat>(entity =>
        {
            entity.HasKey(e => e.Maphieuphat).HasName("PK__phieupha__0C54CA02767075C3");

            entity.ToTable("phieuphat");

            entity.Property(e => e.Maphieuphat)
                .HasMaxLength(255)
                .HasColumnName("maphieuphat");
            entity.Property(e => e.Lydo)
                .HasMaxLength(255)
                .HasColumnName("lydo");
            entity.Property(e => e.Ngayphat)
                .HasColumnType("date")
                .HasColumnName("ngayphat");
            entity.Property(e => e.Sotienphat)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("sotienphat");
        });

        modelBuilder.Entity<QuyenNguoiDung>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__QuyenNgu__3213E83FA12A99DD");

            entity.ToTable("QuyenNguoiDung");

            entity.Property(e => e.Id)
                .HasMaxLength(255)
                .HasColumnName("id");
            entity.Property(e => e.Tenquyennguoidung)
                .HasMaxLength(255)
                .HasColumnName("tenquyennguoidung");
        });

        modelBuilder.Entity<QuyenNguoiMuon>(entity =>
        {
            entity.HasKey(e => e.Maquyen).HasName("PK__QuyenNgu__AA0E356EC9694DD8");

            entity.ToTable("QuyenNguoiMuon");

            entity.Property(e => e.Maquyen)
                .HasMaxLength(50)
                .HasColumnName("maquyen");
            entity.Property(e => e.Tenquyen)
                .HasMaxLength(255)
                .HasColumnName("tenquyen");

            entity.HasMany(d => d.Masaches).WithMany(p => p.Maquyens)
                .UsingEntity<Dictionary<string, object>>(
                    "SachQuyen",
                    r => r.HasOne<Sach>().WithMany()
                        .HasForeignKey("Masach")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Sach_Quye__masac__6B24EA82"),
                    l => l.HasOne<QuyenNguoiMuon>().WithMany()
                        .HasForeignKey("Maquyen")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Sach_Quye__maquy__6A30C649"),
                    j =>
                    {
                        j.HasKey("Maquyen", "Masach").HasName("PK__Sach_Quy__D641126E6167BCC9");
                        j.ToTable("Sach_Quyen");
                    });
        });

        modelBuilder.Entity<Sach>(entity =>
        {
            entity.HasKey(e => e.Masach).HasName("PK__Sach__C4F2700694999961");

            entity.ToTable("Sach");

            entity.Property(e => e.Masach)
                .HasMaxLength(255)
                .HasColumnName("masach");
            entity.Property(e => e.Anh)
                .HasMaxLength(255)
                .HasColumnName("anh");
            entity.Property(e => e.Giatien)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("giatien");
            entity.Property(e => e.Kichthuoc)
                .HasMaxLength(1)
                .HasColumnName("kichthuoc");
            entity.Property(e => e.Mnxb)
                .HasMaxLength(255)
                .HasColumnName("mnxb");
            entity.Property(e => e.Mota).HasColumnName("mota");
            entity.Property(e => e.Namxuatban).HasColumnName("namxuatban");
            entity.Property(e => e.Nhande)
                .HasMaxLength(255)
                .HasColumnName("nhande");
            entity.Property(e => e.Noidung)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("noidung");
            entity.Property(e => e.PdfData).HasColumnName("pdfData");
            entity.Property(e => e.Soluong).HasColumnName("soluong");
            entity.Property(e => e.Sotrang).HasColumnName("sotrang");

            entity.HasOne(d => d.MnxbNavigation).WithMany(p => p.Saches)
                .HasForeignKey(d => d.Mnxb)
                .HasConstraintName("FK__Sach__mnxb__49C3F6B7");

            entity.HasMany(d => d.Matheloais).WithMany(p => p.Masaches)
                .UsingEntity<Dictionary<string, object>>(
                    "SachTheLoai",
                    r => r.HasOne<Theloai>().WithMany()
                        .HasForeignKey("Matheloai")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Sach_TheL__mathe__6754599E"),
                    l => l.HasOne<Sach>().WithMany()
                        .HasForeignKey("Masach")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Sach_TheL__masac__66603565"),
                    j =>
                    {
                        j.HasKey("Masach", "Matheloai").HasName("PK__Sach_The__EDF02D24D06F8892");
                        j.ToTable("Sach_TheLoai");
                    });
        });

        modelBuilder.Entity<TacGium>(entity =>
        {
            entity.HasKey(e => e.Mtg).HasName("PK__TacGia__DF50975B329DB49F");

            entity.Property(e => e.Mtg)
                .HasMaxLength(255)
                .HasColumnName("mtg");
            entity.Property(e => e.Ttg)
                .HasMaxLength(255)
                .HasColumnName("ttg");

            entity.HasMany(d => d.Masaches).WithMany(p => p.Mtgs)
                .UsingEntity<Dictionary<string, object>>(
                    "SachTacGium",
                    r => r.HasOne<Sach>().WithMany()
                        .HasForeignKey("Masach")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Sach_TacG__masac__6383C8BA"),
                    l => l.HasOne<TacGium>().WithMany()
                        .HasForeignKey("Mtg")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Sach_TacGia__mtg__628FA481"),
                    j =>
                    {
                        j.HasKey("Mtg", "Masach").HasName("PK__Sach_Tac__A31FB05BD93715A7");
                        j.ToTable("Sach_TacGia");
                    });
        });

        modelBuilder.Entity<Theloai>(entity =>
        {
            entity.HasKey(e => e.Matheloai).HasName("PK__theloai__9025D223CA8A9377");

            entity.ToTable("theloai");

            entity.Property(e => e.Matheloai)
                .HasMaxLength(255)
                .HasColumnName("matheloai");
            entity.Property(e => e.Tentheloai)
                .HasMaxLength(255)
                .HasColumnName("tentheloai");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
