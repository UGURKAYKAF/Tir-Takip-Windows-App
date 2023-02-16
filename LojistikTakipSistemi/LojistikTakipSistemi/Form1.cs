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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Init_Data();
        }
        private void Init_Data()
        {
            if (Properties.Settings.Default.Username != string.Empty)
            {
                if (Properties.Settings.Default.Remember == true)
                {
                    maskedTextBox1.Text = Properties.Settings.Default.Username;
                    maskedTextBox2.Text = Properties.Settings.Default.Password;
                    checkBox3.Checked = true;
                }
                else
                {
                    maskedTextBox1.Text = Properties.Settings.Default.Username;
                    maskedTextBox2.Text = Properties.Settings.Default.Password;
                }
            }
        }
        private void Save_Data()
        {
            if (checkBox3.Checked)
            {
                Properties.Settings.Default.Username = maskedTextBox1.Text.Trim();
                Properties.Settings.Default.Password = maskedTextBox2.Text.Trim();
                Properties.Settings.Default.Remember = true;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.Username = "";
                Properties.Settings.Default.Password = "";
                Properties.Settings.Default.Remember = false;
                Properties.Settings.Default.Save();
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
           
        }
        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                maskedTextBox2.PasswordChar = '\0';
                checkBox1.ImageIndex = 3;
            }
            else
            {
                maskedTextBox2.PasswordChar = '*';
                checkBox1.ImageIndex = 4;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        SqlCommand cmd;
        SqlDataReader dr;
        private void button2_Click(object sender, EventArgs e)
        {
            string user = maskedTextBox1.Text;
            string pass = maskedTextBox2.Text;
            SqlConnection baglanti = new SqlConnection(@"Data Source=.; Initial Catalog=takipS;Integrated Security=True");
            cmd = new SqlCommand();
            baglanti.Open();
            cmd.Connection = baglanti;
            cmd.CommandText = "SELECT * FROM Kullanici where MailAdresi='" + maskedTextBox1.Text + "' AND Sifre='" + maskedTextBox2.Text + "'";
            Save_Data();
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                int id = Convert.ToInt32(dr["Id"]);
                int durum = Convert.ToInt32(dr["Durum"]);
                if (durum==1)
                {
                    MessageBox.Show("Giriş Başarılı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    takip takip = new takip();
                    takip.id = id;
                    takip.ShowDialog();
                    this.Hide();
                }
                else if (durum==0)
                {
                    MessageBox.Show("Giriş Başarılı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    AdminPanel panel =new AdminPanel();
                    panel.id = id;
                    panel.ShowDialog();
                    this.Hide();
                }
            }
            else
            {
                MessageBox.Show("Mail Adresi Veya Şifre Hatalı","Hata",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            if (checkBox3.Checked)
            {
                takip takip = new takip();
                takip.remember = true;
            }
            else
            {
                takip takip = new takip();
                takip.remember = false;
            }
            baglanti.Close();
            
            
                
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ForgetPassword fgt = new ForgetPassword();
            fgt.Show();
            this.Hide();
        }
        int Move;
        int Mouse_X;
        int Mouse_Y;
        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            Move = 0;
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            Move = 1;
            Mouse_X = e.X;
            Mouse_Y = e.Y;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (Move == 1)
            {
                this.SetDesktopLocation(MousePosition.X - Mouse_X, MousePosition.Y - Mouse_Y);
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            kayit kyt = new kayit();
            kyt.Show();
            this.Hide();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                takip takip = new takip();
                takip.remember = true;
                checkBox3.BackColor = Color.Black;
                checkBox3.ForeColor = Color.White;
            }
            else
            {
                takip takip = new takip();
                takip.remember = false;
                checkBox3.BackColor = Color.Beige;
                checkBox3.ForeColor = Color.Black;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
        }
    }
}
