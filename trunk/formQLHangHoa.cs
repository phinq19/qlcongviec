using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using QLKD;
using System.Data.SqlClient;

namespace PN_QLKD.DanhMuc
{
    public partial class formQLHangHoa : DevExpress.XtraEditors.XtraForm
    {
        bool Saved; 
        public HangHoaClass _matHang;
        public int flagChinhSua = 0;

        public formQLHangHoa()
        {
            InitializeComponent();
            _matHang = null;   
            Saved = true;
        }
        public string status = "NORMAL";
        public void GetResource()
        {
            btnLuu.Text = MyFunction.GetResourceName("btnLuu");
            btnHuy.Text = MyFunction.GetResourceName("btnXoa");
            btnThoat.Text = MyFunction.GetResourceName("btnThoat");

        }
        public PhanQuyenClass _phanQuyen;
        private void frmQLMatHang_Load(object sender, EventArgs e)
        {
            if (flagChinhSua == 0)
            {
                _phanQuyen = PhanQuyenClass.KiemTraPhanQuyen(MyFunction.NguoiDungHienTai.ID, "MatHang");
            }
            else
            {
                _phanQuyen = PhanQuyenClass.KiemTraPhanQuyen(MyFunction.NguoiDungHienTai.ID, "DieuChinhThongTinSanPham");
            }

            if (_phanQuyen == null)
            {
                MessageBox.Show("Không có quyền mở màn hình này.", "Phân quyền", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            GetResource();
            _InitData();          

            if (_matHang != null&& status=="EDIT")
            {
                _SetFormInfo();
            }
            else
            {
                _ClearForm();
            }
            if (flagChinhSua == 1)
            {
               
                btnHuy.Enabled = false;
                calctGiaNhap.Enabled = false;
                calctGiaNhap.Properties.DisplayFormat.FormatString = "***,***";
            }
            _setFormStatus(status);
        }       


        private void _ClearForm()
        {
 
            txtMa.Text = "";
            txtBarCode.Text = "";
            txtTen.Text = "";         
            lkpNhom.EditValue = null;
            lkpHang.EditValue = null;
            lookUpEditDonViTinh.EditValue = null;
            lookUpEditDonViTinhPhu.EditValue = null;
            txtGhiChu.Text = "";
            txtKhuyenMai.Text = "";
            calcBaoHanh.Value = 12;

            calctThueNhap.Value = 0;
            calctGiaBanSi.Value = 0;
            calctGiaBanLe.Value = 0;
            calctThueBan.Value = 0;
            calctGiaNhap.Value = 0;
            calThuTu.Value = 0;
            chkTrangThai.Checked = true;
            chkBaoGia.Checked = true;
            txtMa.Text = "";
            txtMa.Focus();
            _matHang = null;
            Saved = true;
        }

        private void _setFormStatus(string s)//-1 new -saved ; 0 edited new - not save ;
        {
            status = s;
            switch (s)
            {
                case "EDIT":
                    txtMa.Enabled = false;
                    btnLuu.Enabled = true;
                    btnXoa.Enabled = true;
                    Saved = true;
                    btnHuy.Text = MyFunction.GetResourceName("btnXoa");
                    txtTen.Focus();
                    break;
                case "NEW":
                    txtMa.Enabled = true;
                    btnLuu.Enabled = true;
                    btnXoa.Enabled = false;
                    btnHuy.Text = MyFunction.GetResourceName("btnHuy");
                    Saved = false;
                    txtMa.Focus();
                    break;
                case "NORMAL":
                    btnLuu.Enabled = true;  
                    Saved = false;
                    break;
            }
            
        
        }

        private void _InitData()
        {
            _LoadDonViTinh();
            _LoadNhomMatHang();
            _LoadHangSX(); 
        }
       
        private void _LoadNhomMatHang()
        {
            CommonClass._LoadDanhSachNhomHang(lkpNhom);
       
        }
        private void _LoadHangSX()
        {
            CommonClass._LoadDanhSachHangSX(lkpHang);
        }
        private void _LoadDonViTinh()
        {
            CommonClass._LoadDanhSachDonVi(lookUpEditDonViTinh);
            CommonClass._LoadDanhSachDonVi(lookUpEditDonViTinhPhu);
            
       }


        private void btnLuu_Click(object sender, EventArgs e)
        {   
            //    update = false;
            HangHoaClass temp = _GetFormData();
            if (temp != null)
            {
                if (_matHang == null&&status=="NEW") //thêm mới
                {
                    if (_phanQuyen == null || _phanQuyen.Them == false)
                    {
                        MessageBox.Show("Không có quyền thực hiện chức năng này", "Phân quyền", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    if (HangHoaClass.CheckExist(temp.MaHH,-1)==false)
                    {
                        long Ma=HangHoaClass.Insert(temp);

                        DialogResult = DialogResult.OK;
                        if (chkCloseAlterSave.Checked)
                        {
                            
                            this.Close();
                            return;
                        }
                        else
                        {
                            
                            MessageBox.Show("Đã thêm mới thành công");
                        }
                        _ClearForm();
                        _setFormStatus("NEW");
                        
                    }
                    else
                    {
                        MessageBox.Show("Mã hàng đã tồn tại!");
                    }                    
                }
                else if (_matHang != null && status == "EDIT")
                {
                    if (_phanQuyen == null || _phanQuyen.Sua == false)
                    {
                        MessageBox.Show("Không có quyền thực hiện chức năng này", "Phân quyền", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    temp.Ma = _matHang.Ma;
                    if (!HangHoaClass.CheckExist(temp.MaHH,temp.Ma))
                    {
                        temp.SoLuong = _matHang.SoLuong;
                        HangHoaClass.Update(temp);
                        DialogResult = DialogResult.OK;
                        if (chkCloseAlterSave.Checked)
                        {
                            
                            this.Close();
                            return;
                        }
                        else
                        {
                            MessageBox.Show("Lưu thông tin cập nhập thành công");
                            this.Close();
                        }
                        
                      
                    }
                    //else
                    //{
                    //    MessageBox.Show("Mã hàng đã tồn tại!");
                    //}

                }
            }
            
        }

        private HangHoaClass _GetFormData()
        {
            HangHoaClass temp = null;
            if (txtMa.Text != "")
            {
                
                if (txtTen.Text != "")
                {
                   
                    if (lkpNhom.EditValue != null)
                    {
                       
                        if (lookUpEditDonViTinh.EditValue!= null)
                        {
                            if (calctGiaNhap.Value > 0)
                            {
                                temp = new HangHoaClass();
                                temp.MaHH = txtMa.Text;
                                temp.TenHH = txtTen.Text;
                                temp.MaLoaiHH = lkpNhom.EditValue.ToString();
                                temp.MaHangSX = lkpHang.EditValue.ToString();
                                temp.BaoHanh = int.Parse(calcBaoHanh.Value.ToString());
                                temp.BarCode = txtBarCode.Text;
                                temp.GhiChu = txtGhiChu.Text;
                                temp.KhuyenMai = txtKhuyenMai.Text;

                                temp.MaDVT = (int) lookUpEditDonViTinh.EditValue;
                                if (lookUpEditDonViTinhPhu.EditValue != null)
                                {
                                    temp.MaDVTPhu = (int) lookUpEditDonViTinhPhu.EditValue;
                                }
                                // temp.QuyCach = txtQuyCach.Text;                    
                                temp.ThueNhap = (int) calctThueNhap.Value;
                                temp.GiaBanSi = (double) calctGiaBanSi.Value;
                                temp.GiaBanLe = (double) calctGiaBanLe.Value;
                                temp.ThueBan = (int) calctThueBan.Value;
                                temp.GiaNhap = (double) calctGiaNhap.Value;
                                temp.TinhTrang = chkTrangThai.Checked;
                                temp.BaoGia = chkBaoGia.Checked;
                                temp.ThuTu = (long) (calThuTu.Value);
                            }
                            else
                            {
                                MessageBox.Show("Nhập giá nhập.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                calctGiaNhap.Focus();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Nhập đơn vị tính", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            lookUpEditDonViTinh.Focus();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Nhập nhóm mặt hàng", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        lkpNhom.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Nhập tên mặt hàng", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTen.Focus();
                }

            }
            else
            {
                MessageBox.Show("Nhập mã mặt hàng", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMa.Focus();
            }

            return temp;
        }

        private void btnThemMoi_Click(object sender, EventArgs e)
        {
           // _ClearForm();
            //if (!Saved)
            //{
            //    if (MessageBox.Show("Mặt hàng chưa được lưu. Bạn có muốn huỷ các thay đổi", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            //    {
            //        _ClearForm();
            //        _setFormStatus(-1);
            //    }
            //}
            //else
            {
                _ClearForm();
                _setFormStatus("NEW");
            }
            
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            //if (!Saved)
            //{
            //    if (MessageBox.Show("Thông tin chưa được lưu. Bạn có muốn huỷ các thay đổi?", "Thông báo",MessageBoxButtons.YesNo) == DialogResult.Yes)
            //    {
            //        this.Close();
            //    }
            //}
            //else
            {
                this.Close();
            }
        }  
        private void _SetFormInfo()
        {
            if (_matHang != null)
            {
                txtMa.Text = _matHang.MaHH;
                txtTen.Text = _matHang.TenHH;
               // txtQuyCach.Text = _matHang.QuyCach;
                lkpNhom.EditValue = _matHang.MaLoaiHH;
                lkpHang.EditValue = _matHang.MaHangSX;
                lookUpEditDonViTinh.EditValue = _matHang.MaDVT;
                lookUpEditDonViTinhPhu.EditValue = _matHang.MaDVTPhu;
                calcBaoHanh.Value = _matHang.BaoHanh;
                txtKhuyenMai.Text = _matHang.KhuyenMai;
                txtGhiChu.Text = _matHang.GhiChu;
                txtBarCode.Text = _matHang.BarCode;
                calctGiaNhap.Value = (decimal)_matHang.GiaNhap;
                calctThueNhap.Value = _matHang.ThueNhap;
                calctThueBan.Value = _matHang.ThueBan;
                calctGiaBanSi.Value = (decimal)_matHang.GiaBanSi;
                calctGiaBanLe.Value = (decimal)_matHang.GiaBanLe;
                calThuTu.Value = (decimal)_matHang.ThuTu;
                chkBaoGia.Checked = _matHang.BaoGia;
                chkTrangThai.Checked = _matHang.TinhTrang;

            }
           
        }

        //private MatHang _GetMatHang(DataRow dataRow)
        //{      
        //    //long MaMH = long.Parse(dataRow["Ma"].ToString());
        //    //MatHang mh = MatHang.ExecuteSelect(MaMH);
        //    //return mh;
        //}

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (_matHang != null&&status=="EDIT")
            {
                if (MessageBox.Show("Bạn có muốn xoá mặt hàng này", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {       
                    try
                    {
                         HangHoaClass.Delete(_matHang.Ma);
                        _ClearForm();
                        MessageBox.Show("Xóa thành công mặt hàng này.");
                        this.Close();
                    }
                    catch
                    {
                        MessageBox.Show("Không thể xoá mặt hàng đang được sử dụng");
                    }
                }
            }
        }
    
        private void lookUpEditDonViTinhPhu_EditValueChanged(object sender, EventArgs e)
        {
            if (lookUpEditDonViTinhPhu.EditValue != null)
            {

                lookUpEditDonViTinhPhu.EditValue = lookUpEditDonViTinh.EditValue;

            }
       
        }

        private void lookUpEditDonViTinh_EditValueChanged(object sender, EventArgs e)
        {
            if (lookUpEditDonViTinh.EditValue != null)
            {
                lookUpEditDonViTinh.EditValue = lookUpEditDonViTinhPhu.EditValue;
            } 
            
        }

        private void btnThemNhomMoi_Click(object sender, EventArgs e)
        {
            formLoaiHangHoa frm = new formLoaiHangHoa();
            frm.ShowDialog();
            object temp = lkpNhom.EditValue;
            _LoadNhomMatHang();
            lkpNhom.EditValue = temp;

        }

        private void btnThemDVTMoi_Click(object sender, EventArgs e)
        {
            formDonViTinh frm = new formDonViTinh();
            frm.ShowDialog();
            object temp1 = lookUpEditDonViTinh.EditValue;
            object temp2 = lookUpEditDonViTinhPhu.EditValue;
            _LoadDonViTinh();
            lookUpEditDonViTinh.EditValue = temp1;
            lookUpEditDonViTinhPhu.EditValue= temp2;
        }

       

        private void txtMa_Leave(object sender, EventArgs e)
        {
           if(status=="NEW")
               {
                if(HangHoaClass.CheckExist(txtMa.Text,-1)==true)
                {
                    MessageBox.Show("Mã hàng này đã tồn tại");
                    txtMa.Focus();
                }
            }
            else if(status=="EDIT" && _matHang!=null)
               {
                   if (HangHoaClass.CheckExist(txtMa.Text, _matHang.Ma) == true)
                   {
                       MessageBox.Show("Mã hàng này đã tồn tại");
                       txtMa.Focus();
                   }
               }

        }

       
        private void btnThemHangMoi_Click(object sender, EventArgs e)
        {
            formHangSX frm = new formHangSX();
            frm.ShowDialog();
            object temp = lkpHang.EditValue;
            _LoadHangSX();
            lkpHang.EditValue = temp;

        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            if (status == "EDIT")
            {
                if (_phanQuyen == null || _phanQuyen.Xoa == false)
                {
                    MessageBox.Show("Không có quyền thực hiện chức năng này", "Phân quyền", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (MessageBox.Show("Bạn có muốn xoá mặt hàng này", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        HangHoaClass.Delete(_matHang.Ma);
                        _ClearForm();
                        DialogResult = DialogResult.OK;
                        MessageBox.Show("Xóa thành công mặt hàng này.");
                        this.Close();
                    }
                    catch
                    {
                        MessageBox.Show("Không thể xoá mặt hàng đang được sử dụng");
                    }
                }
            }
            else if(status=="NEW")
            {
                _ClearForm();
                _setFormStatus("NEW");
            }
        }

        private void btnThemDonVi_Click(object sender, EventArgs e)
        {
            formDonViTinh frm = new formDonViTinh();
            frm.ShowDialog();
            object temp = lookUpEditDonViTinh.EditValue;
            _LoadDonViTinh();
            lookUpEditDonViTinh.EditValue = temp;

        }



      
    }
}