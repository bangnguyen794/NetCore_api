using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ApiCore_facebook.Models
{
    public partial class db_facebook_vmContext : DbContext
    {
        public db_facebook_vmContext()
        {
        }

        public db_facebook_vmContext(DbContextOptions<db_facebook_vmContext> options)
            : base(options)
        {
        }
       

        public virtual DbSet<ExcludedQueries> ExcludedQueries { get; set; }
        public virtual DbSet<FbAccountQuyen> FbAccountQuyen { get; set; }
        public virtual DbSet<FbBaoxau> FbBaoxau { get; set; }
        public virtual DbSet<FbBlocks> FbBlocks { get; set; }
        public virtual DbSet<FbBotKichban> FbBotKichban { get; set; }
        public virtual DbSet<FbCauhinhChucnang> FbCauhinhChucnang { get; set; }
        public virtual DbSet<FbChitietGiaohang> FbChitietGiaohang { get; set; }
        public virtual DbSet<FbChitietTinQc> FbChitietTinQc { get; set; }
        public virtual DbSet<FbDonhang> FbDonhang { get; set; }
        public virtual DbSet<FbDonhangChitiet> FbDonhangChitiet { get; set; }
        public virtual DbSet<FbDonviVanchuyen> FbDonviVanchuyen { get; set; }
        public virtual DbSet<FbDonviVanchuyenChitiet> FbDonviVanchuyenChitiet { get; set; }
        public virtual DbSet<FbGhichu> FbGhichu { get; set; }
        public virtual DbSet<FbGoicuoc> FbGoicuoc { get; set; }
        public virtual DbSet<FbGoicuocChitiet> FbGoicuocChitiet { get; set; }
        public virtual DbSet<FbGoicuocFanpage> FbGoicuocFanpage { get; set; }
        public virtual DbSet<FbGuitinHangloat> FbGuitinHangloat { get; set; }
        public virtual DbSet<FbLichhen> FbLichhen { get; set; }
        public virtual DbSet<FbLichsuxem> FbLichsuxem { get; set; }
        public virtual DbSet<FbMessages> FbMessages { get; set; }
        public virtual DbSet<FbMessages2> FbMessages2 { get; set; }
        public virtual DbSet<FbPageDetail> FbPageDetail { get; set; }
        public virtual DbSet<FbSanpham> FbSanpham { get; set; }
        public virtual DbSet<FbSetting> FbSetting { get; set; }
        public virtual DbSet<FbTag> FbTag { get; set; }
        public virtual DbSet<FbTagTrangthai> FbTagTrangthai { get; set; }
        public virtual DbSet<FbThongbao> FbThongbao { get; set; }
        public virtual DbSet<FbThongbaoVanchuyen> FbThongbaoVanchuyen { get; set; }
        public virtual DbSet<FbTinQc> FbTinQc { get; set; }
        public virtual DbSet<FbUserToken> FbUserToken { get; set; }
        public virtual DbSet<FbWebsite> FbWebsite { get; set; }

        public virtual DbSet<pro_getUsser> pro_getUsser { get; set; }

        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=45.125.239.149,1433;Initial Catalog=db_facebook_vm;Persist Security Info=False;User ID=sa;Password=BrightDR@123;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          

            modelBuilder.Entity<ExcludedQueries>(entity =>
            {
                entity.HasKey(e => e.QueryId);

                entity.ToTable("excluded_queries", "oqs");

                entity.Property(e => e.QueryId)
                    .HasColumnName("query_id")
                    .ValueGeneratedNever();
            });

            modelBuilder.Entity<FbAccountQuyen>(entity =>
            {
                entity.ToTable("Fb_account_quyen");

                entity.HasIndex(e => new { e.WebConnect, e.Account })
                    .HasName("index_web_connect_account");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Account)
                    .HasColumnName("account")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AccountNhanvien)
                    .HasColumnName("account_nhanvien")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Active)
                    .HasColumnName("active")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Avatar)
                    .HasColumnName("avatar")
                    .HasMaxLength(100);

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedTime)
                    .HasColumnName("created_time")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Diachi).HasColumnName("diachi");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Hoten)
                    .HasColumnName("hoten")
                    .HasMaxLength(100);

                entity.Property(e => e.Page)
                    .HasColumnName("page")
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasColumnName("phone")
                    .HasMaxLength(13)
                    .IsUnicode(false);

                entity.Property(e => e.Quyen)
                    .HasColumnName("quyen")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.WebConnect).HasColumnName("web_connect");
            });

            modelBuilder.Entity<FbBaoxau>(entity =>
            {
                entity.ToTable("FB_baoxau");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdPage)
                    .HasColumnName("id_page")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IdUser)
                    .HasColumnName("id_user")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Ngaytao)
                    .HasColumnName("ngaytao")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Nguoitao)
                    .HasColumnName("nguoitao")
                    .HasMaxLength(250);

                entity.Property(e => e.Noidung).HasColumnName("noidung");
            });

            modelBuilder.Entity<FbBlocks>(entity =>
            {
                entity.ToTable("Fb_blocks");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdMessage)
                    .HasColumnName("id_message")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IdPage)
                    .HasColumnName("id_page")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IdUser)
                    .HasColumnName("id_user")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Ngaytao)
                    .HasColumnName("ngaytao")
                    .HasColumnType("datetime");

                entity.Property(e => e.Nguoichan)
                    .HasColumnName("nguoichan")
                    .HasMaxLength(250);
            });

            modelBuilder.Entity<FbBotKichban>(entity =>
            {
                entity.ToTable("Fb_bot_kichban");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IdPage)
                    .HasColumnName("id_page")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Noidung)
                    .HasColumnName("noidung")
                    .HasColumnType("ntext");

                entity.Property(e => e.Trangthai).HasColumnName("trangthai");

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("update_time")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<FbCauhinhChucnang>(entity =>
            {
                entity.ToTable("FB_cauhinh_chucnang");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ChamsocKh)
                    .HasColumnName("chamsoc_kh")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Goptrang)
                    .HasColumnName("goptrang")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.GuitinQc)
                    .HasColumnName("guitin_qc")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Taodon)
                    .HasColumnName("taodon")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Ten)
                    .HasColumnName("ten")
                    .HasMaxLength(50);

                entity.Property(e => e.TimKh)
                    .HasColumnName("tim_kh")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.TimSdt)
                    .HasColumnName("tim_sdt")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Tuvan)
                    .HasColumnName("tuvan")
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<FbChitietGiaohang>(entity =>
            {
                entity.ToTable("FB_Chitiet_giaohang");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedTime)
                    .HasColumnName("created_time")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Ghichu)
                    .HasColumnName("ghichu")
                    .HasMaxLength(10);

                entity.Property(e => e.Lydo)
                    .HasColumnName("lydo")
                    .HasMaxLength(250);

                entity.Property(e => e.Magiaohang)
                    .HasColumnName("magiaohang")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Mahd)
                    .HasColumnName("mahd")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.New)
                    .HasColumnName("new")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Reason)
                    .HasColumnName("reason")
                    .HasMaxLength(250);

                entity.Property(e => e.ReasonCode).HasColumnName("reason_code");

                entity.Property(e => e.ThoigianCapnhap)
                    .HasColumnName("thoigian_capnhap")
                    .HasColumnType("datetime");

                entity.Property(e => e.TrangthaiDonhang).HasColumnName("trangthai_donhang");

                entity.Property(e => e.TrangthaiVanchuyen).HasColumnName("trangthai_vanchuyen");

                entity.Property(e => e.View)
                    .HasColumnName("view")
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<FbChitietTinQc>(entity =>
            {
                entity.ToTable("FB_chitiet_tin_qc");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BroadcastId)
                    .HasColumnName("broadcast_id")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IdPage)
                    .HasColumnName("id_page")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<FbDonhang>(entity =>
            {
                entity.HasKey(e => e.Mahd);

                entity.ToTable("Fb_donhang");

                entity.Property(e => e.Mahd)
                    .HasColumnName("mahd")
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Diachi).HasColumnName("diachi");

                entity.Property(e => e.DienThoai)
                    .HasMaxLength(14)
                    .IsUnicode(false);

                entity.Property(e => e.DonViVanChuyen)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Ghichu).HasColumnName("ghichu");

                entity.Property(e => e.Giamgia)
                    .HasColumnName("giamgia")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.HoTenKhach).HasMaxLength(250);

                entity.Property(e => e.Khachno).HasDefaultValueSql("((0))");

                entity.Property(e => e.LoaiThanhtoan)
                    .HasColumnName("loaiThanhtoan")
                    .HasMaxLength(50);

                entity.Property(e => e.LuuTtVanchuyen).HasColumnName("luu_tt_vanchuyen");

                entity.Property(e => e.Magiao)
                    .HasColumnName("magiao")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Makho)
                    .HasColumnName("makho")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Ngaycapnhap)
                    .HasColumnName("ngaycapnhap")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Ngayhuy).HasColumnType("datetime");

                entity.Property(e => e.Ngaymua)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Nguoicapnhap)
                    .HasColumnName("nguoicapnhap")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nguoitao)
                    .HasColumnName("nguoitao")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nhanhang)
                    .HasColumnName("nhanhang")
                    .HasMaxLength(50);

                entity.Property(e => e.Nhanvien)
                    .HasColumnName("nhanvien")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Page)
                    .HasColumnName("page")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Phiship)
                    .HasColumnName("phiship")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Quanhuyen)
                    .HasColumnName("quanhuyen")
                    .HasMaxLength(150);

                entity.Property(e => e.Thanhtien)
                    .HasColumnName("thanhtien")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Tiencuoc)
                    .HasColumnName("tiencuoc")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Tienkhach).HasDefaultValueSql("((0))");

                entity.Property(e => e.Tientrakhach).HasDefaultValueSql("((0))");

                entity.Property(e => e.Tinhthanh)
                    .HasColumnName("tinhthanh")
                    .HasMaxLength(150);

                entity.Property(e => e.Tongtien).HasDefaultValueSql("((0))");

                entity.Property(e => e.Tongtra)
                    .HasColumnName("tongtra")
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<FbDonhangChitiet>(entity =>
            {
                entity.ToTable("Fb_donhang_chitiet");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Giagoc).HasColumnName("giagoc");

                entity.Property(e => e.IdSp).HasColumnName("id_sp");

                entity.Property(e => e.Mahd)
                    .HasColumnName("mahd")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Soluong).HasColumnName("soluong");

                entity.Property(e => e.Tensp)
                    .HasColumnName("tensp")
                    .HasMaxLength(250);

                entity.Property(e => e.Thanhtien).HasColumnName("thanhtien");
            });

            modelBuilder.Entity<FbDonviVanchuyen>(entity =>
            {
                entity.ToTable("Fb_Donvi_vanchuyen");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ApiKey)
                    .HasColumnName("api_key")
                    .IsUnicode(false);

                entity.Property(e => e.CusidViettel)
                    .HasColumnName("cusid_viettel")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IdDomain).HasColumnName("id_domain");

                entity.Property(e => e.Link)
                    .HasColumnName("link")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Logo)
                    .HasColumnName("logo")
                    .HasMaxLength(150);

                entity.Property(e => e.MakhoViettel)
                    .HasColumnName("makho_viettel")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Ngaytao)
                    .HasColumnName("ngaytao")
                    .HasColumnType("datetime");

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Ten)
                    .HasColumnName("ten")
                    .HasMaxLength(100);

                entity.Property(e => e.Trangthai).HasColumnName("trangthai");

                entity.Property(e => e.Username)
                    .HasColumnName("username")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Weight)
                    .HasColumnName("weight")
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<FbDonviVanchuyenChitiet>(entity =>
            {
                entity.ToTable("Fb_donvi_vanchuyen_chitiet");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ApiKey)
                    .HasColumnName("api_key")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AppId)
                    .HasColumnName("app_id")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CusidViettel)
                    .HasColumnName("cusid_viettel")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DiachiLayhang)
                    .HasColumnName("diachi_layhang")
                    .HasMaxLength(250);

                entity.Property(e => e.IdDonviVanchuyen).HasColumnName("id_donvi_vanchuyen");

                entity.Property(e => e.IdPage)
                    .HasColumnName("id_page")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Link)
                    .HasColumnName("link")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.MakhoViettel)
                    .HasColumnName("makho_viettel")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Ngaysua)
                    .HasColumnName("ngaysua")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Ngaytao)
                    .HasColumnName("ngaytao")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Nguoisua)
                    .HasColumnName("nguoisua")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nguoitao)
                    .HasColumnName("nguoitao")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Quanhuyen)
                    .HasColumnName("quanhuyen")
                    .HasMaxLength(150);

                entity.Property(e => e.SodienthoaiNguoilienhe)
                    .HasColumnName("sodienthoai_nguoilienhe")
                    .HasMaxLength(13)
                    .IsUnicode(false);

                entity.Property(e => e.TenNguoilienhe)
                    .HasColumnName("ten_nguoilienhe")
                    .HasMaxLength(150);

                entity.Property(e => e.Tinhthanh)
                    .HasColumnName("tinhthanh")
                    .HasMaxLength(150);

                entity.Property(e => e.Trangthai)
                    .HasColumnName("trangthai")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Username)
                    .HasColumnName("username")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Weight)
                    .HasColumnName("weight")
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<FbGhichu>(entity =>
            {
                entity.ToTable("FB_ghichu");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdPage)
                    .HasColumnName("id_page")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IdUser)
                    .HasColumnName("id_user")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NameFc)
                    .HasColumnName("name_fc")
                    .HasMaxLength(150);

                entity.Property(e => e.Noidung).HasColumnName("noidung");

                entity.Property(e => e.Time)
                    .HasColumnName("time")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<FbGoicuoc>(entity =>
            {
                entity.ToTable("FB_goicuoc");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Cauhinh).HasColumnName("cauhinh");

                entity.Property(e => e.Giatien).HasColumnName("giatien");

                entity.Property(e => e.GioihanGui)
                    .HasColumnName("gioihan_gui")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Songuoi).HasColumnName("songuoi");

                entity.Property(e => e.Sotrang).HasColumnName("sotrang");

                entity.Property(e => e.Ten)
                    .HasColumnName("ten")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<FbGoicuocChitiet>(entity =>
            {
                entity.ToTable("FB_goicuoc_chitiet");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AppId)
                    .HasColumnName("app_id")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.GioihanGui)
                    .HasColumnName("gioihan_gui")
                    .HasDefaultValueSql("((100))");

                entity.Property(e => e.IdUser)
                    .HasColumnName("id_user")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MaGoicuoc)
                    .IsRequired()
                    .HasColumnName("ma_goicuoc")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Mienphi)
                    .HasColumnName("mienphi")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Nangcap)
                    .HasColumnName("nangcap")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.NgayKetthuc)
                    .HasColumnName("ngay_ketthuc")
                    .HasColumnType("datetime");

                entity.Property(e => e.NgayKichhoat)
                    .HasColumnName("ngay_kichhoat")
                    .HasColumnType("datetime");

                entity.Property(e => e.Ngaytao)
                    .HasColumnName("ngaytao")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Nguoicapnhap)
                    .HasColumnName("nguoicapnhap")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nguoikichhoat)
                    .HasColumnName("nguoikichhoat")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nguoinangcap)
                    .HasColumnName("nguoinangcap")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SenMailThongke)
                    .HasColumnName("sen_mail_thongke")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Sonhanvien).HasColumnName("sonhanvien");

                entity.Property(e => e.SonhanvienNc)
                    .HasColumnName("sonhanvien_nc")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Sothang).HasColumnName("sothang");

                entity.Property(e => e.SothangNc)
                    .HasColumnName("sothang_nc")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Sotien)
                    .HasColumnName("sotien")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Sotrang).HasColumnName("sotrang");

                entity.Property(e => e.SotrangNc)
                    .HasColumnName("sotrang_nc")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ThoigianCapnhap)
                    .HasColumnName("thoigian_capnhap")
                    .HasColumnType("datetime");

                entity.Property(e => e.ThoigianNangcap)
                    .HasColumnName("thoigian_nangcap")
                    .HasColumnType("datetime");

                entity.Property(e => e.TienNangcap)
                    .HasColumnName("tien_nangcap")
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UserActive)
                    .HasColumnName("user_active")
                    .IsUnicode(false);
            });

            modelBuilder.Entity<FbGoicuocFanpage>(entity =>
            {
                entity.ToTable("FB_goicuoc_fanpage");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Active)
                    .HasColumnName("active")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.IdPage)
                    .HasColumnName("id_page")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MaGoicuoc)
                    .HasColumnName("ma_goicuoc")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Ngaycapnhap)
                    .HasColumnName("ngaycapnhap")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Ngaytao)
                    .HasColumnName("ngaytao")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Nguoitao)
                    .HasColumnName("nguoitao")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nhanvien)
                    .HasColumnName("nhanvien")
                    .IsUnicode(false);
            });

            modelBuilder.Entity<FbGuitinHangloat>(entity =>
            {
                entity.ToTable("Fb_guitin_hangloat");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DanhsachPage).HasColumnName("danhsach_page");

                entity.Property(e => e.Hengio)
                    .HasColumnName("hengio")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.KhCotuongtac)
                    .HasColumnName("kh_cotuongtac")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.KhDamuahang)
                    .HasColumnName("kh_damuahang")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.KhKhongphanhoi)
                    .HasColumnName("kh_khongphanhoi")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.KhTuongtactot)
                    .HasColumnName("kh_tuongtactot")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.KhoangNgaygui).HasColumnName("khoang_ngaygui");

                entity.Property(e => e.NgayBdgui)
                    .HasColumnName("ngay_bdgui")
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Noidung).HasColumnName("noidung");

                entity.Property(e => e.ThoigianHengui)
                    .HasColumnName("thoigian_hengui")
                    .HasColumnType("datetime");

                entity.Property(e => e.TtDamuahang)
                    .HasColumnName("tt_damuahang")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TtKhongmuahang)
                    .HasColumnName("tt_khongmuahang")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TtTuvan)
                    .HasColumnName("tt_tuvan")
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<FbLichhen>(entity =>
            {
                entity.ToTable("Fb_lichhen");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Ghichu).HasColumnName("ghichu");

                entity.Property(e => e.IdMessage)
                    .HasColumnName("id_message")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IdPage)
                    .HasColumnName("id_page")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Ngayhen)
                    .HasColumnName("ngayhen")
                    .HasColumnType("date");

                entity.Property(e => e.Nguoitao)
                    .HasColumnName("nguoitao")
                    .HasMaxLength(250);

                entity.Property(e => e.Xong)
                    .HasColumnName("xong")
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<FbLichsuxem>(entity =>
            {
                entity.ToTable("FB_lichsuxem");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Account)
                    .HasColumnName("account")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Datetime)
                    .HasColumnName("datetime")
                    .HasColumnType("datetime");

                entity.Property(e => e.IdMessage)
                    .HasColumnName("id_message")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IdPage)
                    .HasColumnName("id_page")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Xemboi)
                    .HasColumnName("xemboi")
                    .HasMaxLength(250);
            });

            modelBuilder.Entity<FbMessages>(entity =>
            {
                entity.HasKey(e => e.I);

                entity.ToTable("FB_messages");

                entity.HasIndex(e => e.IdPage)
                    .HasName("NonClusteredIndex-id_page");

                entity.HasIndex(e => new { e.IdPage, e.Id })
                    .HasName("index_id_message_id_page");

                entity.HasIndex(e => new { e.IdPage, e.IdUser })
                    .HasName("index_id_page_id_user");

                entity.Property(e => e.I).HasColumnName("i");

                entity.Property(e => e.Address).HasColumnName("address");

                entity.Property(e => e.Advisory)
                    .HasColumnName("advisory")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Block)
                    .HasColumnName("block")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Boqua)
                    .HasColumnName("boqua")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Change)
                    .HasColumnName("change")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Cotuongtac)
                    .HasColumnName("cotuongtac")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Damuahang)
                    .HasColumnName("damuahang")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Done)
                    .HasColumnName("done")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(50);

                entity.Property(e => e.FindPhone)
                    .HasColumnName("find_phone")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Goi)
                    .HasColumnName("goi")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Hoten)
                    .HasColumnName("hoten")
                    .HasMaxLength(250);

                entity.Property(e => e.Id)
                    .IsRequired()
                    .HasColumnName("id")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IdPage)
                    .HasColumnName("id_page")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IdUser)
                    .HasColumnName("id_user")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Khongmuahang)
                    .HasColumnName("khongmuahang")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Khongphanhoi)
                    .HasColumnName("khongphanhoi")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Message)
                    .HasColumnName("message")
                    .HasColumnType("ntext");

                entity.Property(e => e.NameUser)
                    .HasColumnName("name_user")
                    .HasMaxLength(250);

                entity.Property(e => e.Nguoicapnhap).HasMaxLength(250);

                entity.Property(e => e.Note).HasColumnName("note");

                entity.Property(e => e.Phone)
                    .HasColumnName("phone")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneAo)
                    .HasColumnName("phone_ao")
                    .HasMaxLength(13)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneKhac)
                    .HasColumnName("phone_khac")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Psid)
                    .HasColumnName("psid")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Quanhuyen)
                    .HasColumnName("quanhuyen")
                    .HasMaxLength(150);

                entity.Property(e => e.Success)
                    .HasColumnName("success")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ThoigianTao)
                    .HasColumnName("thoigian_tao")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ThoigianTuongtac)
                    .HasColumnName("thoigian_tuongtac")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Thoigiangoi).HasColumnName("thoigiangoi");

                entity.Property(e => e.Tinhthanh)
                    .HasColumnName("tinhthanh")
                    .HasMaxLength(150);

                entity.Property(e => e.Tuongtactot)
                    .HasColumnName("tuongtactot")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Unknow)
                    .HasColumnName("unknow")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("update_time")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Views).HasColumnName("views");

                entity.Property(e => e.ViewsBy)
                    .HasColumnName("views_by")
                    .HasColumnType("ntext");

                entity.Property(e => e.ViewsUpdate).HasColumnName("views_update");

                entity.Property(e => e.YelledCapital)
                    .HasColumnName("yelled_capital")
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<FbMessages2>(entity =>
            {
                entity.HasKey(e => e.I);

                entity.ToTable("FB_messages2");

                entity.Property(e => e.I).HasColumnName("i");

                entity.Property(e => e.Address).HasColumnName("address");

                entity.Property(e => e.Advisory).HasColumnName("advisory");

                entity.Property(e => e.Block).HasColumnName("block");

                entity.Property(e => e.Boqua).HasColumnName("boqua");

                entity.Property(e => e.Change).HasColumnName("change");

                entity.Property(e => e.Cotuongtac).HasColumnName("cotuongtac");

                entity.Property(e => e.Damuahang).HasColumnName("damuahang");

                entity.Property(e => e.Done).HasColumnName("done");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(50);

                entity.Property(e => e.FindPhone).HasColumnName("find_phone");

                entity.Property(e => e.Goi).HasColumnName("goi");

                entity.Property(e => e.Hoten)
                    .HasColumnName("hoten")
                    .HasMaxLength(250);

                entity.Property(e => e.Id)
                    .IsRequired()
                    .HasColumnName("id")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IdPage)
                    .HasColumnName("id_page")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IdUser)
                    .HasColumnName("id_user")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Khongmuahang).HasColumnName("khongmuahang");

                entity.Property(e => e.Khongphanhoi).HasColumnName("khongphanhoi");

                entity.Property(e => e.Message)
                    .HasColumnName("message")
                    .HasColumnType("ntext");

                entity.Property(e => e.NameUser)
                    .HasColumnName("name_user")
                    .HasMaxLength(250);

                entity.Property(e => e.Nguoicapnhap).HasMaxLength(250);

                entity.Property(e => e.Note).HasColumnName("note");

                entity.Property(e => e.Phone)
                    .HasColumnName("phone")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneAo)
                    .HasColumnName("phone_ao")
                    .HasMaxLength(13)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneKhac)
                    .HasColumnName("phone_khac")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Psid)
                    .HasColumnName("psid")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Quanhuyen)
                    .HasColumnName("quanhuyen")
                    .HasMaxLength(150);

                entity.Property(e => e.Success).HasColumnName("success");

                entity.Property(e => e.ThoigianTao)
                    .HasColumnName("thoigian_tao")
                    .HasColumnType("datetime");

                entity.Property(e => e.ThoigianTuongtac)
                    .HasColumnName("thoigian_tuongtac")
                    .HasColumnType("datetime");

                entity.Property(e => e.Thoigiangoi).HasColumnName("thoigiangoi");

                entity.Property(e => e.Tinhthanh)
                    .HasColumnName("tinhthanh")
                    .HasMaxLength(150);

                entity.Property(e => e.Tuongtactot).HasColumnName("tuongtactot");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Unknow)
                    .HasColumnName("unknow")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("update_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.Views).HasColumnName("views");

                entity.Property(e => e.ViewsBy)
                    .HasColumnName("views_by")
                    .HasColumnType("ntext");

                entity.Property(e => e.ViewsUpdate).HasColumnName("views_update");

                entity.Property(e => e.YelledCapital).HasColumnName("yelled_capital");
            });

            modelBuilder.Entity<FbPageDetail>(entity =>
            {
                entity.ToTable("FB_page_detail");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Chude)
                    .HasColumnName("chude")
                    .HasMaxLength(250);

                entity.Property(e => e.IdPage)
                    .HasColumnName("id_page")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IdUser)
                    .HasColumnName("id_user")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Quyen)
                    .HasColumnName("quyen")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Xoa)
                    .HasColumnName("xoa")
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<FbSanpham>(entity =>
            {
                entity.ToTable("Fb_sanpham");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Gia)
                    .HasColumnName("gia")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Hinhanh)
                    .HasColumnName("hinhanh")
                    .HasMaxLength(250);

                entity.Property(e => e.IdPage)
                    .HasColumnName("id_page")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Khoiluong)
                    .HasColumnName("khoiluong")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Mota).HasColumnName("mota");

                entity.Property(e => e.Ngaysua)
                    .HasColumnName("ngaysua")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Ngaytao)
                    .HasColumnName("ngaytao")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Nguoitao)
                    .HasColumnName("nguoitao")
                    .HasMaxLength(250);

                entity.Property(e => e.Tensp)
                    .HasColumnName("tensp")
                    .HasMaxLength(250);

                entity.Property(e => e.Thungrac)
                    .HasColumnName("thungrac")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Trangthai)
                    .HasColumnName("trangthai")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.WebConnect).HasColumnName("web_connect");
            });

            modelBuilder.Entity<FbSetting>(entity =>
            {
                entity.ToTable("Fb_setting");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Baotri)
                    .HasColumnName("baotri")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.EmailNhanThanhtoan)
                    .HasColumnName("email_nhan_thanhtoan")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FullQuyen)
                    .HasColumnName("full_quyen")
                    .IsUnicode(false);

                entity.Property(e => e.GuimailLoi).HasColumnName("Guimail_loi");

                entity.Property(e => e.SNhanvien)
                    .HasColumnName("s_nhanvien")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.SThang)
                    .HasColumnName("s_thang")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.STrang)
                    .HasColumnName("s_trang")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.SaveEtimedout)
                    .HasColumnName("save_ETIMEDOUT")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.SvMsText)
                    .HasColumnName("sv_ms_text")
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<FbTag>(entity =>
            {
                entity.ToTable("FB_tag");

                entity.HasIndex(e => e.IdPage)
                    .HasName("NonClusteredIndex-20190628-165800");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Color)
                    .HasColumnName("color")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedIdBy)
                    .HasColumnName("created_id_by")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedTime)
                    .HasColumnName("created_time")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IdPage)
                    .HasColumnName("id_page")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<FbTagTrangthai>(entity =>
            {
                entity.HasKey(e => e.LabelId);

                entity.ToTable("Fb_tag_trangthai");

                entity.Property(e => e.LabelId)
                    .HasColumnName("label_id")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.IdPage)
                    .HasColumnName("id_page")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<FbThongbao>(entity =>
            {
                entity.ToTable("FB_thongbao");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Button)
                    .HasColumnName("button")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Gimdau)
                    .HasColumnName("gimdau")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.IdUser)
                    .HasColumnName("id_user")
                    .IsUnicode(false);

                entity.Property(e => e.Link)
                    .HasColumnName("link")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Ngaytao)
                    .HasColumnName("ngaytao")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Nguoidoc).HasColumnName("nguoidoc");

                entity.Property(e => e.Nguoitao)
                    .HasColumnName("nguoitao")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Noidung).HasColumnName("noidung");

                entity.Property(e => e.Style)
                    .HasColumnName("style")
                    .IsUnicode(false);

                entity.Property(e => e.Thoigian)
                    .HasColumnName("thoigian")
                    .HasColumnType("datetime");

                entity.Property(e => e.Thungrac)
                    .HasColumnName("thungrac")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Tieude)
                    .HasColumnName("tieude")
                    .HasMaxLength(250);

                entity.Property(e => e.Trangthai)
                    .HasColumnName("trangthai")
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<FbThongbaoVanchuyen>(entity =>
            {
                entity.ToTable("Fb_thongbao_vanchuyen");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Account)
                    .HasColumnName("account")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AccountNhanvien)
                    .HasColumnName("account_nhanvien")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ChitietDon)
                    .HasColumnName("chitiet_don")
                    .HasMaxLength(250);

                entity.Property(e => e.IdPage)
                    .HasColumnName("id_page")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Link)
                    .HasColumnName("link")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Magiaohang)
                    .HasColumnName("magiaohang")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Mahd)
                    .HasColumnName("mahd")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Thoigiancapnhap)
                    .HasColumnName("thoigiancapnhap")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.TrangthaiGiaohang).HasColumnName("trangthai_giaohang");
            });

            modelBuilder.Entity<FbTinQc>(entity =>
            {
                entity.HasKey(e => e.BroadcastId);

                entity.ToTable("Fb_tin_qc");

                entity.Property(e => e.BroadcastId)
                    .HasColumnName("broadcast_id")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.ContentMsCreative).HasColumnName("content_ms_creative");

                entity.Property(e => e.CountTin)
                    .HasColumnName("count_tin")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CustomLabelId)
                    .HasColumnName("custom_label_id")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DateTime)
                    .HasColumnName("date_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.IdPage)
                    .HasColumnName("id_page")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MessageCreativeId)
                    .HasColumnName("message_creative_id")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasMaxLength(20);

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<FbUserToken>(entity =>
            {
                entity.ToTable("FB_user_token");

                entity.HasIndex(e => e.IdUser)
                    .HasName("index_id_user_page");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AccessToken)
                    .HasColumnName("access_token")
                    .IsUnicode(false);

                entity.Property(e => e.ActiveTime)
                    .HasColumnName("active_time")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ApiConnect)
                    .HasColumnName("api_connect")
                    .IsUnicode(false);

                entity.Property(e => e.AppId)
                    .HasColumnName("app_id")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BatNhieutag)
                    .HasColumnName("bat_nhieutag")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.CheckConnect)
                    .HasColumnName("check_connect")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ClickRightTag)
                    .HasColumnName("click_right_tag")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Goicuoc)
                    .HasColumnName("goicuoc")
                    .HasDefaultValueSql("((3))");

                entity.Property(e => e.HienthiTentag)
                    .HasColumnName("hienthi_tentag")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.IdUser)
                    .IsRequired()
                    .HasColumnName("id_user")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LinkConnect).HasColumnName("link_connect");

                entity.Property(e => e.LocNhieutag)
                    .HasColumnName("loc_nhieutag")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.MaThanhtoan)
                    .HasColumnName("ma_thanhtoan")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.NameUser).HasColumnName("name_user");

                entity.Property(e => e.Page)
                    .HasColumnName("page")
                    .IsUnicode(false);

                entity.Property(e => e.PageActive)
                    .HasColumnName("page_active")
                    .IsUnicode(false);

                entity.Property(e => e.Perms)
                    .HasColumnName("perms")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.SaveInforHopthoai)
                    .HasColumnName("save_infor_hopthoai")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Sync)
                    .HasColumnName("sync")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Typing)
                    .HasColumnName("typing")
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<FbWebsite>(entity =>
            {
                entity.ToTable("FB_website");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ApiKey)
                    .HasColumnName("api_key")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.GioihanGui)
                    .HasColumnName("gioihan_gui")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.KhoaGhichu)
                    .HasColumnName("khoa_ghichu")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Link)
                    .HasColumnName("link")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Ngaytao)
                    .HasColumnName("ngaytao")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Nguoitao)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NoPaste)
                    .HasColumnName("no_paste")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SenMailThongke)
                    .HasColumnName("sen_mail_thongke")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Ten)
                    .HasColumnName("ten")
                    .HasMaxLength(50);

                entity.Property(e => e.Trangthai)
                    .HasColumnName("trangthai")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.UrlImg)
                    .HasColumnName("url_img")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

           
        }
    }
}
