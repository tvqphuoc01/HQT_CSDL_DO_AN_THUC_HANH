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
    public partial class KhachHang : Form
    {
        SqlConnection connect;
        public string temp;
        public string temp1;
        public string MADH;

        public KhachHang()
        {
            InitializeComponent();
            tabControl1.Enabled = false;
        }


        private void displayData1()
        {
            string sqlselect1 = "SELECT MASP as N'Mã sản phẩm', MADT as N'Mã đối tác', TENSP as N'Tên SP', SLTON as N'Số lượng tồn', DONGIA as N'Đơn giá'  FROM SANPHAM";
            SqlCommand cmd = new SqlCommand(sqlselect1, connect);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataGridView2.DataSource = dt;
            dataGridView2.RowHeadersVisible = false;
            dataGridView2.ColumnHeadersVisible = true;
        }

        private void displayData2()
        {
            string sqlselect3 = "SELECT MADH as N'Mã đơn hàng', MASP as N'Mã sản phẩm', SLSANPHAM as N'SL sản phẩm', THANHTIEN as N'Thành tiền' FROM CTDONHANG WHERE MADH = '" + textBox3.Text.Trim() + "'";
            SqlCommand cmd3 = new SqlCommand(sqlselect3, connect);
            SqlDataReader dr1 = cmd3.ExecuteReader();
            DataTable dt1 = new DataTable();
            dt1.Load(dr1);
            dataGridView2.DataSource = dt1;
            dataGridView2.ColumnHeadersVisible = true;
        }
        private void displayData3()
        {
            string sqlselect1 = "SELECT MADH as N'Mã ĐH', TTDONHANG AS N'Tình trạng DH', PHIVC as N'Phí vận chuyển', TONGTIEN AS N'Tổng tiền' FROM DONHANG WHERE MAKH = '" + temp + "'";
            SqlCommand cmd = new SqlCommand(sqlselect1, connect);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataGridView3.DataSource = dt;
            dataGridView3.RowHeadersVisible = false;
        }

        private void displayData4()
        {
            string sqlselect1 = "SELECT * FROM DONGIAOHANG WHERE MADH = '" + textBox12.Text + "'";
            SqlCommand cmd = new SqlCommand(sqlselect1, connect);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataGridView4.DataSource = dt;
            dataGridView4.RowHeadersVisible = false;
        }

        private void getMADH()
        {
            string sqlselect2 = "SELECT TOP 1 MADH FROM DONHANG ORDER BY MADH DESC";
            SqlCommand cmd2 = new SqlCommand(sqlselect2, connect);
            MADH = Convert.ToString(cmd2.ExecuteScalar());
        }

        private void KhachHang_Load(object sender, EventArgs e)
        {
            connect = new SqlConnection(@"Data Source=HONGQUAN\SQLEXPRESS;Initial Catalog=QL_DH_GH_ONLINE;Integrated Security=True;MultipleActiveResultSets=true");
            connect.Open();

            label3.Text = temp;
            label5.Text = temp1;
            textBox9.Text = temp;
            textBox4.Text = temp1;

            string sqlselect3 = "SELECT SĐT FROM KHACHHANG WHERE MAKH = '" + temp + "'";
            SqlCommand cmd3 = new SqlCommand(sqlselect3, connect);
            textBox5.Text = Convert.ToString(cmd3.ExecuteScalar());

            string sqlselect4 = "SELECT DIACHI FROM KHACHHANG WHERE MAKH = '" + temp + "'";
            SqlCommand cmd4 = new SqlCommand(sqlselect4, connect);
            textBox10.Text = Convert.ToString(cmd4.ExecuteScalar());

            string sqlselect5 = "SELECT USERNAME FROM LOGIN WHERE USERID = '" + temp + "'";
            SqlCommand cmd5 = new SqlCommand(sqlselect5, connect);
            textBox7.Text = Convert.ToString(cmd5.ExecuteScalar());

            string sqlselect6 = "SELECT PASSWORD FROM LOGIN WHERE USERID = '" + temp + "'";
            SqlCommand cmd6 = new SqlCommand(sqlselect6, connect);
            textBox8.Text = Convert.ToString(cmd6.ExecuteScalar());

            string sqlselect7 = "SELECT EMAIL FROM KHACHHANG WHERE MAKH = '" + temp + "'";
            SqlCommand cmd7 = new SqlCommand(sqlselect7, connect);
            textBox11.Text = Convert.ToString(cmd7.ExecuteScalar());

            displayData3();
            //displayData4();
            button9.Enabled = true;
        }

        private void KhachHang_Close(object sender, FormClosingEventArgs e)
        {
            connect.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
        }


        private void button5_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Bạn có muốn đăng xuất không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                connect.Close();
                Form1 form1 = new Form1();
                form1.Show();
                this.Hide();
            }
            else
            {
                return;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string sqlselect4 = "UPDATE KHACHHANG SET SĐT = @SDT, EMAIL = @EM, DIACHI = @DC WHERE MAKH = '" + textBox9.Text.Trim() + "'";
            SqlCommand cmd4 = new SqlCommand(sqlselect4, connect);
            cmd4.Parameters.Add(new SqlParameter("@SDT", textBox5.Text.Trim()));
            cmd4.Parameters.Add(new SqlParameter("@EM", textBox11.Text.Trim()));
            cmd4.Parameters.Add(new SqlParameter("@DC", textBox10.Text.Trim()));
            cmd4.ExecuteNonQuery();

            DialogResult res = MessageBox.Show("Cập nhật thành công!", "Thông báo");
            if (res == DialogResult.OK)
            {
                return;
            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            string sqlselect4 = "UPDATE LOGIN SET PASSWORD = @PW WHERE USERID = '" + temp + "'";
            SqlCommand cmd4 = new SqlCommand(sqlselect4, connect);
            cmd4.Parameters.Add(new SqlParameter("@PW", textBox8.Text.Trim()));
            cmd4.ExecuteNonQuery();

            DialogResult res = MessageBox.Show("Đổi mật khẩu thành công!", "Thông báo");
            if (res == DialogResult.OK)
            {
                return;
            }
        }
        private void button9_Click(object sender, EventArgs e)
        {
            displayData1();
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            int count = 0;
            if (textBox1.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập địa chỉ giao hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                count++;
            }


            if (comboBox2.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập Thành phố (Tỉnh)!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                count++;
            }

            if (count == 2)
            {
                string sqlselect1 = "EXEC SP_INSERT_DH '" + temp + "', N'Đang xử lý', @DC ," + 0 + ", " + 0;
                SqlCommand cmd1 = new SqlCommand(sqlselect1, connect);
                cmd1.Parameters.Add(new SqlParameter("@DC", textBox1.Text));
                cmd1.ExecuteNonQuery();
                getMADH();
                textBox3.Text = MADH;
                button6.Enabled = false;
                textBox2.Enabled = true;
                numericUpDown1.Enabled = true;
                button1.Enabled = true;
                button2.Enabled = true;
                button3.Enabled = true;
            }
            displayData2();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sqlselect = "DELETE CTDONHANG WHERE MADH = @MADH AND MASP = @MASP";
            SqlCommand cmd = new SqlCommand(sqlselect, connect);
            cmd.Parameters.Add(new SqlParameter("@MADH", textBox3.Text));
            cmd.Parameters.Add(new SqlParameter("@MASP", textBox2.Text));
            cmd.ExecuteNonQuery();
            displayData2();

            textBox3.Text = "";
            textBox2.Text = "";
            numericUpDown1.Value = 0;
            button6.Enabled = true;
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Đơn hàng của bạn đang được xử lý. \nVui lòng xem thông tin đơn hàng của bạn tại mục 'Đơn hàng của bạn'", "Thông báo");
            if (res == DialogResult.OK)
            {
                string sqlselect4 = "SELECT MATX FROM TAIXE, KHUVUCHOATDONG TABLESAMPLE (80 PERCENT) WHERE TAIXE.MAKV = KHUVUCHOATDONG.MAKV AND TTDONGPHITHECHAN = N'Đã đóng'  AND DIACHIKV = @DC";
                SqlCommand cmd4 = new SqlCommand(sqlselect4, connect);
                cmd4.Parameters.Add(new SqlParameter("@DC", comboBox2.Text));
                string MATX = Convert.ToString(cmd4.ExecuteScalar());

                string sqlselect1 = "UPDATE DONHANG SET TTDONHANG = N'Đã nhận' WHERE MADH = @madh";
                SqlCommand cmd1 = new SqlCommand(sqlselect1, connect);
                cmd1.Parameters.Add(new SqlParameter("@madh", textBox3.Text));
                cmd1.ExecuteNonQuery();


                string sqlselect = "EXEC SP_INSERT_DGH '" + MATX + "', '" + textBox3.Text + "', N'Đang vận chuyển'";
                SqlCommand cmd = new SqlCommand(sqlselect, connect);
                cmd.ExecuteNonQuery();
                button3.Enabled = true;
                displayData3();

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            getMADH();

            try
            {
                if (numericUpDown1.Value == 0)
                {
                    MessageBox.Show("Số lượng đặt phải lớn hơn 0!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    if (radioButton2.Checked)
                    {
                        string sqlselect4 = "EXEC KH1_MUASP '" + textBox3.Text.Trim() + "', @MASP, @SLSP";
                        SqlCommand cmd4 = new SqlCommand(sqlselect4, connect);
                        cmd4.Parameters.Add(new SqlParameter("@MASP", textBox2.Text));
                        cmd4.Parameters.Add(new SqlParameter("@SLSP", numericUpDown1.Text));
                        cmd4.ExecuteNonQuery();
                    }
                    else if (radioButton1.Checked)
                    {
                        string sqlselect4 = "EXEC KH1_MUASP_FIX '" + textBox3.Text.Trim() + "', @MASP, @SLSP";
                        SqlCommand cmd4 = new SqlCommand(sqlselect4, connect);
                        cmd4.Parameters.Add(new SqlParameter("@MASP", textBox2.Text));
                        cmd4.Parameters.Add(new SqlParameter("@SLSP", numericUpDown1.Text));
                        cmd4.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Mã sản phẩm không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            displayData2();
        }


        private void button10_Click(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                string sqlselect1 = "EXEC KH_XEMGIA_SP '" + textBox6.Text.Trim() + "'";
                SqlCommand cmd = new SqlCommand(sqlselect1, connect);
                SqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                dataGridView1.DataSource = dt;
                dataGridView1.RowHeadersVisible = false;
            }
            else if (radioButton1.Checked)
            {
                string sqlselect1 = "EXEC KH_XEMGIA_SP_FIX '" + textBox6.Text.Trim() + "'";
                SqlCommand cmd = new SqlCommand(sqlselect1, connect);
                SqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                dataGridView1.DataSource = dt;
                dataGridView1.RowHeadersVisible = false;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked || radioButton2.Checked)
            {
                tabControl1.Enabled = true;
            }
            else
            {
                tabControl1.Enabled = false;
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (textBox12.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập mã đơn hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                string sqlselect1 = "";
                if (radioButton2.Checked)
                {
                    sqlselect1 = "EXEC KTRA_TTGH '" + label3.Text + "', '" + textBox12.Text + "'";
                }
                else if (radioButton1.Checked)
                {
                    sqlselect1 = "EXEC KTRA_TTGH_FIX '" + label3.Text + "', '" + textBox12.Text + "'";
                }    
                SqlCommand cmd = new SqlCommand(sqlselect1, connect);
                SqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                dataGridView4.DataSource = dt;
                dataGridView4.RowHeadersVisible = false;

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
