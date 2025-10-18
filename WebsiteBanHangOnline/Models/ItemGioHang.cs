using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebsiteBanHangOnline.Models
{
    public class ItemGioHang
    {

        public int MaSP { get; set; }
        public string TenSP { get; set; }
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public decimal ThanhTien { get; set; }
        public string HinhAnh { get; set; }
        public int MaLoai { get; set; }
        public int Maphong { get; set; }
        public ItemGioHang()
        {

        }
        public ItemGioHang(int maSP)
        {
            using (QuanLyBanHangOnlineEntities2 db = new QuanLyBanHangOnlineEntities2())
            {
                this.MaSP = maSP;
                SanPham sanPham = db.SanPhams.Single(m => m.MaSP == maSP);
                this.TenSP = sanPham.TenSP;
                this.HinhAnh = sanPham.HinhAnh;
                this.DonGia = sanPham.DonGia.Value;
                this.ThanhTien = DonGia * SoLuong;
                this.MaLoai = (int)sanPham.MaLoaiSP;
                this.Maphong = (int)sanPham.MaPhong;
            }
        }
        public ItemGioHang(int maSP, int soLuong)
        {
            using (QuanLyBanHangOnlineEntities2 db = new QuanLyBanHangOnlineEntities2())
            {
                this.MaSP = maSP;
                SanPham sanPham = db.SanPhams.Single(m => m.MaSP == maSP);
                this.TenSP = sanPham.TenSP;
                this.HinhAnh = sanPham.HinhAnh;
                this.DonGia = sanPham.DonGia.Value;
                this.SoLuong = soLuong;
                this.SoLuong = 1;
                this.ThanhTien = DonGia * SoLuong;
                this.MaLoai = (int)sanPham.MaLoaiSP;
                this.Maphong = (int)sanPham.MANSX;
            }
        }
    }
}