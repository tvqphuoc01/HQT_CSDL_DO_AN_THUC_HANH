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
    public partial class NhanVien : Form
    {
        SqlConnection connect;
        public string temp;
        public string temp1;
        public string CN;

        public NhanVien()
        {
            InitializeComponent();
            tabControl1.Enabled = false;
        }


        public void display1()
        {
            string sqlselect1 = "SELECT * FROM HOPDONG WHERE MACN = '" + label14.Text.Trim() + "'";
            SqlCommand cmd = new SqlCommand(sqlselect1, connect);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataGridView1.DataSource = dt;
            dataGridView1.RowHeadersVisible = false;
        }

        private void NhanVien_Load(object sender, EventArgs e)
        {
            connect = new SqlConnection(@"Data Source=HONGQUAN\SQLEXPRESS;Initial Catalog=QL_DH_GH_ONLINE;Integrated Security=True;MultipleActiveResultSets=true");
            connect.Open();

            label3.Text = temp;
            label5.Text = temp1;
            label14.Text = CN;

            textBox3.Text = temp;
            textBox4.Text = temp1;

            string sqlselect3 = "SELECT SDT FROM NHANVIEN WHERE MANV = '" + temp + "'";
            SqlCommand cmd3 = new SqlCommand(sqlselect3, connect);
            textBox5.Text = Convert.ToString(cmd3.ExecuteScalar());

            string sqlselect4 = "SELECT DIACHI FROM NHANVIEN WHERE MANV = '" + temp + "'";
            SqlCommand cmd4 = new SqlCommand(sqlselect4, connect);
            textBox6.Text = Convert.ToString(cmd4.ExecuteScalar());

            string sqlselect5 = "SELECT USERNAME FROM LOGIN WHERE USERID = '" + temp + "'";
            SqlCommand cmd5 = new SqlCommand(sqlselect5, connect);
            textBox7.Text = Convert.ToString(cmd5.ExecuteScalar());

            string sqlselect6 = "SELECT PASSWORD FROM LOGIN WHERE USERID = '" + temp + "'";
            SqlCommand cmd6 = new SqlCommand(sqlselect6, connect);
            textBox8.Text = Convert.ToString(cmd6.ExecuteScalar());

            string sqlselect7 = "SELECT MACN FROM NHANVIEN WHERE MANV = '" + temp + "'";
            SqlCommand cmd7 = new SqlCommand(sqlselect7, connect);
            textBox9.Text = Convert.ToString(cmd7.ExecuteScalar());

        }

        private void button5_Click_1(object sender, EventArgs e)
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            comboBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
               
                if (radioButton2.Checked)
                {
                    string sqlselect = "EXEC CPNHAT_TTHDONG '" + textBox1.Text.Trim() + "', N'" + comboBox1.Text.Trim() + "'";
                    SqlCommand cmd = new SqlCommand(sqlselect, connect);
                    cmd.ExecuteNonQuery();
                    //if (comboBox1.Text == "Hết hạn")
                    //{
                    //    dateTimePicker1.Enabled = false;
                    //    textBox2.Enabled = false;
                    //    button2.Enabled = false;
                    //}
                    //else if (comboBox1.Text == "Còn hiệu lực")
                    //{
                    //    dateTimePicker1.Enabled = true;
                    //    textBox2.Enabled = true;
                    //    button2.Enabled = true;
                    //}
                    dateTimePicker1.Enabled = true;
                    textBox2.Enabled = true;
                    button2.Enabled = true;
                    display1();
                }
                else if (radioButton1.Checked)
                {
                    string sqlselect = "EXEC CPNHAT_TTHDONG_FIX '" + textBox1.Text.Trim() + "', N'" + comboBox1.Text.Trim() + "'";
                    SqlCommand cmd = new SqlCommand(sqlselect, connect);
                    cmd.ExecuteNonQuery();

                    dateTimePicker1.Enabled = true;
                    textBox2.Enabled = true;
                    button2.Enabled = true;
                    display1();
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 1205)
                {
                    MessageBox.Show("Đã xảy ra lỗi conversion deadlock", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            string sqlselect3 = "";
            if (comboBox1.SelectedIndex == 1)
            {
                sqlselect3 = "EXEC CNHAPHOPDONG @MAHD, @NUM, @NGAYCN";
            }
            else
            {
                sqlselect3 = "UPDATE HOPDONG SET NGAYKT = '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' WHERE MAHD = @MAHD";
            }
            SqlCommand cmd3 = new SqlCommand(sqlselect3, connect);
            float num = float.Parse(textBox2.Text.Trim());
            cmd3.Parameters.Add(new SqlParameter("@MAHD", textBox1.Text.Trim()));
            cmd3.Parameters.Add(new SqlParameter("@NGAYCN", dateTimePicker1.Value.ToString("yyyy-MM-dd")));
            cmd3.Parameters.Add(new SqlParameter("@NUM", num));
            cmd3.ExecuteNonQuery();

            //string sqlselect4 = "UPDATE HOPDONG SET NGAYKT = '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' WHERE MAHD = @MAHD";
            //SqlCommand cmd4 = new SqlCommand(sqlselect4, connect);
            //cmd4.Parameters.Add(new SqlParameter("@MAHD", textBox1.Text.Trim()));
            //cmd4.ExecuteNonQuery();

            DialogResult res = MessageBox.Show("Cập nhật thành công!", "Thông báo");
            if (res == DialogResult.OK)
            {
                display1();
            }    
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string sqlselect4 = "UPDATE NHANVIEN SET SDT = @SDT WHERE MANV = '" + temp + "'";
            SqlCommand cmd4 = new SqlCommand(sqlselect4, connect);
            cmd4.Parameters.Add(new SqlParameter("@SDT", textBox5.Text.Trim()));
            cmd4.ExecuteNonQuery();

            DialogResult res = MessageBox.Show("Cập nhật thành công!", "Thông báo");
            if (res == DialogResult.OK)
            {
                return;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                string sqlselect4 = "UPDATE NHANVIEN SET DIACHI = @DC, SDT = @SDT, MACN = @CN WHERE MANV = '" + temp + "'";
                SqlCommand cmd4 = new SqlCommand(sqlselect4, connect);
                cmd4.Parameters.Add(new SqlParameter("@DC", textBox6.Text.Trim()));
                cmd4.Parameters.Add(new SqlParameter("@SDT", textBox5.Text.Trim()));
                cmd4.Parameters.Add(new SqlParameter("@CN", textBox9.Text.Trim()));
                cmd4.ExecuteNonQuery();
                label14.Text = textBox9.Text;
                DialogResult res = MessageBox.Show("Cập nhật thành công!", "Thông báo");
                if (res == DialogResult.OK)
                {
                    return;
                }
            }
            catch(Exception)
            {
                MessageBox.Show("Mã chi nhánh không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
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


        private void button3_Click_1(object sender, EventArgs e)
        {
            try
            {
                string sqlselect4 = "";
                if (radioButton2.Checked)
                {
                    sqlselect4 = "EXEC XEM_DSHD @MACN";
                }
                else if (radioButton1.Checked)
                {
                    sqlselect4 = "EXEC XEM_DS_HD_FIX @MACN";
                }
                SqlCommand cmd4 = new SqlCommand(sqlselect4, connect);
                cmd4.Parameters.Add(new SqlParameter("@MACN", label14.Text));
                SqlDataReader dr = cmd4.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                dataGridView1.DataSource = dt;
                dataGridView1.RowHeadersVisible = false;
                display1();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void button7_Click(object sender, EventArgs e)
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
