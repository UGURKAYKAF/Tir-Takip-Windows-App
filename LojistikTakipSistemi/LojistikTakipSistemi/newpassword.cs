using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;

namespace LojistikTakipSistemi
{
    public partial class newpassword : Form
    {
        public newpassword()
        {
            InitializeComponent();
        }
        int Move;
        int Mouse_X;
        int Mouse_Y;
        public int Id;
        SqlCommand cmd;
        SqlDataReader dr;
        private void button2_Click(object sender, EventArgs e)
        {
           
            if (maskedTextBox1.Text==maskedTextBox2.Text)
            {
                SqlConnection baglanti = new SqlConnection(@"Data Source=.; Initial Catalog=takipS;Integrated Security=True");
                cmd = new SqlCommand();
                baglanti.Open();
                cmd.Connection = baglanti;
                string komut = "Update Kullanici set Sifre=@Sifre Where Id=" + Id;
                cmd = new SqlCommand(komut,baglanti);
                cmd.Parameters.AddWithValue("@Sifre",maskedTextBox1.Text);
                cmd.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Şife Değiştirme Başarılı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Form1 frm = new Form1();
                frm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Şifreler Uyuşmuyor", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                maskedTextBox1.PasswordChar = '\0';
                checkBox1.ImageIndex = 1;
            }
            else
            {
                maskedTextBox1.PasswordChar = '*';
                checkBox1.ImageIndex = 2;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                maskedTextBox2.PasswordChar = '\0';
                checkBox2.ImageIndex = 1;
            }
            else
            {
                maskedTextBox2.PasswordChar = '*';
                checkBox2.ImageIndex = 2;
            }
        }

        private void newpassword_MouseUp(object sender, MouseEventArgs e)
        {
            Move = 0;
        }

        private void newpassword_MouseMove(object sender, MouseEventArgs e)
        {
            if (Move == 1)
            {
                this.SetDesktopLocation(MousePosition.X - Mouse_X, MousePosition.Y - Mouse_Y);
            }
        }

        private void newpassword_MouseDown(object sender, MouseEventArgs e)
        {
            Move = 1;
            Mouse_X = e.X;
            Mouse_Y = e.Y;
        }

        private void newpassword_Load(object sender, EventArgs e)
        {
            //MessageBox.Show(Id.ToString());
        }
    }
}
