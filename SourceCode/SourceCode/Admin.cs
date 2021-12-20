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
    public partial class Admin : Form
    {
        SqlConnection connect;
        DataTable table = new DataTable();
        int selectedRow;
        public string user_id;

        public Admin()
        {
            InitializeComponent();
            tabControl1.Enabled = false;
        }

        public void DisplayNV()
        {
            string sqlselect1 = "SELECT MANV AS N'Mã NV', TENNV AS N'Tên NV', SDT AS N'Số điện thoại', DIACHI AS N'Địa chỉ', MACN AS N'Mã chi nhánh' from NHANVIEN";
            SqlCommand cmd = new SqlCommand(sqlselect1, connect);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataGridView1.DataSource = dt;
            dataGridView1.RowHeadersVisible = false;

            string sqlselect3 = "SELECT COUNT(*) FROM NHANVIEN";
            SqlCommand cmd3 = new SqlCommand(sqlselect3, connect);
            label2.Text = Convert.ToString(cmd3.ExecuteScalar());
        }

        public void DisplayLG()
        {
            string sqlselect1 = "SELECT * from LOGIN";
            SqlCommand cmd = new SqlCommand(sqlselect1, connect);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataGridView2.DataSource = dt;
            dataGridView2.RowHeadersVisible = false;
        }

        public void DisplayKH()
        {
            string sqlselect1 = "SELECT * FROM KHACHHANG";
            SqlCommand cmd = new SqlCommand(sqlselect1, connect);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataGridView3.DataSource = dt;
            dataGridView3.RowHeadersVisible = false;

            string sqlselect3 = "SELECT COUNT(*) FROM KHACHHANG";
            SqlCommand cmd3 = new SqlCommand(sqlselect3, connect);
            label14.Text = Convert.ToString(cmd3.ExecuteScalar());
        }

        public void DisplayDT()
        {
            string sqlselect1 = "SELECT * FROM DOITAC";
            SqlCommand cmd = new SqlCommand(sqlselect1, connect);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataGridView4.DataSource = dt;
            dataGridView4.RowHeadersVisible = false;

            string sqlselect3 = "SELECT COUNT(*) FROM DOITAC";
            SqlCommand cmd3 = new SqlCommand(sqlselect3, connect);
            label16.Text = Convert.ToString(cmd3.ExecuteScalar());
        }

        public void DisplayTX()
        {
            string sqlselect1 = "SELECT * FROM TAIXE";
            SqlCommand cmd = new SqlCommand(sqlselect1, connect);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataGridView5.DataSource = dt;
            dataGridView5.RowHeadersVisible = false;

            string sqlselect3 = "SELECT COUNT(*) FROM TAIXE";
            SqlCommand cmd3 = new SqlCommand(sqlselect3, connect);
            label18.Text = Convert.ToString(cmd3.ExecuteScalar());
        }

        private void Admin_Load(object sender, EventArgs e)
        {
            connect = new SqlConnection(@"Data Source=HONGQUAN\SQLEXPRESS;Initial Catalog=QL_DH_GH_ONLINE;Integrated Security=True;MultipleActiveResultSets=true");
            connect.Open();

            DisplayNV();
            DisplayKH();
            DisplayDT();
            DisplayTX();
            textBox10.Enabled = false;

            if (user_id == "QT002")
            {
                groupBox2.Enabled = false;
                groupBox3.Enabled = false;
                button1.Enabled = false;
                dataGridView6.Enabled = false;
                dataGridView7.Enabled = false;
            }
            else
            {
                groupBox4.Enabled = false;
                groupBox5.Enabled = false;
                button2.Enabled = false;
                dataGridView8.Enabled = false;
                dataGridView9.Enabled = false;
            }   
        }


        private void button1_Click(object sender, EventArgs e)
        {
            int count = 0;
            if (textBox2.Text == "")
            {
                MessageBox.Show("Họ tên nhân viên còn trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                count++;
            }    

            if (textBox3.Text == "")
            {
                MessageBox.Show("Số điện thoại còn trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }    
            else
            {
                count++;
            }

            if (textBox4.Text == "")
            {
                MessageBox.Show("Địa chỉ còn trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                count++;
            }

            if (textBox5.Text == "")
            {
                MessageBox.Show("Mã chi nhánh còn trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                count++;
            }

            if (count == 4)
            {
                try
                {
                    string sqlselect4 = "INSERT INTO NHANVIEN(TENNV, SDT, DIACHI, MACN) VALUES (@TENNV, @SDT, @DIACHI, @MACN)";
                    SqlCommand cmd4 = new SqlCommand(sqlselect4, connect);
                    cmd4.Parameters.Add(new SqlParameter("@TENNV", textBox2.Text.Trim()));
                    cmd4.Parameters.Add(new SqlParameter("@SDT", textBox3.Text.Trim()));
                    cmd4.Parameters.Add(new SqlParameter("@DIACHI", textBox4.Text.Trim()));
                    cmd4.Parameters.Add(new SqlParameter("@MACN", textBox5.Text.Trim()));
                    cmd4.ExecuteNonQuery();
                    DialogResult res = MessageBox.Show("Thêm thành công", "Thông báo", MessageBoxButtons.OK);
                    if (res == DialogResult.OK)
                    {
                        DisplayNV();
                        textBox1.Text = "";
                        textBox2.Text = "";
                        textBox3.Text = "";
                        textBox4.Text = "";
                        textBox5.Text = "";
                    }
                }
                catch(Exception)
                {
                    MessageBox.Show("Mã chi nhánh không hợp lệ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }    
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRow = e.RowIndex;
            DataGridViewRow row = dataGridView1.Rows[selectedRow];
            textBox1.Text = row.Cells[0].Value.ToString();
            textBox2.Text = row.Cells[1].Value.ToString();
            textBox3.Text = row.Cells[2].Value.ToString();
            textBox4.Text = row.Cells[3].Value.ToString();
            textBox5.Text = row.Cells[4].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sqlselect4 = "UPDATE NHANVIEN SET TENNV = @TENNV, SDT = @SDT, DIACHI = @DIACHI, MACN = @MACN WHERE MANV = '" + textBox1.Text.Trim() + "'";
            SqlCommand cmd4 = new SqlCommand(sqlselect4, connect);
            cmd4.Parameters.Add(new SqlParameter("@TENNV", textBox2.Text.Trim()));
            cmd4.Parameters.Add(new SqlParameter("@SDT", textBox3.Text.Trim()));
            cmd4.Parameters.Add(new SqlParameter("@DIACHI", textBox4.Text.Trim()));
            cmd4.Parameters.Add(new SqlParameter("@MACN", textBox5.Text.Trim()));
            cmd4.ExecuteNonQuery();

            DialogResult res = MessageBox.Show("Cập nhật thành công", "Thông báo", MessageBoxButtons.OK);
            if (res == DialogResult.OK)
            {
                DisplayNV();
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string sqlselect4 = "DELETE NHANVIEN WHERE MANV = '" + textBox1.Text.Trim() + "'";
            SqlCommand cmd4 = new SqlCommand(sqlselect4, connect);
            cmd4.ExecuteNonQuery();

            DialogResult res = MessageBox.Show("Bạn có muốn xóa nhân viên có mã số '" + textBox1.Text.Trim() + "'", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                DisplayNV();
            }    
            else
            {
                return;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            DisplayNV();
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
            selectedRow = e.RowIndex;
            DataGridViewRow row = dataGridView2.Rows[selectedRow];
            textBox10.Text = row.Cells[0].Value.ToString();
            textBox9.Text = row.Cells[1].Value.ToString();
            textBox8.Text = row.Cells[2].Value.ToString();
            textBox7.Text = row.Cells[3].Value.ToString();
            comboBox1.Text = row.Cells[4].Value.ToString();

            textBox9.Enabled = false;
            textBox8.Enabled = false;
            comboBox1.Enabled = false;
            textBox10.Enabled = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox10.Text = "";
            textBox9.Text = "";
            textBox8.Text = "";
            textBox7.Text = "";
            comboBox1.Text = "";
            textBox9.Enabled = true;
            textBox8.Enabled = true;
            comboBox1.Enabled = true;
            textBox10.Enabled=false;
            DisplayLG();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            string sqlselect4 = "EXEC SP_INSERT_LOGIN @USERID, @UN, @PW, @ROLE";
            SqlCommand cmd4 = new SqlCommand(sqlselect4, connect);
            cmd4.Parameters.Add(new SqlParameter("@USERID", textBox9.Text.Trim()));
            cmd4.Parameters.Add(new SqlParameter("@UN", textBox8.Text.Trim()));
            cmd4.Parameters.Add(new SqlParameter("@PW", textBox7.Text.Trim()));
            cmd4.Parameters.Add(new SqlParameter("@ROLE", comboBox1.Text.Trim()));
            cmd4.ExecuteNonQuery();

            textBox9.Enabled = false;
            textBox8.Enabled = false;
            comboBox1.Enabled = false;
            DisplayLG();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string sqlselect4 = "UPDATE LOGIN SET PASSWORD = @PW WHERE IDLG = '" + textBox10.Text.Trim() + "'";
            SqlCommand cmd4 = new SqlCommand(sqlselect4, connect);
            cmd4.Parameters.Add(new SqlParameter("@PW", textBox7.Text.Trim()));
            cmd4.ExecuteNonQuery();

            DisplayLG();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string sqlselect4 = "DELETE LOGIN WHERE IDLG = '" + textBox10.Text.Trim() + "'";
            SqlCommand cmd4 = new SqlCommand(sqlselect4, connect);
            cmd4.ExecuteNonQuery();

            DialogResult res = MessageBox.Show("Bạn có muốn xóa tài khoản có mã số '" + textBox9.Text.Trim() + "'", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                textBox10.Text = "";
                textBox9.Text = "";
                textBox8.Text = "";
                textBox7.Text = "";
                comboBox1.Text = "";
                DisplayLG();
            }
            else
            {
                return;
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                string sqlselect4 = "";
                if (radioButton2.Checked)
                {
                    sqlselect4 = "EXEC TIM_NV_CN @MACN";
                }
                else if (radioButton1.Checked)
                {
                    sqlselect4 = "EXEC TIM_NV_CN_FIX @MACN";
                }
                SqlCommand cmd4 = new SqlCommand(sqlselect4, connect);
                cmd4.Parameters.Add(new SqlParameter("@MACN", textBox6.Text));
                SqlDataReader dr = cmd4.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                dataGridView1.DataSource = dt;
                dataGridView1.RowHeadersVisible = false;
                // DisplayNV();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void displayTX()
        {
            string sqlselect1 = "SELECT * FROM TAIXE WHERE MATX = @MATX";
            SqlCommand cmd = new SqlCommand(sqlselect1, connect);
            cmd.Parameters.Add(new SqlParameter("@MATX", textBox11.Text.Trim()));
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataGridView7.DataSource = dt;
            dataGridView7.RowHeadersVisible = false;

            dataGridView9.DataSource = dt;
            dataGridView9.RowHeadersVisible = false;
        }

        private void displayLG()
        {
            string sqlselect1 = "SELECT * FROM LOGIN WHERE USERID = @UI";
            SqlCommand cmd = new SqlCommand(sqlselect1, connect);
            cmd.Parameters.Add(new SqlParameter("@UI", textBox14.Text.Trim()));
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataGridView6.DataSource = dt;
            dataGridView6.RowHeadersVisible = false;

            dataGridView8.DataSource = dt;
            dataGridView8.RowHeadersVisible = false;
        }

        private void displayLG1()
        {
            string sqlselect1 = "SELECT * FROM LOGIN WHERE USERID = @UI";
            SqlCommand cmd = new SqlCommand(sqlselect1, connect);
            cmd.Parameters.Add(new SqlParameter("@UI", textBox16.Text.Trim()));
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);

            dataGridView8.DataSource = dt;
            dataGridView8.RowHeadersVisible = false;
        }

        private void displayTX1()
        {
            string sqlselect1 = "SELECT * FROM TAIXE WHERE MATX = @MATX";
            SqlCommand cmd = new SqlCommand(sqlselect1, connect);
            cmd.Parameters.Add(new SqlParameter("@MATX", textBox18.Text.Trim()));
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);

            dataGridView9.DataSource = dt;
            dataGridView9.RowHeadersVisible = false;
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (radioButton2.Checked)
                {
                    string sqlselect4 = "EXEC QL1_DEADLOCK @USERID, @PW_NEW, @MATX, @CMND_NEW";
                    SqlCommand cmd4 = new SqlCommand(sqlselect4, connect);
                    cmd4.Parameters.Add(new SqlParameter("@USERID", textBox14.Text.Trim()));
                    cmd4.Parameters.Add(new SqlParameter("@PW_NEW", textBox13.Text.Trim()));
                    cmd4.Parameters.Add(new SqlParameter("@MATX", textBox11.Text.Trim()));
                    cmd4.Parameters.Add(new SqlParameter("@CMND_NEW", textBox12.Text.Trim()));
                    cmd4.ExecuteNonQuery();
                    displayTX();
                    displayLG();
                }
                else if (radioButton1.Checked)
                {
                    string sqlselect4 = "EXEC QL1_DEADLOCK_FIX @USERID, @PW_NEW, @MATX, @CMND_NEW";
                    SqlCommand cmd4 = new SqlCommand(sqlselect4, connect);
                    cmd4.Parameters.Add(new SqlParameter("@USERID", textBox14.Text.Trim()));
                    cmd4.Parameters.Add(new SqlParameter("@PW_NEW", textBox13.Text.Trim()));
                    cmd4.Parameters.Add(new SqlParameter("@MATX", textBox11.Text.Trim()));
                    cmd4.Parameters.Add(new SqlParameter("@CMND_NEW", textBox12.Text.Trim()));
                    cmd4.ExecuteNonQuery();
                    displayTX();
                    displayLG();
                }    
            } 
            catch(SqlException ex)
            {
                if (ex.Number == 1205)
                {
                    MessageBox.Show("Đã xảy ra lỗi cycle deadlock", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (radioButton2.Checked)
                {
                    string sqlselect4 = "EXEC QL2_DEADLOCK @USERID, @PW_NEW, @MATX, @CMND_NEW";
                    SqlCommand cmd4 = new SqlCommand(sqlselect4, connect);
                    cmd4.Parameters.Add(new SqlParameter("@USERID", textBox16.Text.Trim()));
                    cmd4.Parameters.Add(new SqlParameter("@PW_NEW", textBox15.Text.Trim()));
                    cmd4.Parameters.Add(new SqlParameter("@MATX", textBox18.Text.Trim()));
                    cmd4.Parameters.Add(new SqlParameter("@CMND_NEW", textBox17.Text.Trim()));
                    cmd4.ExecuteNonQuery();
                    displayTX1();
                    displayLG1();
                }
                else if (radioButton1.Checked)
                {
                    string sqlselect4 = "EXEC QL2_DEADLOCK_FIX @USERID, @PW_NEW, @MATX, @CMND_NEW";
                    SqlCommand cmd4 = new SqlCommand(sqlselect4, connect);
                    cmd4.Parameters.Add(new SqlParameter("@USERID", textBox16.Text.Trim()));
                    cmd4.Parameters.Add(new SqlParameter("@PW_NEW", textBox15.Text.Trim()));
                    cmd4.Parameters.Add(new SqlParameter("@MATX", textBox18.Text.Trim()));
                    cmd4.Parameters.Add(new SqlParameter("@CMND_NEW", textBox17.Text.Trim()));
                    cmd4.ExecuteNonQuery();
                    displayTX1();
                    displayLG1();
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 1205)
                {
                    MessageBox.Show("Đã xảy ra lỗi cycle deadlock", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            
        }

        private void button3_Click_1(object sender, EventArgs e)
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
            try
            {
                string sqlselect1 = "";
                if (radioButton2.Checked)
                {
                    sqlselect1 = "EXEC XEM_DS_LOGIN";
                }
                else if (radioButton1.Checked)
                {
                    sqlselect1 = "EXEC XEM_DS_LOGIN_FIX";
                }

                SqlCommand cmd = new SqlCommand(sqlselect1, connect);
                cmd.Parameters.Add(new SqlParameter("@MATX", textBox18.Text.Trim()));
                SqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                dataGridView2.DataSource = dt;
                dataGridView2.RowHeadersVisible = false;
                DisplayLG();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
