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
    public partial class Form4 : Form
    {
        SqlConnection connect;
        public Form4()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            connect = new SqlConnection(@"Data Source=HONGQUAN\SQLEXPRESS;Initial Catalog=QL_DH_GH_ONLINE;Integrated Security=True");
            connect.Open();
        }

        private void Form4_Close(object sender, FormClosingEventArgs e)
        {
            connect.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == string.Empty || textBox2.Text.Trim() == string.Empty || textBox3.Text.Trim() == string.Empty || textBox6.Text.Trim() == string.Empty || textBox9.Text.Trim() == string.Empty || textBox5.Text.Trim() == string.Empty || comboBox1.Text.Trim() == string.Empty || comboBox2.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin \n(Nếu không có email, có thể bỏ qua)", "Thông báo");
                return;
            }
            else
            {
                if (textBox2.Text.Length != 10)
                {
                    MessageBox.Show("Số điện thoại phải 10 số!", "Thông báo");
                    return;
                }

                string sqlselect1 = "SELECT MAKV FROM KHUVUCHOATDONG WHERE DIACHIKV = N'" + comboBox2.Text + "'";
                SqlCommand cmd1 = new SqlCommand(sqlselect1, connect);
                string MAKV = Convert.ToString(cmd1.ExecuteScalar());

                string sqlselect = "EXEC SP_INSERT_TX '" + MAKV + "',@HOTEN,@CMND,@SDT,@DC, @BSX, @TKNH, @EMAIL,@TTDP, '" + 0 + "'";
                SqlCommand cmd = new SqlCommand(sqlselect, connect);
                cmd.Parameters.Add(new SqlParameter("@HOTEN", textBox1.Text));
                cmd.Parameters.Add(new SqlParameter("@SDT", textBox2.Text));
                cmd.Parameters.Add(new SqlParameter("@DC", textBox3.Text));
                cmd.Parameters.Add(new SqlParameter("@EMAIL", textBox4.Text));
                cmd.Parameters.Add(new SqlParameter("@CMND", textBox5.Text));
                cmd.Parameters.Add(new SqlParameter("@BSX", textBox6.Text));
                cmd.Parameters.Add(new SqlParameter("@TKNH", textBox9.Text));
                cmd.Parameters.Add(new SqlParameter("@TTDP", comboBox1.Text));
                cmd.ExecuteNonQuery();
                
                groupBox2.Visible = true;
                button3.Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sqlselect1 = "SELECT TOP 1 MATX FROM TAIXE ORDER BY MATX DESC";
            SqlCommand cmd1 = new SqlCommand(sqlselect1, connect);
            string MATX = Convert.ToString(cmd1.ExecuteScalar());

            if (textBox8.Text.Trim() == string.Empty || textBox7.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Vui lòng nhập UserName hoặc Password!", "Thông báo");
                return;
            }

            string sqlselect11 = "SELECT USERNAME FROM LOGIN WHERE USERNAME = '" + textBox8.Text + "'";
            SqlDataAdapter sda = new SqlDataAdapter(sqlselect11, connect);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                MessageBox.Show("Tài khoản đã bị trùng! \nVui lòng nhập lại.", "Thông báo");
                return;
            }

            string sqlselect2 = "EXEC SP_INSERT_LOGIN '" + MATX + "', @username, @pass, N'Tài xế'";
            SqlCommand cmd2 = new SqlCommand(sqlselect2, connect);
            cmd2.Parameters.Add(new SqlParameter("@username", textBox8.Text));
            cmd2.Parameters.Add(new SqlParameter("@pass", textBox7.Text));
            cmd2.ExecuteNonQuery();

            DialogResult res = MessageBox.Show("Đăng ký thành công!", "Thông báo");
            if (res == DialogResult.OK)
            {
                Form1 form1 = new Form1();
                form1.Show();
                this.Hide();
            }
        }
    }
}
