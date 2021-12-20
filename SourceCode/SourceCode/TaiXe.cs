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
    public partial class TaiXe : Form
    {
        SqlConnection connect;
        public string temp;
        public string temp1;

        public TaiXe()
        {
            InitializeComponent();
        }

       public void display2()
        {
            string sqlselect1 = "SELECT DH.MADH AS N'Mã ĐH', DH.MAKH AS N'Mã KH', KH.HOTEN AS N'Tên KH', DH.TTDONHANG AS N'Tình trạng ĐH', DH.DIACHIGH AS N'Địa chỉ GH', CAST(DH.PHIVC AS numeric(10,0)) AS N'Phí VC', CAST(TONGTIEN AS numeric(10,0)) AS N'Tổng tiền' FROM DONHANG DH, DONGIAOHANG DGH, KHACHHANG KH WHERE KH.MAKH = DH.MAKH AND DGH.MADH = DH.MADH AND MATX = '" + label3.Text.Trim() + "'";
            SqlCommand cmd = new SqlCommand(sqlselect1, connect);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataGridView2.DataSource = dt;
            dataGridView2.RowHeadersVisible = false;
        }

        public void display1()
        {
            string sqlselect1 = "SELECT MATX AS N'Mã Tài xế', MADH AS 'Mã đơn hàng', TTGIAOHANG AS N'Tình trạng giao hàng' FROM DONGIAOHANG WHERE MATX = '" + label3.Text.Trim() + "'";
            SqlCommand cmd = new SqlCommand(sqlselect1, connect);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataGridView1.DataSource = dt;
            dataGridView1.RowHeadersVisible = false;
        }

        public void SLDH()
        {
            string sqlselect4 = "SELECT COUNT(*) FROM DONGIAOHANG WHERE MATX = @TX AND TTGIAOHANG = N'Đã vận chuyển'";
            SqlCommand cmd4 = new SqlCommand(sqlselect4, connect);
            cmd4.Parameters.Add(new SqlParameter("@TX", label3.Text.Trim()));
            label10.Text = Convert.ToString(cmd4.ExecuteScalar());
        }

        public void SLDH_DaNhan()
        {
            string sqlselect4 = "SELECT COUNT(*) FROM DONGIAOHANG WHERE MATX = @TX";
            SqlCommand cmd4 = new SqlCommand(sqlselect4, connect);
            cmd4.Parameters.Add(new SqlParameter("@TX", label3.Text.Trim()));
            label14.Text = Convert.ToString(cmd4.ExecuteScalar());
        }

        public void ThuNhap()
        {
            string sqlselect4 = "SELECT CAST(SUM(0.9 * DH.PHIVC) AS DECIMAL(10,0)) FROM DONGIAOHANG DGH, DONHANG DH WHERE DH.MADH = DGH.MADH AND DGH.MATX = @MATX AND DGH.TTGIAOHANG = N'Đã vận chuyển'";
            SqlCommand cmd4 = new SqlCommand(sqlselect4, connect);
            cmd4.Parameters.Add(new SqlParameter("@MATX", label3.Text.Trim()));
            label11.Text = Convert.ToString(cmd4.ExecuteScalar()) + " đồng";

            string sqlselect5 = "UPDATE TAIXE SET THUNHAP = @TN WHERE MATX = @MATX";
            SqlCommand cmd5 = new SqlCommand(sqlselect5, connect);
            cmd5.Parameters.Add(new SqlParameter("@MATX", label3.Text));
            cmd5.Parameters.Add(new SqlParameter("@TN", Convert.ToString(cmd4.ExecuteScalar())));
            cmd5.ExecuteNonQuery();
        }

        private void TaiXe_Load(object sender, EventArgs e)
        {
            connect = new SqlConnection(@"Data Source=HONGQUAN\SQLEXPRESS;Initial Catalog=QL_DH_GH_ONLINE;Integrated Security=True;MultipleActiveResultSets=true");
            connect.Open();
            label3.Text = temp;
            label5.Text = temp1;

            textBox9.Text = temp;
            textBox4.Text = temp1;

            string sqlselect3 = "SELECT CMND FROM TAIXE WHERE MATX = '" + temp + "'";
            SqlCommand cmd3 = new SqlCommand(sqlselect3, connect);
            textBox10.Text = Convert.ToString(cmd3.ExecuteScalar());

            string sqlselect4 = "SELECT DIACHI FROM TAIXE WHERE MATX = '" + temp + "'";
            SqlCommand cmd4 = new SqlCommand(sqlselect4, connect);
            textBox6.Text = Convert.ToString(cmd4.ExecuteScalar());

            string sqlselect7 = "SELECT SĐT FROM TAIXE WHERE MATX = '" + temp + "'";
            SqlCommand cmd7 = new SqlCommand(sqlselect7, connect);
            textBox5.Text = Convert.ToString(cmd7.ExecuteScalar());

            string sqlselect8 = "SELECT TKNGANHANG FROM TAIXE WHERE MATX = '" + temp + "'";
            SqlCommand cmd8 = new SqlCommand(sqlselect8, connect);
            textBox12.Text = Convert.ToString(cmd8.ExecuteScalar());

            string sqlselect9 = "SELECT BIENSOXE FROM TAIXE WHERE MATX = '" + temp + "'";
            SqlCommand cmd9 = new SqlCommand(sqlselect9, connect);
            textBox11.Text = Convert.ToString(cmd9.ExecuteScalar());

            string sqlselect10 = "SELECT EMAIL FROM TAIXE WHERE MATX = '" + temp + "'";
            SqlCommand cmd10 = new SqlCommand(sqlselect10, connect);
            textBox13.Text = Convert.ToString(cmd10.ExecuteScalar());

            string sqlselect11 = "SELECT TTDONGPHITHECHAN FROM TAIXE WHERE MATX = '" + temp + "'";
            SqlCommand cmd11 = new SqlCommand(sqlselect11, connect);
            comboBox2.Text = Convert.ToString(cmd11.ExecuteScalar());

            string sqlselect5 = "SELECT USERNAME FROM LOGIN WHERE USERID = '" + temp + "'";
            SqlCommand cmd5 = new SqlCommand(sqlselect5, connect);
            textBox7.Text = Convert.ToString(cmd5.ExecuteScalar());

            string sqlselect6 = "SELECT PASSWORD FROM LOGIN WHERE USERID = '" + temp + "'";
            SqlCommand cmd6 = new SqlCommand(sqlselect6, connect);
            textBox8.Text = Convert.ToString(cmd6.ExecuteScalar());

            display2();
            display1();
            SLDH();
            ThuNhap();
            SLDH_DaNhan();
            tabControl1.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sqlselect4 = "EXEC TX_CAPNHAT_TTGH @MATX, @MADH, @TTGH";
            SqlCommand cmd4 = new SqlCommand(sqlselect4, connect);
            cmd4.Parameters.Add(new SqlParameter("@TTGH", comboBox1.Text));
            cmd4.Parameters.Add(new SqlParameter("@MADH", textBox1.Text));
            cmd4.Parameters.Add(new SqlParameter("@MATX", label3.Text.Trim()));
            cmd4.ExecuteNonQuery();
            display1();
            SLDH();
            ThuNhap();
            SLDH_DaNhan();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            comboBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
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

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox2.Text = dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString();
            dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString();
            dataGridView2.Rows[e.RowIndex].Cells[2].Value.ToString();
            dataGridView2.Rows[e.RowIndex].Cells[3].Value.ToString();
            dataGridView2.Rows[e.RowIndex].Cells[4].Value.ToString();
            textBox3.Text = dataGridView2.Rows[e.RowIndex].Cells[5].Value.ToString();
            dataGridView2.Rows[e.RowIndex].Cells[6].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sqlselect4 = "UPDATE DONHANG SET PHIVC = @VC WHERE MADH = @madh";
            SqlCommand cmd4 = new SqlCommand(sqlselect4, connect);
            cmd4.Parameters.Add(new SqlParameter("@VC", textBox3.Text));
            cmd4.Parameters.Add(new SqlParameter("@madh", textBox2.Text));
            cmd4.ExecuteNonQuery();
            display2();
            ThuNhap();
            SLDH();
            SLDH_DaNhan();
        }

        private void button6_Click(object sender, EventArgs e)
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

        private void button4_Click(object sender, EventArgs e)
        {
            string sqlselect4 = "UPDATE TAIXE SET DIACHI = @DC, SĐT = @SDT, BIENSOXE = @BSX, TKNGANHANG = @TK, EMAIL = @EM, TTDONGPHITHECHAN = @TT WHERE MATX = '" + temp + "'";
            SqlCommand cmd4 = new SqlCommand(sqlselect4, connect);
            cmd4.Parameters.Add(new SqlParameter("@SDT", textBox5.Text.Trim()));
            cmd4.Parameters.Add(new SqlParameter("@DC", textBox6.Text.Trim()));
            cmd4.Parameters.Add(new SqlParameter("@BSX", textBox11.Text.Trim()));
            cmd4.Parameters.Add(new SqlParameter("@TK", textBox12.Text.Trim()));
            cmd4.Parameters.Add(new SqlParameter("@EM", textBox13.Text.Trim()));
            cmd4.Parameters.Add(new SqlParameter("@TT", comboBox2.Text.Trim()));
            cmd4.ExecuteNonQuery();

            DialogResult res = MessageBox.Show("Cập nhật thành công!", "Thông báo");
            if (res == DialogResult.OK)
            {
                return;
            }
        }

        private void button3_Click(object sender, EventArgs e)
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
    }
}
