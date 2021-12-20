using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SourceCode
{
    public partial class Form1 : Form
    {
        SqlConnection connect;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            connect = new SqlConnection(@"Data Source=HONGQUAN\SQLEXPRESS;Initial Catalog=QL_DH_GH_ONLINE;Integrated Security=True");
            connect.Open();
        }

        private void Form_Close(object sender, FormClosingEventArgs e)
        {
            connect.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;
            string role = comboBox1.Text.ToString();

            string sqlselect = "SELECT * FROM dbo.LOGIN WHERE USERNAME = '" + username + "' AND PASSWORD = '" + password + "' AND ROLE_USER = N'" + role + "'";
            SqlDataAdapter sda = new SqlDataAdapter(sqlselect, connect);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            string sqlselect1 = "SELECT USERID FROM LOGIN WHERE USERNAME = '" + username + "' AND PASSWORD = '" + password + "' AND ROLE_USER = N'" + role + "'";
            SqlCommand cmd1 = new SqlCommand(sqlselect1, connect);
            string id = Convert.ToString(cmd1.ExecuteScalar());


            if (dt.Rows.Count == 1)
            {
                DialogResult res = MessageBox.Show("Đăng nhập thành công!", "Thông báo!");
                
                if (res == DialogResult.OK)
                {
                 
                    if (role == "Đối tác")
                    {
                        string sqlselect2 = "SELECT TENDOITAC FROM DOITAC WHERE MADT = '" + id + "'";
                        SqlCommand cmd2 = new SqlCommand(sqlselect2, connect);
                        string name = Convert.ToString(cmd2.ExecuteScalar());

                        DoiTac doiTac = new DoiTac();
                        doiTac.temp = id;
                        doiTac.temp1 = name;
                        doiTac.Show();
                        this.Hide();
                    }
                    else if (role == "Nhân viên")
                    {
                        string sqlselect2 = "SELECT TENNV FROM NHANVIEN WHERE MANV = '" + id + "'";
                        SqlCommand cmd2 = new SqlCommand(sqlselect2, connect);
                        string name = Convert.ToString(cmd2.ExecuteScalar());

                        string sqlselect3 = "SELECT MACN FROM NHANVIEN WHERE MANV = '" + id + "'";
                        SqlCommand cmd3 = new SqlCommand(sqlselect3, connect);
                        string cn = Convert.ToString(cmd3.ExecuteScalar());

                        string sqlselect4 = "SELECT DIACHI FROM CHINHANH WHERE MACN = '" + cn + "'";
                        SqlCommand cmd4 = new SqlCommand(sqlselect4, connect);
                        string name_cn = Convert.ToString(cmd4.ExecuteScalar());

                        NhanVien nhanvien = new NhanVien();
                        nhanvien.temp = id;
                        nhanvien.temp1 = name;
                        nhanvien.CN = cn;
                        nhanvien.Show();
                        this.Hide();
                    }
                    else if (role == "Khách hàng")
                    {
                        string sqlselect2 = "SELECT HOTEN FROM KHACHHANG WHERE MAKH = '" + id + "'";
                        SqlCommand cmd2 = new SqlCommand(sqlselect2, connect);
                        string name = Convert.ToString(cmd2.ExecuteScalar());

                        KhachHang khachhang = new KhachHang();
                        khachhang.temp = id;
                        khachhang.temp1 = name;
                        khachhang.Show();
                        this.Hide();
                    }   
                    else if (role == "Tài xế")
                    {
                        
                        string sqlselect2 = "SELECT HOTEN FROM TAIXE WHERE MATX = '" + id + "'";
                        SqlCommand cmd2 = new SqlCommand(sqlselect2, connect);
                        string name = Convert.ToString(cmd2.ExecuteScalar());

                        TaiXe taixe = new TaiXe();
                        taixe.temp = id;
                        taixe.temp1 = name;
                        taixe.Show();
                        this.Hide();
                    }   
                    else if (role == "Quản trị")
                    {
                        Admin ad = new Admin();
                        ad.user_id = id;
                        ad.Show();
                        this.Hide();
                    }    
                }    
            }
            else
            {
                MessageBox.Show("Đăng nhập không thành công!", "Thông báo!");
            }    
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
            this.Hide();
        }


    }
}
