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
    public partial class Form5 : Form
    {
        SqlConnection connect;
        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            connect = new SqlConnection(@"Data Source=HONGQUAN\SQLEXPRESS;Initial Catalog=QL_DH_GH_ONLINE;Integrated Security=True");
            connect.Open();
        }

        private void Form5_Close(object sender, FormClosingEventArgs e)
        {
            connect.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1();
            frm.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == string.Empty || textBox3.Text.Trim() == string.Empty || textBox4.Text.Trim() == string.Empty || textBox2.Text.Trim() == string.Empty || comboBox2.Text.Trim() == string.Empty || textBox6.Text.Trim() == string.Empty || textBox9.Text.Trim() == string.Empty || comboBox1.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin \n(Nếu không có email, có thể bỏ qua)", "Thông báo");
                return;
            }
            else
            {
                if (textBox4.Text.Length != 10)
                {
                    MessageBox.Show("Số điện thoại phải 10 số!", "Thông báo");
                    return;
                }
                // INSERT NGUOIDAIDIEN
                string sqlselect1 = "EXEC SP_INSERT_NDD @HOTEN";
                SqlCommand cmd1 = new SqlCommand(sqlselect1, connect);
                cmd1.Parameters.Add(new SqlParameter("@HOTEN", textBox9.Text));
                cmd1.ExecuteNonQuery();

                string sqlselect2 = "SELECT MANDD FROM NGUOIDAIDIEN WHERE TENNGUOIDAIDIEN = N'" + textBox9.Text + "'";
                SqlCommand cmd2 = new SqlCommand(sqlselect2, connect);
                string MANDD = Convert.ToString(cmd2.ExecuteScalar());

                string sqlselect21 = "SELECT MALH FROM LHVANCHUYEN WHERE TENLOAIHANG = N'" + comboBox2.Text + "'";
                SqlCommand cmd21 = new SqlCommand(sqlselect21, connect);
                string MALH = Convert.ToString(cmd21.ExecuteScalar());

                string sqlselect3 = "EXEC SP_INSERT_DT @TENDT, @TP, @QUAN, @DC, @SDT, @EMAIL, @MST, '" + MALH + "', '" + MANDD + "'";
                SqlCommand cmd3 = new SqlCommand(sqlselect3, connect);
                cmd3.Parameters.Add(new SqlParameter("@TENDT", textBox1.Text));
                cmd3.Parameters.Add(new SqlParameter("@DC", textBox2.Text));
                cmd3.Parameters.Add(new SqlParameter("@SDT", textBox4.Text));
                cmd3.Parameters.Add(new SqlParameter("@EMAIL", textBox11.Text));
                cmd3.Parameters.Add(new SqlParameter("@QUAN", textBox3.Text));
                cmd3.Parameters.Add(new SqlParameter("@TP", comboBox1.Text));
                cmd3.Parameters.Add(new SqlParameter("@MST", textBox6.Text));
                cmd3.Parameters.Add(new SqlParameter("@MALH", comboBox2.Text));
                cmd3.ExecuteNonQuery();

                groupBox2.Visible = true;
                button3.Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sqlselect1 = "SELECT TOP 1 MADT FROM DOITAC ORDER BY MADT DESC";
            SqlCommand cmd1 = new SqlCommand(sqlselect1, connect);
            string MADT = Convert.ToString(cmd1.ExecuteScalar());

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

            string sqlselect2 = "EXEC SP_INSERT_LOGIN '" + MADT + "', @username, @pass, N'Đối tác'";
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
